using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WorkspaceManager : MonoBehaviour, IDataPersistence
{
    [Header("User Interface")]
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;
    [SerializeField] GameObject panelNotif;
    [SerializeField] Animator animNotif;
    public GameObject imageWarning;
    public Sprite[] images;
    public TMP_Text notifText;
    public GameObject panelUnlock;
    public TMP_Text textMotifUnlock;
    public int level;
    public TMP_Text workspaceUnlockPrice;

    public GameObject panelUpgrade;
    public TMP_Text textMotifUpgrade;
    public TMP_Text textUpgradeLevel;
    public TMP_Text textUpgradePrice;
    public int workspaceIndex;
    public Animator animUnlock;
    public Animator animUpgrade;

    [Header("Unlock")]
    [SerializeField] Button[] unlocks;
    [SerializeField] GameObject[] textUnlocks;
    [SerializeField] GameObject[] textOwned;
    [SerializeField] GameObject[] slotKosong;
    [SerializeField] GameObject[] buttonUnlocks;
    [SerializeField] GameObject[] vfxUnlock;

    [Header("Upgrade")]
    [SerializeField] Button[] upgrades;
    [SerializeField] GameObject[] textUpgrades;
    [SerializeField] GameObject[] textNoImage;
    [SerializeField] TMP_Text[] textHeadName;
    [SerializeField] TMP_Text[] textDesc;

    [Header("Change Workspace")]
    [SerializeField] GameObject[] workspaceColor1;
    [SerializeField] GameObject[] workspaceColor2;
    [SerializeField] GameObject[] workspaceColor3;
    [SerializeField] GameObject[] workspaceColor4;
    [SerializeField] GameObject[] workspaceColor5;

    [Header("General")]
    public int motifUnlocked;
    [SerializeField] GameObject[] workspaces;
    [SerializeField] int[] workspacePrice;

    [Header("Upgrade Settings")]
    [SerializeField] private int maxUpgradeLevel = 5;
    [SerializeField] private float[] timePerLevel;
    [SerializeField] int[] currentUpgradeLevel;
    [SerializeField] private int[] upgradePrice1;
    [SerializeField] private int[] upgradePrice2;
    [SerializeField] private int[] upgradePrice3;
    [SerializeField] private int[] upgradePrice4;
    [SerializeField] private int[] upgradePrice5;

    [Header("Reference")]
    Money money;
    BookMenu bookMenu;
    Tutorial tutorial;
    DayManager dayManager;

    [Header("Audio")]
    AudioSetter audioSetter;
    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }
    public void LoadData(GameData data)
    {
        motifUnlocked = data.motifUnlocked;
    }

    public void SaveData(ref GameData data)
    {
        data.motifUnlocked = motifUnlocked;
    }

    private void Start()
    {
        panelUnlock.SetActive(false);
        panelUpgrade.SetActive(false);

        money = FindAnyObjectByType<Money>();
        tutorial = FindAnyObjectByType<Tutorial>();
        dayManager = FindAnyObjectByType<DayManager>();

        for (int i = 0; i < currentUpgradeLevel.Length; i++)
        {
            currentUpgradeLevel[i] = workspaces[i].GetComponent<Workspace>().level_workspace;
        }
        EnableWorkspace();

        bookMenu = FindAnyObjectByType<BookMenu>();

        if (motifUnlocked == 0)
        {
            for (int i = 0; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                slotKosong[i].SetActive(true);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 1)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);

            slotKosong[0].SetActive(false);

            for (int i = 1; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                slotKosong[i].SetActive(true);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 2)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);
            unlocks[1].interactable = false;
            textUnlocks[1].SetActive(false);
            textOwned[1].SetActive(true);

            slotKosong[0].SetActive(false);
            slotKosong[1].SetActive(false);

            for (int i = 2; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = true;
                textUnlocks[i].SetActive(true);
                textOwned[i].SetActive(false);

                slotKosong[i].SetActive(true);

                textUnlocks[i].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[i]);
            }
        }
        else if (motifUnlocked == 3)
        {
            unlocks[0].interactable = false;
            textUnlocks[0].SetActive(false);
            textOwned[0].SetActive(true);
            unlocks[1].interactable = false;
            textUnlocks[1].SetActive(false);
            textOwned[1].SetActive(true);
            unlocks[2].interactable = false;
            textUnlocks[2].SetActive(false);
            textOwned[2].SetActive(true);

            slotKosong[0].SetActive(false);
            slotKosong[1].SetActive(false);
            slotKosong[2].SetActive(false);

            unlocks[3].interactable = true;
            textUnlocks[3].SetActive(true);
            textOwned[3].SetActive(false);
            textUnlocks[3].GetComponentInChildren<TMP_Text>().text = PriceCount(workspacePrice[3]);

            slotKosong[3].SetActive(true);
        }
        else if (motifUnlocked == 4)
        {
            for (int i = 0; i < unlocks.Length; i++)
            {
                unlocks[i].interactable = false;
                textUnlocks[i].SetActive(false);
                textOwned[i].SetActive(true);

                slotKosong[i].SetActive(false);
            }
        }

        DisableButtonUpgrade();
        UpdateWorkspaceColors();
    }

    private void Update() 
    {
        if(tutorial.isStartTutor)
        {
            for (int i = 0; i < buttonUnlocks.Length; i++)
            {
                buttonUnlocks[i].SetActive(false);
            }
        }
        else
        {
            if(dayManager.dayIsStarted)
            {
                for (int i = 0; i < buttonUnlocks.Length; i++)
                {
                    buttonUnlocks[i].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < buttonUnlocks.Length; i++)
                {
                    buttonUnlocks[i].SetActive(true);
                }
            }
        }    
    }

    void SetWorkspaceColors(GameObject[] workspaceColors, int level)
    {
        for (int i = 0; i < workspaceColors.Length; i++)
        {
            workspaceColors[i].SetActive(i == level - 1);
        }
    }

    void UpdateWorkspaceColors()
    {
        SetWorkspaceColors(workspaceColor1, workspaces[0].GetComponent<Workspace>().level_workspace);
        SetWorkspaceColors(workspaceColor2, workspaces[1].GetComponent<Workspace>().level_workspace);
        SetWorkspaceColors(workspaceColor3, workspaces[2].GetComponent<Workspace>().level_workspace);
        SetWorkspaceColors(workspaceColor4, workspaces[3].GetComponent<Workspace>().level_workspace);
        SetWorkspaceColors(workspaceColor5, workspaces[4].GetComponent<Workspace>().level_workspace);
    }

    private void DisableButtonUpgrade()
    {
        if (motifUnlocked == 0)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].interactable = false;
                textUpgrades[i].SetActive(false);
                textNoImage[i].SetActive(true);
                textNoImage[i].GetComponentInChildren<TMP_Text>().text = "Terkunci";
            }

            PriceMatchLevelUpgradeButtonW1();
        }
        else if (motifUnlocked == 1)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].interactable = false;
                textUpgrades[i].SetActive(false);
                textNoImage[i].SetActive(true);
                textNoImage[i].GetComponentInChildren<TMP_Text>().text = "Terkunci";
            }

            PriceMatchLevelUpgradeButtonW1();
            PriceMatchLevelUpgradeButtonW2();
        }
        else if (motifUnlocked == 2)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].interactable = false;
                textUpgrades[i].SetActive(false);
                textNoImage[i].SetActive(true);
                textNoImage[i].GetComponentInChildren<TMP_Text>().text = "Terkunci";
            }

            PriceMatchLevelUpgradeButtonW1();
            PriceMatchLevelUpgradeButtonW2();
            PriceMatchLevelUpgradeButtonW3();
        }
        else if (motifUnlocked == 3)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].interactable = false;
                textUpgrades[i].SetActive(false);
                textNoImage[i].SetActive(true);
                textNoImage[i].GetComponentInChildren<TMP_Text>().text = "Terkunci";
            }

            PriceMatchLevelUpgradeButtonW1();
            PriceMatchLevelUpgradeButtonW2();
            PriceMatchLevelUpgradeButtonW3();
            PriceMatchLevelUpgradeButtonW4();
        }
        else if (motifUnlocked == 4)
        {
            for (int i = 0; i < upgrades.Length; i++)
            {
                upgrades[i].interactable = false;
                textUpgrades[i].SetActive(false);
                textNoImage[i].SetActive(true);
                textNoImage[i].GetComponentInChildren<TMP_Text>().text = "Terkunci";
            }

            PriceMatchLevelUpgradeButtonW1();
            PriceMatchLevelUpgradeButtonW2();
            PriceMatchLevelUpgradeButtonW3();
            PriceMatchLevelUpgradeButtonW4();
            PriceMatchLevelUpgradeButtonW5();
        }
    }

    private void PriceMatchLevelUpgradeButtonW1()
    {
        if (workspaces[0].GetComponent<Workspace>().level_workspace == 1 || workspaces[0].GetComponent<Workspace>().level_workspace == 0)
        {
            upgrades[0].interactable = true;
            textUpgrades[0].SetActive(true);
            textUpgrades[0].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice1[0]);
            textNoImage[0].SetActive(false);

            textHeadName[0].text = "Batik Kawung: Level 1";
            textDesc[0].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 10 detik";
        }
        else if (workspaces[0].GetComponent<Workspace>().level_workspace == 2)
        {
            upgrades[0].interactable = true;
            textUpgrades[0].SetActive(true);
            textUpgrades[0].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice1[1]);
            textNoImage[0].SetActive(false);

            textHeadName[0].text = "Batik Kawung: Level 2";
            textDesc[0].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 8 detik";
        }
        else if (workspaces[0].GetComponent<Workspace>().level_workspace == 3)
        {
            upgrades[0].interactable = true;
            textUpgrades[0].SetActive(true);
            textUpgrades[0].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice1[2]);
            textNoImage[0].SetActive(false);

            textHeadName[0].text = "Batik Kawung: Level 3";
            textDesc[0].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 6 detik";
        }
        else if (workspaces[0].GetComponent<Workspace>().level_workspace == 4)
        {
            upgrades[0].interactable = true;
            textUpgrades[0].SetActive(true);
            textUpgrades[0].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice1[3]);
            textNoImage[0].SetActive(false);

            textHeadName[0].text = "Batik Kawung: Level 4";
            textDesc[0].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 4 detik";
        }
        else if (workspaces[0].GetComponent<Workspace>().level_workspace == 5)
        {
            upgrades[0].interactable = true;
            textUpgrades[0].SetActive(false);
            textNoImage[0].GetComponentInChildren<TMP_Text>().text = "Max";
            textNoImage[0].SetActive(true);

            textHeadName[0].text = "Batik Kawung: Level 5";
            textDesc[0].text = "Kecepatan proses otomatis 4 detik (Max)";
        }
    }

    private void PriceMatchLevelUpgradeButtonW2()
    {
        if (workspaces[1].GetComponent<Workspace>().level_workspace == 1 || workspaces[1].GetComponent<Workspace>().level_workspace == 0)
        {
            upgrades[1].interactable = true;
            textUpgrades[1].SetActive(true);
            textUpgrades[1].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[0]);
            textNoImage[1].SetActive(false);

            textHeadName[1].text = "Batik Megamendung: Level 1";
            textDesc[1].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 10 detik";
        }
        else if (workspaces[1].GetComponent<Workspace>().level_workspace == 2)
        {
            upgrades[1].interactable = true;
            textUpgrades[1].SetActive(true);
            textUpgrades[1].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[1]);
            textNoImage[1].SetActive(false);

            textHeadName[1].text = "Batik Megamendung: Level 2";
            textDesc[1].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 8 detik";
        }
        else if (workspaces[1].GetComponent<Workspace>().level_workspace == 3)
        {
            upgrades[1].interactable = true;
            textUpgrades[1].SetActive(true);
            textUpgrades[1].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[2]);
            textNoImage[1].SetActive(false);

            textHeadName[1].text = "Batik Megamendung: Level 3";
            textDesc[1].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 6 detik";
        }
        else if (workspaces[1].GetComponent<Workspace>().level_workspace == 4)
        {
            upgrades[1].interactable = true;
            textUpgrades[1].SetActive(true);
            textUpgrades[1].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[3]);
            textNoImage[1].SetActive(false);

            textHeadName[1].text = "Batik Megamendung: Level 4";
            textDesc[1].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 4 detik";
        }
        else if (workspaces[1].GetComponent<Workspace>().level_workspace == 5)
        {
            upgrades[1].interactable = true;
            textUpgrades[1].SetActive(false);
            textNoImage[1].GetComponentInChildren<TMP_Text>().text = "Max";
            textNoImage[1].SetActive(true);

            textHeadName[1].text = "Batik Megamendung: Level 5";
            textDesc[1].text = "Kecepatan proses otomatis 4 detik (Max)";
        }
    }

    private void PriceMatchLevelUpgradeButtonW3()
    {
        if (workspaces[2].GetComponent<Workspace>().level_workspace == 1 || workspaces[2].GetComponent<Workspace>().level_workspace == 0)
        {
            upgrades[2].interactable = true;
            textUpgrades[2].SetActive(true);
            textUpgrades[2].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[0]);
            textNoImage[2].SetActive(false);

            textHeadName[2].text = "Batik Truntum: Level 1";
            textDesc[2].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 10 detik";
        }
        else if (workspaces[2].GetComponent<Workspace>().level_workspace == 2)
        {
            upgrades[2].interactable = true;
            textUpgrades[2].SetActive(true);
            textUpgrades[2].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[1]);
            textNoImage[2].SetActive(false);

            textHeadName[2].text = "Batik Truntum: Level 2";
            textDesc[2].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 8 detik";
        }
        else if (workspaces[2].GetComponent<Workspace>().level_workspace == 3)
        {
            upgrades[2].interactable = true;
            textUpgrades[2].SetActive(true);
            textUpgrades[2].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[2]);
            textNoImage[2].SetActive(false);

            textHeadName[2].text = "Batik Truntum: Level 3";
            textDesc[2].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 6 detik";
        }
        else if (workspaces[2].GetComponent<Workspace>().level_workspace == 4)
        {
            upgrades[2].interactable = true;
            textUpgrades[2].SetActive(true);
            textUpgrades[2].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[3]);
            textNoImage[2].SetActive(false);

            textHeadName[2].text = "Batik Truntum: Level 4";
            textDesc[2].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 4 detik";
        }
        else if (workspaces[2].GetComponent<Workspace>().level_workspace == 5)
        {
            upgrades[2].interactable = true;
            textUpgrades[2].SetActive(false);
            textNoImage[2].GetComponentInChildren<TMP_Text>().text = "Max";
            textNoImage[2].SetActive(true);

            textHeadName[2].text = "Batik Truntum: Level 5";
            textDesc[2].text = "Kecepatan proses otomatis 4 detik (Max)";
        }
    }

    private void PriceMatchLevelUpgradeButtonW4()
    {
        if (workspaces[3].GetComponent<Workspace>().level_workspace == 1 || workspaces[3].GetComponent<Workspace>().level_workspace == 0)
        {
            upgrades[3].interactable = true;
            textUpgrades[3].SetActive(true);
            textUpgrades[3].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[0]);
            textNoImage[3].SetActive(false);

            textHeadName[3].text = "Batik Parang: Level 1";
            textDesc[3].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 10 detik";
        }
        else if (workspaces[3].GetComponent<Workspace>().level_workspace == 2)
        {
            upgrades[3].interactable = true;
            textUpgrades[3].SetActive(true);
            textUpgrades[3].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[1]);
            textNoImage[3].SetActive(false);

            textHeadName[3].text = "Batik Parang: Level 2";
            textDesc[3].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 8 detik";
        }
        else if (workspaces[3].GetComponent<Workspace>().level_workspace == 3)
        {
            upgrades[3].interactable = true;
            textUpgrades[3].SetActive(true);
            textUpgrades[3].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[2]);
            textNoImage[3].SetActive(false);

            textHeadName[3].text = "Batik Parang: Level 3";
            textDesc[3].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 6 detik";
        }
        else if (workspaces[3].GetComponent<Workspace>().level_workspace == 4)
        {
            upgrades[3].interactable = true;
            textUpgrades[3].SetActive(true);
            textUpgrades[3].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[3]);
            textNoImage[3].SetActive(false);

            textHeadName[3].text = "Batik Parang: Level 4";
            textDesc[3].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 4 detik";
        }
        else if (workspaces[3].GetComponent<Workspace>().level_workspace == 5)
        {
            upgrades[3].interactable = true;
            textUpgrades[3].SetActive(false);
            textNoImage[3].GetComponentInChildren<TMP_Text>().text = "Max";
            textNoImage[3].SetActive(true);

            textHeadName[3].text = "Batik Parang: Level 5";
            textDesc[3].text = "Kecepatan proses otomatis 4 detik (Max)";
        }
    }

    private void PriceMatchLevelUpgradeButtonW5()
    {
        if (workspaces[4].GetComponent<Workspace>().level_workspace == 1 || workspaces[4].GetComponent<Workspace>().level_workspace == 0)
        {
            upgrades[4].interactable = true;
            textUpgrades[4].SetActive(true);
            textUpgrades[4].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[0]);
            textNoImage[4].SetActive(false);

            textHeadName[4].text = "Batik Simbut: Level 1";
            textDesc[4].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 10 detik";
        }
        else if (workspaces[4].GetComponent<Workspace>().level_workspace == 2)
        {
            upgrades[4].interactable = true;
            textUpgrades[4].SetActive(true);
            textUpgrades[4].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[1]);
            textNoImage[4].SetActive(false);

            textHeadName[4].text = "Batik Simbut: Level 2";
            textDesc[4].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 8 detik";
        }
        else if (workspaces[4].GetComponent<Workspace>().level_workspace == 3)
        {
            upgrades[4].interactable = true;
            textUpgrades[4].SetActive(true);
            textUpgrades[4].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[2]);
            textNoImage[4].SetActive(false);

            textHeadName[4].text = "Batik Simbut: Level 3";
            textDesc[4].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 6 detik";
        }
        else if (workspaces[4].GetComponent<Workspace>().level_workspace == 4)
        {
            upgrades[4].interactable = true;
            textUpgrades[4].SetActive(true);
            textUpgrades[4].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[3]);
            textNoImage[4].SetActive(false);

            textHeadName[4].text = "Batik Simbut: Level 4";
            textDesc[4].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi 4 detik";
        }
        else if (workspaces[4].GetComponent<Workspace>().level_workspace == 5)
        {
            upgrades[4].interactable = true;
            textUpgrades[4].SetActive(false);
            textNoImage[4].GetComponentInChildren<TMP_Text>().text = "Max";
            textNoImage[4].SetActive(true);

            textHeadName[4].text = "Batik Simbut: Level 5";
            textDesc[4].text = "Kecepatan proses otomatis 4 detik (Max)";
        }
    }
    private void EnableWorkspace()
    {
        for (int i = 0; i < workspaces.Length; i++)
        {
            workspaces[i].SetActive(false);
        }
        for (int i = 0; i < motifUnlocked + 1; i++)
        {
            workspaces[i].SetActive(true);
            workspaces[i].GetComponent<Workspace>().SetupAssignArea(canvasWorldSpace, cam);
            workspaces[i].GetComponent<Workspace>().SetupUpgradeButton(canvasWorldSpace, cam);
        }
    }

    public void UnlockWorkspace()
    {
        if (level == 2)
        {
            if (motifUnlocked == 0)
            {
                if (money.CanAfford(workspacePrice[0]))
                {
                    audioSetter.PlaySFX(audioSetter.UnlockWS);
                    unlocks[0].interactable = false;
                    textUnlocks[0].SetActive(false);
                    textOwned[0].SetActive(true);

                    notifText.text = "<color=#888800>Unlock workspace motif megamendung berhasil</color>";
                    imageWarning.GetComponent<Image>().sprite = images[1];

                    ShowNotif();

                    money.ReduceMoney(workspacePrice[0]);
                    motifUnlocked = 1;
                    slotKosong[0].SetActive(false);
                    EnableWorkspace();
                    CloseUpgradeUnlock();
                    StartCoroutine(ShowVfx(1));

                    upgrades[1].interactable = true;
                    textUpgrades[1].SetActive(true);
                    textUpgrades[1].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[0]);
                    textNoImage[1].SetActive(false);

                }
                else
                {
                    notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan unlock workspace motif megamendung</color>";
                    imageWarning.GetComponent<Image>().sprite = images[0];

                    ShowNotif();
                    audioSetter.PlaySFX(audioSetter.notif);
                }
            }
            else
            {
                notifText.text = "<color=#9E3535>Unlock workspace motif sebelumnya terlebih dahulu</color>";
                imageWarning.GetComponent<Image>().sprite = images[0];
                ShowNotif();
                audioSetter.PlaySFX(audioSetter.notif);
            }
        }
        else if (level == 3)
        {
            if (motifUnlocked == 1)
            {
                if (money.CanAfford(workspacePrice[1]))
                {
                    audioSetter.PlaySFX(audioSetter.UnlockWS);
                    unlocks[1].interactable = false;
                    textUnlocks[1].SetActive(false);
                    textOwned[1].SetActive(true);

                    notifText.text = "<color=#888800>Unlock workspace motif truntum berhasil</color>";
                    imageWarning.GetComponent<Image>().sprite = images[1];

                    ShowNotif();

                    money.ReduceMoney(workspacePrice[1]);
                    motifUnlocked = 2;
                    slotKosong[1].SetActive(false);
                    EnableWorkspace();
                    CloseUpgradeUnlock();
                    StartCoroutine(ShowVfx(2));

                    upgrades[2].interactable = true;
                    textUpgrades[2].SetActive(true);
                    textUpgrades[2].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[0]);
                    textNoImage[2].SetActive(false);
                }
                else
                {
                    notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan unlock workspace motif truntum</color>";
                    imageWarning.GetComponent<Image>().sprite = images[0];
                    ShowNotif();
                    audioSetter.PlaySFX(audioSetter.notif);
                }
            }
            else
            {
                notifText.text = "<color=#9E3535>Unlock workspace motif sebelumnya terlebih dahulu</color>";
                imageWarning.GetComponent<Image>().sprite = images[0];
                ShowNotif();
                audioSetter.PlaySFX(audioSetter.notif);
            }
        }
        else if (level == 4)
        {
            if (motifUnlocked == 2)
            {
                if (money.CanAfford(workspacePrice[2]))
                {
                    audioSetter.PlaySFX(audioSetter.UnlockWS);
                    unlocks[2].interactable = false;
                    textUnlocks[2].SetActive(false);
                    textOwned[2].SetActive(true);

                    notifText.text = "<color=#888800>Unlock workspace motif parang berhasil</color>";
                    imageWarning.GetComponent<Image>().sprite = images[1];

                    ShowNotif();

                    money.ReduceMoney(workspacePrice[2]);
                    motifUnlocked = 3;
                    slotKosong[2].SetActive(false);
                    EnableWorkspace();
                    CloseUpgradeUnlock();
                    StartCoroutine(ShowVfx(3));

                    upgrades[3].interactable = true;
                    textUpgrades[3].SetActive(true);
                    textUpgrades[3].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[0]);
                    textNoImage[3].SetActive(false);
                }
                else
                {
                    notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan unlock workspace motif parang</color>";
                    imageWarning.GetComponent<Image>().sprite = images[0];
                    ShowNotif();
                    audioSetter.PlaySFX(audioSetter.notif);
                }
            }
            else
            {
                notifText.text = "<color=#9E3535>Unlock workspace motif sebelumnya terlebih dahulu</color>";
                imageWarning.GetComponent<Image>().sprite = images[0];
                ShowNotif();
                audioSetter.PlaySFX(audioSetter.notif);
            }
        }
        if (level == 5)
        {
            if (motifUnlocked == 3)
            {
                if (money.CanAfford(workspacePrice[3]))
                {
                    audioSetter.PlaySFX(audioSetter.UnlockWS);
                    unlocks[3].interactable = false;
                    textUnlocks[3].SetActive(false);
                    textOwned[3].SetActive(true);

                    notifText.text = "<color=#888800>Unlock workspace motif simbut berhasil</color>";
                    imageWarning.GetComponent<Image>().sprite = images[1];

                    ShowNotif();

                    money.ReduceMoney(workspacePrice[3]);
                    motifUnlocked = 4;
                    slotKosong[3].SetActive(false);
                    EnableWorkspace();
                    CloseUpgradeUnlock();
                    StartCoroutine(ShowVfx(4));

                    upgrades[4].interactable = true;
                    textUpgrades[4].SetActive(true);
                    textUpgrades[4].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[0]);
                    textNoImage[4].SetActive(false);
                }
                else
                {
                    notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan unlock workspace motif simbut</color>";
                    imageWarning.GetComponent<Image>().sprite = images[0];
                    ShowNotif();
                    audioSetter.PlaySFX(audioSetter.notif);
                }
            }
            else
            {
                notifText.text = "<color=#9E3535>Unlock workspace motif sebelumnya terlebih dahulu</color>";
                imageWarning.GetComponent<Image>().sprite = images[0];
                ShowNotif();
                audioSetter.PlaySFX(audioSetter.notif);
            }
        }
    }

    public void UpgradeWorkspace()
    {
        if (workspaceIndex >= 0 && workspaceIndex < workspaces.Length)
        {
            Workspace workspace = workspaces[workspaceIndex].GetComponent<Workspace>();
            int upgradeLevel = (currentUpgradeLevel[workspaceIndex]) + 1;

            if (upgradeLevel <= maxUpgradeLevel)
            {
                if (workspaceIndex == 0)
                {
                    if (money.CanAfford(upgradePrice1[upgradeLevel - 2]))
                    {
                        audioSetter.PlaySFX(audioSetter.UpgradeWS);
                        money.ReduceMoney(upgradePrice1[upgradeLevel - 2]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");

                        notifText.text = "<color=#888800>Upgrade workspace motif kawung berhasil</color>";
                        imageWarning.GetComponent<Image>().sprite = images[1];

                        ShowNotif();

                        SetWorkspaceColors(workspaceColor1, workspaces[0].GetComponent<Workspace>().level_workspace);

                        if (upgradeLevel == 5)
                        {
                            upgrades[workspaceIndex].interactable = false;
                            textUpgrades[workspaceIndex].SetActive(false);
                            textNoImage[workspaceIndex].SetActive(true);
                            textNoImage[workspaceIndex].GetComponent<TMP_Text>().text = "Max";

                            textHeadName[workspaceIndex].text = "Batik Kawung: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Kecepatan proses otomatis 4 detik (Max)";
                        }
                        else
                        {
                            upgrades[workspaceIndex].interactable = true;
                            textUpgrades[workspaceIndex].SetActive(true);
                            textUpgrades[workspaceIndex].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice1[upgradeLevel - 1]);
                            textNoImage[workspaceIndex].SetActive(false);

                            textHeadName[workspaceIndex].text = "Batik Kawung: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi " + newProgresTime + " detik";
                        }

                        CloseUpgradeUnlock();
                        StartCoroutine(ShowVfx(0));
                    }
                    else
                    {
                        notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan upgrade workspace motif kawung</color>";
                        imageWarning.GetComponent<Image>().sprite = images[0];
                        ShowNotif();
                        audioSetter.PlaySFX(audioSetter.notif);
                        return;
                    }
                }
                else if (workspaceIndex == 1)
                {
                    if (money.CanAfford(upgradePrice2[upgradeLevel - 2]))
                    {
                        audioSetter.PlaySFX(audioSetter.UpgradeWS);
                        money.ReduceMoney(upgradePrice2[upgradeLevel - 2]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");

                        notifText.text = "<color=#888800>Upgrade workspace motif megamendung berhasil</color>";
                        imageWarning.GetComponent<Image>().sprite = images[1];

                        ShowNotif();

                        SetWorkspaceColors(workspaceColor2, workspaces[1].GetComponent<Workspace>().level_workspace);

                        if (upgradeLevel == 5)
                        {
                            upgrades[workspaceIndex].interactable = false;
                            textUpgrades[workspaceIndex].SetActive(false);
                            textNoImage[workspaceIndex].SetActive(true);
                            textNoImage[workspaceIndex].GetComponent<TMP_Text>().text = "Max";

                            textHeadName[workspaceIndex].text = "Batik Megamendung: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Kecepatan proses otomatis 4 detik (Max)";
                        }
                        else
                        {
                            upgrades[workspaceIndex].interactable = true;
                            textUpgrades[workspaceIndex].SetActive(true);
                            textUpgrades[workspaceIndex].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice2[upgradeLevel - 1]);
                            textNoImage[workspaceIndex].SetActive(false);

                            textHeadName[workspaceIndex].text = "Batik Megamendung: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi " + newProgresTime + " detik";
                        }

                        CloseUpgradeUnlock();
                        StartCoroutine(ShowVfx(1));
                    }
                    else
                    {
                        notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan upgrade workspace motif megamendung</color>";
                        imageWarning.GetComponent<Image>().sprite = images[0];
                        ShowNotif();
                        audioSetter.PlaySFX(audioSetter.notif);
                        return;
                    }
                }
                else if (workspaceIndex == 2)
                {
                    if (money.CanAfford(upgradePrice3[upgradeLevel - 2]))
                    {
                        audioSetter.PlaySFX(audioSetter.UpgradeWS);
                        money.ReduceMoney(upgradePrice3[upgradeLevel - 2]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");

                        notifText.text = "<color=#888800>Upgrade workspace motif truntum berhasil</color>";
                        imageWarning.GetComponent<Image>().sprite = images[1];

                        ShowNotif();

                        SetWorkspaceColors(workspaceColor3, workspaces[2].GetComponent<Workspace>().level_workspace);

                        if (upgradeLevel == 5)
                        {
                            upgrades[workspaceIndex].interactable = false;
                            textUpgrades[workspaceIndex].SetActive(false);
                            textNoImage[workspaceIndex].SetActive(true);
                            textNoImage[workspaceIndex].GetComponent<TMP_Text>().text = "Max";

                            textHeadName[workspaceIndex].text = "Batik Truntum: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Kecepatan proses otomatis 4 detik (Max)";
                        }
                        else
                        {
                            upgrades[workspaceIndex].interactable = true;
                            textUpgrades[workspaceIndex].SetActive(true);
                            textUpgrades[workspaceIndex].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice3[upgradeLevel - 1]);
                            textNoImage[workspaceIndex].SetActive(false);

                            textHeadName[workspaceIndex].text = "Batik Truntum: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi " + newProgresTime + " detik";
                        }

                        CloseUpgradeUnlock();
                        StartCoroutine(ShowVfx(2));
                    }
                    else
                    {
                        notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan upgrade workspace motif truntum</color>";
                        imageWarning.GetComponent<Image>().sprite = images[0];
                        ShowNotif();
                        audioSetter.PlaySFX(audioSetter.notif);
                        return;
                    }
                }
                else if (workspaceIndex == 3)
                {
                    if (money.CanAfford(upgradePrice4[upgradeLevel - 2]))
                    {
                        audioSetter.PlaySFX(audioSetter.UpgradeWS);
                        money.ReduceMoney(upgradePrice4[upgradeLevel - 2]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");

                        notifText.text = "<color=#888800>Upgrade workspace motif parang berhasil</color>";
                        imageWarning.GetComponent<Image>().sprite = images[1];

                        ShowNotif();

                        SetWorkspaceColors(workspaceColor4, workspaces[3].GetComponent<Workspace>().level_workspace);

                        if (upgradeLevel == 5)
                        {
                            upgrades[workspaceIndex].interactable = false;
                            textUpgrades[workspaceIndex].SetActive(false);
                            textNoImage[workspaceIndex].SetActive(true);
                            textNoImage[workspaceIndex].GetComponent<TMP_Text>().text = "Max";

                            textHeadName[workspaceIndex].text = "Batik Parang: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Kecepatan proses otomatis 4 detik (Max)";
                        }
                        else
                        {
                            upgrades[workspaceIndex].interactable = true;
                            textUpgrades[workspaceIndex].SetActive(true);
                            textUpgrades[workspaceIndex].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice4[upgradeLevel - 1]);
                            textNoImage[workspaceIndex].SetActive(false);

                            textHeadName[workspaceIndex].text = "Batik Parang: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi " + newProgresTime + " detik";
                        }

                        CloseUpgradeUnlock();
                        StartCoroutine(ShowVfx(3));
                    }
                    else
                    {
                        notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan upgrade workspace motif parang</color>";
                        imageWarning.GetComponent<Image>().sprite = images[0];
                        ShowNotif();
                        audioSetter.PlaySFX(audioSetter.notif);
                        return;
                    }
                }
                else if (workspaceIndex == 4)
                {
                    if (money.CanAfford(upgradePrice5[upgradeLevel - 2]))
                    {
                        audioSetter.PlaySFX(audioSetter.UpgradeWS);
                        money.ReduceMoney(upgradePrice5[upgradeLevel - 2]);
                        int newProgresTime = Mathf.RoundToInt(timePerLevel[upgradeLevel - 1]);
                        workspace.UpdateProgresTime(newProgresTime);
                        currentUpgradeLevel[workspaceIndex] = upgradeLevel;
                        workspace.level_workspace = upgradeLevel;
                        Debug.Log($"Workspace {workspaceIndex + 1} upgraded to level {upgradeLevel} with new progress time: {newProgresTime} seconds.");

                        notifText.text = "<color=#888800>Upgrade workspace motif simbut berhasil</color>";
                        imageWarning.GetComponent<Image>().sprite = images[1];

                        ShowNotif();

                        SetWorkspaceColors(workspaceColor5, workspaces[4].GetComponent<Workspace>().level_workspace);

                        if (upgradeLevel == 5)
                        {
                            upgrades[workspaceIndex].interactable = false;
                            textUpgrades[workspaceIndex].SetActive(false);
                            textNoImage[workspaceIndex].SetActive(true);
                            textNoImage[workspaceIndex].GetComponent<TMP_Text>().text = "Max";

                            textHeadName[workspaceIndex].text = "Batik Simbut: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Kecepatan proses otomatis 4 detik (Max)";
                        }
                        else
                        {
                            upgrades[workspaceIndex].interactable = true;
                            textUpgrades[workspaceIndex].SetActive(true);
                            textUpgrades[workspaceIndex].GetComponentInChildren<TMP_Text>().text = PriceCount(upgradePrice5[upgradeLevel - 1]);
                            textNoImage[workspaceIndex].SetActive(false);

                            textHeadName[workspaceIndex].text = "Batik Simbut: Level " + upgradeLevel;
                            textDesc[workspaceIndex].text = "Meningkatkan kecepatan proses otomatis -2 detik menjadi " + newProgresTime + " detik";
                        }

                        CloseUpgradeUnlock();
                        StartCoroutine(ShowVfx(4));
                    }
                    else
                    {
                        notifText.text = "<color=#9E3535>Uang tidak cukup untuk melakukan upgrade workspace motif simbut</color>";
                        imageWarning.GetComponent<Image>().sprite = images[0];
                        ShowNotif();
                        audioSetter.PlaySFX(audioSetter.notif);
                        return;
                    }
                }
            }
            else
            {
                Debug.LogWarning("Max upgrade level reached.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid workspace index.");
        }
    }

    private string PriceCount(int price)
    {
        if (price < 1000)
        {
            return ((int)price).ToString() + "rb";
        }
        else
        {
            return ((float)price / 1000f).ToString("0.##") + "Jt";
        }
    }

    public void ShowNotif()
    {
        StartCoroutine(ShowNotifDelay());
    }

    IEnumerator ShowNotifDelay()
    {
        panelNotif.SetActive(true);
        yield return new WaitForSeconds(1f);
        animNotif.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.5f);
        panelNotif.SetActive(false);
    }

    IEnumerator ShowVfx(int index)
    {
        vfxUnlock[index].SetActive(true);
        yield return new WaitForSeconds(1f);
        vfxUnlock[index].SetActive(false);
    }

    public void OpenUnlock(int index)
    {
        panelUnlock.SetActive(true);

        if(index == 0)
        {
            level = 2;
            textMotifUnlock.text = "Unlock Workspace Motif Batik Megamendung?";
            workspaceUnlockPrice.text = PriceCount(workspacePrice[0]);
        }
        else if(index == 1)
        {
            level = 3;
            textMotifUnlock.text = "Unlock Workspace Motif Batik Truntum?";
            workspaceUnlockPrice.text = PriceCount(workspacePrice[1]);
        }
        else if(index == 2)
        {
            level = 4;
            textMotifUnlock.text = "Unlock Workspace Motif Batik Parang?";
            workspaceUnlockPrice.text = PriceCount(workspacePrice[2]);
        }
        else if(index == 3)
        {
            level = 5;
            textMotifUnlock.text = "Unlock Workspace Motif Batik Simbut?";
            workspaceUnlockPrice.text = PriceCount(workspacePrice[3]);
        }
    }

    public void CloseUnlock()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        StartCoroutine(CloseUnlockDelay());
    }

    public void OpenUpgrade(int index)
    {
        panelUpgrade.SetActive(true);

        if(index == 0)
        {
            workspaceIndex = 0;
            Workspace ws1 = workspaces[0].GetComponent<Workspace>();
            textMotifUpgrade.text = "Upgrade Workspace Motif Batik Kawung ke";
            textUpgradeLevel.text =  "Level " + (ws1.level_workspace + 1) + "?";
            textUpgradePrice.text = PriceCount(upgradePrice1[ws1.level_workspace - 1]);
        }
        else if(index == 1)
        {
            workspaceIndex = 1;
            Workspace ws2 = workspaces[1].GetComponent<Workspace>();
            textMotifUpgrade.text = "Upgrade Workspace Motif Batik Megamendung ke";
            textUpgradeLevel.text =  "Level " + (ws2.level_workspace + 1) + "?";
            textUpgradePrice.text = PriceCount(upgradePrice2[ws2.level_workspace - 1]);
        }
        else if(index == 2)
        {
            workspaceIndex = 2;
            Workspace ws3 = workspaces[2].GetComponent<Workspace>();
            textMotifUpgrade.text = "Upgrade Workspace Motif Batik Truntum ke";
            textUpgradeLevel.text =  "Level " + (ws3.level_workspace + 1) + "?";
            textUpgradePrice.text = PriceCount(upgradePrice3[ws3.level_workspace - 1]);
        }
        else if(index == 3)
        {
            workspaceIndex = 3;
            Workspace ws4 = workspaces[3].GetComponent<Workspace>();
            textMotifUpgrade.text = "Upgrade Workspace Motif Batik Parang ke";
            textUpgradeLevel.text =  "Level " + (ws4.level_workspace + 1) + "?";
            textUpgradePrice.text = PriceCount(upgradePrice4[ws4.level_workspace - 1]);
        }
        else if(index == 4)
        {
            workspaceIndex = 4;
            Workspace ws5 = workspaces[4].GetComponent<Workspace>();
            textMotifUpgrade.text = "Upgrade Workspace Motif Batik Simbut ke";
            textUpgradeLevel.text =  "Level " + (ws5.level_workspace + 1) + "?";
            textUpgradePrice.text = PriceCount(upgradePrice5[ws5.level_workspace - 1]);
        }
    }

    public void CloseUpgrade()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        StartCoroutine(CloseUpgradeDelay());
    }

    public void CloseUpgradeUnlock()
    {
        StartCoroutine(CloseUnlockUpgradeDelay());
    }

     IEnumerator CloseUnlockUpgradeDelay()
    {
        if(panelUnlock.activeSelf)
        {
            animUnlock.SetTrigger("IsEnd");
            yield return new WaitForSeconds(0.25f);
            panelUnlock.SetActive(false);
        }
        else if(panelUpgrade.activeSelf)
        {
            animUpgrade.SetTrigger("IsEnd");
            yield return new WaitForSeconds(0.25f);
            panelUpgrade.SetActive(false);
        }
    }

    IEnumerator CloseUnlockDelay()
    {
        animUnlock.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        panelUnlock.SetActive(false);
    }

    IEnumerator CloseUpgradeDelay()
    {
        animUpgrade.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        panelUpgrade.SetActive(false);
    }

}