﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // GameObjects references
    private CharacterController characterController;
    public GameObject LookRoot;
    public InfoBar StaminaInfoBar;
    // Movement
    private Vector3 moveTo;
    public float sprintSpeed = 8f;
    public float normalSpeed = 4f;
    public float crouchSpeed = 1.5f;
    [SerializeField]
    private float currentSpeed;
    // Stamina
    public float stamina = 100f;
    private float maxStamina;
    public float staminaLostPerSecond = 10f;
    public float staminaRegeneratedPerSecond = 5f;
    // States
    private bool isCrouching = false;
    private bool isSprinting = false;
    // Height
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    // Jump
    public float jumpForce = 10f;
    // Gravity
    public float gravity = -20f;
    // TODO: Clamp verticalVelocity
    private float verticalVelocity;
    // Sounds and Audio
        // Footsteps
    private PlayerFootsteps playerFootsteps;
    public float minTimeBetweenFootsepsSprinting = 0.25f;
    public float minStepVolumeSprinting = 0.8f;
    public float maxStepVolumeSprinting = 1f;
    public float minTimeBetweenFootsepsWalking = 0.4f;
    public float minStepVolumeWalking = 0.6f;
    public float maxStepVolumeWalking = 0.4f;
    public float minTimeBetweenFootsepsCrouching = 0.5f;
    public float minStepVolumeCrouching = 0.1f;
    public float maxStepVolumeCrouching = 0.2f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set default speed
        currentSpeed = normalSpeed;
        // Set default sounds
        setWalkingSounds();
        // Set default stamina
        maxStamina = stamina;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        MovePlayer();
        ManageStamina();
    }

    private void Sprint()
    {
        if (!isCrouching)
        {
            // If it has stamina
            if (stamina > 0)
            {
                // Start sprinting
                if (Input.GetKey(PlayerControlTags.Sprint))
                {
                    isSprinting = true;
                    currentSpeed = sprintSpeed;
                    playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsSprinting, minStepVolumeSprinting, maxStepVolumeSprinting);
                    return;
                }
                // Stop sprinting
                if (Input.GetKeyUp(PlayerControlTags.Sprint))
                {
                    isSprinting = false;
                    ChangeValuesToWalk();
                    return;
                }
                return;
            }
            // TODO: CHECK STAMINA WHEN GETS TO ZERO
            // If it hasn't stamina
            isSprinting = false;
            ChangeValuesToWalk();
        }
        
    }

    private void ChangeValuesToWalk()
    {
        currentSpeed = normalSpeed;
        setWalkingSounds();
    }

    private void ManageStamina()
    {
        float staminaModification = (isSprinting && characterController.velocity.sqrMagnitude > 0) 
            ? -staminaLostPerSecond 
            : staminaRegeneratedPerSecond;
        staminaModification *= Time.deltaTime;

        updateStamina(staminaModification);
    }

    private void updateStamina(float newStamina)
    {
        stamina = UnityEngine.Mathf.Clamp(stamina + newStamina, 0, maxStamina);
        StaminaInfoBar.updateData(stamina, maxStamina);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(PlayerControlTags.Crouch))
        {
            // To stand up
            if (isCrouching)
            {
                LookRoot.transform.localPosition = new Vector3(0f, standHeight);
                isCrouching = false;
                ChangeValuesToWalk();
                return;
            }

            // To crouch
            LookRoot.transform.localPosition = new Vector3(0f, crouchHeight);
            currentSpeed = crouchSpeed;
            isCrouching = true;
            playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsCrouching, minStepVolumeCrouching, maxStepVolumeCrouching);
        }
    }

    private void MovePlayer()
    {
        moveTo = new Vector3(Input.GetAxis(PlayerControlTags.HORIZONTAL), 0f, Input.GetAxis(PlayerControlTags.VERTICAL));
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
        if (characterController.isGrounded && Input.GetKeyDown(PlayerControlTags.JUMP))
        {
            verticalVelocity = jumpForce;
        }
    }

    private void setWalkingSounds()
    {
        playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsWalking, minStepVolumeWalking, maxStepVolumeWalking);
    }
}
