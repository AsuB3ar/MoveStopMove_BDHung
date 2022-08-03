using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.ContentCreation.Weapon
{
    using Core;
    public enum BulletType
    {
        Normal = 0,
        HorizontalRotation = 1
    }
    public class BaseBullet : MonoBehaviour
    {
        [SerializeField]
        BulletType Type;
        [SerializeField]
        float rotationSpeed = 30f;
        [SerializeField]
        float speed = 0.1f;

        Vector3 direction = Vector3.zero;

        public Collider SelfCharacterCollider;
        
        private void FixedUpdate()
        {
            if(Type == BulletType.HorizontalRotation)
            {
                transform.Rotate(0, 0, -rotationSpeed * Time.fixedDeltaTime * 60,Space.Self);
            }
            if(direction.sqrMagnitude > 0.001)
            {
                transform.Translate(direction * speed * Time.fixedDeltaTime * 60,Space.World);
            }
            
        }
        public void OnHit(BaseCharacter character)
        {

        }

        public void OnFire(Vector3 direction)
        {
            this.direction = direction;
            this.direction.y = 0;
        }
    }
}