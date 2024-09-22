using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public int index;
    public Animator anim;
    Tutorial tutorial;
    FollowMouse followMouse;

    private void Start() 
    {
        if(anim == null)
        {
            anim = GetComponent<Animator>();
        }
        tutorial = FindObjectOfType<Tutorial>();
        followMouse = GetComponent<FollowMouse>();
    }

    private void Update() 
    {
        if(tutorial.isStartTutor && tutorial.index != 5 && index == 0)
        {
            anim.SetBool("IsTutor", false);
        }
        else if(tutorial.isStartTutor && tutorial.index == 5 && index == 1 && followMouse.instantiatedImage == null)
        {
            anim.SetBool("IsTutor", true);
        }
        else if(tutorial.isStartTutor && tutorial.index == 7 && index == 1 && followMouse.instantiatedImage == null)
        {
            anim.SetBool("IsTutor", true);
        }
        else if(tutorial.isStartTutor && tutorial.index == 5 && index == 1 && followMouse.instantiatedImage != null)
        {
            anim.SetBool("IsTutor", false);
        }
        else if(tutorial.isStartTutor && tutorial.index == 7 && index == 1 && followMouse.instantiatedImage != null)
        {
            anim.SetBool("IsTutor", false);
        }
        else if(tutorial.isStartTutor && tutorial.index != 7 && index == 2)
        {
            anim.SetBool("IsTutor", false);
        }
        else if(tutorial.isAlreadyTutor)
        {
            anim.SetBool("IsTutor", false);
        }
    }


}
