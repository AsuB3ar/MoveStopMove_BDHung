using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilitys
{
    public class Pool : MonoBehaviour
    {
        [SerializeField]
        protected GameObject mainPool;
        [SerializeField]
        public bool IsSetParent = false;
        [HideInInspector]
        private GameObject obj;
        Queue<GameObject> objects;

        Quaternion initQuaternion;
        int numObj = 10;
        public void Initialize(GameObject obj,Quaternion initQuaternion = default,int numObj = 10)
        {
            this.numObj = numObj;
            this.obj = obj;
            objects = new Queue<GameObject>();
            this.initQuaternion = initQuaternion;
            AddObject();
        }


        public void AddObject()
        {
            for (int i = 0; i < numObj; i++)
            {
                GameObject obj = Instantiate(this.obj, Vector3.zero, this.initQuaternion, mainPool.transform);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
        }

        public void Push(GameObject obj,bool checkContain = true)
        {           
            if (checkContain)
            {
                if (objects.Contains(obj))
                    return;
            }            
            objects.Enqueue(obj);
          
            if (IsSetParent)
            {
                //obj.transform.parent = mainPool.transform;
                obj.transform.SetParent(mainPool.transform);
            }
            obj.SetActive(false);
            obj.transform.position = Vector3.zero;
        }

        public GameObject Pop()
        {
            if(objects.Count == 0)
            {
                AddObject();
            }

            GameObject returnObj = objects.Dequeue();
            returnObj.SetActive(true);
            return returnObj;
        }

    }
}

