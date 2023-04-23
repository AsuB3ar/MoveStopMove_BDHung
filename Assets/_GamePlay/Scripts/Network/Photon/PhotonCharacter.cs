using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCharacter : MonoBehaviourPun
{
    // Start is called before the first frame update
    public event Action<int[]> _OnInitData;
    [PunRPC]
    protected void RPC_OnInit(object[] data)
    {
        int[] lastData = new int[data.Length];
        for(int i = 0; i < data.Length; i++)
        {
            lastData[i] = (int)data[i];
        }
        _OnInitData?.Invoke(lastData);
    }

    public void InitNetworkData(int[] data)
    {
        if (!photonView.IsMine) return;

        object[] lastData = new object[data.Length];
        for(int i = 0; i < data.Length; i++)
        {
            lastData[i] = data[i];
        }
        photonView.RPC(nameof(RPC_OnInit), RpcTarget.Others, data as object);
    }
}
