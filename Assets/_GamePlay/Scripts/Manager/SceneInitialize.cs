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
        STANDARD_PVE = 2,
        STANDARD_PVP = 3,
    }
    [SerializeField]
    SCENE_TYPE type;

    [ConditionalField(nameof(type), false, SCENE_TYPE.INIT)]
    [SerializeField]
    GameData gameData;
    [ConditionalField(nameof(type), false, SCENE_TYPE.STANDARD_PVE, SCENE_TYPE.STANDARD_PVP)]
    [SerializeField]
    Camera playerCamera;
    [ConditionalField(nameof(type), false, SCENE_TYPE.STANDARD_PVE)]
    [SerializeField]
    BaseCharacter playerScript;
    [ConditionalField(nameof(type), false, SCENE_TYPE.STANDARD_PVE, SCENE_TYPE.STANDARD_PVP)]
    [SerializeField]
    GameObject targetIndicator;
    [ConditionalField(nameof(type), false, SCENE_TYPE.STANDARD_PVE, SCENE_TYPE.STANDARD_PVP)]
    [SerializeField]
    CameraMove cameraMove;
    [ConditionalField(nameof(type), false, SCENE_TYPE.STANDARD_PVE, SCENE_TYPE.STANDARD_PVP)]
    [SerializeField]
    GameObject playerPvp;
    private void Awake()
    {
        switch (type)
        {
            case SCENE_TYPE.INIT:
                SceneManager.Inst.LoadScene(GAMECONST.LOAD_START_SCENE);
                SceneManager.Inst._OnSceneLoaded += (name) =>
                {
                    if (string.Compare(name, GAMECONST.LOAD_START_SCENE) == 0)
                    {
                        SceneManager.Inst.LoadScene(GAMECONST.STANDARD_PVE_SCENE);
                    }
                };
                break;
            case SCENE_TYPE.LOAD_START:
                break;
            case SCENE_TYPE.STANDARD_PVE:
                GameplayManager.Inst.GameMode = GAMECONST.GAMEPLAY_MODE.STANDARD_PVE;
                GameplayManager.Inst.PlayerScript = playerScript;
                GameplayManager.Inst.PlayerCamera = playerCamera;
                GameplayManager.Inst.TargetIndicator = targetIndicator;
                GameplayManager.Inst.CameraMove = cameraMove;
                gameData.OnInitData();
                
                break;
            case SCENE_TYPE.STANDARD_PVP:
                GameplayManager.Inst.GameMode = GAMECONST.GAMEPLAY_MODE.STANDARD_PVP;
                GameplayManager.Inst.PlayerCamera = playerCamera;
                GameplayManager.Inst.TargetIndicator = targetIndicator;
                GameplayManager.Inst.CameraMove = cameraMove;

                GameObject character = NetworkManager.Inst.Instantiate(playerPvp.name);
                //character.transform.parent = Level;
                BaseCharacter characterScript = Cache.GetBaseCharacter(character);
                GameplayManager.Inst.PlayerScript = characterScript;
                GameplayManager.Inst.SetCameraFollow(GameplayManager.Inst.Player.transform);
                GameplayManager.Inst.Player.transform.parent = gameObject.transform;
                GameplayManager.Inst.PlayerScript.OnInit();
                break;
        }
    }

    private void Start()
    {
        switch (type)
        {
            case SCENE_TYPE.STANDARD_PVE:
                UIManager.Inst.OpenUI(UIID.UICMainMenu);
                break;
            case SCENE_TYPE.STANDARD_PVP:
                UIManager.Inst.OpenUI(UIID.UICPvpMainMenu);
                break;
        }
    }


}
