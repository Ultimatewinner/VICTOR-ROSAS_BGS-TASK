using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = money.ToString() + "$";
    }

    public void SubstractMoney(int moneyNegative)
    {
        money -= moneyNegative;
        UpdateMoney();
    }

    public void AddMoney(int moneyPositive)
    {
        money += moneyPositive;
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        moneyText.text = money.ToString() + "$";
    }
}
