using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public class MoveState : BaseState
    {
        public MoveState(StateMachine StateMachine, BasicStateInsts States, LogicParameter Parameter, LogicData Data, LogicEvent Event) : base(StateMachine, States ,Parameter, Data, Event)
        {

        }
        public override void Enter()
        {
            base.Enter();
            //TODO: Play Move Animation
            //TODO: Set up Timer
        }


        public override void EventUpdate(Type type, string code)
        {
            base.EventUpdate(type, code);
        }



        public override int LogicUpdate()
        {
            if (Parameter.Die)
            {
                StateMachine.ChangeState(States.GetState(State.Die));
                return -1;
            }
            else if(Parameter.MoveDirection.sqrMagnitude < 0.001)
            {
                StateMachine.ChangeState(States.GetState(State.Idle));                
            }

            Event.SetVelocity(Parameter.MoveDirection * Data.CharacterData.Speed);
            return 0;
        }

        public override int PhysicUpdate()
        {
            return base.PhysicUpdate();
        }

    }
}