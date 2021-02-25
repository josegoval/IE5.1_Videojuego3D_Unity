using System;
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
    // Mobile buttons
    private bool isPressingJumpButton = false;
    private bool isPressingSprintButton = false;
    private bool isPressingCrouchButton = false;
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
            // Check inputs
            CheckSprintAction();
            if (isSprinting)
            {
                // If it has stamina
                if (stamina > 0)
                {
                    ChangeValuesToSprint();
                    return;
                }
            }
            // If it hasn't stamina or stop sprinting
            ChangeValuesToWalk();
            return;
        }
        
    }

    private void ChangeValuesToWalk()
    {
        currentSpeed = normalSpeed;
        setWalkingSounds();
    }

    private void ChangeValuesToSprint()
    {
        currentSpeed = sprintSpeed;
        playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsSprinting, minStepVolumeSprinting, maxStepVolumeSprinting);
    }

    private void ManageStamina()
    {
        bool isMoving = characterController.velocity.sqrMagnitude > 0;
        // if it's not moving regenerate faster
        float staminaModification = !isMoving 
            ? staminaRegeneratedPerSecond * 2 
            // decrease stamina if it moves while sprinting otherwise regenerate
            : isSprinting && !isCrouching
                ? -staminaLostPerSecond
                : staminaRegeneratedPerSecond;
        staminaModification *= Time.deltaTime;

        updateStamina(staminaModification);
    }

    private void updateStamina(float newStamina)
    {
        stamina = Mathf.Clamp(stamina + newStamina, 0, maxStamina);
        StaminaInfoBar.updateData(Mathf.Round(stamina), maxStamina);
    }

    private void Crouch()
    {
        CheckCrouchAction();
        // To crouch
        if (isCrouching)
        {
            //isSprinting = false;
            ChangeValuesToCrouch();
            return;
        }
        // To stand up
        ChangeValuesToStopCrouching();
        if (!isSprinting)
        {
            ChangeValuesToWalk();
            return;
        }
    }

    private void ChangeValuesToCrouch()
    {
        LookRoot.transform.localPosition = new Vector3(0f, crouchHeight);
        currentSpeed = crouchSpeed;
        playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsCrouching, minStepVolumeCrouching, maxStepVolumeCrouching);
    }

    private void ChangeValuesToStopCrouching()
    {
        LookRoot.transform.localPosition = new Vector3(0f, standHeight);
    }

    private void MovePlayer()
    {
        moveTo = GetMovementVector();
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
        if (characterController.isGrounded && IsPressingJumpAction())
        {
            verticalVelocity = jumpForce;
            ChangeIsPressingJumpButton(false);
        }
    }

    private void setWalkingSounds()
    {
        playerFootsteps.ChangeAudioValues(minTimeBetweenFootsepsWalking, minStepVolumeWalking, maxStepVolumeWalking);
    }

    // Input checks
    protected virtual Vector3 GetMovementVector()
    {
        return new Vector3(Input.GetAxis(PlayerControlTags.HORIZONTAL), 0f, Input.GetAxis(PlayerControlTags.VERTICAL));
    }

    protected virtual bool IsPressingJumpAction()
    {
        return Input.GetKeyDown(PlayerControlTags.JUMP)|| isPressingJumpButton;
    }

    protected virtual void CheckSprintAction()
    {
        if (Input.GetKey(PlayerControlTags.Sprint) || isPressingSprintButton)
        {
            isSprinting = true;
            return;
        }

        if (Input.GetKeyUp(PlayerControlTags.Sprint) || !isPressingSprintButton)
        {
            isSprinting = false;
            return;
        }
    }
    
    protected virtual void CheckCrouchAction()
    {
        if (Input.GetKeyDown(PlayerControlTags.Crouch) || isPressingCrouchButton)
        {
            isCrouching = !isCrouching;
            return;
        }
    }

    // Buttons
    // Change pressing buttons
    public void ChangeIsPressingJumpButton(bool value)
    {
        isPressingJumpButton = value;
    }
    public void ChangeIsPressingSprintButton(bool value)
    {
        isPressingSprintButton = value;
    }
}
