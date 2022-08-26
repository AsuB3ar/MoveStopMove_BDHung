using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MoveStopMove.Manager;
public class CanvasVictory : UICanvas
{
    public TMP_Text score_txt;
    public int currentLevel = 1;

    public void SetScore(int score)
    {
        score_txt.text = score.ToString();
    }
    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }

    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void NextLevelButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        LevelManager.Inst.OpenLevel(currentLevel + 1);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    public void PlayAgainButton()
    {
        UIManager.Inst.OpenUI(UIID.UICGamePlay);
        LevelManager.Inst.OpenLevel(currentLevel);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }
}
