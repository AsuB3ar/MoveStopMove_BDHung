using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
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

    public override void OnJoinedRoom()
    {
        _OnJoinedRoom?.Invoke();
        Debug.Log($"<color=green>NETWORK</color>: Joined Room");
    }

    public GameObject Instantiate(string name, Vector3 position = default, Quaternion rotation = default)
    {
        return PhotonNetwork.Instantiate(name, position, rotation);
    }
    public void ClearEvent()
    {
        _OnConnectedToMaster = null;
        _OnJoinedLobby = null;
        _OnJoinedRoom = null;
    }
}
