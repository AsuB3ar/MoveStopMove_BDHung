using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFail : UICanvas
{
    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);

        Close();
    }
}
