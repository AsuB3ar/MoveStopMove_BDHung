using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Manager
{
    using Core;
    using Utilitys;
    public class LevelManager : Singleton<LevelManager>,IInit
    {
        public Transform Level;
        public Transform StaticEnvironment;

        private List<BaseCharacter> characters = new List<BaseCharacter>();
        [SerializeField]
        private GameObject Player;
        private Vector3 position = Vector3.zero;
        [SerializeField]
        private Vector3 size;

        public void OnInit()
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnCharacter();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
        }
        private void Start()
        {
            OnInit();
        }

        private void OnDie(BaseCharacter character)
        {
            character.OnDie -= OnDie;
            characters.Remove(character);
            PrefabManager.Inst.PushToPool(character.gameObject, PoolName.Character);
            SpawnCharacter();
        }
        private void SpawnCharacter()
        {
            GameObject character = PrefabManager.Inst.PopFromPool(PoolName.Character);
            character.transform.parent = Level;
            character.transform.localPosition = GetRandomPositionLevel();

            BaseCharacter characterScript = Cache.GetBaseCharacter(character);
            characterScript.OnInit();
            characterScript.OnDie += OnDie;

            characters.Add(characterScript);
        }

        private Vector3 GetRandomPositionLevel()
        {
            int value = Random.Range(0, 4);
            float vecX;
            float vecZ;
            if (value == 0)
            {
                vecX = size.x - 1;
                vecZ = Random.Range(-(size.z - 1) + position.z, size.z - 1 + position.z);
            }
            else if(value == 1)
            {
                vecX = -(size.x - 1);
                vecZ = Random.Range(-(size.z - 1) + position.z, size.z - 1 + position.z);
            }
            else if(value == 2)
            {
                vecZ = size.z - 1;
                vecX = Random.Range(-(size.x - 1) + position.x, size.x - 1 + position.x);
            }
            else
            {
                vecZ = -(size.z - 1);
                vecX = Random.Range(-(size.x - 1) + position.x, size.x - 1 + position.x);
            }
            return new Vector3(vecX, GameConst.INIT_CHARACTER_HEIGHT, vecZ);
        }

    }
}