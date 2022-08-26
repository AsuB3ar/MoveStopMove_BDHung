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
            Vector3 newDirection;
            direction = Parameter.CharacterPositions[0] - Parameter.PlayerTF.position;
            targetPosition = Parameter.CharacterPositions[0];

            direction.y = 0;
            for (int i = 1; i < Parameter.CharacterPositions.Count; i++)
            {
                newDirection = Parameter.CharacterPositions[i] - Parameter.PlayerTF.position;
                newDirection.y = 0;

                if(newDirection.sqrMagnitude < direction.sqrMagnitude)
                {
                    direction = newDirection;
                    targetPosition = Parameter.CharacterPositions[i];
                }
            }
            
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

            if (Parameter.CharacterPositions.Count > 0)
            {             
                Event.SetTargetIndicatorPosition?.Invoke(targetPosition, true);
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
            Event.SetTargetIndicatorPosition?.Invoke(Vector3.zero, false);
            Event.SetBool_Anim(GameConst.ANIM_IS_ATTACK, false);
        }

        

    }
}