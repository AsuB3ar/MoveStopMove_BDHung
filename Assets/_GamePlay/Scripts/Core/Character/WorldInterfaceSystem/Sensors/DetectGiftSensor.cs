using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MoveStopMove.Core.Character.WorldInterfaceSystem
{
    public class DetectGiftSensor : BaseSensor
    {
        public readonly Vector3 checkRadiusUnit = new Vector3(0.3f, 0.6f, 0.3f);
        public event Action<bool> OnSpecialAttack;

        [SerializeField]
        Transform checkPoint;       
        Vector3 lastCheckRadius;
        public override void UpdateData()
        {
            lastCheckRadius = checkRadiusUnit * Parameter.CharacterData.Size;
            Data.IsSpecialAttack = Physics.CheckBox(checkPoint.transform.position, lastCheckRadius, Quaternion.identity, layer);
            if (Data.IsSpecialAttack)
            {
                OnSpecialAttack?.Invoke(true);
            }
        }

        private void OnDrawGizmos()
        {
            if (checkPoint != null)
            {
                Gizmos.DrawCube(checkPoint.position, lastCheckRadius * 2);
            }
        }
    }
}