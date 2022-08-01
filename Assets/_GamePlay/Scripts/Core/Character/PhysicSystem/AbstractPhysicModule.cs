using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.PhysicSystem
{
    public abstract class AbstractPhysicModule : AbstractModuleSystem<PhysicData,PhysicParameter>
    {

        protected PhysicData Data;
        protected PhysicParameter Parameter;
        public override void Initialize(PhysicData Data,PhysicParameter Parameter)
        {
            this.Data = Data;
            this.Parameter = Parameter;
        }
        public abstract void SetVelocity(Vector3 velocity);
        public abstract void SetRotation(string gameObj,Quaternion rotation);
        public abstract void SetSmoothRotation(string gameObj, Vector3 direction);
    }
}
