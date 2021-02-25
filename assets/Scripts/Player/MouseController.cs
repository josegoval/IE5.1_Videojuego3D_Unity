using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Actor References
    [SerializeField]
    protected GameObject lookRootComponent;
    // Control customization
    public float mouseSensibility = 3f;
    public bool invertControls = false;
    protected Vector3 currentRotation;
    // Clamp Values
    [SerializeField]
    protected float maxLookUp = 80f;
    [SerializeField]
    protected float minLookUp = -70f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor at the beginning
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMouse();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            MouseRotation();
        }
    }

    private void ToggleMouse()
    {
        if (Input.GetKeyDown(PlayerControlTags.TOGGLE_MOUSE))
        {
            // Lock mouse
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                return;
            }
            // Unlock mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    protected virtual void MouseRotation()
    {
        // Get mouse inputs 
        float lookHorizontal = Input.GetAxis(PlayerControlTags.MOUSE_X);
        float lookUp = Input.GetAxis(PlayerControlTags.MOUSE_Y);
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
