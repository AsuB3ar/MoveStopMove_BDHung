using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    public class StateMachine
    {
        public BaseState CurrentState { get; private set; }
        public bool IsStarted { get; private set; } = false; 
        public bool Report = false;
        public void Initialize(BaseState initState)
        {
            CurrentState = initState;
            CurrentState.Enter();
            IsStarted = true;
        }
        public void ChangeState(BaseState newState)
        {
            if (newState != null)
            {
                if (Report)
                {
                    Debug.Log("Change to" + newState.ToString());
                }
                
                CurrentState.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
            else
            {
                Debug.LogError("NUll STATE");
            }
        }

    }
}
