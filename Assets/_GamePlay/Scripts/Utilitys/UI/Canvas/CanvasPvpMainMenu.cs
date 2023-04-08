using MoveStopMove.Manager;
using UnityEngine;

public class CanvasPvpMainMenu : UICanvas
{
    public void PlayGameButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        GameManager.Inst.StartGame();
        Close();
    }

    public void SkinShopButton()
    {
        UIManager.Inst.OpenUI(UIID.UICShopSkin);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void WeaponShopButton()
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
            //GameplayManager.Inst.PlayerScript.Reset();
        GameManager.Inst.StopGame();
        LevelManager.Inst.OpenLevel(GameManager.Inst.GameData.CurrentRegion);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        UpdateData();
    }

    public void UpdateData()
    {

    }
}
