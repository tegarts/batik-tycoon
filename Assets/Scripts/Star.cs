using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    Animator animator;
    public bool isStarted;

    private void Start() 
    {
        animator = GetComponent<Animator>();    
    }

    private void Update() 
    {
        if (isStarted)
        {
            animator.SetBool("IsStart", true);
        }
    }
}
