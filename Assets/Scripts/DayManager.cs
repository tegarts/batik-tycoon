using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour, IDataPersistence
{
    public int day;
    public bool dayIsStarted;
    public void LoadData(GameData data)
    {
        day = data.day;
    }

    public void SaveData(ref GameData data)
    {
        data.day = day;
    }

    private void Update() 
    {
        if(dayIsStarted)
        {
            
        }    
    }
}
