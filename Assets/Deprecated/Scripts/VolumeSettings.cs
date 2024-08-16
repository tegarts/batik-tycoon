using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetDefaultVolume();
        }

        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        if (volume == 0)
        {
            myMixer.SetFloat("music", -80); // Set to a very low value to mute
        }
        else
        {
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        if (volume == 0)
        {
            myMixer.SetFloat("SFX", -80); // Set to a very low value to mute
        }
        else
        {
            myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        }
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }

    private void SetDefaultVolume()
    {
        musicSlider.value = 0.5f; // Default value for the slider
        SFXSlider.value = 0.5f; // Default value for the slider

        SetMusicVolume();
        SetSFXVolume();
    }
}
