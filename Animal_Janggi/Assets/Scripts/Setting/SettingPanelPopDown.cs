using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingPanelPopDown : MonoBehaviour
{
    public GameObject panel;

    public void OnPointerClick(PointerEventData eventData) {
        HideImage();
    }

    public void HideImage() {
        panel.SetActive(false);
    }
}
