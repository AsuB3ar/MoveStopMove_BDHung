
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Data {
    public class CharacterData : ScriptableObject
    {
        public float Speed = 3;
        public int Score = 0;
        public float Size = 1;
        public float BaseAttackRange = 3f;
        public int Hp = 1;
        public float AttackRange => BaseAttackRange * Size;
    }
}