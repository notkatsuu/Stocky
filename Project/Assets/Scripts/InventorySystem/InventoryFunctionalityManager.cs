using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryFunctionalityManager : MonoBehaviour
{
    public static InventoryFunctionalityManager Instance;

    public GameObject selectedItem;
    public int coins;

    [SerializeField] private Transform playerInventory;
    [SerializeField] private Transform shopInventory;
    [SerializeField] private Text coinsText;
    [SerializeField] private ConsumeItem itemConsumer;
    [SerializeField] private Animator shopkeeperAnimator;
    [SerializeField] private int health;

    private InventoryFunctionalityManager()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        selectedItem = null;
    }

    public void Awake()
    {
        ReloadCoins();
    }

    public void SetSelectedItem(GameObject gameObject)
    {
        selectedItem = gameObject;
    }

    public void SellSelectedItem()
    {
        if (selectedItem != null)
        {
            Debug.Log("Selling Item");

            if (selectedItem.GetComponent<InventorySlotPlayer>() && selectedItem.transform.childCount != 0)
            {
                Transform item = selectedItem.transform.GetChild(0);

                for (int i = 0; i < shopInventory.childCount; i++)
                {
                    if (shopInventory.GetChild(i).childCount == 0)
                    {
                        if (item.GetComponent<InventoryUIElement>().Ammount > 1)
                        {
                            item.GetComponent<InventoryUIElement>().Ammount--;
                            Transform duplicate = Instantiate(item, shopInventory.GetChild(i));
                            duplicate.GetComponent<InventoryUIElement>().Ammount = 1;
                            duplicate.GetComponent<InventoryUIElement>().ReloadString();
                        }
                        else
                        {
                            item.SetParent(shopInventory.GetChild(i));
                        }

                        item.GetComponent<InventoryUIElement>().ReloadString();
                        coins += item.GetComponent<InventoryUIElement>().item.Price;
                        ReloadCoins();
                        shopkeeperAnimator.SetBool("Laugh", true);
                        shopkeeperAnimator.SetBool("Laugh", false);
                        break;
                    }
                    else
                    {
                        if (shopInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().item == item.GetComponent<InventoryUIElement>().item)
                        {
                            if (item.GetComponent<InventoryUIElement>().Ammount > 1)
                            {
                                item.GetComponent<InventoryUIElement>().Ammount--;
                                shopInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().Ammount++;
                                shopInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().ReloadString();
                                item.GetComponent<InventoryUIElement>().ReloadString();
                            }
                            else
                            {
                                shopInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().Ammount++;
                                shopInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().ReloadString();
                                Destroy(item.gameObject);
                            }

                            coins += item.GetComponent<InventoryUIElement>().item.Price;
                            ReloadCoins();
                            shopkeeperAnimator.SetBool("Laugh", true);
                            shopkeeperAnimator.SetBool("Laugh", false);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void UseSelectedItem()
    {
        if (selectedItem != null)
        {
            Debug.Log("Using Item");

            if (selectedItem.GetComponent<InventorySlotPlayer>() && selectedItem.transform.childCount != 0)
            {
                Transform item = selectedItem.transform.GetChild(0);

                if (item.GetComponent<InventoryUIElement>().item is ConsumableItem consumableItem)
                {
                    consumableItem.Use(itemConsumer);
                    item.GetComponent<InventoryUIElement>().Ammount--;
                    item.GetComponent<InventoryUIElement>().ReloadString();
                    if (item.GetComponent<InventoryUIElement>().Ammount <= 0)
                    {
                        Destroy(item.gameObject);
                    }
                }
            }
        }
    }

    public void BuySelectedItem()
    {
        if (selectedItem != null)
        {
            Debug.Log("Buying Item");

            if (selectedItem.GetComponent<InventorySlotShop>() && selectedItem.transform.childCount != 0)
            {
                Transform item = selectedItem.transform.GetChild(0);

                if (item.GetComponent<InventoryUIElement>().item.Price <= coins)
                {
                    for (int i = 0; i < playerInventory.childCount; i++)
                    {
                        if (playerInventory.GetChild(i).childCount == 0)
                        {
                            if (item.GetComponent<InventoryUIElement>().Ammount > 1)
                            {
                                item.GetComponent<InventoryUIElement>().Ammount--;
                                Transform duplicate = Instantiate(item, playerInventory.GetChild(i));
                                duplicate.GetComponent<InventoryUIElement>().Ammount = 1;
                                duplicate.GetComponent<InventoryUIElement>().ReloadString();
                            }
                            else
                            {
                                item.SetParent(playerInventory.GetChild(i));
                            }

                            item.GetComponent<InventoryUIElement>().ReloadString();
                            coins -= item.GetComponent<InventoryUIElement>().item.Price;
                            ReloadCoins();
                            shopkeeperAnimator.SetBool("Laugh", true);
                            shopkeeperAnimator.SetBool("Laugh", false);
                            break;
                        }
                        else
                        {
                            if (playerInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().item == item.GetComponent<InventoryUIElement>().item)
                            {
                                if (item.GetComponent<InventoryUIElement>().Ammount > 1)
                                {
                                    item.GetComponent<InventoryUIElement>().Ammount--;
                                    playerInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().Ammount++;
                                    playerInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().ReloadString();
                                    item.GetComponent<InventoryUIElement>().ReloadString();
                                }
                                else
                                {
                                    playerInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().Ammount++;
                                    playerInventory.GetChild(i).GetChild(0).GetComponent<InventoryUIElement>().ReloadString();
                                    Destroy(item.gameObject);
                                }

                                coins -= item.GetComponent<InventoryUIElement>().item.Price;
                                ReloadCoins();
                                shopkeeperAnimator.SetBool("Laugh", true);
                                shopkeeperAnimator.SetBool("Laugh", false);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void ReloadCoins()
    {
        coinsText.text = coins.ToString();
    }

    public void ReloadHealth()
    {
        coinsText.text = coins.ToString();
    }

    public void AddCoins(int coinsToAdd)
    {
        coins -= coinsToAdd;
        ReloadCoins();
    }
}
