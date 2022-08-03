using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public class DieState : BaseState
    {
        public DieState(StateMachine StateMachine, BasicStateInsts States , LogicParameter Parameter, LogicData Data, LogicEvent Event) 
            : base(StateMachine, States,Parameter, Data, Event)
        {

        }
        public override void Enter()
        {
            Event.SetVelocity(Vector3.zero);
            Event.SetBool_Anim(GameConst.ANIM_IS_DEAD, true);
            base.Enter();
        }


        public override void EventUpdate(Type type, string code)
        {
            base.EventUpdate(type, code);
        }

        public override void Exit()
        {
            base.Exit();
        }


        public override int LogicUpdate()
        {
            return base.LogicUpdate();
        }

        public override int PhysicUpdate()
        {
            return base.PhysicUpdate();
        }

    }
}