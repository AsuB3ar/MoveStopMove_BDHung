using MoveStopMove.Core;
using MoveStopMove.Core.Data;
using MoveStopMove.Manager;
using CustomAttribute;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SceneInitialize : MonoBehaviour
{
    public enum SCENE_TYPE
    {
        INIT = 0,
        LOAD_START = 1,
        PVE = 2,
        PVP = 3,
    }
    [SerializeField]
    SCENE_TYPE type;

    [ConditionalField(nameof(type), false, SCENE_TYPE.INIT)]
    [SerializeField]
    GameData gameData;
    [ConditionalField(nameof(type), false, SCENE_TYPE.PVE, SCENE_TYPE.PVP)]
    [SerializeField]
    Camera playerCamera;
    [ConditionalField(nameof(type), false, SCENE_TYPE.PVE, SCENE_TYPE.PVP)]
    [SerializeField]
    BaseCharacter playerScript;
    [ConditionalField(nameof(type), false, SCENE_TYPE.PVE, SCENE_TYPE.PVP)]
    [SerializeField]
    GameObject targetIndicator;
    [ConditionalField(nameof(type), false, SCENE_TYPE.PVE, SCENE_TYPE.PVP)]
    [SerializeField]
    CameraMove cameraMove;
    private void Awake()
    {
        switch (type)
        {
            case SCENE_TYPE.INIT:
                SceneManager.Inst.LoadScene("LoadStart");
                SceneManager.Inst._OnSceneLoaded += (name) =>
                {
                    if (string.Compare(name, "LoadStart") == 0)
                    {
                        SceneManager.Inst.LoadScene("PveScene");
                        Debug.Log($"<color=green>Complete Load LoadStart Scene</color>");
                    }
                };
                break;
            case SCENE_TYPE.LOAD_START:
                break;
            case SCENE_TYPE.PVE:
                GameplayManager.Inst.PlayerScript = playerScript;
                GameplayManager.Inst.PlayerCamera = playerCamera;
                GameplayManager.Inst.TargetIndicator = targetIndicator;
                GameplayManager.Inst.CameraMove = cameraMove;
                gameData.OnInitData();
                
                break;
            case SCENE_TYPE.PVP:
                GameplayManager.Inst.PlayerScript = playerScript;
                GameplayManager.Inst.PlayerCamera = playerCamera;
                GameplayManager.Inst.TargetIndicator = targetIndicator;
                GameplayManager.Inst.CameraMove = cameraMove;
                break;
        }
    }

    private void Start()
    {
        switch (type)
        {
            case SCENE_TYPE.PVE:
                UIManager.Inst.OpenUI(UIID.UICMainMenu);
                break;
            case SCENE_TYPE.PVP:
                UIManager.Inst.OpenUI(UIID.UICMainMenu);
                break;
        }
    }


}
