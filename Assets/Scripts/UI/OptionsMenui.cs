using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Text BGMvolumeText;
    [SerializeField] private Text SFXvolumeText;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private float currentVolume = 100;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string op = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(op);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        BGMvolumeText.text = ($"VOLUME: {audioMixer.GetFloat("Volume", out currentVolume)}");
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ApplyButton()
    {
        PlayerPrefs.Save();
    }
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BgmVolume", volume);
        BGMvolumeText.text =  ($"Music: {(int)volume + 80}");
        currentVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        SFXvolumeText.text =  ($"SFX: {(int)volume + 80}");
        currentVolume = volume;
    }
    public void SetGraphicsQuality(int qualIndex)
    {
        QualitySettings.SetQualityLevel(qualIndex);
    }
    public void ToggleVSync(bool isEnabled)
    {
        QualitySettings.vSyncCount = isEnabled ? 1 : 0;
        Debug.Log($"vsync status: {QualitySettings.vSyncCount}");
    }
    public void ToggleFullscreen(bool isEnabled)
    {
        Screen.fullScreen = isEnabled;
    }
}
