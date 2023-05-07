using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public event Action<Player, bool> _OnPlayerStatusRoomChange;
    public event Action _OnDisconnectServer;
    public event Action _OnLeftRoom;
    public event Action _OnJoinedRoom;
    public event Action _OnConnectedToMaster;
    public event Action _OnJoinedLobby;

    private static NetworkManager inst = null;
    public static NetworkManager Inst => inst;

    private void Awake()
    {
        if(inst != null)
        {
            Destroy(gameObject);
        }
        else
        {
            inst = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log($"<color=green>NETWORK</color>: Connecting To Server");
    }

    public override void OnConnectedToMaster()
    {
        _OnConnectedToMaster?.Invoke();
        Debug.Log($"<color=green>NETWORK</color>: Connected To Master");
    }

    public override void OnJoinedLobby()
    {
        _OnJoinedLobby?.Invoke();
        Debug.Log($"<color=green>NETWORK</color>: Joined Lobby");
    }

    public void CreateRoom(string name)
    {
        PhotonNetwork.CreateRoom(name);
        Debug.Log($"<color=green>NETWORK</color>: Create Room");
    }

    public void JoinRoom(string name)
    {
        PhotonNetwork.JoinRoom(name);
        Debug.Log($"<color=green>NETWORK</color>: Join Room");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void DisconnectMaster()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnJoinedRoom()
    {       
        _OnJoinedRoom?.Invoke();
        Debug.Log($"<color=green>NETWORK</color>: Joined Room");
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        _OnLeftRoom?.Invoke();
        for (int i = 0; i < PhotonPropertyGameObject.AllObjects.Count; i++)
        {
            if (PhotonPropertyGameObject.AllObjects[i].IsMine)
                Destroy(PhotonPropertyGameObject.AllObjects[i].gameObject);
        }
        Debug.Log($"<color=green>NETWORK</color>: Leaved Room");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        _OnDisconnectServer?.Invoke();
        Debug.Log($"<color=green>NETWORK</color>: Disconnected Server");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            Debug.Log($"<color=green>NETWORK - ROOM</color>:{roomList[0].Name}");
        }
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        _OnPlayerStatusRoomChange?.Invoke(newPlayer, true);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _OnPlayerStatusRoomChange?.Invoke(otherPlayer, false);
    }
    public GameObject Instantiate(string name, Vector3 position = default, Quaternion rotation = default)
    {
        return PhotonNetwork.Instantiate(name, position, rotation);
    }
    public void Destroy(GameObject gameObject)
    {
        PhotonNetwork.Destroy(gameObject);
    }
    public bool IsMasterClient => PhotonNetwork.IsMasterClient;
    public void ClearEvent()
    {
        _OnConnectedToMaster = null;
        _OnJoinedLobby = null;
        _OnJoinedRoom = null;
        _OnDisconnectServer = null;
        _OnLeftRoom = null;
    }
}
