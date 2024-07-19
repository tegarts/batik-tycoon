using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerManager : MonoBehaviour
{
    TimeManager timeManager;

    private void Start() 
    {
        timeManager = FindAnyObjectByType<TimeManager>();
    }

    
}
