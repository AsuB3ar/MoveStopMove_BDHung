using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.PhysicSystem
{
    using System;
    using WorldInterfaceSystem;
    public class CharacterPhysicSystem : AbstractCharacterSystem<AbstractPhysicModule,PhysicData,PhysicParameter>
    {
        public CharacterPhysicSystem(AbstractPhysicModule module)
        {
            Data = ScriptableObject.CreateInstance(typeof(PhysicData)) as PhysicData;
            Parameter = ScriptableObject.CreateInstance(typeof(PhysicParameter)) as PhysicParameter;
            this.module = module;
            module.Initialize(Data,Parameter);
        }

        public void SetVelocity(Vector3 velocity)
        {
            module.SetVelocity(velocity);
        }

        public void SetRotation(string gameObj,Quaternion rotation)
        {
            module.SetRotation(gameObj,rotation);
        }

        public void SetSmoothRotation(string gameObj, Vector3 direction)
        {
            module.SetSmoothRotation(gameObj, direction);
        }
    }
}