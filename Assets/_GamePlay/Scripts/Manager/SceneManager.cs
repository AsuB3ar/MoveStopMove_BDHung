using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utilitys;

namespace MoveStopMove.Manager
{
    [DefaultExecutionOrder(-1000)]
    public class SceneManager : SingletonPersistent<SceneManager>
    {
        public event Action<string> _OnSceneLoaded;
        public async void LoadScene(string sceneName)
        {
            var scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;
            do
            {
                await Task.Delay(100);
            } while (scene.progress < 0.9f);

            await Task.Delay(500);
            scene.allowSceneActivation = true;
            Debug.Log($"<color=red>COMPLETE LOADING SCENE</color>");
            _OnSceneLoaded.Invoke(sceneName);
        }

        public void LoadScene(string sceneName, Action destructScene)
        {
            destructScene?.Invoke();
            LoadScene(sceneName);
        }
    }
}