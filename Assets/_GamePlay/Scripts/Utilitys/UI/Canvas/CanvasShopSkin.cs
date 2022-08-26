using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;
using MoveStopMove.ContentCreation;
using UnityEngine.UI;
using Utilitys;
public class CanvasShopSkin : UICanvas
{
    [SerializeField]
    Button firstButton;
    [SerializeField]
    private List<ItemData> hairItemDatas = new List<ItemData>();
    [SerializeField]
    private List<ItemData> pantItemDatas = new List<ItemData>(); 
    [SerializeField]
    private List<ScrollViewController> scrollViews;
    private List<UIItem> items = new List<UIItem>();

    private void Start()
    {
        firstButton.Select();
        for(int i = 0; i < hairItemDatas.Count; i++)
        {
            UIItem UIItemScript = scrollViews[0].AddUIItem(hairItemDatas[i]);       
            Subscribe(UIItemScript);
        }

        for(int i = 0; i < pantItemDatas.Count; i++)
        {
            UIItem UIItemScript = scrollViews[1].AddUIItem(pantItemDatas[i]);
            Subscribe(UIItemScript);
        }
    }
    public override void Open()
    {
        base.Open();
        GameplayManager.Inst.SetCameraPosition(CameraPosition.ShopSkin);
    }
    public void OpenTab(int type)
    {
        for (int i = 0; i < scrollViews.Count; i++)
        {
            if (i == type)
            {
                scrollViews[i].gameObject.SetActive(true);
                continue;
            }
            scrollViews[i].gameObject.SetActive(false);
        }
    }
    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        Close();
    }
    
    public void Subscribe(UIItem item)
    {
        items.Add(item);
        item.OnSelectItem += OnItemClick;
    }

    public void UnSubscribe(UIItem item)
    {
        items.Remove(item);
        item.OnSelectItem -= OnItemClick;
    }

    public void OnItemClick(PoolID name,PantSkin pant ,UIItemType type)
    {
        if(type == UIItemType.Hair)
        {
            GameplayManager.Inst.PlayerScript.ChangeHair(name);
        }
        else if(type == UIItemType.Pant)
        {
            GameplayManager.Inst.PlayerScript.ChangePant(pant);
        }
    }
}
