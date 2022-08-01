using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.NavigationSystem
{
    using MoveStopMove.Core.Data;
    using WorldInterfaceSystem;
    public class CharacterNavigationSystem : AbstractCharacterSystem<AbstractNavigationModule,NavigationData,NavigationParameter>
    {
        public CharacterNavigationSystem(AbstractNavigationModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(NavigationData)) as NavigationData;
            Parameter = ScriptableObject.CreateInstance(typeof(NavigationParameter)) as NavigationParameter;
            this.module = module;
            module.Initialize(Data,Parameter);
        }

        public void ReceiveInformation(WorldInterfaceData Data)
        {
            Parameter.IsHaveGround = Data.IsHaveGround;
            Parameter.IsGrounded = Data.IsGrounded;
        }

        public void SetCharacterInformation(Transform Player,Transform SensorTF ,int PlayerInstanceID)
        {
            Parameter.PlayerTF = Player;
            Parameter.PlayerInstanceID = PlayerInstanceID;
            Parameter.SensorTF = SensorTF;
        }

        public void SetCharacterData(CharacterData CharacterData)
        {
            Parameter.CharacterData = CharacterData;
        }
    }
}