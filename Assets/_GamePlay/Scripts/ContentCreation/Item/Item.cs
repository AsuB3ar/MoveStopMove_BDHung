using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.ContentCreation
{
    using Manager;
    public class Item : MonoBehaviour, IDespawn
    {
        [SerializeField]
        protected ItemData data;
        [SerializeField]
        protected PoolName PoolName;
        public void SetTranformData()
        {
            transform.localPosition = data.position;
            transform.localRotation = Quaternion.Euler(data.rotation);
            transform.localScale = data.scale;
        }


        public void OnDespawn()
        {
            PrefabManager.Inst.PushToPool(this.gameObject, PoolName, false);
        }
    }
}