using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public abstract class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    private Sprite slot;
    private Sprite selectedSlot;
    protected Image current;

    public void Awake()
    {
        slot = Resources.Load<Sprite>("Sprites/Inventory/Slots/Slot");
        selectedSlot = Resources.Load<Sprite>("Sprites/Inventory/Slots/SelectedSlot");
        current = GetComponent<Image>();
    }

    public abstract void OnDrop(PointerEventData eventData);
    public void OnPointerClick(PointerEventData eventData)
    {
        if (InventoryFunctionalityManager.Instance.selectedItem != null)
        {
            InventoryFunctionalityManager.Instance.selectedItem.GetComponent<Image>().sprite = slot;
        }

        InventoryFunctionalityManager.Instance.SetSelectedItem(gameObject);
        current.sprite = selectedSlot;
    }
}