using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LongPressButton : MonoBehaviour
{
      public Image fillImage; // Reference to the Image that fills up
    public float holdTime = 2f; // Time required to hold the button down
    private bool isHolding = false;
    private float holdCounter = 0f;

    void Update()
    {
        if (isHolding)
        {
            holdCounter += Time.deltaTime;
            fillImage.fillAmount = holdCounter / holdTime;

            if (fillImage.fillAmount >= 1f)
            {
                Debug.Log("Button fully pressed!");
                isHolding = false; // Stop the filling process
                holdCounter = 0f; // Reset the counter if needed
                fillImage.fillAmount = 0f; // Reset the fill amount if needed
            }
        }
    }

    public void OnPointerDown()
    {
        isHolding = true;
    }

    public void OnPointerUp()
    {
        isHolding = false;
        holdCounter = 0f;
        fillImage.fillAmount = 0f;
    }
}
