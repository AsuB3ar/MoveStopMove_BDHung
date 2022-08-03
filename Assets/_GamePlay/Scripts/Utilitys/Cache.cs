using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache 
{
    private static Dictionary<int, List<Vector3>> hashCodeToList = new Dictionary<int, List<Vector3>>();
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

}
