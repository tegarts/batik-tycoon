using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartDay : MonoBehaviour
{
    public GameObject startDayButton;
    public GameObject startDayPanel;
    public Animator animPanel;
    public Animator animButton;
    AudioSetter audioSetter;

    private void Awake()
    {
        audioSetter = GameObject.FindWithTag("Audio").GetComponent<AudioSetter>();
    }


    private void Start() 
    {
        startDayPanel.SetActive(false);   
    }

    public void OpenStartDayPanel()
    {
        audioSetter.PlaySFX(audioSetter.OpenPanel);
        startDayPanel.SetActive(true);
    }

    public void CloseStartDayPanel()
    {
        audioSetter.PlaySFX(audioSetter.ClosePanel);
        StartCoroutine(CloseStartDayPanelWithDelay());
    }

    IEnumerator CloseStartDayPanelWithDelay()
    {
        animPanel.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        startDayPanel.SetActive(false);
    }


    public void CloseStartDayButton()
    {
        StartCoroutine(CloseStartDayButtonDelay());
    }

    IEnumerator CloseStartDayButtonDelay()
    {
        animButton.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.5f);
        startDayButton.SetActive(false);
    }

}
