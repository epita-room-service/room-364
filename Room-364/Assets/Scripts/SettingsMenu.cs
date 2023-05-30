
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public TMPro.TMP_Dropdown resDropDown;
    Resolution[] resolutions;
    
    public void Start()
    {
        resolutions = resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resDropDown.ClearOptions();
        List<string> options = new List<string>();
        int resIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                resIndex = i;
            }
        }

        resDropDown.AddOptions(options);
        resDropDown.value = resIndex;
        resDropDown.RefreshShownValue();

        Screen.fullScreen = true;
    }
    
    public AudioMixer audio;

    public string Menu;

    public void SetVolume(float volume)
    {
        audio.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolIndex)
    {
        Resolution resolution = resolutions[resolIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void CloseSettings()
    {
        SceneManager.LoadScene(Menu);
    }

}
