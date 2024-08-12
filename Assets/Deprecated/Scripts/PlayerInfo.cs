using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour, IDataPersistence
{
    public int money;
    public int kain;
    [SerializeField] public TMP_Text moneyText;
    public TMP_Text kainText;
    TimeManager timeManager;

    
    public void LoadData(GameData data)
    {
        money = data.money;
        kain = data.kain;
    }

    public void SaveData(ref GameData data)
    {
        if(!timeManager.isStartDay)
        {
            data.money = money;
            data.kain = kain;
        }
    }

    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        UpdateMoneyText();
        UpdateKainText();
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

    public void AddKain(int amount)
    {
        kain += amount;
        Debug.Log("Kain nambah: " + amount);
        UpdateKainText();
    }

    public void ReduceKain(int amount)
    {
        kain -= amount;
        Debug.Log("Kain ngurang: " + amount);
        UpdateKainText();
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = money.ToString();
        }
    }

    private void UpdateKainText()
    {
        if(kainText != null)
        {
            kainText.text = kain.ToString();
        }
    }
}
