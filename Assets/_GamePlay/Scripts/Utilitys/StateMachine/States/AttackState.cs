using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public class AttackState : BaseState
    {
        private float currentTime;
        Vector3 direction;
        public AttackState(StateMachine StateMachine, BasicStateInsts States, LogicParameter Parameter, LogicData Data, LogicEvent Event) : base(StateMachine,States ,Parameter, Data, Event)
        {

        }
        public override void Enter()
        {
            //TODO: Play Attack Animation
            base.Enter();
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, true);
            Event.SetVelocity(Vector3.zero);

            direction = Parameter.CharacterPositions[0] - Parameter.PlayerTF.position;
            Quaternion rot = MathHelper.GetQuaternion2Vector(Vector2.up, new Vector2(-direction.x, direction.z));

            
            Event.SetRotation(GameConst.Type.Model, rot);
            Event.SetRotation(GameConst.Type.Sensor, rot);
            
            Data.AttackCount = 0;
        }

        public override int LogicUpdate()
        {
            if (Data.CharacterData.Hp <= 0)
            {
                StateMachine.ChangeState(States.GetState(State.Die));
            }

            return 0;
        }


        public override void EventUpdate(Type type, string code)
        {
            if (code.IndexOf("EndAnimation") != -1)
            {
                StateMachine.ChangeState(States.GetState(State.Idle));
            }
            else if (code.IndexOf("AttackAnimation") != -1)
            {
                Event.DealDamage(direction.normalized, Data.CharacterData.AttackRange);
            }

        }
        public override void Exit()
        {
            base.Exit();
            
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, false);
        }


    }
}