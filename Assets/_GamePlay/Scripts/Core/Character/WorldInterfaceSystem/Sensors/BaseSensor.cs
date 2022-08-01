using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.WorldInterfaceSystem
{
    public abstract class BaseSensor : MonoBehaviour
    {
        protected WorldInterfaceData Data;
        public void Initialize(WorldInterfaceData Data)
        {
            this.Data = Data;
        }
        public abstract void UpdateData();
        
    }
}