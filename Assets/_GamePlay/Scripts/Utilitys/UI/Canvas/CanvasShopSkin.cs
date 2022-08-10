using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;

public class CanvasShopSkin : UICanvas
{
    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        Close();
    }
}
