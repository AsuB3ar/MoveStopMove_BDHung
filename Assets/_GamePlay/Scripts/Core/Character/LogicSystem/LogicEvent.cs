using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MoveStopMove.Core.Character.LogicSystem
{
    using System;
    public class LogicEvent : ScriptableObject
    {
        public Action<Vector3> SetVelocity;
        public Action<string,Quaternion> SetRotation;
        public Action<string, Vector3> SetSmoothRotation;

        public Action<string, bool> SetBool_Anim;
        public Action<string, float> SetFloat_Anim;
        public Action<string, int> SetInt_Anim;
    }
}