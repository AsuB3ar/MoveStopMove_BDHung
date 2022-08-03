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
        [SerializeField]
        protected PoolName BulletPoolName;
        [SerializeField]
        protected WeaponType WeaponType;
        public abstract void DealDamage(float value,Vector3 direction);

    }
}