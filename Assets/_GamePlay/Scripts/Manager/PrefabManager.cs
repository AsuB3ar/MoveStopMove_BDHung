using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolName
{
    Character = 0,
    Axe1 = 1,
    Knife1 = 2,
}
namespace MoveStopMove.Manager
{
    using Utilitys;

    [DefaultExecutionOrder(-1)]
    public class PrefabManager : Singleton<PrefabManager>
    {
        //NOTE:Specific for game,remove to reuse
        [SerializeField]
        GameObject Character;
        [SerializeField]
        GameObject Axe1;
        [SerializeField]
        GameObject Knife1;
        
        //-----

        public GameObject pool;
        Dictionary<PoolName, Pool> poolData = new Dictionary<PoolName, Pool>();
        protected override void Awake()
        {
            base.Awake();
            CreatePool(Character, PoolName.Character, Quaternion.Euler(0, 0, 0), 15);
            CreatePool(Axe1, PoolName.Axe1, Quaternion.Euler(0, 0, 0));
            CreatePool(Knife1, PoolName.Knife1, Quaternion.Euler(0, 0, 0));
        }


        public void CreatePool(GameObject obj, PoolName namePool, Quaternion quaternion = default, int numObj = 10)
        {
            if (!poolData.ContainsKey(namePool))
            {
                GameObject newPool = Instantiate(pool, Vector3.zero, Quaternion.identity);
                Pool poolScript = newPool.GetComponent<Pool>();
                newPool.name = namePool.ToString();
                poolScript.Initialize(obj, quaternion, numObj);
                poolData.Add(namePool, poolScript);
            }
        }

        public void PushToPool(GameObject obj, PoolName namePool, bool checkContain = true)
        {
            if (!poolData.ContainsKey(namePool))
            {
                CreatePool(obj, namePool);
            }

            poolData[namePool].Push(obj, checkContain);
        }

        public GameObject PopFromPool(PoolName namePool, GameObject obj = null)
        {
            if (!poolData.ContainsKey(namePool))
            {
                if (obj == null)
                {
                    Debug.LogError("No pool name " + namePool + " was found!!!");
                    return null;
                }
            }

            return poolData[namePool].Pop();
        }

    }
}