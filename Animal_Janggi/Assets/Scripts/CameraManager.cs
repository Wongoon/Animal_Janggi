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

    void Update()
    {
        MouseRotation();
    }

    void MouseRotation()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * turnSpeed * Time.fixedDeltaTime * Time.timeScale;
        float mouseY = Input.GetAxisRaw("Mouse Y") * turnSpeed * Time.fixedDeltaTime * Time.timeScale;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    public void CameraRotation() {
        StartCoroutine(AdjustCamera());
    }

    IEnumerator AdjustCamera() {
        Quaternion secondRotation = Quaternion.Euler(0, AnimalJanggi._instance.GetTeam() == "Red" ? 0 : 180, 0);

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

        obj.localRotation = targetRotation;
    }
}
