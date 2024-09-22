using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTimer : MonoBehaviour
{
    public GameObject[] npcs;
    public Image[] npcUsedFace;
    public TMP_Text[] npcTimerText;
    public Sprite[] npcFaces;

    DrawingManager drawingManager;
    NPCSpawn nPCSpawn;


    DayManager dayManager;

    private void Start() 
    {
        dayManager = FindAnyObjectByType<DayManager>();
        drawingManager = FindAnyObjectByType<DrawingManager>();
        nPCSpawn = FindAnyObjectByType<NPCSpawn>();

        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].SetActive(false);
        }
    }

    private void Update() 
    {
        // if(drawingManager.canvasCanting.activeSelf)
        // {
        //     for(int i = 0; i < npcs.Length; i++)
        //     {
        //         if(i < nPCSpawn.activeNPCs.Count)
        //         {
        //             npcs[i].SetActive(true);

        //             float countdown = Mathf.Max(0, 15 - nPCSpawn.activeNPCs[i].elapsedTime);
        //             npcTimerText[i].text = countdown.ToString("F0");

        //             if(nPCSpawn.activeNPCs[i].indexKarakter == 0)
        //             {
        //                 npcUsedFace[i].sprite = npcFaces[0];
        //             }
        //             else if(nPCSpawn.activeNPCs[i].indexKarakter == 1)
        //             {
        //                 npcUsedFace[i].sprite = npcFaces[1];
        //             }
        //             else if(nPCSpawn.activeNPCs[i].indexKarakter == 2)
        //             {
        //                 npcUsedFace[i].sprite = npcFaces[2];
        //             }
        //         }
        //         else
        //         {
        //             npcs[i].SetActive(false);
        //         }
        //     }
        // }

        if(dayManager.dayIsStarted)
        {
            for(int i = 0; i < npcs.Length; i++)
            {
                if(i < nPCSpawn.activeNPCs.Count)
                {
                    npcs[i].SetActive(true);

                    float countdown = Mathf.Max(0, 25 - nPCSpawn.activeNPCs[i].elapsedTime);
                    npcTimerText[i].text = string.Format("00:{0:00}", countdown);

                    if(nPCSpawn.activeNPCs[i].indexKarakter == 0)
                    {
                        npcUsedFace[i].sprite = npcFaces[0];
                    }
                    else if(nPCSpawn.activeNPCs[i].indexKarakter == 1)
                    {
                        npcUsedFace[i].sprite = npcFaces[1];
                    }
                    else if(nPCSpawn.activeNPCs[i].indexKarakter == 2)
                    {
                        npcUsedFace[i].sprite = npcFaces[2];
                    }
                    else if(nPCSpawn.activeNPCs[i].indexKarakter == 3)
                    {
                        npcUsedFace[i].sprite = npcFaces[3];
                    }
                }
                else
                {
                    npcs[i].SetActive(false);
                }
            }
        }
    }
}
