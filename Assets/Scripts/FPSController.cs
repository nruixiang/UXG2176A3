using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]  
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    private bool doubleJump;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = 0; // Initialize with default value
        if (canMove)
        {
            if (isRunning)
            {
                curSpeedX = runSpeed * Input.GetAxis("Vertical");
            }
            else
            {
                curSpeedX = walkSpeed * Input.GetAxis("Vertical");
            }
        }
        else
        {
            curSpeedX = 0; // This line is technically redundant since curSpeedX is already initialized to 0
        }

        float curSpeedY = 0; // Initialize with default value
        if (canMove)
        {
            if (isRunning)
            {
                curSpeedY = runSpeed * Input.GetAxis("Horizontal");
            }
            else
            {
                curSpeedY = walkSpeed * Input.GetAxis("Horizontal");
            }
        }
        else
        {
            curSpeedY = 0; // Again, redundant since curSpeedY starts at 0
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY; //<- the fix, moved it  here
        #endregion

        #region Jumping

        if(characterController.isGrounded && !Input.GetButtonDown("Jump"))
        {
            doubleJump = false;
            Debug.Log("Jump Resetted");
        }

        if(Input.GetButtonDown("Jump") && canMove)
        {
            if(characterController.isGrounded || doubleJump)
            {
                Debug.Log("JUMPING TWICE");
                moveDirection.y = jumpPower;
                doubleJump = !doubleJump;
            }
        }

        //If in air let gravity pull the characters down
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

  

        #endregion

        #region Handles Rotation

        characterController.Move(moveDirection * Time.deltaTime);

        if(canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;  
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);  //Prevents camera from rotating too far
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); //Rotate the cam
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);  //Rotates the player left and right
        
        }
        #endregion

    }

    
}
