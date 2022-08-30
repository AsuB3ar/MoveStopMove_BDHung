using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoveStopMove.Manager;
using MoveStopMove.ContentCreation;
using UnityEngine.UI;
using Utilitys;
using MoveStopMove.Core.Data;
using TMPro;

public class CanvasShopSkin : UICanvas
{
    [SerializeField]
    TMP_Text cash;  
    [SerializeField]
    Button buyButton;
    [SerializeField]
    TMP_Text buyButtonText;
    [SerializeField]
    List<Button> tabButtons;
    [SerializeField]
    private List<ItemData> hairItemDatas = new List<ItemData>();
    [SerializeField]
    private List<ItemData> pantItemDatas = new List<ItemData>(); 
    [SerializeField]
    private List<ScrollViewController> scrollViews;
    private List<UIItem> items = new List<UIItem>();

    private GameData Data;
    private Button currentButtonTab;
    private UIItem currentItem;
    private int currentPrice;
    private void Awake()
    {
        Data = GameManager.Inst.GameData;
        currentButtonTab = tabButtons[0];
    }
    private void Start()
    {
        currentButtonTab.Select();
        buyButton.gameObject.SetActive(false);
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
        LoadData();
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
            SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
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

    public void OnItemClick(UIItem item)
    {
        if (!buyButton.gameObject.activeInHierarchy)
        {
            buyButton.gameObject.SetActive(true);
        }

        currentItem = item;
        currentButtonTab.Select();
        currentPrice = item.Price;        

        SoundManager.Inst.PlaySound(SoundManager.Sound.Button_Click);
        if (item.Type == UIItemType.Hair)
        {
            GameplayManager.Inst.PlayerScript.ChangeHair(item.ItemName);
            currentButtonTab = tabButtons[0];
        }
        else if(item.Type == UIItemType.Pant)
        {
            GameplayManager.Inst.PlayerScript.ChangePant(item.PantType);
            currentButtonTab = tabButtons[1];
        }
        buyButtonText.text = currentPrice.ToString();
    }

    private void LoadData()
    {
        cash.text = Data.Cash.ToString();
    }
}
