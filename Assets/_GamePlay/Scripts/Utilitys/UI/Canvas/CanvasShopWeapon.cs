using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;
using MoveStopMove.ContentCreation;
using MoveStopMove.Core.Data;
using TMPro;

public class CanvasShopWeapon : UICanvas
{
    [SerializeField]
    TMP_Text cash;
    [SerializeField]
    private List<ItemData> itemDatas = new List<ItemData>();
    [SerializeField]
    private Transform ContentTF;
    private List<UIItem> items = new List<UIItem>();

    [SerializeField]
    GameObject cameraScreenSpace;

    GameData Data;
    ItemData weaponUnlock;
    private void Awake()
    {
        Data = GameManager.Inst.GameData;
    }

    private void Start()
    {
        for (int i = 0; i < itemDatas.Count; i++)
        {
            GameObject uiItem = PrefabManager.Inst.PopFromPool(PoolID.UIItem);
            uiItem.transform.position = Vector3.zero;

            UIItem UIItemScript = Cache.GetUIItem(uiItem);
            UIItemScript.SetData(itemDatas[i]);
            UIItemScript.SetLock(itemDatas[i].state);

            uiItem.transform.SetParent(ContentTF);

            Subscribe(UIItemScript);
        }
    }

    public override void Open()
    {
        base.Open();
        GameplayManager.Inst.SetCameraPosition(CameraPosition.ShopWeapon);
        //cameraScreenSpace.SetActive(true);
        LoadData();
        
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

    public void OnItemClick(UIItem item)
    {
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);

        if (item.Type == UIItemType.Weapon)
        {
            GameObject weapon = PrefabManager.Inst.PopFromPool(item.ItemName);
            GameplayManager.Inst.PlayerScript.ChangeWeapon(Cache.GetBaseWeapon(weapon));
        }
    }

    public override void Close()
    {
        base.Close();
        //cameraScreenSpace.SetActive(false);
    }
    public void CloseButton()
    {
        UIManager.Inst.OpenUI(UIID.UICMainMenu);
        GameplayManager.Inst.SetCameraPosition(CameraPosition.MainMenu);
        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        Close();
    }

    private void LoadData()
    {
        cash.text = Data.Cash.ToString();

        for(int i = 0; i < itemDatas.Count; i++)
        {
            itemDatas[i].state = (ItemState)Data.PoolID2State[itemDatas[i].poolID];
            if(itemDatas[i].state == ItemState.Unlock)
            {
                Debug.Log(itemDatas[i].poolID);
                weaponUnlock = itemDatas[i];
            }
        }
    }

}
