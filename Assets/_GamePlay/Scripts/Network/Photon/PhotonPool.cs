using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys;

public class PhotonPool : MonoBehaviourPun, IPunObservable, ISyncState
{
    List<int> data;
    [SerializeField]
    Pool pool;
    public bool IsChange = true;
    ISyncState.STATE state = ISyncState.STATE.ON_INIT;
    public ISyncState.STATE State => state;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            state = ISyncState.STATE.READY;
        }
        if (PhotonNetwork.IsMasterClient)
            NetworkManager.Inst._OnPlayerStatusRoomChange += OnPlayerEnterRoom;
    }   
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (data == null) return;
        //if (stream.IsWriting && IsChange)
        //{
        //    stream.SendNext(data.Count);
        //    for (int i = 0; i < data.Count; i++)
        //    {
        //        stream.SendNext(data[i]);
        //    }
        //    IsChange = false;
        //}
        //else if (stream.IsReading)
        //{
        //    int count = (int)stream.ReceiveNext();
        //    data.Clear();
        //    for (int i = 0; i < count; i++)
        //    {
        //        data.Add((int)stream.ReceiveNext());
        //    }
        //    pool.UpdatePhotonData();            
        //    Debug.Log("ON POOL SERIALIZE");
        //    state = ISyncState.STATE.READY;
        //}
    }
    [PunRPC]
    protected void RPC_Init_Data(object[] data)
    {
        int count = (int)data[0];
        this.data.Clear();
        for (int i = 0; i < count; i++)
        {
            this.data.Add((int)data[i + 1]);
        }
        pool.InitPhotonData();
        Debug.Log("ON POOL SERIALIZE");
        state = ISyncState.STATE.READY;
    }

    private void OnPlayerEnterRoom(Player player, bool value)
    {
        if (value)
        {
            object[] data = new object[this.data.Count + 1];
            data[0] = this.data.Count;
            for (int i = 0; i < this.data.Count; i++)
            {
                data[i + 1] = this.data[i];
            }
            photonView.RPC(nameof(RPC_Init_Data), player, data as object);
        }
    }
    public void SetSerializeData(List<int> data)
    {
        this.data = data;
    }

    private void OnDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
            NetworkManager.Inst._OnPlayerStatusRoomChange -= OnPlayerEnterRoom;
    }
}
