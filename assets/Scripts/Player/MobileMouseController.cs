using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMouseController : MouseController
{
    [Header("Mobile Setup")]
    public Joystick rotationJoystick;

    protected override void MouseRotation()
    {
        // Get mouse inputs 
        float lookHorizontal = rotationJoystick.Horizontal;
        float lookUp = rotationJoystick.Vertical;
        // Invert controls if necessary
        if (!invertControls)
        {
            lookUp *= -1f;
        }

        // Add the sensibility and add id to the vector
        currentRotation.x += lookUp * mouseSensibility;
        currentRotation.y += lookHorizontal * mouseSensibility;
        // Clamp the values
        currentRotation.x = Mathf.Clamp(currentRotation.x, minLookUp, maxLookUp);

        // Add the rotations
        lookRootComponent.transform.localRotation = Quaternion.Euler(currentRotation.x, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, currentRotation.y, 0f);
    }
}
