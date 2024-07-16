using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Transform player;
    [SerializeField] float heightAbove = 2f;

    private void Start() 
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    private void LateUpdate() 
    {
        if(player != null)
        {
            Vector3 newPosition = player.transform.position + Vector3.up * heightAbove;
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player transform is not set");
        }
    }
}
