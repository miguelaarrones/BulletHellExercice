using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public event EventHandler<OnSprintAmountChangedArgs> OnSprintAmountChanged;
    public class OnSprintAmountChangedArgs : EventArgs
    {
        public float sprintAmountNormalized;
    };

    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float runSpeed = 12f;
    [SerializeField] float jumpPower = 7f;
    [SerializeField] float gravity = 10f;

    [SerializeField] private float sprintAmountMax = 5f;
    private float sprintAmount;

    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;

    private bool canMove = true;
    private bool canRun = true;
    private bool canJump = true;
    private bool canDoubleJump = false;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0f;
    private float moveDirectionY;

    private CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sprintAmount = sprintAmountMax;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleMovement()
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

                if (sprintAmount <= 0)
                {
                    canRun = false;
                } else
                {
                    sprintAmount -= Time.deltaTime;
                    sprintAmount = Mathf.Clamp(sprintAmount, 0f, sprintAmountMax);
                    OnSprintAmountChanged?.Invoke(this, new OnSprintAmountChangedArgs
                    {
                        sprintAmountNormalized = (float)sprintAmount / sprintAmountMax,
                    });
                }
            }
            else
            {
                curSpeedX = walkSpeed * Input.GetAxis("Vertical");
                curSpeedY = walkSpeed * Input.GetAxis("Horizontal");

                
                if (sprintAmount == sprintAmountMax)
                {
                    canRun = true;
                } else
                {
                    sprintAmount += Time.deltaTime;
                    sprintAmount = Mathf.Clamp(sprintAmount, 0f, sprintAmountMax);
                    OnSprintAmountChanged?.Invoke(this, new OnSprintAmountChangedArgs
                    {
                        sprintAmountNormalized = (float)sprintAmount / sprintAmountMax,
                    });
                }
            }
        }

        moveDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
    }

    private void HandleRotation()
    {
        // Handle rotation:
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private void HandleJumping()
    {
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
    }
}
