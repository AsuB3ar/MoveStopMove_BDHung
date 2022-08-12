using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public enum UIItemType
{
    Hair = 0,
    Pant = 1
}
public class UIItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public event Action<PoolName, UIItemType> OnSelectItem;
    [SerializeField]
    PoolName itemName;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
