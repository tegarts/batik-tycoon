using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSetter : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    
    [Header("Audio Clip")]
    public AudioClip bgMenu;
    public AudioClip bgIngame;
    public AudioClip clickedButton;
    public AudioClip cursorArea;
    public AudioClip openedDoor;
    public AudioClip lightSpread;
    public AudioClip OpenPanel;
    public AudioClip ClosePanel;

    [Header("Audio Clip NPC")]
    public AudioClip npcHappyGirl;
    public AudioClip npcFlatGirl;
    public AudioClip npcAngryGirl;
    public AudioClip npcHappyBoy;
    public AudioClip npcFlatBoy;
    public AudioClip npcAngryBoy;
    public AudioClip bellEntrance;

    [Header("Audio Clip Workspace")]
    public AudioClip UnlockWS;
    public AudioClip UpgradeWS;
    public AudioClip desain;
    public AudioClip canting;
    public AudioClip mewarnai;
    public AudioClip menjemur;
    public AudioClip lorod;
    public AudioClip angin;
    [Header("Audio Clip Notif")]
    public AudioClip notif;
    public AudioClip gameOver;
    public AudioClip afterCanting;
    public AudioClip error;

    [Header("Audio Clip Game Result")]
    public AudioClip gameResult;
    public AudioClip numberCounter;
    public AudioClip star;
    public AudioClip reactions;

    [Header("Other")]
    public static AudioSetter instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBackgroundMusic();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic(); 
    }

    public void PlayBackgroundMusic()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            musicSource.clip = bgMenu;
        }
        // Masukkan scene main

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        musicSource.Stop();
    }

    public void StopSFX()
    {
        SFXSource.Stop();
    }

    public void SFX_ClickedButton()
    {
        SFXSource.clip = clickedButton;
        SFXSource.Play();
    }

    public void SFX_ButtonCursorArea()
    {
        SFXSource.clip = cursorArea;
        SFXSource.Play();
    }
}
