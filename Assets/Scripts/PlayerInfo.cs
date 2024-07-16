using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int money;
    [SerializeField] public TMP_Text moneyText;

    void Start()
    {
        UpdateMoneyText();
    }

    public bool CanAfford(int amount)
    {
        return money >= amount;
    }

    public void ReduceMoney(int amount)
    {
        if (CanAfford(amount))
        {
            money -= amount;
            Debug.Log("Uang berkurang: " + amount + ". Uang sekarang: " + money);
            UpdateMoneyText();
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        Debug.Log("Uang bertambah: " + amount + ". Uang sekarang: " + money);
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }
}
