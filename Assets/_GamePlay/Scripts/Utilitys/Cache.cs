using MoveStopMove.ContentCreation.Weapon;
using MoveStopMove.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    private static Dictionary<int, List<Vector3>> hashCodeToList = new Dictionary<int, List<Vector3>>();
    private static Dictionary<GameObject, BaseCharacter> gameObj2BaseCharacter = new Dictionary<GameObject, BaseCharacter>();
    private static Dictionary<Collider, BaseCharacter> collider2BaseCharacter = new Dictionary<Collider, BaseCharacter>();

    private static Dictionary<GameObject, BaseWeapon> gameObj2BaseWeapon = new Dictionary<GameObject, BaseWeapon>();
    private static Dictionary<GameObject, BaseBullet> gameObj2BaseBullet = new Dictionary<GameObject, BaseBullet>();
     
    public static List<Vector3> GetCacheList(int hashCode)
    {
        if (!hashCodeToList.ContainsKey(hashCode))
        {
            hashCodeToList.Add(hashCode, new List<Vector3>());
        }
        hashCodeToList[hashCode].Clear();
        return hashCodeToList[hashCode];
    }

    public static List<Vector3> GetCacheList(int hashCode , List<Vector3> list)
    {
        List<Vector3> vec3 = GetCacheList(hashCode);
        for(int i = 0; i < list.Count; i++)
        {
            vec3.Add(list[i]);
        }
        return vec3;
    }

    public static BaseCharacter GetBaseCharacter(GameObject obj)
    {
        if (!gameObj2BaseCharacter.ContainsKey(obj))
        {
            gameObj2BaseCharacter.Add(obj, obj.GetComponent<BaseCharacter>());
        }
        return gameObj2BaseCharacter[obj];
    }

    public static BaseCharacter GetBaseCharacter(Collider col)
    {
        BaseCharacter scripts = col.GetComponent<BaseCharacter>();
        if (!collider2BaseCharacter.ContainsKey(col))
        {
            collider2BaseCharacter.Add(col, scripts);
        }

        if (!gameObj2BaseCharacter.ContainsKey(col.gameObject))
        {
            gameObj2BaseCharacter.Add(col.gameObject, scripts);
        }
        return collider2BaseCharacter[col];
    }

    public static BaseBullet GetBaseBullet(GameObject obj)
    {
        if (!gameObj2BaseBullet.ContainsKey(obj))
        {
            gameObj2BaseBullet.Add(obj, obj.GetComponent<BaseBullet>());
        }
        return gameObj2BaseBullet[obj];
    }

    public static BaseWeapon GetBaseWeapon(GameObject obj)
    {
        if (!gameObj2BaseWeapon.ContainsKey(obj))
        {
            gameObj2BaseWeapon.Add(obj, obj.GetComponent<BaseWeapon>());
        }
        return gameObj2BaseWeapon[obj];
    }
}
