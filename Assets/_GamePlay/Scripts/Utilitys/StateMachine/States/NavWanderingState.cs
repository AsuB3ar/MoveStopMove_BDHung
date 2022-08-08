using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.NavigationSystem;
    using Utilitys.Timer;
    public class NavWanderingState : BaseState<NavigationParameter,NavigationData>
    {
        STimer timer = new STimer();
        float timeWandering;
        float avgTimeWandering = 2f;
        public NavWanderingState(StateMachine<NavigationParameter, NavigationData> StateMachine, NavigationParameter Parameter, NavigationData Data)
            :base(StateMachine,Parameter,Data,null)
        {
            timer.TimeOut1 += TimerEvent;
        }

        public override void Enter()
        {
            base.Enter();
            ChangeRandomDirection();
            timeWandering = Random.Range(avgTimeWandering * 0.8f, avgTimeWandering * 1.2f);            
            timer.Start(timeWandering);

        }
        public override int LogicUpdate()
        {
            if(Parameter.CharacterPositions.Count > 0)
            {
                StateMachine.ChangeState(State.Combat);
            }
            else if(Parameter.IsGrounded && !Parameter.IsHaveGround)
            {
                WhenMeetWall();
            }
            else if (Parameter.IsHaveObstances)
            {
                WhenMeetObstance();
            }
            return base.LogicUpdate();
        }

        public override void Exit()
        {
            base.Exit();
            timer.Stop();
        }

        public void TimerEvent(int code)
        {
            ChangeRandomDirection();
            timeWandering = Random.Range(avgTimeWandering * 0.8f, avgTimeWandering * 1.2f);
            timer.Start(timeWandering);
        }

        private void WhenMeetObstance()
        {
            float value = Parameter.SensorTF.localRotation.eulerAngles.y + 90;
            Vector2 newDirection = MathHelper.GetRandomDirection(value - 90, value + 90);

            Data.MoveDirection = new Vector3(newDirection.x, 0, newDirection.y);
        }

        private void WhenMeetWall()
        {
            float value = Parameter.SensorTF.localRotation.eulerAngles.y - 90;
            Vector2 newDirection = MathHelper.GetRandomDirection(value - 90, value + 90);

            Data.MoveDirection = new Vector3(newDirection.x, 0, newDirection.y);
        }

        private void ChangeRandomDirection()
        {
            int value = Random.Range(0, 5);
            Vector2 newDirection;
            if (value < 3.5)
            {
                newDirection = MathHelper.GetRandomDirection();
            }
            else
            {
                newDirection = Vector2.zero;
            }
             
            Data.MoveDirection = new Vector3(newDirection.x, 0, newDirection.y);
        }

        ~NavWanderingState()
        {
            timer.TimeOut1 -= TimerEvent;
        }
    }
}