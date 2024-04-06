using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlotPlayer : InventorySlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryUIElement inventoryUIItem = dropped.GetComponent<InventoryUIElement>();
        InventoryUIElement otherUIItem;

        if (transform.childCount == 0)
        {
            if (inventoryUIItem.parent.GetComponent<InventorySlotShop>())
            {
                if (inventoryUIItem.item.Price * inventoryUIItem.Ammount > InventoryFunctionalityManager.Instance.coins)
                {
                    return;
                }

                InventoryFunctionalityManager.Instance.AddCoins(inventoryUIItem.item.Price * inventoryUIItem.Ammount);
            }

            inventoryUIItem.parent = transform;
        }
        else
        {
            otherUIItem = transform.GetChild(0).GetComponent<InventoryUIElement>();

            if (otherUIItem.item == inventoryUIItem.item)
            {
                if (inventoryUIItem.parent.GetComponent<InventorySlotShop>())
                {
                    if (inventoryUIItem.item.Price * inventoryUIItem.Ammount > InventoryFunctionalityManager.Instance.coins)
                    {
                        return;
                    }

                    InventoryFunctionalityManager.Instance.AddCoins(inventoryUIItem.item.Price * inventoryUIItem.Ammount);
                }

                otherUIItem.Ammount += inventoryUIItem.Ammount;
                otherUIItem.ReloadString();
                Debug.Log(otherUIItem.Ammount);
                Destroy(inventoryUIItem.gameObject);
            }
            else
            {
                if (inventoryUIItem.parent.GetComponent<InventorySlotPlayer>())
                {
                    otherUIItem.parent = inventoryUIItem.parent;
                    otherUIItem.transform.SetParent(otherUIItem.parent);
                    inventoryUIItem.parent = transform;
                }
            }
        }
    }
}