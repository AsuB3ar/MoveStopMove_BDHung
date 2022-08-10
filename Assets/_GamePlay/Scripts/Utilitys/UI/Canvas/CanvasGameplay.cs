using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.Input;

public class CanvasGameplay : UICanvas
{
    public JoyStick joyStick;
    public void SettingButton()
    {
        UIManager.Inst.OpenUI(UIID.UICSetting);
    }

    public void FailButton()
    {
        UIManager.Inst.OpenUI(UIID.UICFail);

        Close();
    }

    public void VictoryButton()
    {
        UIManager.Inst.OpenUI<CanvasVictory>(UIID.UICVictory).OnInitData(Random.Range(0, 100));

        Close();
    }

    
}
