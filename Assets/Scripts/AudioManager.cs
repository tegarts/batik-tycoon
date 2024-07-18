using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Clip")]
    public AudioClip bgMenu;
    public AudioClip bgIngame;
    public AudioClip clickedButton;
    public AudioClip cursorArea;
    public AudioClip correctArrow;
    public AudioClip wrongArrow;
    public AudioClip successArrow;

    public static AudioManager instance;

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
        PlayBackgroundMusic(); // Memainkan musik latar sesuai dengan scene yang dimuat
    }

    public void PlayBackgroundMusic()
    {
        // TODO - Ganti nama scene
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            musicSource.clip = bgMenu;
        }
        else if (SceneManager.GetActiveScene().name == "Control")
        {
            musicSource.clip = bgIngame;
        }

        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
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
