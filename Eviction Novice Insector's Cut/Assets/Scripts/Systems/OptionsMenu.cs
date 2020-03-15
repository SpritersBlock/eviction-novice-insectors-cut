using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Quality")]
    [SerializeField] TMP_Dropdown qualityDropdown;

    private void Start()
    {
        SetInitialDropdown();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetInitialDropdown()
    {
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
