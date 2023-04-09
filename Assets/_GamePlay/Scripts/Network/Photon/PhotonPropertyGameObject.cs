using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPropertyGameObject : MonoBehaviourPun,IPunObservable
{
    [SerializeField]
    bool SyncActive = true;
    [SerializeField]
    bool SyncName = true;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if(SyncActive)
                stream.SendNext(gameObject.activeInHierarchy);
            if (SyncName)
                stream.SendNext(gameObject.name);
            Debug.Log($"<color=green>NETWORK: SEND DATA </color>");
        }
        else if (stream.IsReading)
        {
            if (SyncActive)
                gameObject.SetActive((bool)stream.ReceiveNext());
            if (SyncName)
                gameObject.name = (string)stream.ReceiveNext();
            Debug.Log($"<color=green>NETWORK: RECEIVE DATA </color>");
        }
    }
}
