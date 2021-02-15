using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    // Movement
    private Vector3 moveTo;
    public float speed = 5f;
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
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveTo = new Vector3(Input.GetAxis(PlayerControls.HORIZONTAL), 0f, Input.GetAxis(PlayerControls.VERTICAL));
        moveTo = transform.TransformDirection(moveTo);
        moveTo *= speed * Time.deltaTime;

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
