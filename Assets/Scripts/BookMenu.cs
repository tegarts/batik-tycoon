using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMenu : MonoBehaviour
{
    public GameObject bookPanel;
    [SerializeField] GameObject reportPanel;
    [SerializeField] GameObject unlockPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject pediaPanel;
    [SerializeField] GameObject buttonReportPanel;
    [SerializeField] GameObject buttonUnlockPanel;
    [SerializeField] GameObject buttonUpgradePanel;
    [SerializeField] GameObject buttonPediaPanel;
    [SerializeField] GameObject moneyBox;
    public Animator animBook;

    private void Start()
    {
        bookPanel.SetActive(false);
        buttonReportPanel.SetActive(false);
        buttonUnlockPanel.SetActive(false);
        buttonUpgradePanel.SetActive(false);
        buttonPediaPanel.SetActive(false);
    }
    public void OpenBook()
    {
        Debug.Log("test");
        bookPanel.SetActive(true);
        reportPanel.SetActive(true);
        buttonReportPanel.SetActive(false);

        unlockPanel.SetActive(false);
        buttonUnlockPanel.SetActive(true);
        upgradePanel.SetActive(false);
        buttonUpgradePanel.SetActive(true);
        pediaPanel.SetActive(false);
        buttonPediaPanel.SetActive(true);
        moneyBox.SetActive(false);
    }

    public void CloseBook()
    {
        StartCoroutine(CloseBookDelay());
    }

    public void OpenReport()
    {
        reportPanel.SetActive(true);
        buttonReportPanel.SetActive(false);

        unlockPanel.SetActive(false);
        upgradePanel.SetActive(false);
        pediaPanel.SetActive(false);

        buttonUnlockPanel.SetActive(true);
        buttonUpgradePanel.SetActive(true);
        buttonPediaPanel.SetActive(true);
        moneyBox.SetActive(false);
    }

    public void OpenUnlock()
    {
        unlockPanel.SetActive(true);
        buttonUnlockPanel.SetActive(false);

        reportPanel.SetActive(false);
        upgradePanel.SetActive(false);
        pediaPanel.SetActive(false);

        buttonReportPanel.SetActive(true);
        buttonUpgradePanel.SetActive(true);
        buttonPediaPanel.SetActive(true);
        moneyBox.SetActive(true);
    }

    public void OpenUpgrade()
    {
        upgradePanel.SetActive(true);
        buttonUpgradePanel.SetActive(false);

        unlockPanel.SetActive(false);
        reportPanel.SetActive(false);
        pediaPanel.SetActive(false);

        buttonReportPanel.SetActive(true);
        buttonUnlockPanel.SetActive(true);
        buttonPediaPanel.SetActive(true);
        moneyBox.SetActive(true);
    }

    public void OpenPedia()
    {
        pediaPanel.SetActive(true);
        buttonPediaPanel.SetActive(false);

        unlockPanel.SetActive(false);
        upgradePanel.SetActive(false);
        reportPanel.SetActive(false);

        buttonReportPanel.SetActive(true);
        buttonUnlockPanel.SetActive(true);
        buttonUpgradePanel.SetActive(true);
        moneyBox.SetActive(false);
    }

    IEnumerator CloseBookDelay()
    {
        Debug.Log("belum mulai");
        animBook.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        bookPanel.SetActive(false);
        Debug.Log("selesai");
    }

}
