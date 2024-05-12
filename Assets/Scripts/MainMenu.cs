using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string sceneToLoad;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> optionsList = new List<string>();

        int currentRes = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            optionsList.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        Debug.Log("Loading scene "+ sceneToLoad);
    }

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        Debug.Log("Fullscreen changed to " + fullScreen);
    }

    public void SetResolution(int resolution)
    {
        Resolution res = resolutions[resolution];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        Debug.Log("Resolution set to " + res.width + " " + res.height);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting aplication");
    }

}
