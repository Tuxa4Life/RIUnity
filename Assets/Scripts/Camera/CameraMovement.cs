using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform playerBody;
    private CameraEffects cameraEffects;
    private Leaning leaning;

    private float xRotation = 0f;

    float leanTilt = 0;
    float targetTilt = 0;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraEffects = GetComponent<CameraEffects>();
        leaning = GetComponent<Leaning>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // leaning offset
        if (leaning.leaningRight)
            targetTilt = -15f;
        else if (leaning.leaningLeft)
            targetTilt = 15f;
        else
            targetTilt = 0f;

        leanTilt = Mathf.Lerp(leanTilt, targetTilt, Time.deltaTime * 5);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, cameraEffects.cameraTilt + leanTilt); // Left and Right
        playerBody.Rotate(Vector3.up * mouseX); // Up and Down
    }
}
