using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MoveStopMove.Manager
{
    using Core;
    using Core.Data;
    using Utilitys;
    public class LevelManager : Singleton<LevelManager>,IInit
    {
        public event Action OnLevelWin;
        public const int MARGIN = 2;
        public Transform Level;
        public Transform StaticEnvironment;

        [HideInInspector]
        public List<BaseCharacter> characters = new List<BaseCharacter>();
        [SerializeField]
        int difficulty = 3;
        [SerializeField]
        LevelData data;
        [SerializeField]
        private GameObject Ground;
        [SerializeField]
        private GameObject Player;       
        private BaseCharacter PlayerScript;
        private Vector3 position = Vector3.zero;
        private Vector3 groundSize;
        private List<GameObject> obstances = new List<GameObject>();
        private int numOfSpawnPlayers;
        private int numOfRemainingPlayers;
        private int currentLevel = 1;

        CanvasGameplay gameplay;

        protected override void Awake()
        {
            base.Awake();
            PlayerScript = Cache.GetBaseCharacter(Player);
            gameplay = UIManager.Inst.GetUI(UIID.UICGamePlay) as CanvasGameplay;
            gameplay.Close();
        }

        private void Start()
        {
            OpenLevel(1);          
        }

        public void OnInit()
        {
            characters.Clear();
            obstances.Clear();
            numOfRemainingPlayers = data.numOfPlayers;
            numOfSpawnPlayers = data.numOfPlayers;
            
            for (int i = 0; i < 10; i++)
            {
                //NOTE: UI Target Indicator
                gameplay.SubscribeTarget(SpawnCharacter());
            }
            PlayerScript.OnInit();
            ConstructLevel();
        }
        
        public void OpenLevel(int level)
        {
            //TODO: Set Data Level
            DestructLevel();
            PlayerScript.Reset();
            OnInit();
        }
        public void ConstructLevel()
        {
            groundSize = Vector3.one * data.Size * 2;
            groundSize.y = 1;
            Ground.transform.localScale = groundSize;
            for (int i = 0; i < data.ObstancePositions.Count; i++)
            {
                GameObject obstance = PrefabManager.Inst.PopFromPool(PoolID.Obstance);
                obstance.transform.parent = StaticEnvironment;

                float value = UnityEngine.Random.Range(1, 4f);
                Vector3 scale = new Vector3(value, value, value);
                obstance.transform.localScale = scale;

                Vector3 pos = data.ObstancePositions[i] * data.Size;
                pos.y = 0.5f;
                obstance.transform.localPosition = pos;
                obstance.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

                
                obstances.Add(obstance);
            }
        }

        public void DestructLevel()
        {
            for(int i = 0; i < obstances.Count; i++)
            {
                PrefabManager.Inst.PushToPool(obstances[i], PoolID.Obstance);
            }

            for(int i = 0; i < characters.Count; i++)
            {
                characters[i].OnDespawn();
            }
        }
        

        private void OnDie(BaseCharacter character)
        {
            character.OnDie -= OnDie;
            characters.Remove(character);
            numOfRemainingPlayers -= 1;

            //NOTE: UI Target Indicator
            if(numOfRemainingPlayers > 0)
            {
                gameplay.SubscribeTarget(SpawnCharacter());
            }
            else
            {
                OnLevelWin?.Invoke();
                CanvasVictory victory = UIManager.Inst.OpenUI(UIID.UICVictory) as CanvasVictory;
                victory.SetScore(PlayerScript.Level);
                victory.SetCurrentLevel(currentLevel);
                currentLevel += 1;
            }
        }
        private BaseCharacter SpawnCharacter()
        {
            if(numOfSpawnPlayers <= 0)
            {
                return null;
            }

            numOfSpawnPlayers -= 1;
            GameObject character = PrefabManager.Inst.PopFromPool(PoolID.Character);
            character.transform.parent = Level;           

            BaseCharacter characterScript = Cache.GetBaseCharacter(character);
            
            Vector3 randomPos;
            do
            {
                randomPos = GetRandomPositionCharacter();
            } while ((randomPos - Player.transform.position).sqrMagnitude < 2 * PlayerScript.AttackRange);
            
            
            character.transform.localPosition = randomPos;

            int level;
            if(PlayerScript.Level <= difficulty)
            {
                level = UnityEngine.Random.Range(1, PlayerScript.Level + difficulty);
            }
            else
            {
                level = UnityEngine.Random.Range(PlayerScript.Level - difficulty, PlayerScript.Level + difficulty);
            }

            characterScript.SetLevel(level);
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

        private Vector3 GetRandomPositionCharacter()
        {
            int value = UnityEngine.Random.Range(0, 4);
            float vecX;
            float vecZ;
            if (value == 0)
            {
                vecX = data.Size - MARGIN;
                vecZ = UnityEngine.Random.Range(-(data.Size - MARGIN) + position.z, data.Size - MARGIN + position.z);
            }
            else if(value == 1)
            {
                vecX = -(data.Size - MARGIN);
                vecZ = UnityEngine.Random.Range(-(data.Size - MARGIN) + position.z, data.Size - MARGIN + position.z);
            }
            else if(value == 2)
            {
                vecZ = data.Size - MARGIN;
                vecX = UnityEngine.Random.Range(-(data.Size - MARGIN) + position.x, data.Size - MARGIN + position.x);
            }
            else
            {
                vecZ = -(data.Size - MARGIN);
                vecX = UnityEngine.Random.Range(-(data.Size - MARGIN) + position.x, data.Size - MARGIN + position.x);
            }
            return new Vector3(vecX, GameConst.INIT_CHARACTER_HEIGHT, vecZ);
        }

        
    }
}