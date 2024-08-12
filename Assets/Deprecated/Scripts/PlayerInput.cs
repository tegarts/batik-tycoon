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

        if(playerMovement.moveDirection != Vector3.zero)
        {
            playerMovement.lookDirection = playerMovement.moveDirection;
        }

    // // // Mekanik Mouse
    //  if (MouseLocation.Instance != null && MouseLocation.Instance.IsValid) {
	// 		//Find the point the player should look at by subtracting the player's position from the mouse's position
	// 		Vector3 lookPoint = MouseLocation.Instance.MousePosition - playerMovement.transform.position;
	// 		//Tell the player what direction to look
	// 		playerMovement.lookDirection = lookPoint;
	// 	}
        
    }
}
