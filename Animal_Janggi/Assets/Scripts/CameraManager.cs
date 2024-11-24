using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    private float xRotation;
    private float yRotation;
    
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        MouseRotation();
    }

    void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime * Time.timeScale;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime * Time.timeScale;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 0, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
