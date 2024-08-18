using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    public float textGoneDelay;
    private void Start() 
    {
        StartCoroutine(TextGone());
    }

    IEnumerator TextGone()
    {
        yield return new WaitForSeconds(textGoneDelay);
        Destroy(gameObject);
    }
}
