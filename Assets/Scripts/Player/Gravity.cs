using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController player;
    private SpeedManipulation speedManipulation;
    private Stamina stamina;

    [SerializeField] float gravity = -35;
    [SerializeField] float jumpHeight = 1.5f;

    public bool isGrounded;

    [SerializeField] Transform groundCheck;
    [SerializeField] float maxGroundDistance = .4f;
    [SerializeField] LayerMask groundMask;

    public bool roofIsAbove = false;
    [SerializeField] LayerMask roofMask;
    [SerializeField] Transform roofCheck;


    private Vector3 velocity;

    void Start()
    {
        player = GetComponent<CharacterController>();
        speedManipulation = GetComponent<SpeedManipulation>();
        stamina = GetComponent<Stamina>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, maxGroundDistance, groundMask);
        roofIsAbove = Physics.CheckSphere(roofCheck.position, maxGroundDistance, roofMask);

        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);


        if (isGrounded && stamina.stamina > 10.0f && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (speedManipulation.isCrouching) player.height = 1; 
        else
        {
            if (player.height < 2)
            {
                player.height += Time.deltaTime * 5;
            }
        };
        
    }
}
