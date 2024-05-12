using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public GameObject prefab;
    public int quantity;
    public Sprite sprite;
    public bool stackable;
    public int sellPrice;
}
