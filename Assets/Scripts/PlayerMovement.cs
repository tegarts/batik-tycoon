using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Search;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public Vector3 lookDirection = Vector3.forward;
    [SerializeField] float speed = 6f;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rigidBody;
    public bool canMove = true;

    [Header("Printilan")]
    [SerializeField] GameObject pensil;

    private void Start() 
    {
        pensil.SetActive(false);
    }
    

    private void Reset() 
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() 
    {
        

        if(!canMove)
        return;

        moveDirection.Set(moveDirection.x, 0, moveDirection.z);
        rigidBody.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);

        if(lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            rigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f));
        }

        // // // Mekanik Mouse
        // lookDirection.Set(lookDirection.x, 0, lookDirection.z);
        // rigidBody.MoveRotation(Quaternion.LookRotation(lookDirection));
        animator.SetBool("IsWalking", moveDirection.sqrMagnitude > 0);
    }

    public void MendesainAnimation(bool con)
    {
        animator.SetBool("IsMendesain", con);
        pensil.SetActive(con);
        animator.SetBool("IsWalking", false);
    }
}
