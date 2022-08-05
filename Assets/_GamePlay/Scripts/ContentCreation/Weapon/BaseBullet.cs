using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.ContentCreation.Weapon
{
    using Core;
    using Manager;
    public enum BulletType
    {
        Normal = 0,
        HorizontalRotation = 1
    }
    public class BaseBullet : MonoBehaviour
    {
        float range;
        BaseCharacter parentCharacter;
        [SerializeField]
        LayerMask characterLayer;
        [SerializeField]
        BulletType Type;
        [SerializeField]
        float rotationSpeed = 30f;
        [SerializeField]
        float speed = 0.1f;
        float amplifyParameter = 60;
        Vector3 direction = Vector3.zero;

        float lastSpeed => speed * Time.fixedDeltaTime * amplifyParameter;
        public Collider SelfCharacterCollider;

        private void FixedUpdate()
        {
            if(Type == BulletType.HorizontalRotation)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime * amplifyParameter,Space.Self);
            }
            if(direction.sqrMagnitude > 0.001)
            {
                transform.Translate(direction * lastSpeed,Space.World);
            }

            
            if(range < 0)
            {
                PrefabManager.Inst.PushToPool(this.gameObject, PoolName.Axe1, false);
            }
            else
            {
                range -= lastSpeed;
            }
            
        }
        public void OnHit(BaseCharacter character)
        {
            if(character != parentCharacter)
            {
                if (!character.IsDie)
                {
                    PrefabManager.Inst.PushToPool(this.gameObject, PoolName.Axe1, false);
                    character.TakeDamage(1);
                    parentCharacter.AddStatus(); 
                }
                
            }

        }

        public void OnFire(Vector3 direction,float range,BaseCharacter parentCharacter)
        {
            this.direction = direction;
            this.direction.y = 0;
            this.range = range - lastSpeed * 6;
            this.parentCharacter = parentCharacter;
        }

        private void OnTriggerEnter(Collider col)
        {
            if(Mathf.Pow(2, col.gameObject.layer) == characterLayer)
            {
                OnHit(Cache.GetBaseCharacter(col));
            }
        }
    }
}