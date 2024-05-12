using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    public MoneyManager moneyManager;
    public GameObject inventoryUI;
    public Transform itemsParent;
    public InventorySystem shop;
    public bool isShop = false;
    private bool isInventoryOpen = false;

    private Dictionary<InventoryItem, GameObject> itemPrefabInstances = new Dictionary<InventoryItem, GameObject>();

    void Start()
    {
        if (inventory.Count > 0)
        {
            foreach (var item in inventory)
            {
                if (isShop)
                {
                    CreateItemShop(item);
                }
                else
                {
                    CreateItemInv(item);
                }

            }
        }
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isShop)
        {
            ToggleInventory();
            if(shop.inventoryUI.activeInHierarchy)
            {
                shop.ToggleInventory();
            }
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }

    public void AddItemInventory(InventoryItem item)
    {
        if (item.stackable)
        {
            InventoryItem existingItem = inventory.Find(i => i.itemName == item.itemName);
            if (existingItem != null)
            {
                existingItem.quantity++;
                UpdateItemUI(existingItem);
                return;
            }
        }

        inventory.Add(item);
        CreateItemInv(item);
    }
    public void AddItemShop(InventoryItem item)
    {
        if (item.stackable)
        {
            InventoryItem existingItem = inventory.Find(i => i.itemName == item.itemName);
            if (existingItem != null)
            {
                existingItem.quantity++;
                UpdateItemUI(existingItem);
                return;
            }
        }

        inventory.Add(item);
        CreateItemShop(item);
    }

    private void CreateItemInv(InventoryItem item)
    {
        GameObject itemPrefabInstance = Instantiate(item.prefabBuy, itemsParent);

        itemPrefabInstances[item] = itemPrefabInstance;
    }
    private void CreateItemShop(InventoryItem item)
    {
        GameObject itemPrefabInstance = Instantiate(item.prefabSell, itemsParent);

        itemPrefabInstances[item] = itemPrefabInstance;
    }

    private void UpdateItemUI(InventoryItem item)
    {
        GameObject itemPrefabInstance = itemPrefabInstances[item];
        itemPrefabInstance.GetComponentInChildren<Text>().text = item.quantity.ToString();
    }

    public void RemoveItem(InventoryItem item)
    {
        if (item != null)
        {
            inventory.Remove(item);
        }
    }

    public bool SellItem(InventoryItem item)
    {
        if (item != null)
        {
            moneyManager.AddMoney(item.sellPrice);
            RemoveItem(item);
            shop.AddItemShop(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool BuyItem(InventoryItem item)
    {
        if (moneyManager.money > item.buyPrice)
        {
            moneyManager.SubstractMoney(item.buyPrice);
            shop.RemoveItem(item);
            AddItemInventory(item);
            return true;
        }
        else
        {
            return false;
        }
    }
}