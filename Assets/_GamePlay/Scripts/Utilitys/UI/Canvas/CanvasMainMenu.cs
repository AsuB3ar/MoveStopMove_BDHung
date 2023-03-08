using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using MoveStopMove.Manager;
using MoveStopMove.Core.Data;

public class CanvasMainMenu : UICanvas
{
    bool isDirty = false;
    
    [SerializeField]
    TMP_Text descriptionPlayText;
    [SerializeField]
    TMP_Text cash;


    GameData Data;
    private const string ZONE = "Zone:";
    private const string BEST = " - Best:#";

    public void Awake()
    {
        Data = GameManager.Inst.GameData;
    }
    public void PlayGameButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        GameManager.Inst.StartGame();
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
        CanvasShopWeapon shopCanvas = UIManager.Inst.OpenUI<CanvasShopWeapon>(UIID.UICShopWeapon, RenderMode.ScreenSpaceCamera);
        shopCanvas.SetCanvasCamera(GameplayManager.Inst.PlayerCamera);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void SettingButton()
    {
        UIManager.Inst.OpenUI(UIID.UICSetting);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
    }

    public override void Open()
    {
        base.Open();       
        if (isDirty)
        {
            //GameplayManager.Inst.PlayerScript.Reset();
            GameManager.Inst.StopGame();
            LevelManager.Inst.OpenLevel(Data.CurrentRegion);
            SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);            
        }
        else
        {
            isDirty = true;
        }
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        UpdateData();
    }

    public void UpdateData()
    {
        string des = ZONE + (Data.CurrentRegion + 1).ToString() + BEST + Data.HighestRank.ToString();
        descriptionPlayText.text = des;
        cash.text = Data.Cash.ToString();
    }

}
