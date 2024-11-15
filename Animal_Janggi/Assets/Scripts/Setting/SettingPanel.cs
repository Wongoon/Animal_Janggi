using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingPanel : MonoBehaviour
{
    public GameObject panel;

    public void PanelUp() {
        panel.SetActive(true);
    }
    
    public void PanelDown() {
        panel.SetActive(false);
    }
}
