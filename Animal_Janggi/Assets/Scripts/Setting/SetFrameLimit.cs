using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetFrameLimit : MonoBehaviour
{
    public Text text;

    private void VSyncOff() {
        QualitySettings.vSyncCount = 0;
    }
    public void SetVSync() {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = -1;
        text.text = "V-Sync";
    }

    public void Set60FPS() {
        VSyncOff();
        Application.targetFrameRate = 60;
        text.text = "60 FPS";
    }

    public void Set120FPS() {
        VSyncOff();
        Application.targetFrameRate = 120;
        text.text = "120 FPS";
    }

    public void Set144FPS() {
        VSyncOff();
        Application.targetFrameRate = 144;
        text.text = "144 FPS";
    }

    public void Set240FPS() {
        VSyncOff();
        Application.targetFrameRate = 240;
        text.text = "240 FPS";
    }

    public void Set360FPS() {
        VSyncOff();
        Application.targetFrameRate = 360;
        text.text = "360 FPS";
    }

    public void SetFPSUnlimit() {
        VSyncOff();
        Application.targetFrameRate = 1000;
        text.text = "Unlimit";
    }
}
