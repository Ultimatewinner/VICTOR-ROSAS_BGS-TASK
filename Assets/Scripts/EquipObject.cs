using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipObject : MonoBehaviour
{
    public bool isFire = false, isCrown = false, isHat = false;
    public PlayerController playerController;
    public InventorySystem invSystem;
    public InventoryItem item;

    private void OnEnable()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        invSystem = GameObject.Find("InventoryCanvas").GetComponent<InventorySystem>();
    }

    public void EquipTheObject()
    {
        if (isFire)
        {
            playerController.fire.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (isCrown)
        {
            playerController.anim.SetBool("Crown", true);
            playerController.hat.SetActive(false);
            gameObject.SetActive(false);
        }
        else if (isHat)
        {
            playerController.hat.SetActive(true);
            playerController.anim.SetBool("Crown", false);
            gameObject.SetActive(false);
        }
    }

    public void BuyTheObject()
    {
        bool success = invSystem.BuyItem(item);
        if (success)
        {
            gameObject.SetActive(false);
        }
    }
    public void SellTheObject()
    {
        bool success = invSystem.SellItem(item);
        if (success)
        {
            gameObject.SetActive(false);
        }
    }
}