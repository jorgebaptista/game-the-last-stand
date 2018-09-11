using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [Header ("Audio Settings")]
    [Space]
    [SerializeField]
    private Toggle soundEnabledToggle;

    [SerializeField]
    private Slider[] soundSliders;

    [Space]
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sFXSlider;

    [Space]
    [SerializeField]
    private Text graphicsQualityText;

    private int currentQualityIndex;

    private void Awake()
    {
        currentQualityIndex = QualitySettings.GetQualityLevel();
    }
    private void Start()
    {
        UpdateSoundToggle();
        UpdateQualitySettingsButtonText();

        //soundEnabledToggle.isOn = AudioManagerScript.instance.GetAudioState();
        musicSlider.value = AudioManagerScript.instance.GetMusicVolume();
        sFXSlider.value = AudioManagerScript.instance.GetSFxVoluma();
    }

    public void UpdateSoundToggle()
    {
        if (!soundEnabledToggle.isOn)
        {
            for (int sliderIndex = 0; sliderIndex < soundSliders.Length; ++sliderIndex)
            {
                soundSliders[sliderIndex].interactable = false;
            }

            AudioManagerScript.instance.DisableSound(true);
        }
        else
        {
            for (int sliderIndex = 0; sliderIndex < soundSliders.Length; ++sliderIndex)
            {
                soundSliders[sliderIndex].interactable = true;
            }

            AudioManagerScript.instance.DisableSound(false);
        }
    }

    public void ChangeQualitySettingsLevel()
    {
        if (currentQualityIndex >= QualitySettings.names.Length - 1)
        {
            currentQualityIndex = 0;
            QualitySettings.SetQualityLevel(currentQualityIndex);
        }
        else
        {
            QualitySettings.SetQualityLevel(++currentQualityIndex);
        }

        graphicsQualityText.text = QualitySettings.names[currentQualityIndex].ToString();
    }
    
    private void UpdateQualitySettingsButtonText()
    {
        graphicsQualityText.text = QualitySettings.names[currentQualityIndex].ToString();
    }

    public void UpdateSfxVolume()
    {
        AudioManagerScript.instance.UpdateSfxVolume(sFXSlider.value);
    }

    public void UpdateMusicVolume()
    {
        AudioManagerScript.instance.UpdateMusicVolume(musicSlider.value);
    }
}
