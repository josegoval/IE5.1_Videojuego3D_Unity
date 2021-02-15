using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    // Control customization
    public float mouseSensibility = 3f;
    public bool invertControls = false;
    private Vector3 currentRotation;
    // Clamp Values
    [SerializeField]
    private float maxLookUp = 80f;
    [SerializeField]
    private float minLookUp = -70f;
    // Actor References
    [SerializeField]
    private GameObject lookRootComponent;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor at the beginning
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMouse();

        if (Cursor.lockState == CursorLockMode.Locked);
        {
            MouseRotation();
        }
    }

    private void ToggleMouse()
    {
        if (Input.GetKeyDown(PlayerControls.TOGGLE_MOUSE))
        {
            // Lock mouse
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            // Unlock mouse
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void MouseRotation()
    {
        // Get mouse inputs 
        float lookHorizontal = Input.GetAxis(PlayerControls.MOUSE_X);
        float lookUp = Input.GetAxis(PlayerControls.MOUSE_Y);
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
