using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCharacter : MonoBehaviourPun
{
    // Start is called before the first frame update
    public event Action<int[]> _OnInitData;
    public event Action<GameObject, GameObject> _OnUpdateCharacter;
    public event Action _OnInitialize;
    [SerializeField]
    PhotonPropertyGameObject propertyPhoton;

    bool isInit = false;
    bool isPropertyInit = false;
    GameObject weapon;
    GameObject hair;

    object[] characterData;
    private void Awake()
    {
        propertyPhoton._OnCompleteInit += OnPropertyInitComplete;
        if(photonView.IsMine)
            NetworkManager.Inst._OnPlayerStatusRoomChange += OnPlayerEnterRoom;
    }
    [PunRPC]
    protected void RPC_OnInit(object[] data)
    {
        int[] lastData = new int[data.Length - 2];
        for(int i = 0; i < data.Length - 2; i++)
        {
            lastData[i] = (int)data[i + 2];
        }
        weapon = PhotonView.Find((int)data[0]).gameObject;
        hair = PhotonView.Find((int)data[1]).gameObject;

        _OnInitData?.Invoke(lastData);
        isInit = true;
        if (isInit && isPropertyInit && !photonView.IsMine)
            _OnUpdateCharacter?.Invoke(weapon,hair);

    }

    public void SetNetworkData(GameObject weapon,GameObject hair,ref int[] data)
    {
        if (!photonView.IsMine) return;

        characterData = new object[data.Length];
        data[0] = weapon.GetComponent<PhotonView>().ViewID;
        data[1] = hair.GetComponent<PhotonView>().ViewID;
        for(int i = 0; i < data.Length; i++)
        {
            characterData[i] = data[i];
        }
        photonView.RPC(nameof(RPC_OnInit), RpcTarget.Others, characterData as object);
    }

    private void OnPlayerEnterRoom(Player player, bool value)
    {
        if (value)
        {
            photonView.RPC(nameof(RPC_OnInit), player, characterData as object);
        }
    }
    private void OnPropertyInitComplete()
    {
        _OnInitialize?.Invoke();
        isPropertyInit = true;
        if (isInit && isPropertyInit && !photonView.IsMine)
            _OnUpdateCharacter?.Invoke(weapon, hair);
    }
}
