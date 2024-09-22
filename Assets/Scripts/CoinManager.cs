using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] bool moveCoin = false;

    public Animator coin;
    AudioSetter coinEnter;

    void Start()
    {
        coinPrefab.SetActive(false);
        coinEnter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }

    public void SpawnCoins()
    {
        StartCoroutine(DelaySpawn());
    }

    IEnumerator DelaySpawn()
    {
        //float randomValue = Random.Range(0, 0.3f);
        //yield return new WaitForSeconds(randomValue);

        coinPrefab.SetActive(true);
        coin.SetTrigger("MoveUp");

        yield return new WaitForSeconds(0.55f);
        coinEnter.PlaySFX(coinEnter.coinEnter);

        yield return new WaitForSeconds(0.4f);
        coinPrefab.SetActive(false);
    }

    // Fungsi untuk memeriksa apakah animasi tertentu telah selesai
    //private bool IsAnimationFinished(string animationName)
    //{
    //    return coin.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
    //           coin.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    //}
}
