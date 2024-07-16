using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    void Update()
    {
        if (MouseLocation.Instance != null && MouseLocation.Instance.IsValid)
		{
			transform.LookAt (MouseLocation.Instance.MousePosition);
		}
    }
}
