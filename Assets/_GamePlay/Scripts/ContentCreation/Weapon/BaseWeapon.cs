using MoveStopMove.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Normal  = 0,
    Has3Ray = 1
}
namespace MoveStopMove.ContentCreation.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        float Range;
        [SerializeField]
        protected PoolName BulletPoolName;
        [SerializeField]
        protected WeaponType WeaponType;

        public BaseCharacter Character;
        public abstract void DealDamage(Vector3 direction, float range);

    }
}