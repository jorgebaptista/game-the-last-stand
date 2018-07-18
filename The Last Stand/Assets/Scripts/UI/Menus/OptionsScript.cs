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

    [Header("Video Settings")]
    [Space]
    [SerializeField]
    private Toggle windowedModeToggle;

    [Space]
    [SerializeField]
    private Text graphicsQualityText;

    private int currentQualityIndex;
    private void Awake()
    {
        currentQualityIndex = currentQualityIndex = QualitySettings.GetQualityLevel();
    }
    private void Start()
    {
        UpdateSoundToggle();
        UpdateQualitySettingsButtonText();
    }
    public void UpdateSoundToggle()
    {
        if (!soundEnabledToggle.isOn)
        {
            for (int sliderIndex = 0; sliderIndex < soundSliders.Length; ++sliderIndex)
            {
                soundSliders[sliderIndex].interactable = false;
                Image[] sliderImages = soundSliders[sliderIndex].GetComponentsInChildren<Image>();

                for (int imageIndex = 0; imageIndex < sliderImages.Length; ++imageIndex)
                {
                    sliderImages[imageIndex].color = Color.gray;
                }
            }
        }
        else
        {
            for (int sliderIndex = 0; sliderIndex < soundSliders.Length; ++sliderIndex)
            {
                soundSliders[sliderIndex].interactable = true;
                Image[] sliderImages = soundSliders[sliderIndex].GetComponentsInChildren<Image>();

                for (int imageIndex = 0; imageIndex < sliderImages.Length; ++imageIndex)
                {
                    sliderImages[imageIndex].color = Color.white;
                }
            }
        }
    }

    //**********************************************************
    public void ToggleWindowedMode()
    {
        Debug.Log(windowedModeToggle.isOn);
        //Screen.fullScreen = !windowedModeToggle.isOn;
        if (windowedModeToggle.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
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
}
