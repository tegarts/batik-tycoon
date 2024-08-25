using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour
{
    public static Daily instance { get; private set; }
    bool isStarted;
    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    [SerializeField] float progress = 0;
    [Header("Rating")]
    public GameObject[] stars;
    float maxProgress;
    float progress40;
    float progress60;
    float progress80;

    [Header("Reaction")]
    public int happyReaction;
    public int flatReaction;
    public int angryReaction;
    [Header("References")]
    NPCSpawn nPCSpawn;
    DayManager dayManager;
    BookMenu bookMenu;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        nPCSpawn = FindAnyObjectByType<NPCSpawn>();
        dayManager = FindAnyObjectByType<DayManager>();
        bookMenu = FindAnyObjectByType<BookMenu>();

        progressBar.value = progress;
        progressBar.maxValue = maxProgress;


    }

    public void IncreaseProgress(float value)
    {
        progress += value;
        progressBar.value = progress;
    }

    private void Update()
    {
        if (dayManager.dayIsStarted)
        {
            if (!isStarted)
            {
                progress = 0;
                progressBar.value = progress;
                isStarted = true;
            }
            progressBar.maxValue = nPCSpawn.initializeNPC * 25;
            progress40 = nPCSpawn.initializeNPC * 20 * 0.4f;
            progress60 = nPCSpawn.initializeNPC * 20 * 0.6f;
            progress80 = nPCSpawn.initializeNPC * 20 * 0.8f;
        }
        else if (!dayManager.dayIsStarted)
        {
            isStarted = false;

            if (dayManager.day == 0)
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].SetActive(false);
                }
            }
            else
            {
                if (progress >= progress80)
                {
                   StartCoroutine(ActivateStarsWithDelay(3));
                }
                else if (progress >= progress60)
                {
                   StartCoroutine(ActivateStarsWithDelay(2));
                }
                else if (progress >= progress40)
                {
                    StartCoroutine(ActivateStarsWithDelay(1));
                }
            }
        }
    }

    private IEnumerator ActivateStarsWithDelay(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
            stars[i].GetComponent<Star>().isStarted = true;
            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before activating the next star
        }
    }

}
