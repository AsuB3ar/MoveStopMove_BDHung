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


        void HandleOnPlayModeChanged(PauseState mode)
        {
            //Debug.Log("Enter");
            // This method is run whenever the playmode state is changed.
            //if (EditorApplication.isPaused)
            //{
            //    StopGame();
            //}
            //else
            //{
            //    if(Time.timeScale != 0)
            //    {
            //        StartGame();
            //    }
                
            //}
            //else if (EditorApplication.isPlaying)
            //{
            //    StartGame();
            //    Debug.Log("Start Game");
            //}
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


        //public static void ChangeState(GameState state)
        //{
        //    gameState = state;
        //}

        //public static bool IsState(GameState state)
        //{
        //    return gameState == state;
        //}

    }
}