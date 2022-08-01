using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public abstract class AttackState : BaseState
    {
        public AttackState(StateMachine StateMachine, BasicStateInsts States, LogicParameter Parameter, LogicData Data, LogicEvent Event) : base(StateMachine,States ,Parameter, Data, Event)
        {

        }
        public override void Enter()
        {
            //TODO: Play Attack Animation
            Event.SetVelocity(Vector3.zero);
            base.Enter();
        }

        public override int LogicUpdate()
        {
            if (Parameter.Die)
            {
                StateMachine.ChangeState(States.GetState(State.Die));
            }
            return 0;
        }
        public override void Exit()
        {
            base.Exit();
        }

    }
}