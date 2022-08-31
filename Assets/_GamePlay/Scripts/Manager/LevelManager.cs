using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MoveStopMove.Manager
{
    using Core;
    using Core.Data;
    using Utilitys;
    public class LevelManager : Singleton<LevelManager>,IInit,IPersistentData
    {
        public event Action OnWinLevel;
        public event Action OnLoseLevel;
        public const int MARGIN = 2;
        public const float GROUNG_HEIGHT_PARAMETER = 0.42f;

        public Transform Level;
        public Transform StaticEnvironment;

        private List<BaseCharacter> characters = new List<BaseCharacter>();
        [SerializeField]
        int difficulty = 3;
        [SerializeField]
        List<LevelData> levelDatas;
        
        [SerializeField]
        private GameObject Ground;
        [SerializeField]             
        private Vector3 position = Vector3.zero;
        LevelData currentLevelData;
        private Vector3 groundSize;
        private List<GameObject> obstances = new List<GameObject>();


        private int numOfSpawnPlayers;
        private int numOfRemainingPlayers;
        private int currentLevel = 0;

        private int NumOfRemainingPlayers
        {
            get => numOfRemainingPlayers;
            set
            {
                numOfRemainingPlayers = value;
                gameplay.SetRemainingPlayerNumber(value + 1);
            }
        }

        CanvasGameplay gameplay;

        protected override void Awake()
        {
            base.Awake();          
            gameplay = UIManager.Inst.GetUI(UIID.UICGamePlay) as CanvasGameplay;
            gameplay.Close();
        }

        private void Start()
        {
            GameplayManager.Inst.PlayerScript = Cache.GetBaseCharacter(GameplayManager.Inst.Player);
            GameplayManager.Inst.PlayerScript.OnDie += OnPlayerDie;
            GameManager.Inst.OnStartGame += RunLevel;
            OpenLevel(GameManager.Inst.GameData.CurrentRegion);          
        }

        public void OnInit()
        {
            characters.Clear();
            obstances.Clear();
            NumOfRemainingPlayers = currentLevelData.numOfPlayers;
            numOfSpawnPlayers = currentLevelData.numOfPlayers;
            
            for (int i = 0; i < 10; i++)
            {
                //NOTE: UI Target Indicator
                gameplay.SubscribeTarget(SpawnCharacter());
            }
            GameplayManager.Inst.PlayerScript.OnInit();
            ConstructLevel();
        }
        
        public void OpenLevel(int level)
        {
            //TODO: Set Data Level
            currentLevel = level;
            currentLevelData = levelDatas[currentLevel];
            DestructLevel();
            GameplayManager.Inst.PlayerScript.Reset();
            OnInit();
        }

        public void RunLevel()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].Run();
            }
        }

        public void ConstructLevel()
        {
            groundSize = Vector3.one * currentLevelData.Size * 2;
            Ground.transform.localScale = groundSize;
            Ground.transform.localPosition = -Vector3.up * currentLevelData.Size * 2 * GROUNG_HEIGHT_PARAMETER;
            for (int i = 0; i < currentLevelData.ObstancePositions.Count; i++)
            {
                GameObject obstance = PrefabManager.Inst.PopFromPool(PoolID.Obstance);
                obstance.transform.parent = StaticEnvironment;

                float value = UnityEngine.Random.Range(3, 6f);
                Vector3 scale = new Vector3(value, value, value);
                obstance.transform.localScale = scale;

                Vector3 pos = currentLevelData.ObstancePositions[i] * currentLevelData.Size;
                pos.y = 0.5f;
                obstance.transform.localPosition = pos;
                obstance.transform.localRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

                
                obstances.Add(obstance);
            }
        }

        public void DestructLevel()
        {
            numOfSpawnPlayers = 0;
            for (int i = 0; i < obstances.Count; i++)
            {
                PrefabManager.Inst.PushToPool(obstances[i], PoolID.Obstance);
            }

            while(characters.Count > 0)
            {          
                characters[0].OnDespawn();
                RemoveCharacter(characters[0]);
            }
        }
        
        private void OnPlayerDie(BaseCharacter player)
        {
            ((CanvasFail)UIManager.Inst.OpenUI(UIID.UICFail)).SetRank(NumOfRemainingPlayers + 1);
            UIManager.Inst.CloseUI(UIID.UICGamePlay);
            
            OnLoseLevel?.Invoke();
            
        }
        private void OnEnemyDie(BaseCharacter character)
        {
            RemoveCharacter(character);
            NumOfRemainingPlayers -= 1;

            //NOTE: UI Target Indicator
            if(NumOfRemainingPlayers > 0)
            {
                gameplay.SubscribeTarget(SpawnCharacter());
            }
            else
            {
                OnWinLevel?.Invoke();
                CanvasVictory victory = UIManager.Inst.OpenUI(UIID.UICVictory) as CanvasVictory;               
                victory.SetScore(GameplayManager.Inst.PlayerScript.Level);
                victory.SetCurrentLevel(currentLevel);
                currentLevel += 1;
                UIManager.Inst.CloseUI(UIID.UICGamePlay);
            }
        }

        private void RemoveCharacter(BaseCharacter character)
        {
            character.OnDie -= OnEnemyDie;
            characters.Remove(character);
            gameplay.UnsubcribeTarget(character);
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
            } while ((randomPos - GameplayManager.Inst.Player.transform.position).sqrMagnitude < 2 * GameplayManager.Inst.PlayerScript.AttackRange);
            
            
            characterScript.SetPosition(randomPos);

            int level;
            if(GameplayManager.Inst.PlayerScript.Level <= difficulty)
            {
                level = UnityEngine.Random.Range(1, GameplayManager.Inst.PlayerScript.Level + difficulty);
            }
            else
            {
                level = UnityEngine.Random.Range(GameplayManager.Inst.PlayerScript.Level - difficulty, GameplayManager.Inst.PlayerScript.Level + difficulty);
            }

            characterScript.SetLevel(level);
            characterScript.OnInit();
            if (GameManager.Inst.GameIsRun)
            {
                characterScript.Run();
            }
            else
            {
                characterScript.Stop();
            }
            characterScript.ChangeWeapon(GameplayManager.Inst.GetRandomWeapon());
            characterScript.OnDie += OnEnemyDie;

            

            GameColor color = GameplayManager.Inst.GetRandomColor();
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
                vecX = currentLevelData.Size - MARGIN;
                vecZ = UnityEngine.Random.Range(-(currentLevelData.Size - MARGIN) + position.z, currentLevelData.Size - MARGIN + position.z);
            }
            else if(value == 1)
            {
                vecX = -(currentLevelData.Size - MARGIN);
                vecZ = UnityEngine.Random.Range(-(currentLevelData.Size - MARGIN) + position.z, currentLevelData.Size - MARGIN + position.z);
            }
            else if(value == 2)
            {
                vecZ = currentLevelData.Size - MARGIN;
                vecX = UnityEngine.Random.Range(-(currentLevelData.Size - MARGIN) + position.x, currentLevelData.Size - MARGIN + position.x);
            }
            else
            {
                vecZ = -(currentLevelData.Size - MARGIN);
                vecX = UnityEngine.Random.Range(-(currentLevelData.Size - MARGIN) + position.x, currentLevelData.Size - MARGIN + position.x);
            }
            return new Vector3(vecX, GameConst.INIT_CHARACTER_HEIGHT, vecZ);
        }

        public void LoadGame(GameData data)
        {
            currentLevel = data.CurrentRegion;
        }

        public void SaveGame(ref GameData data)
        {
            data.CurrentRegion = currentLevel;
        }
    }
}