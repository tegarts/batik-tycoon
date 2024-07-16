using System.Collections;
using System.Collections.Generic;
#pragma warning disable 0414
using UnityEngine;

public class MouseLocation : MonoBehaviour
{
    public static MouseLocation Instance;

    [HideInInspector] public Vector3 MousePosition;	
	[HideInInspector] public bool IsValid;			

	[SerializeField] LayerMask whatIsGround;		

	Ray mouseRay;									
	RaycastHit hit;									
	Vector2 screenPosition;							
	bool isTouchAiming;		

    void Awake()
	{
		if (Instance == null)
			Instance = this;
		
		else if (Instance != this)
			Destroy(this);
	}

    void Update()
	{
		IsValid = false;

	    screenPosition = Input.mousePosition;

		mouseRay = Camera.main.ScreenPointToRay(screenPosition);

		if (Physics.Raycast(mouseRay, out hit, 100f, whatIsGround))
		{
			IsValid = true;
			
			MousePosition = hit.point;
		}
	}

}
