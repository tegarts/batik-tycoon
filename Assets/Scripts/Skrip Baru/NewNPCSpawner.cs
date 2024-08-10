using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNPCSpawner : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Canvas canvasWorldSpace;
    [SerializeField] private GameObject npc1;

    private void Start() 
    {
        npc1.GetComponent<NPCBuyingManager>().SetupButton(canvasWorldSpace, cam);
    }
}
