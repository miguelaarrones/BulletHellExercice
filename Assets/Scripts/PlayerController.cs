using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 12f;
    [SerializeField] float jumpPower = 7f;
    [SerializeField] float gravity = 10f;
    
    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;

    [SerializeField] bool canMove = true;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;

    private float lastTimeRunning = 0f;
    private float runTimeLimit = 5f;
    private float runCooldownMax = 3f;
    private float runCooldown;
    private bool canRun = true;
    private bool canDoubleJump = false;
    private bool canJump = true;

    private CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle movement:
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        //// Running;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = 0f;
        float curSpeedY = 0f;
        if (canMove)
        {
            if (isRunning && canRun)
            {
                curSpeedX = runSpeed * Input.GetAxis("Vertical");
                curSpeedY = runSpeed * Input.GetAxis("Horizontal");
                
                lastTimeRunning += Time.deltaTime;
                if (lastTimeRunning >= runTimeLimit)
                {
                    canRun = false;
                }
            } 
            else
            {
                curSpeedX = walkSpeed * Input.GetAxis("Vertical");
                curSpeedY = walkSpeed * Input.GetAxis("Horizontal");

                runCooldown += Time.deltaTime;
                if (runCooldown >= runCooldownMax)
                {
                    lastTimeRunning = 0f;
                    canRun = true;
                    runCooldown = 0f;
                }
            }
        }

        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Handle jumping:

        if (Input.GetButtonDown("Jump") && canMove)
        {
            if (canJump && characterController.isGrounded)
            {
                moveDirection.y = jumpPower;
                canDoubleJump = true;
            } 
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection.y = moveDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Handle rotation:
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
