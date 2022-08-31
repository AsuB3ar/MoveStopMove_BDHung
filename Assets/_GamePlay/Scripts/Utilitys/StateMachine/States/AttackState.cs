using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    public class AttackState : BaseState<LogicParameter,LogicData>
    {
        private int timeFrames;
        Vector3 direction;
        Vector3 targetPosition;
        public AttackState(StateMachine<LogicParameter,LogicData> StateMachine, LogicParameter Parameter, LogicData Data, LogicEvent Event) 
            : base(StateMachine ,Parameter, Data, Event)
        {

        }
        public override void Enter()
        {   
            base.Enter();
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, true);
            Event.SetVelocity(Vector3.zero);

            //TODO: Need to change here
            targetPosition = Parameter.TargetCharacter.gameObject.transform.position;
            direction = Parameter.TargetCharacter.gameObject.transform.position - Parameter.PlayerTF.position;
            
            Quaternion rot = MathHelper.GetQuaternion2Vector(Vector2.up, new Vector2(-direction.x, direction.z));

            timeFrames = 0;
            Event.SetRotation(GameConst.Type.Model, rot);
            Event.SetRotation(GameConst.Type.Sensor, rot);        
            Data.CharacterData.AttackCount = 0;
        }

        public override int LogicUpdate()
        {
            if (Data.CharacterData.Hp <= 0)
            {
                StateMachine.ChangeState(State.Die);
            }            
                      
            return 0;
        }

        public override int PhysicUpdate()
        {           
            if (Parameter.MoveDirection.sqrMagnitude > 0.001f)
            {
                StateMachine.ChangeState(State.Move);
            }
            else if (timeFrames >= GameConst.ANIM_IS_ATTACK_FRAMES)
            {
                StateMachine.ChangeState(State.Idle);
            }          

            if (timeFrames == 12)
            {
                Event.DealDamage(direction, Data.CharacterData.AttackRange);
            }

            timeFrames++;
            return 0;
        }


        public override void Exit()
        {
            base.Exit();                     
            Event.SetTargetIndicatorPosition?.Invoke(null, false);
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, false);
        }

        

    }
}