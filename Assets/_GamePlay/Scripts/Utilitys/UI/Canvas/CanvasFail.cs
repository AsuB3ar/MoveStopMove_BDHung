using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoveStopMove.Manager;

public class CanvasFail : UICanvas
{
    [SerializeField]
    TMP_Text text;
    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void SetRank(int rank)
    {
        text.text = '#'+rank.ToString();
    }
}
