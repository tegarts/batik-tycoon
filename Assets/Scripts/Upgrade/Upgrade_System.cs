using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_System : MonoBehaviour
{
    [SerializeField] private GameObject panelUpgrade;
    public void openUpgrade_Panel()
    {
        panelUpgrade.SetActive(true);
    }
}
