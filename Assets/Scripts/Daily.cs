using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public static Daily instance { get; private set; }
    [SerializeField] Slider progressBar;
    [SerializeField] float progress = 0;
    float maxProgress = 100;
    private void Start() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        progressBar.value = progress;
        progressBar.maxValue = maxProgress;
    }

    public void IncreaseProgress(float value)
    {
        progress += value;
        progressBar.value = progress;
    }
}
