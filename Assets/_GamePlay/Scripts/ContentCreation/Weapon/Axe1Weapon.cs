using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.ContentCreation.Weapon
{
    using Manager;
    public class Axe1Weapon : BaseWeapon
    {
        public override void DealDamage(float value, Vector3 direction)
        {
            if(WeaponType == WeaponType.Normal)
            {
                GameObject bullet = PrefabManager.Inst.PopFromPool(BulletPoolName);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.Euler(-90, 0, bullet.transform.rotation.eulerAngles.z);

                BaseBullet bulletScript = Cache.GetBaseBullet(bullet);
                bulletScript.OnFire(direction);
            }
        }
    }
}