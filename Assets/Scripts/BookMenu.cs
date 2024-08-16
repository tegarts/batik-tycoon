using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    [SerializeField] GameObject bookPanel;
    [SerializeField] GameObject reportPanel;
    [SerializeField] GameObject unlockPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject nextDayPanel;
    [SerializeField] GameObject buttonReportPanel;
    [SerializeField] GameObject buttonUnlockPanel;
    [SerializeField] GameObject buttonUpgradePanel;
    [SerializeField] GameObject buttonNextDayPanel;

    private void Start() 
    {
        bookPanel.SetActive(false);
        buttonReportPanel.SetActive(false);
        buttonUnlockPanel.SetActive(false);
        buttonUpgradePanel.SetActive(false);
        buttonNextDayPanel.SetActive(false);
    }
    public void OpenBook()
    {
        bookPanel.SetActive(true);
        reportPanel.SetActive(true);
        buttonReportPanel.SetActive(true);

        unlockPanel.SetActive(false);
        upgradePanel.SetActive(false);
        nextDayPanel.SetActive(false);
    }

    public void CloseBook()
    {
        bookPanel.SetActive(false);
        reportPanel.SetActive(false);
    }

    public void OpenReport()
    {
        reportPanel.SetActive(true);
        buttonReportPanel.SetActive(true);

        unlockPanel.SetActive(false);
        upgradePanel.SetActive(false);
        nextDayPanel.SetActive(false);

        buttonUnlockPanel.SetActive(false);
        buttonUpgradePanel.SetActive(false);
        buttonNextDayPanel.SetActive(false);
    }

    public void OpenUnlock()
    {
        unlockPanel.SetActive(true);
        buttonUnlockPanel.SetActive(true);

        reportPanel.SetActive(false);
        upgradePanel.SetActive(false);
        nextDayPanel.SetActive(false);

        buttonReportPanel.SetActive(false);
        buttonUpgradePanel.SetActive(false);
        buttonNextDayPanel.SetActive(false);
    }

    public void OpenUpgrade()
    {
        upgradePanel.SetActive(true);
        buttonUpgradePanel.SetActive(true);

        unlockPanel.SetActive(false);
        reportPanel.SetActive(false);
        nextDayPanel.SetActive(false);

        buttonReportPanel.SetActive(false);
        buttonUnlockPanel.SetActive(false);
        buttonNextDayPanel.SetActive(false);
    }

    public void OpenNextDay()
    {
        nextDayPanel.SetActive(true);
        buttonNextDayPanel.SetActive(true);

        unlockPanel.SetActive(false);
        reportPanel.SetActive(false);
        upgradePanel.SetActive(false);

        buttonReportPanel.SetActive(false);
        buttonUnlockPanel.SetActive(false);
        buttonUpgradePanel.SetActive(false);
    }
    
}
