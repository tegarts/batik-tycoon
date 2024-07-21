using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingManager : MonoBehaviour
{
    TimeManager timeManager;
    public bool isKoran, isPoster, isBaliho, isPesbuk, isYutup;

    private void Start() 
    {
        timeManager = FindAnyObjectByType<TimeManager>();
    }

    public void KoranAdvertisement()
    {
        
    }
}
