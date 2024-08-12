using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour, IDataPersistence
{
    public int day, hour, minute;
    [SerializeField] TMP_Text dayText, clockText;
    public bool isStartDay;
    double second;

    private const int TIMESCALE = 180; // 180

    public void LoadData(GameData data)
    {
       day = data.day;
    }

    public void SaveData(ref GameData data)
    {
        if(!isStartDay)
        {
            data.day = day;
        }
    }

    private void Start() 
    {
        hour = 18;
        TextCallFunction();
    }

    private void Update() 
    {
        if(isStartDay)
        {
            CalculateTime();
            TextCallFunction();
        }
        
    }

    void CalculateTime()
    {
        second += Time.deltaTime * TIMESCALE;

        if(second>=60)
        {
            minute++;
            second = 0;
        } 
        else if(minute>=60)
        {
            hour++;
            minute=0;
        }

        if(hour >= 18)
        {
            isStartDay = false;
        }
    }

    void TextCallFunction()
    {
        dayText.text = "Day " + day;
        if (minute % 10 == 0)
        {
            clockText.text = string.Format("{0:00}:{1:00}", hour, minute);
        }
    }
}
