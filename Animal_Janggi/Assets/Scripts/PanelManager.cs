using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject panel;

    void Awake() {
        panel.SetActive(false);
    }

    public void PanelUP() {
        panel.SetActive(true);
    }

    public void PanelDown() {
        panel.SetActive(false);
    }
}
