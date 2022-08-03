

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public enum State
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Die = 3,
        Attack = 4
    }
    public class BasicStateInsts
    {
        Dictionary<State,BaseState> states = new Dictionary<State,BaseState>();
        StateMachine StateMachine;
        LogicParameter Parameter;
        LogicData Data;
        LogicEvent Event;

        //DEVELOP:Change condition to "If Name = null -> State = null"
        public BasicStateInsts(List<State> StateNames,StateMachine StateMachine,LogicParameter Parameter, LogicData Data, LogicEvent Event)
        {
            this.StateMachine = StateMachine;
            this.Parameter = Parameter;
            this.Data = Data;
            this.Event = Event;

            for(int i = 0; i < StateNames.Count; i++)
            {
                if (!states.ContainsKey(StateNames[i]))
                {
                    BaseState stateScripts = InitStateScripts(StateNames[i]);
                    states.Add(StateNames[i], stateScripts);                                      
                }
            }
        }
        public BaseState GetState(State name)
        {
            return states[name];
        }

        private BaseState InitStateScripts(State state)
        {
            if(state == State.Idle)
            {
                return new IdleState(StateMachine,this,Parameter, Data, Event);
            }
            else if(state == State.Move)
            {
                return new MoveState(StateMachine,this,Parameter, Data, Event);
            }
            else if(state == State.Die)
            {
                return new DieState(StateMachine,this,Parameter, Data, Event);
            }
            else if(state == State.Attack)
            {
                return new AttackState(StateMachine, this, Parameter, Data, Event);
            }
            else
            {
                return null;
            }
        }
    }
}
