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
        Close();
    }

    public void NextLevelButton()
    {
        LevelManager.Inst.OpenLevel(currentLevel);
        Close();
    }

    public void PlayAgainButton()
    {

    }
}
