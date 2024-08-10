using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject[] motif1_1;
    [SerializeField] GameObject[] motif1_1Target;
    public bool level1isDone;
    public float tolerance = 20f;
    
    private void Update()
    {
        if (level1isDone)
        {
            level1isDone = false;
            for (int i = 0; i < motif1_1.Length; i++)
            {
                if (IsWithinTargetRange(motif1_1[i].GetComponent<RectTransform>().anchoredPosition, 0) ||
                IsWithinTargetRange(motif1_1[i].GetComponent<RectTransform>().anchoredPosition, 1) ||
                IsWithinTargetRange(motif1_1[i].GetComponent<RectTransform>().anchoredPosition, 2) ||
                IsWithinTargetRange(motif1_1[i].GetComponent<RectTransform>().anchoredPosition, 3))
                {
                    Debug.Log("Perfect");
                }
                else
                {
                    Debug.Log("Miss");
                }

            }
        }
    }

    private bool IsWithinTargetRange(Vector2 position, int index)
    {
        Vector2 target = motif1_1Target[index].GetComponent<RectTransform>().anchoredPosition;
        float distance = Vector2.Distance(position, target);
        return distance <= tolerance;
    }
}
