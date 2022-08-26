using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using MoveStopMove.Manager;
using MoveStopMove.Core.Data;

public class CanvasMainMenu : UICanvas,IPersistentData
{
    bool isDirty = false;
    [SerializeField]
    TMP_Text heightRank;
    [SerializeField]
    TMP_Text region;
    public void PlayGameButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        GameManager.Inst.StartGame();
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void ShopSkinButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopSkin);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void ShopWeaponButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopWeapon);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public override void Open()
    {
        base.Open();
        if (isDirty)
        {
            //GameplayManager.Inst.PlayerScript.Reset();
            GameManager.Inst.StopGame();
            LevelManager.Inst.OpenLevel(1);
            SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
            GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        }
        else
        {
            isDirty = true;
        }
    }

    public void LoadGame(GameData data)
    {
        //heightRank.text = data.HighestRank.ToString();
        //region.text = data.CurrentRegion.ToString();

    }

    public void SaveGame(ref GameData data)
    {
        
    }
}
