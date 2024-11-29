using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;
    [SerializeField] float turnSpeed;
    private float xRotation;
    private float yRotation;

    public Transform cameraPosition;
    public float moveSpeed = 1.0f;
    public float rotationSpeed = 1.0f;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

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

    public void CameraRotation() {
        turnSpeed = 0;
        StartCoroutine(AdjustCamera());

        turnSpeed = 1000;
    }

    IEnumerator AdjustCamera() {
        Quaternion firstRotation = Quaternion.Euler(0, 0, 0);
        Quaternion secondRotation = Quaternion.Euler(0, 180, 0);

        yield return StartCoroutine(TargetRotation(transform, firstRotation));

        yield return StartCoroutine(TargetRotation(cameraPosition, secondRotation));
    }

    IEnumerator TargetRotation(Transform obj, Quaternion targetRotation) {
        Quaternion startRotation = obj.rotation;

        float timeElapsed = 0;
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * rotationSpeed;
            obj.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed);
            yield return null;
        }

        obj.rotation = targetRotation;
    }
}
