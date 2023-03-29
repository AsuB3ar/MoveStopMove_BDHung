using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoveStopMove.Manager;

public class CanvasPvpLobby : UICanvas
{
    [SerializeField]
    protected InputField createInput;
    [SerializeField]
    protected InputField joinInput;
    [SerializeField]
    protected Button createButton;
    [SerializeField]
    protected Button joinButton;

    private void Awake()
    {
        createButton.onClick.AddListener(OnClickCreateRoom);
        joinButton.onClick.AddListener(OnClickJoinRoom);
        NetworkManager.Inst.ClearEvent();
        NetworkManager.Inst._OnJoinedRoom += () => SceneManager.Inst.LoadPhotonScene(GAMECONST.STANDARD_PVP_SCENE);
    }

    private void OnClickJoinRoom()
    {
        NetworkManager.Inst.JoinRoom(joinInput.text);
    }

    protected void OnClickCreateRoom()
    {
        NetworkManager.Inst.CreateRoom(createInput.text);
    }
}
