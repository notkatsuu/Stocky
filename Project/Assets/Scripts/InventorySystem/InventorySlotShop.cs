using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlotShop : InventorySlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryUIElement inventoryUIItem = dropped.GetComponent<InventoryUIElement>();
        InventoryUIElement otherUIItem;

        if (transform.childCount == 0)
        {
            if (inventoryUIItem.parent.GetComponent<InventorySlotPlayer>())
            {
                InventoryFunctionalityManager.Instance.AddCoins(-inventoryUIItem.item.Price * inventoryUIItem.Ammount);
            }

            inventoryUIItem.parent = transform;
        } 
        else
        {
            otherUIItem = transform.GetChild(0).GetComponent<InventoryUIElement>();

            if (otherUIItem.item == inventoryUIItem.item)
            {
                if (inventoryUIItem.parent.GetComponent<InventorySlotPlayer>())
                {
                    InventoryFunctionalityManager.Instance.AddCoins(-inventoryUIItem.item.Price * inventoryUIItem.Ammount);
                }

                otherUIItem.Ammount += inventoryUIItem.Ammount;
                otherUIItem.ReloadString();
                Debug.Log(otherUIItem.Ammount);
                Destroy(inventoryUIItem.gameObject);
            }
        }
    }
}


