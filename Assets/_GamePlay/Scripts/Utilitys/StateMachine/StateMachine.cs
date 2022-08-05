using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character;
    public class StateMachine<P,D>
        where P : AbstractParameterSystem
        where D : AbstractDataSystem<D>
    {
        public BaseState<P,D> CurrentState { get; private set; }
        public bool IsStarted { get; private set; } = false; 
        public bool Report = false;
        public void Initialize(BaseState<P,D> initState)
        {
            CurrentState = initState;
            CurrentState.Enter();
            IsStarted = true;
        }
        public void ChangeState(BaseState<P, D> newState)
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
