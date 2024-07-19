using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float sensitivity = 100f;
    [SerializeField] Transform playerBody;
    private CameraEffects cameraEffects;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraEffects = GetComponent<CameraEffects>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, cameraEffects.cameraTilt); // Left and Right
        playerBody.Rotate(Vector3.up * mouseX); // Up and Down
    }
}
