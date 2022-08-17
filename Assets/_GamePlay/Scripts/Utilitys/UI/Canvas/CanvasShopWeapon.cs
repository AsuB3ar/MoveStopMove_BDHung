using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;
using MoveStopMove.ContentCreation;

public class CanvasShopWeapon : UICanvas
{
    [SerializeField]
    private List<ItemData> itemDatas = new List<ItemData>();
    [SerializeField]
    private Transform ContentTF;
    private List<UIItem> items = new List<UIItem>();
    

    private void Start()
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            GameObject uiItem = PrefabManager.Inst.PopFromPool(PoolID.UIItem);
            uiItem.transform.position = Vector3.zero;

            UIItem UIItemScript = Cache.GetUIItem(uiItem);
            UIItemScript.SetIcon(itemDatas[i].icon);
            UIItemScript.SetData(itemDatas[i].poolID, itemDatas[i].type);

            uiItem.transform.SetParent(ContentTF);

            Subscribe(UIItemScript);
        }
    }

    public override void Open()
    {
        base.Open();
        GameplayManager.Inst.SetCameraPosition(CameraPosition.ShopWeapon);
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

    public void OnItemClick(PoolID name, UIItemType type)
    {
        if (type == UIItemType.Weapon)
        {
            GameObject weapon = PrefabManager.Inst.PopFromPool(name);
            GameplayManager.Inst.PlayerScript.ChangeWeapon(Cache.GetBaseWeapon(weapon));
        }
    }

    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        Close();
    }

}
