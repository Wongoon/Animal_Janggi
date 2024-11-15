using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SetResolution : MonoBehaviour
{
    public Text text;

    public Button[] buttons;
    public int[,] resolutions = new int[,]{
        {1280, 720},
        {1366, 768},
        {1600, 900},
        {1920, 1080},
        {2560, 1440},
        {3840, 2160}
    };

    void Start() {
        CheckResolution();
        SetButtonInteractable();
    }

    private void SetButtonInteractable()
    {
        Resolution currentResolution = Screen.currentResolution;

        for(int i = 0; i < buttons.Length; i++) {
            if(currentResolution.width >= resolutions[i, 0] && currentResolution.height >= resolutions[i, 1]) {
                buttons[i].interactable = true;
            }
            else{
                buttons[i].interactable = false;
            }
        }
    }

    public void CheckResolution() {
        Resolution resolution = Screen.currentResolution;
        int x = resolution.width;
        int y = resolution.height;
        text.text = string.Format("{0} × {1}", x, y);

        Screen.SetResolution(x, y, Screen.fullScreen);
    }
    public void ChangeResolution(int width, int height) {
        Screen.SetResolution(width, height, Screen.fullScreen);
        text.text = string.Format("{0} × {1}", width, height);
    }

    public void ResolutionTo720p() {
        int width = 1280;
        int height = 720;
        ChangeResolution(width, height);
    }
    
    public void ResolutionTo768p() {
        int width = 1366;
        int height = 768;
        ChangeResolution(width, height);
    }
    
    public void ResolutionTo900p() {
        int width = 1600;
        int height = 900;
        ChangeResolution(width, height);
    }
    
    public void ResolutionTo1080p() {
        int width = 1920;
        int height = 1080;
        ChangeResolution(width, height);
    }
    
    public void ResolutionTo1440p() {
        int width = 2560;
        int height = 1440;
        ChangeResolution(width, height);
    }
    
    public void ResolutionTo2160p() {
        int width = 3840;
        int height = 2160;
        ChangeResolution(width, height);
    }
}
