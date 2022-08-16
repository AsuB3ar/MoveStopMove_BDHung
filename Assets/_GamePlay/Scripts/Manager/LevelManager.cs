using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Manager
{
    using Core;
    using Utilitys;
    public class LevelManager : Singleton<LevelManager>,IInit
    {
        public const int MARGIN = 2;
        public Transform Level;
        public Transform StaticEnvironment;
        [SerializeField]
        private GameObject Player;

        public List<BaseCharacter> characters = new List<BaseCharacter>();      
        private BaseCharacter PlayerScript;
        private Vector3 position = Vector3.zero;
        [SerializeField]
        private Vector3 size;

        CanvasGameplay gameplay;

        protected override void Awake()
        {
            base.Awake();
            PlayerScript = Cache.GetBaseCharacter(Player);
            gameplay = UIManager.Inst.GetUI(UIID.UICGamePlay) as CanvasGameplay;
            gameplay.Close();
        }

        public void OnInit()
        {
            for (int i = 0; i < 10; i++)
            {
                //NOTE: UI Target Indicator
                gameplay.SubscribeTarget(SpawnCharacter());
            }
            PlayerScript.OnInit();
            
        }
      
        private void Start()
        {
            OnInit();
        }

        private void OnDie(BaseCharacter character)
        {
            character.OnDie -= OnDie;
            characters.Remove(character);
            PrefabManager.Inst.PushToPool(character.gameObject, PoolID.Character);

            //NOTE: UI Target Indicator
            gameplay.SubscribeTarget(SpawnCharacter());
        }
        private BaseCharacter SpawnCharacter()
        {
            GameObject character = PrefabManager.Inst.PopFromPool(PoolID.Character);

            character.transform.parent = Level;           

            BaseCharacter characterScript = Cache.GetBaseCharacter(character);
            
            Vector3 randomPos;
            do
            {
                randomPos = GetRandomPositionLevel();
            } while ((randomPos - Player.transform.position).sqrMagnitude < 2 * PlayerScript.AttackRange);
            
            
            character.transform.localPosition = randomPos;

            characterScript.OnInit();
            characterScript.ChangeWeapon(GameplayManager.Inst.GetRandomWeapon());
            characterScript.OnDie += OnDie;

            

            Color color = GameplayManager.Inst.GetRandomColor();
            characterScript.ChangeColor(color);
            PantSkin pantName = GameplayManager.Inst.GetRandomPantSkin();
            characterScript.ChangePant(pantName);
            PoolID hairname = GameplayManager.Inst.GetRandomHair();
            characterScript.ChangeHair(hairname);

            characters.Add(characterScript);
            return characterScript;
                     
        }

        private Vector3 GetRandomPositionLevel()
        {
            int value = Random.Range(0, 4);
            float vecX;
            float vecZ;
            if (value == 0)
            {
                vecX = size.x - MARGIN;
                vecZ = Random.Range(-(size.z - MARGIN) + position.z, size.z - MARGIN + position.z);
            }
            else if(value == 1)
            {
                vecX = -(size.x - MARGIN);
                vecZ = Random.Range(-(size.z - MARGIN) + position.z, size.z - MARGIN + position.z);
            }
            else if(value == 2)
            {
                vecZ = size.z - MARGIN;
                vecX = Random.Range(-(size.x - MARGIN) + position.x, size.x - MARGIN + position.x);
            }
            else
            {
                vecZ = -(size.z - MARGIN);
                vecX = Random.Range(-(size.x - MARGIN) + position.x, size.x - MARGIN + position.x);
            }
            return new Vector3(vecX, GameConst.INIT_CHARACTER_HEIGHT, vecZ);
        }
        
    }
}