using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorBuyer : MonoBehaviour
{
    public LayoutElement layoutElement;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        // Disable layout control temporarily
        layoutElement.ignoreLayout = true;
        
        // Then start your animation here (via Animator, Tweening, or script)
        AnimateObject();
    }

    void AnimateObject()
    {
        anim.SetTrigger("IsStart");
    }

    void ResetLayoutControl()
    {
        // Re-enable layout control after animation finishes
        layoutElement.ignoreLayout = false;
    }
}
