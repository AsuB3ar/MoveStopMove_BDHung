using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public enum UIItemType
{
    Hair = 0,
    Pant = 1,
    Weapon = 2
}
public class UIItem : MonoBehaviour
{
    public event Action<PoolID, UIItemType> OnSelectItem;
    [SerializeField]
    PoolID itemName;
    [SerializeField]
    UIItemType type;
    [SerializeField]
    Image icon;
    [SerializeField]
    Image background;

    Color color;
    private void Start()
    {
        color = background.color;
    }

    public void OnItemClicked()
    {
        OnSelectItem?.Invoke(itemName, type);
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void SetData(PoolID itemName, UIItemType type)
    {
        this.itemName = itemName;
        this.type = type;
    }
}
