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
        public abstract void SetRotation(GameConst.Type type,Quaternion rotation);
        public abstract void SetSmoothRotation(GameConst.Type type, Vector3 direction);
    }
}
