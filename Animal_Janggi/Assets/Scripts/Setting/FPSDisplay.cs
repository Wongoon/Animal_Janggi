using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public bool showFPS;
    public Text fpsText;
    public Text text;

    private float deltaTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!showFPS) {
            fpsText.enabled = false;
            return;
        }

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("{0:0.} FPS", fps);
        fpsText.enabled = true;
    }

    public void ToggleFPSDisplay() {
        showFPS = !showFPS;
        ChangeText();
    }

    public void ChangeText() {
        if (showFPS) {
            text.text = "ON";
        }
        else {
            text.text = "OFF";
        }
    }
}
