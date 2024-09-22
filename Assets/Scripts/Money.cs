using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour, IDataPersistence
{
    public int moneyValue;
    [SerializeField] TMP_Text moneyTextUpgrade;
    [SerializeField] TMP_Text moneyTextUnlock;
    [SerializeField] TMP_Text moneyTextHUD;

    public void LoadData(GameData data)
    {
        moneyValue = data.money;
    }

    public void SaveData(ref GameData data)
    {
        data.money = moneyValue;
    }

    private void Start() 
    {
        UpdateMoneyText();
    }

    public bool CanAfford(int amount)
    {
        return moneyValue >= amount;
    }

    public void ReduceMoney(int amount)
    {
        if (CanAfford(amount))
        {
            moneyValue -= amount;
            Debug.Log("Uang berkurang: " + amount + ". Uang sekarang: " + moneyValue);
            UpdateMoneyText();
        }
    }

    public void AddMoney(int amount)
    {
        moneyValue += amount;
        Debug.Log("Uang bertambah: " + amount + ". Uang sekarang: " + moneyValue);
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        if (moneyTextUpgrade != null && moneyTextUnlock != null && moneyTextHUD != null)
        {
            if(moneyValue < 1000)
            {
                moneyTextUnlock.text = moneyValue.ToString("0.#")+"rb";
                moneyTextUpgrade.text = moneyValue.ToString("0.#")+"rb";
                moneyTextHUD.text = moneyValue.ToString("0.#")+"rb";
            }
            else
            {
                moneyTextUnlock.text = ((float)moneyValue/1000f).ToString("0.##")+"jt";
                moneyTextUpgrade.text = ((float)moneyValue/1000f).ToString("0.##")+"jt";
                moneyTextHUD.text = ((float)moneyValue/1000f).ToString("0.##")+"jt";
            }
        }
    }
}
