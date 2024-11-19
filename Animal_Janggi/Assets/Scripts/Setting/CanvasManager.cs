 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] bool canvasRender;
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
            Time.timeScale = canvasRender ? 1.0f : 0.0f;
        }
    }
}
