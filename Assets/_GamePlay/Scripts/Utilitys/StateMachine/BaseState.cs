using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys.AI
{
    using System;
    using MoveStopMove.Core.Character.LogicSystem;


    public abstract class BaseState
    {
        protected StateMachine StateMachine;
        protected LogicParameter Parameter;
        protected LogicData Data;
        protected LogicEvent Event;
        protected BasicStateInsts States;
        protected float StartTime { get; private set; }
        public string AnimBoolName { get; private set; }

        protected bool isEndState;
        //protected Rigidbody2D Rb;
        public BaseState(StateMachine StateMachine,BasicStateInsts States, LogicParameter Parameter, LogicData Data, LogicEvent Event)
        {
            this.StateMachine = StateMachine;
            this.Parameter = Parameter;
            this.Data = Data;
            this.Event = Event;
            this.States = States;
            //Rb = player.MovementModule.Rb;
        }      
        public virtual void Enter()
        {
            StartTime = Time.time;
            isEndState = false;       
        }
        public virtual void Exit()
        {
            isEndState = true;
        }
        public virtual int LogicUpdate()
        { 
            return 0;
        }
        public virtual int PhysicUpdate()
        {
            return 0;
        }
        public virtual void EventUpdate(Type type,string code)
        {

        }
    }
}