using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float turnSpeed;
    // Update is called once per frame
    void Update()
    {
        
    }

    void MouseRotation()
    {
        float yRotation = Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime * Time.timeScale;
        float xRotation = Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime * Time.timeScale;
    }
}
