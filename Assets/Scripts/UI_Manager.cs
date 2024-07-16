using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject panelUpgrade;

    void Update()
    {
        // Cek apakah tombol 'U' ditekan
        if (Input.GetKeyDown(KeyCode.U))
        {
            // Jika panel belum aktif, aktifkan
            if (!panelUpgrade.activeSelf)
            {
                openUpgrade_Panel();
            }
            else // Jika panel sudah aktif, nonaktifkan
            {
                closeUpgrade_Panel();
            }
        }
    }

    public void openUpgrade_Panel()
    {
        panelUpgrade.SetActive(true);
    }

    public void closeUpgrade_Panel()
    {
        panelUpgrade.SetActive(false);
    }
}
