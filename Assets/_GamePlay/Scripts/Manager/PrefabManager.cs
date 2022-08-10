using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolName
{
    Character = 0,
    #region Bullet
    Axe1Bullet = 1,
    Knife1Bullet = 2,
    Axe2Bullet = 3,
    #endregion

    #region Weapon
    Axe1 = 100,
    Knife1 = 101,
    Axe2 = 102,
    #endregion

    #region Skins
    Hair_Arrow = 1000,
    Hair_Cowboy = 1001,
    Hair_Headphone = 1002,
    #endregion
    None = 10000

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
        GameObject Axe1Bullet;
        [SerializeField]
        GameObject Knife1Bullet;
        [SerializeField]
        GameObject Axe2Bullet;

        [SerializeField]
        GameObject Axe1;
        [SerializeField]
        GameObject Knife1;
        [SerializeField]
        GameObject Axe2;

        [SerializeField]
        GameObject Hair_Arrow;
        [SerializeField]
        GameObject Hair_Cowboy;
        [SerializeField]
        GameObject Hair_Headphone;

        public readonly List<PoolName> WeaponNames = new List<PoolName>() { PoolName.Axe1, PoolName.Knife1, PoolName.Axe2 };
        //-----

        public GameObject pool;
        Dictionary<PoolName, Pool> poolData = new Dictionary<PoolName, Pool>();
        protected override void Awake()
        {
            base.Awake();
            CreatePool(Character, PoolName.Character, Quaternion.Euler(0, 0, 0), 15);
            CreatePool(Axe1Bullet, PoolName.Axe1Bullet, Quaternion.Euler(0, 0, 0));
            CreatePool(Knife1Bullet, PoolName.Knife1Bullet, Quaternion.Euler(0, 0, 0));
            CreatePool(Axe2Bullet, PoolName.Axe2Bullet, Quaternion.Euler(0, 0, 0));

            CreatePool(Axe1, PoolName.Axe1, Quaternion.Euler(0, 0, 0));
            CreatePool(Knife1, PoolName.Knife1, Quaternion.Euler(0, 0, 0));
            CreatePool(Axe2, PoolName.Axe2, Quaternion.Euler(0, 0, 0));

            CreatePool(Hair_Arrow, PoolName.Hair_Arrow);
            CreatePool(Hair_Cowboy, PoolName.Hair_Cowboy);
            CreatePool(Hair_Headphone, PoolName.Hair_Headphone);
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