using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Added for Text component

public class InventorySystem : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();

    public GameObject inventoryUI;
    public Transform itemsParent; // Parent transform for instantiated item prefabs

    private bool isInventoryOpen = false;

    // Dictionary to store references to instantiated item prefabs
    private Dictionary<InventoryItem, GameObject> itemPrefabInstances = new Dictionary<InventoryItem, GameObject>();

    void Start()
    {
        if (inventory.Count > 0)
        {
            foreach (var item in inventory)
            {
                CreateItemUI(item);
            }
        }

        DisplayInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryUI.SetActive(isInventoryOpen);
    }

    // Method to add items to inventory
    public void AddItem(InventoryItem item)
    {
        // Check if the item is stackable and if it's already in the inventory
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

        // If not stackable or not already in inventory, add as new item
        inventory.Add(item);
        CreateItemUI(item);
    }

    private void CreateItemUI(InventoryItem item)
    {
        // Instantiate the item prefab and set its sprite
        GameObject itemPrefabInstance = Instantiate(item.prefab, itemsParent);

        // Store the reference to the instantiated prefab
        itemPrefabInstances[item] = itemPrefabInstance;
    }

    private void UpdateItemUI(InventoryItem item)
    {
        // Update the quantity display of the item prefab
        GameObject itemPrefabInstance = itemPrefabInstances[item];
        itemPrefabInstance.GetComponentInChildren<Text>().text = item.quantity.ToString();
    }

    public void RemoveItem(string itemName, int quantity)
    {
        InventoryItem itemToRemove = inventory.Find(item => item.itemName == itemName);
        if (itemToRemove != null)
        {
            itemToRemove.quantity -= quantity;
            if (itemToRemove.quantity <= 0)
            {
                inventory.Remove(itemToRemove);
            }
        }
    }

    public void SellItem(string itemName)
    {
        InventoryItem itemToSell = inventory.Find(item => item.itemName == itemName);
        if (itemToSell != null)
        {
            // Add currency based on sell price
            // (You would implement your currency system here)
            // For simplicity, let's just print the sell price for now
            Debug.Log("Sold " + itemName + " for " + itemToSell.sellPrice + " coins.");
            inventory.Remove(itemToSell);
        }
    }

    public void BuyItem(string itemName, int quantity, bool stackable, int sellPrice)
    {
        // You would implement the buying logic here
        // For simplicity, let's just add the item directly to inventory
        AddItem(new InventoryItem() { itemName = itemName, quantity = quantity, stackable = stackable, sellPrice = sellPrice });
        Debug.Log("Bought " + quantity + " " + itemName + "(s).");
    }

    public void DisplayInventory()
    {
        Debug.Log("Inventory:");
        foreach (InventoryItem item in inventory)
        {
            Debug.Log(item.itemName + " - Quantity: " + item.quantity);
        }
    }

    private InventoryItem CreateInventoryItem(string itemName, int quantity, bool stackable, int sellPrice)
    {
        return new InventoryItem() { itemName = itemName, quantity = quantity, stackable = stackable, sellPrice = sellPrice };
    }
}
