using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerController : PlayerController
{
    // Mobile buttons
    [Header("Mobile Setup")]
    public Joystick movementJoystick;
    private bool isPressingJumpButton = false;
    private bool isPressingSprintButton = false;
    private bool isPressingCrouchButton = false;


    // Input checks
    protected override Vector3 GetMovementVector()
    {
        return new Vector3(movementJoystick.Horizontal, 0f, movementJoystick.Vertical);
    }

    protected override bool IsPressingJumpAction()
    {
        return isPressingJumpButton;
    }

    protected override void CheckSprintAction()
    {
        isSprinting = isPressingSprintButton;
    }
    
    protected override void CheckCrouchAction()
    {
        isCrouching = isPressingCrouchButton;
    }

    // Buttons
    // Change pressing buttons
    public override void ChangeIsPressingJumpButton(bool value)
    {
        isPressingJumpButton = value;
    }
    public void ToggleIsPressingSprintButton()
    {
        isPressingSprintButton = !isPressingSprintButton;
    }
    public void ToggleIsPressingCrouchButton()
    {
        isPressingCrouchButton = !isPressingCrouchButton;
    }
}
