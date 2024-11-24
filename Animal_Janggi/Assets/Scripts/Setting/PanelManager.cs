using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingPanel;
    public GameObject panelAll;
    public bool panelActive;

    void Awake() {
        mainPanel.SetActive(panelActive);
    }

    public void PanelUp(String panel) {
        if (panel.Equals("main")) {
            mainPanel.SetActive(true);
        }
        else if (panel.Equals("setting")) {
            settingPanel.SetActive(true);
        }
        else if (panel.Equals("all")) {
            panelAll.SetActive(true);
        }
        else {
            Debug.Log("Up Error");
        }
    }

    public void PanelDown(String panel) {
        if (panel.Equals("main")) {
            mainPanel.SetActive(false);
        }
        else if (panel.Equals("setting")) {
            settingPanel.SetActive(false);
        }
        else if (panel.Equals("all")) {
            panelAll.SetActive(false);
        }
        else {
            Debug.Log("Down Error");
        }
    }
    
    public void ExitGame() {
        Application.Quit();
    }
}
