using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Batikpedia : MonoBehaviour
{
    public GameObject batikpediaPanel;
    [SerializeField] GameObject mainBatikpedia;
    [SerializeField] GameObject detailBatikpedia;
    [SerializeField] Image imageBatikHeader;
    [SerializeField] Image imageBatikInGame;
    [SerializeField] TMP_Text namaBatik;
    [SerializeField] TMP_Text detail1;
    [SerializeField] TMP_Text detail2;
    [SerializeField] Sprite[] batikHeaders;
    [SerializeField] Sprite[] batikInGame;
    WorkspaceManager workspaceManager;
    public Animator animBatikpedia;

    private void Start() 
    {
        workspaceManager = FindObjectOfType<WorkspaceManager>();
        batikpediaPanel.SetActive(false);
    }

    public void OpenBatikPedia()
    {
        batikpediaPanel.SetActive(true);
        mainBatikpedia.SetActive(true);
        detailBatikpedia.SetActive(false);
    }

    public void CloseBatikpedia()
    {
        // audioSetter.PlaySFX(audioSetter.ClosePanel);
        StartCoroutine(CloseBatikpediaDelay());
    }

     IEnumerator CloseBatikpediaDelay()
    {
        animBatikpedia.SetTrigger("IsEnd");
        yield return new WaitForSeconds(0.25f);
        batikpediaPanel.SetActive(false);
        Debug.Log("selesai");
    }

    public void OpenDetailBatik(int index)
    {

        if (index == 0)
        {
            // audioSetter.PlaySFX(audioSetter.OpenPanel);
            detailBatikpedia.SetActive(true);
            mainBatikpedia.SetActive(false);
            imageBatikHeader.sprite = batikHeaders[0];
            imageBatikInGame.sprite = batikInGame[0];
            namaBatik.text = "Batik Kawung";
            detail1.text = "Asal Kota: Yogyakarta dan Surakarta, \n Jawa Tengah, Indonesia";
            detail2.text = "Batik Kawung adalah motif batik klasik dari Jawa, yang dikenal dengan pola geometris berbentuk lingkaran mirip buah kolang-kaling atau aren. Motif ini melambangkan kesucian, keadilan, dan ketulusan hati. Sering digunakan oleh kalangan kerajaan, batik Kawung mencerminkan keseimbangan hidup dan kesederhanaan yang abadi.";
        }
        else if (index == 1)
        {
            if (workspaceManager.motifUnlocked < 1)
            {
                workspaceManager.notifText.text = "<color=#9E3535>Buka Workspace Batik Megamendung terlebih dahulu</color>";
                workspaceManager.imageWarning.GetComponent<Image>().sprite = workspaceManager.images[0];
                workspaceManager.ShowNotif();
                // audioSetter.PlaySFX(audioSetter.notif);
            }
            else
            {
                // audioSetter.PlaySFX(audioSetter.OpenPanel);
                detailBatikpedia.SetActive(true);
                mainBatikpedia.SetActive(false);
                imageBatikHeader.sprite = batikHeaders[1];
                imageBatikInGame.sprite = batikInGame[1];
                namaBatik.text = "Batik Megamendung";
                detail1.text = "Asal Kota: Cirebon, \n Jawa Barat, Indonesia";
                detail2.text = "Batik Megamendung adalah motif khas dari Cirebon yang menggambarkan bentuk awan dengan garis-garis tebal dan warna gradasi yang kontras, menyerupai langit mendung. Motif ini melambangkan ketenangan, kesabaran, dan keseimbangan, meski di tengah badai kehidupan. Terinspirasi oleh budaya Tionghoa, motif Megamendung juga mencerminkan akulturasi budaya di Cirebon.";
            }

        }
        else if (index == 2)
        {
            if (workspaceManager.motifUnlocked < 2)
            {
                workspaceManager.notifText.text = "<color=#9E3535>Buka Workspace Batik Truntum terlebih dahulu</color>";
                workspaceManager.imageWarning.GetComponent<Image>().sprite = workspaceManager.images[0];
                workspaceManager.ShowNotif();
                // audioSetter.PlaySFX(audioSetter.notif);
            }
            else
            {
                // audioSetter.PlaySFX(audioSetter.OpenPanel);
                detailBatikpedia.SetActive(true);
                mainBatikpedia.SetActive(false);
                imageBatikHeader.sprite = batikHeaders[2];
                imageBatikInGame.sprite = batikInGame[2];
                namaBatik.text = "Batik Truntum";
                detail1.text = "Asal Kota: Surakarta, \n Jawa Tengah, Indonesia";
                detail2.text = "Batik Truntum melambangkan simbol kasih sayang, kesetiaan, dan keharmonisan. Diciptakan oleh Ratu Kencana pada abad ke-18, motif ini menggambarkan bunga tanjung dan bintang di langit malam sebagai ekspresi cinta yang bersemi kembali setelah diabaikan oleh Sunan Pakubuwana III. Batik Truntum sering digunakan dalam pernikahan Jawa, melambangkan hubungan yang harmonis dan spiritual.";
            }
        }
        else if (index == 3)
        {
            if (workspaceManager.motifUnlocked < 3)
            {
                workspaceManager.notifText.text = "<color=#9E3535>Buka Workspace Batik Parang terlebih dahulu</color>";
                workspaceManager.imageWarning.GetComponent<Image>().sprite = workspaceManager.images[0];
                workspaceManager.ShowNotif();
                // audioSetter.PlaySFX(audioSetter.notif);
            }
            else
            {
                // audioSetter.PlaySFX(audioSetter.OpenPanel);
                detailBatikpedia.SetActive(true);
                mainBatikpedia.SetActive(false);
                imageBatikHeader.sprite = batikHeaders[3];
                imageBatikInGame.sprite = batikInGame[3];
                namaBatik.text = "Batik Parang";
                detail1.text = "Asal Kota: Yogyakarta dan Surakarta, \n Jawa Tengah, Indonesia";
                detail2.text = "Batik Parang adalah salah satu motif batik tertua dari Jawa, yang melambangkan kekuatan, keberanian, dan semangat yang tak pernah padam. Pola garis-garis diagonal yang menyerupai ombak laut atau parang (sejenis senjata tradisional) melambangkan perjuangan hidup yang terus-menerus. Motif ini sering digunakan oleh keluarga kerajaan sebagai simbol keteguhan dan kewibawaan.";
            }
        }
        else if (index == 4)
        {
            if (workspaceManager.motifUnlocked < 4)
            {
                workspaceManager.notifText.text = "<color=#9E3535>Buka Workspace Batik Simbut terlebih dahulu</color>";
                workspaceManager.imageWarning.GetComponent<Image>().sprite = workspaceManager.images[0];
                workspaceManager.ShowNotif();
                // audioSetter.PlaySFX(audioSetter.notif);
            }
            else
            {
                // audioSetter.PlaySFX(audioSetter.OpenPanel);
                detailBatikpedia.SetActive(true);
                mainBatikpedia.SetActive(false);
                imageBatikHeader.sprite = batikHeaders[4];
                imageBatikInGame.sprite = batikInGame[4];
                namaBatik.text = "Batik Simbut";
                detail1.text = "Asal Kota: Lebak, \n Banten, Indonesia";
                detail2.text = "Batik Simbut adalah motif batik khas suku Badui di Lebak, Banten, yang menggambarkan kesederhanaan dan hubungan harmonis dengan alam. Motifnya menyerupai daun talas dengan pola sederhana dan geometris, melambangkan keseimbangan hidup dan kedekatan suku Badui dengan alam sekitar. Batik ini sering digunakan dalam upacara adat dan kehidupan sehari-hari suku Badui.";
            }

        }

    }

    public void BackBatikPedia()
    {
        // audioSetter.PlaySFX(audioSetter.OpenPanel);
        detailBatikpedia.SetActive(false);
        mainBatikpedia.SetActive(true);
    }
}
