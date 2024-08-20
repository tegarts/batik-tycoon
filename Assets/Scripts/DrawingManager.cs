using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingManager : MonoBehaviour
{
    public GameObject canvasCanting;
    [SerializeField] GameObject[] motifPanelKawungs;
    [SerializeField] GameObject[] motifPanelMegas;
    [SerializeField] GameObject[] motifPanelParangs;
    [SerializeField] GameObject[] motifPanelTruntums;
    [SerializeField] GameObject[] motifPanelSimbuts;

    private void Start() 
    {
        canvasCanting.SetActive(false);    
        for (int i = 0; i < motifPanelKawungs.Length; i++)
            {
                motifPanelKawungs[i].SetActive(false);
            }
        for (int i = 0; i < motifPanelMegas.Length; i++)
            {
                motifPanelMegas[i].SetActive(false);
            }
        for (int i = 0; i < motifPanelParangs.Length; i++)
            {
                motifPanelParangs[i].SetActive(false);
            }
        for (int i = 0; i < motifPanelTruntums.Length; i++)
            {
                motifPanelTruntums[i].SetActive(false);
            }
        for (int i = 0; i < motifPanelSimbuts.Length; i++)
            {
                motifPanelSimbuts[i].SetActive(false);
            }
    }

    public void CanvasController(bool condition)
    {
        canvasCanting.SetActive(condition);

        if(!condition)
        {
            for (int i = 0; i < motifPanelKawungs.Length; i++)
            {
                motifPanelKawungs[i].SetActive(false);
            }
            for (int i = 0; i < motifPanelMegas.Length; i++)
            {
                motifPanelMegas[i].SetActive(false);
            }
            for (int i = 0; i < motifPanelParangs.Length; i++)
            {
                motifPanelParangs[i].SetActive(false);
            }
            for (int i = 0; i < motifPanelTruntums.Length; i++)
            {
                motifPanelTruntums[i].SetActive(false);
            }
            for (int i = 0; i < motifPanelSimbuts.Length; i++)
            {
                motifPanelSimbuts[i].SetActive(false);
            }
        }
    }

    public void MatchMotifKawung()
    {
        int randomIndex = Random.Range(0, motifPanelKawungs.Length);
        motifPanelKawungs[randomIndex].SetActive(true);
    }

    public void MatchMotifMega()
    {
        int randomIndex = Random.Range(0, motifPanelMegas.Length);
        motifPanelMegas[randomIndex].SetActive(true);
    }

    public void MatchMotifParang()
    {
        int randomIndex = Random.Range(0, motifPanelParangs.Length);
        motifPanelParangs[randomIndex].SetActive(true);
    }

    public void MatchMotifTruntum()
    {
        int randomIndex = Random.Range(0, motifPanelTruntums.Length);
        motifPanelTruntums[randomIndex].SetActive(true);
    }

    public void MatchMotifSimbut()
    {
        int randomIndex = Random.Range(0, motifPanelSimbuts.Length);
        motifPanelSimbuts[randomIndex].SetActive(true);
    }
}
