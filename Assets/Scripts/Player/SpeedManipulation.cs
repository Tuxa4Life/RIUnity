using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManipulation : MonoBehaviour
{
    private CharacterController player;
    private Gravity gravity;
    private Stamina stamina;

    [SerializeField] float speedConstant = 3.0f;
    private float actualSpeed;

    private bool isRunningOn = false;
    public bool isRunning;
    [SerializeField] float speedMultiplier = 2.0f;

    private bool isCrouchOn = false;
    public bool isCrouching;

    void Start()
    {
        player = GetComponent<CharacterController>();
        gravity = GetComponent<Gravity>();
        stamina = GetComponent<Stamina>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * x + transform.forward * z;

        bool isMoving = z != 0 || x != 0;

        // Toggle running 
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRunningOn = true;
        if (Input.GetKeyUp(KeyCode.LeftShift)) isRunningOn = false;

        // Toggle Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl)) isCrouchOn = true;
        if (Input.GetKeyUp(KeyCode.LeftControl)) isCrouchOn = false;

        isCrouching = isCrouchOn || gravity.roofIsAbove;
        isRunning = isRunningOn && isMoving;

        if (isRunning) actualSpeed = speedConstant * speedMultiplier;
        else actualSpeed = speedConstant;

        if (isCrouching && !isRunning) actualSpeed = speedConstant / speedMultiplier;
        if (isCrouching && isRunning) actualSpeed = speedConstant;

        if (stamina.stamina < 5.0f) actualSpeed = 1f; // Add to Srialization if slow

        player.Move(actualSpeed * Time.deltaTime * movement);
    }
}
