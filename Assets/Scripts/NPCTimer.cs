using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCTimer : MonoBehaviour
{
    public GameObject[] npcs;
    public TMP_Text[] npcTimerText;
    public Sprite[] npcFaces;

    DrawingManager drawingManager;
    NPCSpawn nPCSpawn;

    private void Start() 
    {
        drawingManager = FindAnyObjectByType<DrawingManager>();
        nPCSpawn = FindAnyObjectByType<NPCSpawn>();

        for(int i = 0; i < npcs.Length; i++)
        {
            npcs[i].SetActive(false);
        }    
    }

    private void Update() 
    {
        if(drawingManager.canvasCanting.activeSelf)
        {
            for(int i = 0; i < npcs.Length; i++)
            {
                if(1 + i < nPCSpawn.activeNPCs.Count)
                {
                    npcs[i].SetActive(true);

                    float countdown = Mathf.Max(0, 15 - nPCSpawn.activeNPCs[i + 1].elapsedTime);
                    npcTimerText[i].text = countdown.ToString("F0");

                    if(nPCSpawn.activeNPCs[i + 1].indexKarakter == 0)
                    {
                        npcs[i].GetComponent<Image>().sprite = npcFaces[0];
                    }
                    else if(nPCSpawn.activeNPCs[i + 1].indexKarakter == 1)
                    {
                        npcs[i].GetComponent<Image>().sprite = npcFaces[1];
                    }
                    else if(nPCSpawn.activeNPCs[i + 1].indexKarakter == 2)
                    {
                        npcs[i].GetComponent<Image>().sprite = npcFaces[2];
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
