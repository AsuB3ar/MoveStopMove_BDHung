using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.WorldInterfaceSystem
{
    using Utilitys;
    public class WorldInterfaceData : AbstractDataSystem<WorldInterfaceData>
    {
        public bool IsHaveGround = false;
        public bool IsGrounded = false;
        public bool IsExitRoom = false;
        public List<Vector3> CharacterPositions = new List<Vector3>();

        protected override void UpdateDataClone()
        {
            if(Clone == null)
            {
                Clone = CreateInstance(typeof(WorldInterfaceData)) as WorldInterfaceData;
            }
            Clone.IsHaveGround = IsHaveGround;
            Clone.IsGrounded = IsGrounded;
            Clone.IsExitRoom = IsExitRoom;

            Clone.CharacterPositions = Cache.GetCacheList(Clone.CharacterPositions.GetHashCode(),CharacterPositions);
            //NOTE: Clone list EatBricks
        }
    }
}