using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSetting : MonoBehaviour
{

    [SerializeField] bool is16v9;
    [SerializeField] bool hasHz;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    List<Resolution> resolutions;

    public int ResolutionIndex
    {
        get => PlayerPrefs.GetInt("ResolutionIndex", 0);
        set => PlayerPrefs.SetInt("ResolutionIndex", value);
    }
    public bool IsFullscreen
    {
        get => PlayerPrefs.GetInt("IsFullscreen", 1) == 1;
        set => PlayerPrefs.SetInt("IsFullscreen", value ? 1 : 0);
    }
    void Start()
    {
#if !UNITY_EDITOR
        Invoke(nameof(SetResolution), 0.1f);
#endif
    }
    void SetResolution()
    {
        resolutions = new List<Resolution>(Screen.resolutions);
        resolutions.Reverse();

        if(is16v9)
        {
            resolutions = resolutions.FindAll(x => (float)x.width / x.height == 16f / 9);
        }

        if(!hasHz && resolutions.Count > 0)
        {
            List<Resolution> tempResolutions = new List<Resolution>();
            int CurWidth = resolutions[0].width;
            int CurHeight = resolutions[0].height;

            tempResolutions.Add(resolutions[0]);
            foreach (var resolution in resolutions)
            {
                if(CurWidth != resolution.width || CurHeight != resolution.height)
                {
                    tempResolutions.Add(resolution);
                    CurWidth = resolution.width;
                    CurHeight = resolution.height;
                }
            }
            resolutions = tempResolutions;
        }

        List<string> options = new List<string>();
        foreach (var resolution in resolutions)
        {
            string option = $"{resolution.width} x {resolution.height}";
            if(hasHz)
            {
                option += $"{resolution.refreshRate}Hz";
            }
            options.Add(option);
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = ResolutionIndex;
        fullscreenToggle.isOn = IsFullscreen;

        resolutionDropdown.RefreshShownValue();
    }

    public void DropdownOptionChanged(int resolutionIndex)
    {
        ResolutionIndex = resolutionIndex;
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void FullscreenToggleChanged(bool isFull)
    {
        IsFullscreen = isFull;
        Screen.fullScreen = isFull;
    }
}

