using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;

public class CanvasSetting : UICanvas
{
    private readonly List<Vector2Int> RESOLUTIONS = new List<Vector2Int>() { new Vector2Int(1080, 1920), new Vector2Int(1440, 1920) };
    public void CloseButton()
    {
        Close();
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
    }

    public void SetResolution(int value)
    {
        if(value == 0)
        {
            GameManager.Inst.SetFullScreen();
        }
        else
        {
            GameManager.Inst.SetResolution(RESOLUTIONS[value - 1].x, RESOLUTIONS[value - 1].y);
        }
        
    }

    public void SetVolume(float value)
    {
        SoundManager.Inst.SetVolume(value);
    }
}
