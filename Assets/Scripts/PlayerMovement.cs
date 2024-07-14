using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Vector3 moveDirection = Vector3.zero;
    [HideInInspector] public Vector3 lookDirection = Vector3.forward;
    [SerializeField] float speed = 6f;
    [SerializeField] Rigidbody rigidBody;
    bool canMove = true;

    private void Reset() 
    {
        rigidBody = GetComponent<Rigidbody>();
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

    }
}
