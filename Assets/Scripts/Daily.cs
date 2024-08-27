using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TMP_Text happyCount;
    public TMP_Text flatCount;
    public TMP_Text angryCount;
    public Animator animReaction;

    [Header("Income")]
    public int dailyIncome;
    public TMP_Text dailyIncomeText;
    public float durationCounting = 1f;
    public Animator animFontIncome;

    [Header("References")]
    NPCSpawn nPCSpawn;
    DayManager dayManager;
    BookMenu bookMenu;

    [Header("UI")]
    public GameObject panelHUD;
    public bool isPanelOn;

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
        panelHUD.SetActive(false);

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
                dailyIncome = 0;
                animReaction.gameObject.SetActive(false);

                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].SetActive(false);
                }

                happyReaction = 0;
                flatReaction = 0;
                angryReaction = 0;

                dailyIncome = 0;
            }
            progressBar.maxValue = nPCSpawn.initializeNPC * 25;
            progress40 = nPCSpawn.initializeNPC * 25 * 0.4f;
            progress60 = nPCSpawn.initializeNPC * 25 * 0.6f;
            progress80 = nPCSpawn.initializeNPC * 25 * 0.8f;

            if (!isPanelOn)
            {
                panelHUD.SetActive(true);
            }
            else
            {
                panelHUD.SetActive(false);
            }

        }
        else if (!dayManager.dayIsStarted)
        {
            panelHUD.SetActive(false);
            if (dayManager.day == 0)
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].SetActive(false);
                }

            }
            else if (isStarted)
            {
                StartCoroutine(CountIncome(dailyIncome, durationCounting));


                happyCount.text = happyReaction.ToString();
                flatCount.text = flatReaction.ToString();
                angryCount.text = angryReaction.ToString();


            }
            isStarted = false;
        }
    }

    private IEnumerator ActivateStarsWithDelay(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
            stars[i].GetComponent<Star>().isStarted = true;
            yield return new WaitForSeconds(0.5f);
            stars[i].GetComponent<Star>().isStarted = false;
        }
    }

    private IEnumerator CountIncome(float targetValue, float duration)
    {
        yield return new WaitForSeconds(0.3f);

        animFontIncome.SetBool("IsStart", true);
        float startValue = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            dailyIncomeText.text = FormatMoney(currentValue);
            yield return null;
        }

        dailyIncomeText.text = FormatMoney(targetValue);

        if (progress >= progress80)
        {
            StartCoroutine(ActivateStarsWithDelay(3));
            yield return new WaitForSeconds(1.5f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
        }
        else if (progress >= progress60)
        {
            StartCoroutine(ActivateStarsWithDelay(2));
            yield return new WaitForSeconds(1f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
        }
        else if (progress >= progress40)
        {
            StartCoroutine(ActivateStarsWithDelay(1));
            yield return new WaitForSeconds(0.5f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
        }

    }

    private string FormatMoney(float amount)
    {
        if (amount < 1000)
        {
            return ((int)amount).ToString() + "rb";
        }
        else
        {
            return ((float)amount / 1000f).ToString("0.##") + "jt";
        }
    }

}
