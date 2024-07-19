using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class Stamina : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI staminaBar;
    private SpeedManipulation speedManipulation;
    private Gravity gravity;

    public float stamina = 100f;

    private void Start()
    {
        speedManipulation = GetComponent<SpeedManipulation>();
        gravity = GetComponent<Gravity>();
    }

    void Update()
    {
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        staminaBar.text = "Stamina: " + Mathf.Floor(stamina).ToString() + "%";

        if (speedManipulation.isRunning) stamina -= Time.deltaTime * 7f;
        else if (speedManipulation.isCrouching && !isMoving) stamina += Time.deltaTime * 5f;
        else stamina += Time.deltaTime * 3f;

        if (gravity.isGrounded && stamina > 10.0f && Input.GetButtonDown("Jump")) stamina -= 10.0f;

        if (stamina < 0.0f) stamina = 0.0f;
        if (stamina > 100.0f) stamina = 100.0f;
    }
}
