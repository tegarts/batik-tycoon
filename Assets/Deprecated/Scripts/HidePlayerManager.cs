using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlayerManager : MonoBehaviour
{
    ArrowMechanic arrow;
    public GameObject mendesain;
    public GameObject mencanting;
    public GameObject mewarnai;
    public GameObject menjemur;
    public GameObject menglodor;
    public GameObject childPlayer;
    public bool isStartDesain, isStartCanting, isStartMewarnai, isStartMenjemur, isStartMenglodor;

    private void Start() 
    {
        arrow = FindAnyObjectByType<ArrowMechanic>();
        mendesain.SetActive(false);
        mencanting.SetActive(false); 
        mewarnai.SetActive(false);
        menjemur.SetActive(false);
        menglodor.SetActive(false);   
    }

    private void Update() 
    {
        if(isStartDesain)
        {
            childPlayer.SetActive(false);
            mendesain.SetActive(true);
        }
        else if(isStartCanting)
        {
            childPlayer.SetActive(false);
            mencanting.SetActive(true);
        }
        else if(isStartMewarnai)
        {
            childPlayer.SetActive(false);
            mewarnai.SetActive(true);
        }
        else if(isStartMenjemur)
        {
            childPlayer.SetActive(false);
            menjemur.SetActive(true);
        }
        else if(isStartMenglodor)
        {
            childPlayer.SetActive(false);
            menglodor.SetActive(true);
        }
        else
        {
            childPlayer.SetActive(true);
            mendesain.SetActive(false);
            mencanting.SetActive(false);
            mewarnai.SetActive(false);
            menjemur.SetActive(false);
            menglodor.SetActive(false);
        }
    }
}
