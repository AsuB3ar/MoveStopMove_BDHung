using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;

public class CanvasSetting : UICanvas
{
    private readonly List<float> RESOLUTIONS = new List<float>() { 1f,9f/16, 10f/16, 3f/4  };
    public void CloseButton()
    {
        Close();
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
    }

    public void SetResolution(int value)
    {
        GameManager.Inst.SetResolution(RESOLUTIONS[value]);             
    }

    public void SetVolume(float value)
    {
        SoundManager.Inst.SetVolume(value);
    }
}
