using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaning : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialPosition;
    public bool leaningRight = false;
    public bool leaningLeft = false;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        initialPosition = transform.localPosition;
        Debug.Log(initialPosition);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            leaningRight = true;
            leaningLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            leaningLeft = true;
            leaningRight = false;
        }

        if (Input.GetKeyUp(KeyCode.E)) leaningRight = false;
        if (Input.GetKeyUp(KeyCode.Q)) leaningLeft = false;

        // Leaning
        Vector3 offset = Vector3.zero;
        float moveSpeed = Time.deltaTime * 2;
        if (leaningRight)
        {
            if (mainCamera.transform.localPosition.x < 0.4f) offset = new Vector3(moveSpeed, 0f, 0f);
        }
        else if (leaningLeft)
        {
            if (mainCamera.transform.localPosition.x > -0.4f) offset = new Vector3(-moveSpeed, 0f, 0f);
        }
        else
        {
            if (mainCamera.transform.localPosition.x != 0) offset = new Vector3(-Mathf.Sign(mainCamera.transform.localPosition.x) * moveSpeed, 0f, 0f);
        }

        mainCamera.transform.localPosition += offset;
        float x = mainCamera.transform.localPosition.x;
        if ((x > 0 && x < 0.01f) || (x < 0 && x > -0.01f)) // This is to fix screen shaking bug after leaning
        {
            mainCamera.transform.localPosition = initialPosition;
        }
    }
}