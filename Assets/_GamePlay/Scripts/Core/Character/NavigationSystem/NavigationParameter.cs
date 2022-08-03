
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.NavigationSystem
{
    using MoveStopMove.Core.Data;
    public class NavigationParameter : AbstractParameterSystem
    {
        public Transform PlayerTF;
        public Transform SensorTF;
        public CharacterData CharacterData;
        public int PlayerInstanceID;

        public bool IsGrounded = false;
        public bool IsHaveGround = false;
        public List<Vector3> EnemyPositions;
    }
}