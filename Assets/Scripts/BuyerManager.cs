using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerManager : MonoBehaviour
{
    PlayerInfo playerInfo;
    [SerializeField] private int kainPrice;
    public GameObject[] textBuying;
    public Transform[] textBuyingLocations;
    public Transform ParentCanvas;
    AudioManager audioManager;

    private void Start() 
    {
        playerInfo = FindAnyObjectByType<PlayerInfo>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void Update() 
    {

    }

    public void NPCBuying1()
    {
        if(playerInfo.kain > 0)
        {
            playerInfo.AddMoney(kainPrice);
            playerInfo.ReduceKain(1);
            audioManager.PlaySFX(audioManager.peopleBuy);
            GameObject instance = Instantiate(textBuying[0], textBuyingLocations[0].position, textBuyingLocations[0].rotation);
            instance.transform.parent = ParentCanvas;
        }
        else
        {
            audioManager.PlaySFX(audioManager.noKain);
        }
        
        
    }

    public void NPCBuying2()
    {
        if(playerInfo.kain > 0)
        {
            playerInfo.AddMoney(kainPrice);
            playerInfo.ReduceKain(1);
            audioManager.PlaySFX(audioManager.peopleBuy);
            GameObject instance = Instantiate(textBuying[1], textBuyingLocations[1].position, textBuyingLocations[1].rotation);
            instance.transform.parent = ParentCanvas;
        }
        else
        {
            audioManager.PlaySFX(audioManager.noKain);
        }
        
    }

    public void NPCBuying3()
    {
        if(playerInfo.kain > 0)
        {
            playerInfo.AddMoney(kainPrice);
            playerInfo.ReduceKain(1);
            audioManager.PlaySFX(audioManager.peopleBuy);
            GameObject instance = Instantiate(textBuying[2], textBuyingLocations[2].position, textBuyingLocations[2].rotation);
            instance.transform.parent = ParentCanvas;
        }
        else
        {
            audioManager.PlaySFX(audioManager.noKain);
        }
        
    }

    
}
