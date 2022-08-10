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
    using Manager;
    public abstract class BaseWeapon : Item
    {
        float Range;
        [SerializeField]
        protected PoolName BulletPoolName;
        [SerializeField]
        protected WeaponType WeaponType;
        [SerializeField]
        protected Transform firePoint;
        
        [HideInInspector]
        public BaseCharacter Character;
        public PoolName Name => BulletPoolName;
             
        public abstract void DealDamage(Vector3 direction, float range, float scale);
    }
}