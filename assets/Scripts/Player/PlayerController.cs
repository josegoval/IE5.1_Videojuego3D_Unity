using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GameObjects references
    private CharacterController characterController;
    public GameObject LookRoot;
    // Movement
    private Vector3 moveTo;
    public float sprintSpeed = 8f;
    public float normalSpeed = 4f;
    public float crouchSpeed = 1.5f;
    [SerializeField]
    private float currentSpeed;
    // States
    private bool isCrouching = false;
    // Height
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    // Jump
    public float jumpForce = 10f;
    // Gravity
    public float gravity = -20f;
    // TODO: Clamp verticalVelocity
    private float verticalVelocity;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        // Set default speed
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        MovePlayer();
    }

    private void Sprint()
    {
        if (!isCrouching)
        {
            // Start sprinting
            if (Input.GetKey(PlayerControls.Sprint))
            {
                currentSpeed = sprintSpeed;
                return;
            }
            // Stop sprinting
            if (Input.GetKeyUp(PlayerControls.Sprint))
            {
                currentSpeed = normalSpeed;
                return;
            }
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(PlayerControls.Crouch))
        {
            // To stand up
            if (isCrouching)
            {
                LookRoot.transform.localPosition = new Vector3(0f, standHeight);
                currentSpeed = normalSpeed;
                isCrouching = false;
                return;
            }

            // To crouch
            LookRoot.transform.localPosition = new Vector3(0f, crouchHeight);
            currentSpeed = crouchSpeed;
            isCrouching = true;
        }
    }

    private void MovePlayer()
    {
        moveTo = new Vector3(Input.GetAxis(PlayerControls.HORIZONTAL), 0f, Input.GetAxis(PlayerControls.VERTICAL));
        moveTo = transform.TransformDirection(moveTo);
        moveTo *= currentSpeed * Time.deltaTime;

        JumpBehaviour();

        ApplyGravity();

        characterController.Move(moveTo);
    }

    private void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
        moveTo.y = verticalVelocity * Time.deltaTime;
    }

    private void JumpBehaviour()
    {
        if (characterController.isGrounded && Input.GetKeyDown(PlayerControls.JUMP))
        {
            verticalVelocity = jumpForce;
        }
    }
}
