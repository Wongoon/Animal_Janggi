 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static bool canvasRender = true;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(canvasRender);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvasRender = !canvasRender;

            canvas.SetActive(canvasRender);
            Cursor.lockState = canvasRender ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = canvasRender;
        }
    }
}
