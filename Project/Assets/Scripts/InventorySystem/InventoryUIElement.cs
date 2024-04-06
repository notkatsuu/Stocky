using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUIElement: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Text ammountText;

    [HideInInspector] public Transform parent;
    public int startAmmount;
    public ItemBasic item;
    public int Ammount { get; set; }
    

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = item.ImageUI;
        Ammount = startAmmount;
        ammountText = GetComponentInChildren<Text>();
        ReloadString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
        parent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        transform.SetParent(parent); 
        image.raycastTarget = true;
    }

    public void ReloadString()
    {
        ammountText.text = Ammount.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //this.tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //this.tooltip.SetActive(false);
    }
}
