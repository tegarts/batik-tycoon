using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Daily : MonoBehaviour, IDataPersistence
{
    public static Daily instance { get; private set; }
    bool isStarted;
    // public bool isGameOver;
    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public float progress = 0;
    [Header("Rating")]
    public GameObject[] stars;
    float maxProgress;
    public float progress40;
    float progress60;
    float progress80;
    public int totalStars;

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
    [SerializeField] Animator animHUD;
    [SerializeField] Animator animBook;
    public GameObject panelGameOver;
    public TMP_Text totalStarsText;
    public TMP_Text dayText;
    [Header("Audio")]
    AudioSetter audioSetter;

    public void LoadData(GameData data)
    {
        totalStars = data.totalStars;
    }

    public void SaveData(ref GameData data)
    {
        data.totalStars = totalStars;
    }

    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }


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
                closePanelHUD();
            }

        }
        else if (!dayManager.dayIsStarted)
        {
            // panelHUD.SetActive(false);
            if (dayManager.day == 0)
            {
                for (int i = 0; i < stars.Length; i++)
                {
                    stars[i].SetActive(false);
                }

            }
            else if (isStarted)
            {
                if (progress > progress40)
                {
                    StartCoroutine(CountIncome(dailyIncome, durationCounting));


                    happyCount.text = happyReaction.ToString();
                    flatCount.text = flatReaction.ToString();
                    angryCount.text = angryReaction.ToString();
                    closePanelHUD();
                }
                // else
                // {
                //     isGameOver = true;
                //     panelGameOver.SetActive(true);
                // }


            }
            isStarted = false;
        }

        if(panelGameOver.activeSelf)
        {
            dayText.text = "Hari " + dayManager.day;
            totalStarsText.text = totalStars.ToString();
        }
    }

    private IEnumerator ActivateStarsWithDelay(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            audioSetter.PlaySFX(audioSetter.star);
            stars[i].SetActive(true);
            stars[i].GetComponent<Star>().isStarted = true;
            yield return new WaitForSeconds(0.5f);
            stars[i].GetComponent<Star>().isStarted = false;
        }
    }

    private IEnumerator CountIncome(float targetValue, float duration)
    {
        dayManager.directionalLight.intensity = 0.2f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(0.3f);

        animFontIncome.SetBool("IsStart", true);
        float startValue = 0f;
        float elapsedTime = 0f;
        audioSetter.PlaySFX(audioSetter.numberCounter);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentValue = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            dailyIncomeText.text = FormatMoney(currentValue);
            yield return null;
        }

        dailyIncomeText.text = FormatMoney(targetValue);
        audioSetter.StopSFX();
        if (progress >= progress80)
        {
            totalStars += 3;
            StartCoroutine(ActivateStarsWithDelay(3));
            yield return new WaitForSeconds(1.5f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
            StartCoroutine(StartSFXReaction());
        }
        else if (progress >= progress60)
        {
            totalStars += 2;
            StartCoroutine(ActivateStarsWithDelay(2));
            yield return new WaitForSeconds(1f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
            StartCoroutine(StartSFXReaction());
        }
        else if (progress >= progress40)
        {
            totalStars += 1;
            StartCoroutine(ActivateStarsWithDelay(1));
            yield return new WaitForSeconds(0.5f);
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
            StartCoroutine(StartSFXReaction());
        }
        else
        {
            animReaction.gameObject.SetActive(true);
            animReaction.SetBool("IsStart", true);
            StartCoroutine(StartSFXReaction());
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

    private void closePanelHUD()
    {
        StartCoroutine(ClosePanelHUDDelay());
    }

    IEnumerator ClosePanelHUDDelay()
    {
        Debug.Log("TEST PANEL HUD");
        animHUD.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.5f);
        panelHUD.SetActive(false);
    }

    IEnumerator StartSFXReaction()
    {
        yield return new WaitForSeconds(0.21f);
        audioSetter.PlaySFX(audioSetter.reactions);
        yield return new WaitForSeconds(0.66f);
        audioSetter.PlaySFX(audioSetter.reactions);
        yield return new WaitForSeconds(0.66f);
        audioSetter.PlaySFX(audioSetter.reactions);
        yield return new WaitForSeconds(0.5f);
        audioSetter.PlaySFX(audioSetter.gameResult);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
