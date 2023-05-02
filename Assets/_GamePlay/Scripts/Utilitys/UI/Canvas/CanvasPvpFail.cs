using MoveStopMove.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilitys.Timer;

public class CanvasPvpFail : UICanvas
{
    private const int REVIVE_TIME = 5;
    [SerializeField]
    TMP_Text timeText;

    STimer timer;
    float reviveTime;
    private void Awake()
    {
        timer = TimerManager.Inst.PopSTimer();
    }
    public override void Open()
    {
        base.Open();
        reviveTime = REVIVE_TIME;
        timeText.text = reviveTime.ToString();
        timer.Start(1f, () => 
        {
            reviveTime -= 1;
            timeText.text = reviveTime.ToString();
            if(reviveTime <= 0)
            {
                timer.Stop();
                LevelManager.Inst.RevivePlayer();
                UIManager.Inst.OpenUI(UIID.UICGamePlay);
                Close();
            }
        }, true);
    }
}
