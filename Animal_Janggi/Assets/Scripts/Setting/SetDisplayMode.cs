using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SetDisplayMode : MonoBehaviour
{
    public Text text;

    void Start() {
        CheckScreenMode();
    }

    public void CheckScreenMode()
    {
        switch (Screen.fullScreenMode) {
            case FullScreenMode.ExclusiveFullScreen:
                text.text = "전체 화면";
                break;
            case FullScreenMode.FullScreenWindow:
                text.text = "전체 창 모드";
                break;
            case FullScreenMode.Windowed:
                text.text = "창 모드";
                break;
            default:
                text.text = "Error";
                break;
        }
    }

    public void SetFullScreen() {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        text.text = "전체 화면";
    }

    public void SetBorderless() {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        text.text = "전체 창 모드";
    }

    public void SetWindowed() {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        text.text = "창 모드";
    }
}
