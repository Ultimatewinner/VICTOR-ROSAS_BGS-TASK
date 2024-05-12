using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShop : MonoBehaviour
{
    public GameObject Shop;
    public bool isShopOpen = false;

    public void ToggleTheShop()
    {
        isShopOpen = !isShopOpen;
        Shop.SetActive(isShopOpen);
    }
}
