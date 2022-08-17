using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoveStopMove.Manager;
public class CanvasMainMenu : UICanvas
{
    bool isDirty = false;
    public void PlayGameButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        GameManager.Inst.StartGame();
        Close();
    }

    public void ShopSkinButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopSkin);
        Close();
    }

    public void ShopWeaponButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopWeapon);
        Close();
    }

    public override void Open()
    {
        base.Open();
        if (isDirty)
        {
            //GameplayManager.Inst.PlayerScript.Reset();
            LevelManager.Inst.DestructLevel();
            LevelManager.Inst.OpenLevel(1);
            GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        }
        else
        {
            isDirty = true;
        }
    }
}
