using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;
using MoveStopMove.ContentCreation;

public class CanvasShopSkin : UICanvas
{
    [SerializeField]
    private List<ItemData> itemDatas = new List<ItemData>();
    [SerializeField]
    private Transform ContentTF;
    private List<UIItem> items = new List<UIItem>();

    private void Start()
    {
        for(int i = 0; i < itemDatas.Count; i++)
        {
            GameObject uiItem = PrefabManager.Inst.PopFromPool(PoolName.UIItem);
            uiItem.transform.position = Vector3.zero;

            UIItem UIItemScript = Cache.GetUIItem(uiItem);
            UIItemScript.SetIcon(itemDatas[i].icon);
            UIItemScript.SetData(itemDatas[i].itemName, itemDatas[i].type);

            uiItem.transform.SetParent(ContentTF);

            Subscribe(UIItemScript);
        }
    }

    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
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

    public void OnItemClick(PoolName name, UIItemType type)
    {
        if(type == UIItemType.Hair)
        {
            GameplayManager.Inst.Player.ChangeHair(name);
        }
    }
}
