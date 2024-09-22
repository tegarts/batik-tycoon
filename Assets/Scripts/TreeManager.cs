using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(PlayAnimationAfterDelay());
    }

    IEnumerator PlayAnimationAfterDelay()
    {
        // Menunggu waktu acak antara 1 hingga 5 detik
        float randomDelay = Random.Range(1.0f, 5.0f);
        yield return new WaitForSeconds(randomDelay);

        // Setelah 2 detik, animasi akan berjalan
        anim.SetBool("isIdle", true); // Tetap idle sebelum waktu tunggu selesai
    }
}
