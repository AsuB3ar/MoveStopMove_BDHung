
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.LogicSystem
{
    using Utilitys.AI;
    public class CharacterLogicModule : AbstractLogicModule
    {
        public readonly List<State> StateName = new List<State>() { State.Idle, State.Move, State.Attack, State.Die };
        public StateMachine StateMachine;
        BasicStateInsts States;

        public override void Initialize(LogicData Data, LogicParameter Parameter, LogicEvent Event)
        {
            base.Initialize(Data, Parameter, Event);
            StateMachine = new StateMachine();
            States = new BasicStateInsts(StateName, StateMachine, Parameter, Data, Event);

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