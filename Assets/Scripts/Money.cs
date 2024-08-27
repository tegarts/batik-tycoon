using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour, IDataPersistence
{
    public int moneyValue;
    [SerializeField] TMP_Text moneyTextInsideBook;
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
        if (moneyTextInsideBook != null && moneyTextHUD != null)
        {
            if(moneyValue < 1000)
            {
                moneyTextInsideBook.text = moneyValue.ToString("0.#")+"rb";
                moneyTextHUD.text = moneyValue.ToString("0.#")+"rb";
            }
            else
            {
                moneyTextInsideBook.text = ((float)moneyValue/1000f).ToString("0.##")+"jt";
                moneyTextHUD.text = ((float)moneyValue/1000f).ToString("0.##")+"jt";
            }
        }
    }
}
