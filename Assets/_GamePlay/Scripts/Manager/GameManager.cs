using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MoveStopMove.Manager
{
    using System;
    using Utilitys;
    using MoveStopMove.Core.Data;
    using MoveStopMove.Core;
    using System.Linq;
    public class GameManager : Singleton<GameManager>
    {
        //[SerializeField] UserData userData;
        //[SerializeField] CSVData csv;
        //private static GameState gameState = GameState.MainMenu;

        // Start is called before the first frame update
        public event Action OnStartGame;
        public event Action OnStopGame;
        bool gameIsRun = false;
        public bool GameIsRun => gameIsRun;

        private List<IPersistentData> persistentDataObjects;
        private GameData gameData;
        protected override void Awake()
        {
            base.Awake();
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            int maxScreenHeight = 1280;
            float ratio = (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
            if (Screen.currentResolution.height > maxScreenHeight)
            {
                Screen.SetResolution(Mathf.RoundToInt(ratio * (float)maxScreenHeight), maxScreenHeight, true);
            }

            //csv.OnInit();
            //userData?.OnInitData();

            //ChangeState(GameState.MainMenu);
            UIManager.Inst.OpenUI(UIID.UICMainMenu);
        }

        private void Start()
        {
            this.persistentDataObjects = FindAllDataPersistentObject();
            LoadGame();
        }

        private List<IPersistentData> FindAllDataPersistentObject()
        {
            IEnumerable<IPersistentData> dataPersistentObjects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistentData>();

            return new List<IPersistentData>(dataPersistentObjects);
        }

        public void StartGame()
        {
            gameIsRun = true;
            Time.timeScale = 1;
            OnStartGame?.Invoke();
        }

        public void StopGame()
        {
            gameIsRun = false;
            OnStopGame?.Invoke();
        }


        public void NewGame()
        {
            gameData = new GameData();
        }
        public void LoadGame()
        {
            if (!PlayerPrefs.HasKey(Player.P_SPEED))
            {
                Debug.Log("Game Data Not Found! Initializing data to defaults");
                NewGame();
            }
            else
            {
                gameData = new GameData();
                gameData.Speed = PlayerPrefs.GetFloat(Player.P_SPEED);
                gameData.Weapon = PlayerPrefs.GetInt(Player.P_WEAPON);

                gameData.Color = PlayerPrefs.GetInt(Player.P_COLOR);
                gameData.Pant = PlayerPrefs.GetInt(Player.P_PANT);
                gameData.Hair = PlayerPrefs.GetInt(Player.P_HAIR);
                gameData.Set = PlayerPrefs.GetInt(Player.P_SET);
            }


            for(int i = 0; i < persistentDataObjects.Count; i++)
            {
                persistentDataObjects[i].LoadGame(gameData);
            }

            Debug.Log("Load Data");
        }
        public void SaveGame()
        {
            for (int i = 0; i < persistentDataObjects.Count; i++)
            {
                persistentDataObjects[i].SaveGame(ref gameData);
            }
            PlayerPrefs.SetFloat(Player.P_SPEED, gameData.Speed);
            PlayerPrefs.SetInt(Player.P_WEAPON, gameData.Weapon);

            PlayerPrefs.SetInt(Player.P_COLOR, gameData.Color);
            PlayerPrefs.SetInt(Player.P_PANT, gameData.Pant);
            PlayerPrefs.SetInt(Player.P_HAIR, gameData.Hair);
            PlayerPrefs.SetInt(Player.P_SET, gameData.Set);

            Debug.Log("Save Data");
        }



        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}