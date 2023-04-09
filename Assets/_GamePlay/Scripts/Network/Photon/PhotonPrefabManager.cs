using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Utilitys;
using System;

public class PhotonPrefabManager : MonoBehaviourPun, IPunObservable
{
    List<KeyValuePair<int, int>> pools;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (pools == null) return;
        if (stream.IsWriting)
        {
            stream.SendNext(pools.Count);
            for(int i = 0; i < pools.Count; i++)
            {
                stream.SendNext(pools[i].Key);
                stream.SendNext(pools[i].Value);
            }
        }
        else if (stream.IsReading)
        {
            int count = (int)stream.ReceiveNext();
            pools.Clear();
            for(int i = 0; i < count; i++)
            {
                pools.Add(new KeyValuePair<int, int>((int)stream.ReceiveNext(), (int)stream.ReceiveNext()));
            }
        }
        Debug.Log("ON PREFAB MANAGER SERIALIZE");
    }

    public void SetDataSerialize(ref List<KeyValuePair<int, int>> pools)
    {
        this.pools = pools;
    }
}
