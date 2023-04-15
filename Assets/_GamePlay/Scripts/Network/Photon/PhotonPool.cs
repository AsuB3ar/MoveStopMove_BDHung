using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys;

public class PhotonPool : MonoBehaviourPun, IPunObservable
{
    List<int> data;
    [SerializeField]
    Pool pool;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (data == null) return;
        if (stream.IsWriting)
        {
            stream.SendNext(data.Count);
            for (int i = 0; i < data.Count; i++)
            {
                stream.SendNext(data[i]);
            }
        }
        else if (stream.IsReading)
        {
            int count = (int)stream.ReceiveNext();
            data.Clear();
            for (int i = 0; i < count; i++)
            {
                data.Add((int)stream.ReceiveNext());
            }
            pool.UpdatePhotonData();
        }
    }

    public void SetSerializeData(List<int> data)
    {
        this.data = data;
    }
}
