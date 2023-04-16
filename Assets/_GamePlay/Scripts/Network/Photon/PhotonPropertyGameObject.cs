using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonPropertyGameObject : MonoBehaviourPun,IPunInstantiateMagicCallback
{
    [SerializeField]
    GAMECONST.NETWORK_OBJECT_TYPE type;
    [SerializeField]
    bool SyncActive = true;
    [SerializeField]
    bool SyncName = true;
    private void Awake()
    {
        NetworkManager.Inst._OnPlayerStatusRoomChange += OnPlayerEnterRoom;
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        switch (type)
        {
            case GAMECONST.NETWORK_OBJECT_TYPE.MANAGER:
                gameObject.SetActive(true);
                break;
            case GAMECONST.NETWORK_OBJECT_TYPE.RESOURCE:
            case GAMECONST.NETWORK_OBJECT_TYPE.GAMEPLAY:
                gameObject.SetActive(false);
                break;
        }
        
    }

    [PunRPC]
    void RPC_SetActive(bool active, string name)
    {
        gameObject.SetActive(active);
        gameObject.name = name;
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void OnPlayerEnterRoom(Player player, bool value)
    {
        if (value && SyncActive)
        {
            photonView.RPC(nameof(RPC_SetActive), player, gameObject.activeInHierarchy, gameObject.name);
        }
    }
}
