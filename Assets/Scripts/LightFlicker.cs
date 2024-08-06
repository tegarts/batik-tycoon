using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light pointLight; // Referensi ke komponen Light
    public float minFlickerTime = 0.05f; // Waktu minimum lampu mati atau menyala
    public float maxFlickerTime = 0.2f; // Waktu maksimum lampu mati atau menyala

    private void Start()
    {
        // Mulai coroutine untuk efek flicker
        StartCoroutine(FlickerLight());
    }

    private IEnumerator FlickerLight()
    {
        while (true)
        {
            // Matikan lampu
            pointLight.enabled = false;
            // Tunggu selama waktu acak antara minFlickerTime dan maxFlickerTime
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            // Nyalakan lampu
            pointLight.enabled = true;
            // Tunggu selama waktu acak antara minFlickerTime dan maxFlickerTime
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        }
    }
}
