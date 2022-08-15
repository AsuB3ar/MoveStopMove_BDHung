using MoveStopMove.Core;
using MoveStopMove.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.Input;

public class CanvasGameplay : UICanvas
{
    private readonly Vector3 TARGET_INDICATOR_UP = Vector3.up * 1.5f;
    float minX = UITargetIndicator.WIDTH / 2;
    float maxX = Screen.width - UITargetIndicator.WIDTH / 2;

    float minY = UITargetIndicator.HEIGHT / 2;
    float maxY = Screen.height - UITargetIndicator.HEIGHT / 2;

    public JoyStick joyStick;
    [SerializeField]
    Transform CanvasTF;
    Dictionary<BaseCharacter, UITargetIndicator> indicators = new Dictionary<BaseCharacter, UITargetIndicator>();
    List<BaseCharacter> characters = new List<BaseCharacter>();
    Camera playerCamera;

    

    private void Start()
    {
        playerCamera = GameplayManager.Inst.PlayerCamera;
    }
    public void Update()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            indicators[characters[i]].SetLevel(characters[i].Level);
            Vector2 pos = playerCamera.WorldToScreenPoint(characters[i].transform.position + TARGET_INDICATOR_UP * characters[i].Size);

            if(Vector3.Dot(characters[i].transform.position - playerCamera.transform.position,playerCamera.transform.forward) < 0)
            {                               
                pos.y = minY;             
            }
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            indicators[characters[i]].transform.position = pos ;
            
        }
    }

    public void SubscribeTarget(BaseCharacter character)
    {
        GameObject uiIndicator = PrefabManager.Inst.PopFromPool(PoolID.UITargetIndicator);
        UITargetIndicator indicatorScript = Cache.GetUIIndicator(uiIndicator);
        //indicatorScript.SetColor(new UnityEngine.Color(1f, 107f/255, 107f/255, 1f));
        indicatorScript.SetColor(GameplayManager.Inst.GetColor(character.Color));
        uiIndicator.transform.SetParent(CanvasTF);
        character.OnDie += UnsubcribeTarget;
        indicators.Add(character, indicatorScript);
        characters.Add(character);
    }

    public void UnsubcribeTarget(BaseCharacter character)
    {
        PrefabManager.Inst.PushToPool(indicators[character].gameObject, PoolID.UITargetIndicator);
        character.OnDie -= UnsubcribeTarget;
        indicators.Remove(character);
        characters.Remove(character);
    }
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
