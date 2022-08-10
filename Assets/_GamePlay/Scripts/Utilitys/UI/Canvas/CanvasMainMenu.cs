using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoveStopMove.Manager;
public class CanvasMainMenu : UICanvas
{
    public void PlayGameButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        GameManager.Inst.StartGame();
        GameplayManager.Inst.SetCameraPosition(CameraPosition.Gameplay);
        Close();
    }

    public void ShopSkinButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopSkin);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.ShopSkin);
        Close();
    }

    public void ShopWeaponButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopWeapon);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.ShopWeapon);
        Close();
    }
}
