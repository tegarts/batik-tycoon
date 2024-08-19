using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Offset;

    private void Awake() 
    {
        if(gameObject.transform.parent.GetComponent<NPCBehav>() != null)
        {
            Target = gameObject.transform.parent.transform;
        }
    }
    private void Update()
    {
        if(Target != null)
        {
            transform.position = Target.position + Offset;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
