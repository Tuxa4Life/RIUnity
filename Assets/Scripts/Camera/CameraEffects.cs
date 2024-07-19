using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialPosition;

    private SpeedManipulation speedManipulation;
    private Stamina stamina;

    [SerializeField] float tiltSensitivity = .25f;
    public float cameraTilt;

    [SerializeField] float periodConstant = 1f;
    private float actualPeriod;

    [SerializeField] float FOVConstant = 60.0f;
    private float actualFOV = 61.0f;


    private void Start()
    {
        mainCamera = GetComponent<Camera>();    
        speedManipulation = GetComponentInParent<SpeedManipulation>();
        stamina = GetComponentInParent<Stamina>();

        initialPosition = transform.localPosition;
    }

    void Update()
    {
        actualPeriod = periodConstant;
        if (speedManipulation.isRunning) actualPeriod /= 2;
        if (speedManipulation.isCrouching) actualPeriod *= 2;


        if (speedManipulation.isRunning && !speedManipulation.isCrouching)
        {
            if (actualFOV < FOVConstant + 8f) actualFOV += Time.deltaTime * 75f;
        } 
        else actualFOV = Mathf.Lerp(actualFOV, FOVConstant, Time.deltaTime * 20);


        // Creating Sine wave
        float cycle = Time.time / actualPeriod;
        float rawSinWave = Mathf.Sin(cycle * Mathf.PI * 2);

        // Adjusting tilt
        cameraTilt = rawSinWave * tiltSensitivity;

        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        if (!isMoving || stamina.stamina < 5.0f) cameraTilt = 0f;

        mainCamera.fieldOfView = actualFOV;
    }
}
