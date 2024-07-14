using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement = null;

    private void Reset() 
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() 
    {
        HandleMoveInput();
    }

    void HandleMoveInput()
    {
        if(playerMovement == null)
        return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        playerMovement.moveDirection = new Vector3(horizontal, 0, vertical);
        // TODO - Tambah code buat arah look player

        if(playerMovement.moveDirection != Vector3.zero)
        {
            playerMovement.lookDirection = playerMovement.moveDirection;
        }
    }
}
