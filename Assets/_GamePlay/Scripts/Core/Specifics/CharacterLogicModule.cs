
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.LogicSystem
{
    using Utilitys.AI;
    public class CharacterLogicModule : AbstractLogicModule
    {
        public readonly List<State> StateName = new List<State>() { State.Idle, State.Move, State.Attack, State.Die };
        public StateMachine<LogicParameter,LogicData> StateMachine;
        BasicStateInsts<LogicParameter,LogicData> States;

        public override void Initialize(LogicData Data, LogicParameter Parameter, LogicEvent Event)
        {
            base.Initialize(Data, Parameter, Event);
            StateMachine = new StateMachine<LogicParameter,LogicData>();
            States = new BasicStateInsts<LogicParameter,LogicData>();

            States.PushState(State.Idle,new IdleState(StateMachine, States, Parameter, Data, Event));
            States.PushState(State.Move,new MoveState(StateMachine, States, Parameter, Data, Event));
            States.PushState(State.Attack,new AttackState(StateMachine, States, Parameter, Data, Event));
            States.PushState(State.Die,new DieState(StateMachine, States, Parameter, Data, Event));

            //StateMachine.Report = true;            
        }

        public override void UpdateData()
        {
            if (StateMachine.IsStarted)
            {
                StateMachine.CurrentState.LogicUpdate();
            }
            
        }

        public override void FixedUpdateData()
        {
            if (StateMachine.IsStarted)
            {
                StateMachine.CurrentState.PhysicUpdate();
            }
        }

        public void StartStateMachine()
        {
            StateMachine.Initialize(States.GetState(State.Idle));          
        }
    }
}