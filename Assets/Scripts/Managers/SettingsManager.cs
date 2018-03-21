using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class settingsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;

    [Header("Resolutions")]
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    void Start()
    {
        // GETTING ALL RESOLUTIONS //
        resolutions = Screen.resolutions;

        // CLEARING PREVIOUS RESOLUTIONS //
        resolutionDropdown.ClearOptions();

        // ADDING NEW RESOLUTIONS TO A LIST //
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        // FOR EVERY RESOLUTION, DISPLAY THE WIDTH AND HEIGHT THEN AD OPTIONS //
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        // SETTING VOLUME WHEN SLIDER IS MOVED //
        Debug.Log(volume);
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        // CHANGE GRAPHICS QUALITY ON DROPDOWN CHANGE //
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
