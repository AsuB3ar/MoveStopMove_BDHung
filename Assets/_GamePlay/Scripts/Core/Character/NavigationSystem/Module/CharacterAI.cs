using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.NavigationSystem
{
    using Utilitys.AI;
    public class CharacterAI : AbstractNavigationModule
    {
        public readonly List<State> StateName = new List<State>() { State.Wandering, State.Combat };
        public StateMachine<NavigationParameter,NavigationData> StateMachine;
        BasicStateInsts<NavigationParameter,NavigationData> States;
        public override void Initialize(NavigationData Data, NavigationParameter Parameter)
        {
            base.Initialize(Data, Parameter);
            StateMachine = new StateMachine<NavigationParameter,NavigationData>();
            States = new BasicStateInsts<NavigationParameter,NavigationData>();
        }
        public override void UpdateData()
        {
            
        }
    }
}