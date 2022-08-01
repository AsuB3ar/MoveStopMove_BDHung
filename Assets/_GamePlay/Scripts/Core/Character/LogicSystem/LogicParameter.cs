
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.LogicSystem
{
    using MoveStopMove.Core.Data;
    public class LogicParameter : AbstractParameterSystem
    {
        public Vector3 Velocity;
        public Vector3 MoveDirection;

        public bool Attack;
        public bool Die;
        public CharacterData CharacterData;
        
    }
}