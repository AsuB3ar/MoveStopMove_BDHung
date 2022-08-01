using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.LogicSystem
{
    using WorldInterfaceSystem;
    using NavigationSystem;
    using PhysicSystem;
    using MoveStopMove.Core.Data;

    public class CharacterLogicSystem : AbstractCharacterSystem<AbstractLogicModule,LogicData,LogicParameter>
    {
        public LogicEvent Event;
        public CharacterLogicSystem(AbstractLogicModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(LogicData)) as LogicData;
            Parameter = ScriptableObject.CreateInstance(typeof(LogicParameter)) as LogicParameter;
            Event = ScriptableObject.CreateInstance(typeof(LogicEvent)) as LogicEvent;
            this.module = module;
            module.Initialize(Data,Parameter,Event);
        }

        public void SetCharacterInformation(CharacterData CharacterData)
        {
            Data.CharacterData = CharacterData;
        }
        public void ReceiveInformation(WorldInterfaceData Data)
        {
            
        }

        public void ReceiveInformation(NavigationData Data)
        {
            Parameter.MoveDirection = Data.MoveDirection;
        }

        public void ReceiveInformation(PhysicData Data)
        {
            Parameter.Velocity = Data.Velocity;
        }
    }
}