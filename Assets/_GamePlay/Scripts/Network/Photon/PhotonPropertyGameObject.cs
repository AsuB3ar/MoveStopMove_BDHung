using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonPropertyGameObject : MonoBehaviourPun,IPunInstantiateMagicCallback
{
    static List<PhotonPropertyGameObject> allObjects = new List<PhotonPropertyGameObject>();
    public event Action _OnCompleteInit;
    public static IReadOnlyList<PhotonPropertyGameObject> AllObjects => allObjects.AsReadOnly();
    [SerializeField]
    GAMECONST.NETWORK_OBJECT_TYPE type;
    [SerializeField]
    bool SyncActive = true;
    bool lastActiveState;

    bool isRpcCall = false;
    bool isOnInit = true;

    public GAMECONST.NETWORK_OBJECT_TYPE Type => type;
    private void Awake()
    {
        allObjects.Add(this);
        if(PhotonNetwork.IsMasterClient)
            NetworkManager.Inst._OnPlayerStatusRoomChange += OnPlayerEnterRoom;
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        switch (type)
        {
            case GAMECONST.NETWORK_OBJECT_TYPE.MANAGER:
                lastActiveState = true;
                gameObject.SetActive(true);
                break;
            case GAMECONST.NETWORK_OBJECT_TYPE.RESOURCE:
            case GAMECONST.NETWORK_OBJECT_TYPE.GAMEPLAY:
                gameObject.transform.parent = NetworkManager.Inst.transform;
                gameObject.SetActive(false);
                if (photonView.IsMine)
                    lastActiveState = true;
                break;
        }
        
    }

    private void OnEnable()
    {
        if(!isRpcCall && !isOnInit)
        {
            SyncActiveState();
        }
    }

    private void OnDisable()
    {
        if (!isRpcCall && !isOnInit)
        {
            SyncActiveState();
        }
    }

    

    public void OnCompleteInit()
    {
        if (type == GAMECONST.NETWORK_OBJECT_TYPE.GAMEPLAY)
        {
            if (photonView.IsMine)
                photonView.RPC(nameof(RPC_OnInit), RpcTarget.Others, gameObject.name);
            //gameObject.GetComponent<MoveStopMove.Core.Player>().Initialize(); //DEV: Cache
        }
        isOnInit = false;
        isRpcCall = true;
        gameObject.SetActive(lastActiveState);
        isRpcCall = false;
        _OnCompleteInit?.Invoke();
    }
    [PunRPC]
    private void RPC_OnInit(string name)
    {
        RPC_SetActive(true, name);
        OnCompleteInit();
    }
    [PunRPC]
    void RPC_SetActive(bool active, string name)
    {
        gameObject.name = name;
        if (isOnInit)
        {
            lastActiveState = active;
        }
        else
        {
            isRpcCall = true;
            gameObject.SetActive(active);           
            isRpcCall = false;
        }
        Debug.Log("ON SYNC ACTIVE");
    }    
    private void SyncActiveState()
    {
        photonView.RPC(nameof(RPC_SetActive), RpcTarget.Others, gameObject.activeInHierarchy, gameObject.name);
    }
    private void OnPlayerEnterRoom(Player player, bool value)
    {        
        if (value && SyncActive)
        {
            photonView.RPC(nameof(RPC_SetActive), player, gameObject.activeInHierarchy, gameObject.name);
        }
    }
    private void OnDestroy()
    {
        allObjects.Remove(this);
        if(PhotonNetwork.IsMasterClient)
            NetworkManager.Inst._OnPlayerStatusRoomChange -= OnPlayerEnterRoom;
    }
}
