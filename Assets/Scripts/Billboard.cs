using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform mendesain;
    [SerializeField] Transform mencanting;
    [SerializeField] Transform mewarnai;
    [SerializeField] Transform menjemur;
    [SerializeField] Transform menglodor;
    [SerializeField] float heightAbove = 2f;
    HidePlayerManager hidePlayerManager;

    private void Start() 
    {
        hidePlayerManager = FindAnyObjectByType<HidePlayerManager>();
    }

    private void LateUpdate() 
    {
        if(hidePlayerManager.isStartDesain)
        {
            Vector3 newPosition = mendesain.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else if(hidePlayerManager.isStartCanting)
        {
            Vector3 newPosition = mencanting.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else if(hidePlayerManager.isStartMewarnai)
        {
            Vector3 newPosition = mewarnai.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else if(hidePlayerManager.isStartMenjemur)
        {
            Vector3 newPosition = menjemur.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else if(hidePlayerManager.isStartMenglodor)
        {
            Vector3 newPosition = menglodor.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = player.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
    }
}
