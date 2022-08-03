using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public class AttackState : BaseState
    {
        private int timeFrames;
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
            timeFrames = GameConst.ANIM_IS_ATTACK_FRAMES;
            
            Data.AttackCount = 0;
        }

        public override int LogicUpdate()
        {
            if (Parameter.Die)
            {
                StateMachine.ChangeState(States.GetState(State.Die));
            }
            else if (timeFrames == 0)
            {
                StateMachine.ChangeState(States.GetState(State.Idle));
            }

            return 0;
        }

        public override int PhysicUpdate()
        {
            if (timeFrames == 15)
            {
                Event.DealDamage(1, direction.normalized);
            }
            

            timeFrames--;
            return 0;
        }
        public override void Exit()
        {
            base.Exit();
            
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, false);
        }


    }
}