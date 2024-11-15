using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;


[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera PLAYERCAMERA;

    //Player Movement 
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    //Camera stats
    public float lookSpeed = 2f;
    public float lookXLimit = 50f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    private bool doubleJump;

    //For Crouching
    public GameObject playerBody;
    public GameObject pivotParent;

    private bool isCrouching = false;

    //For  dash
    public float dashSpeed = 20f;     // How fast the dash is
    public float dashDuration = 0.2f; // How long the dash lasts
    private Rigidbody rb;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private Transform camTransform;

    //For Sound
    public bool isRunningPlaying = false;
    public bool isWalkingPlaying = false;

    // Speed detection
    float walkSpeedMinimum = 0f; // Adjust based on your walkSpeed
    float runSpeedMinimum = 11.0f;  // Adjust based on your runSpeed


    // Start is called before the first frame update
    void Start()
    {
        PLAYERCAMERA = GameObject.Find("PlayerCam").GetComponent<Camera>();

        characterController = GetComponent<CharacterController>();

        //Lock Cursor for First Person View
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor
        Cursor.visible = false;

        //Pivoting for the Crouch
        ChangePivot(playerBody.transform, new Vector3(0, 0, 0));
        pivotParent = GameObject.Find("PivotParent");

        //For Dash
        rb = GetComponent<Rigidbody>();
        camTransform = PLAYERCAMERA.transform;

    }

    // Update is called once per frame
    void Update()
    {

        #region Movement
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);


        // Update movement and check speed
        if (canMove)
        {
            // Calculate current speed from movement input
            float curSpeedX = 0f;
            float curSpeedY = 0f;

            if (Input.GetAxis("Vertical") != 0)
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

            if (Input.GetAxis("Horizontal") != 0)
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

            // Apply movement
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            moveDirection.y = movementDirectionY;

            // Check the actual speed of the CharacterController
            float currentSpeed = characterController.velocity.magnitude;

            //Debug.Log("Current Speed: " + currentSpeed);
            //Debug.Log("Walk Threshold: " + walkSpeedMinimum);
            //Debug.Log("Run Threshold: " + runSpeedMinimum);


            // Determine and play the appropriate sound
            if (currentSpeed > runSpeedMinimum && isRunning)
            {
                // Player is running
                if (!isRunningPlaying)
                {
                    Debug.Log("RUN START");

                    SoundManager.instance.ChangeLoopSound("run");
                    isRunningPlaying = true;
                    isWalkingPlaying = false;
                }
            }
            else if (currentSpeed > walkSpeedMinimum && !isRunning)
            {
                // Player is walking
                if (!isWalkingPlaying)
                {
                    Debug.Log("WALK START");
                    SoundManager.instance.ChangeLoopSound("walk");
                    isRunningPlaying = false;
                    isWalkingPlaying = true;
                }
            }
            else
            {
                // Player is stopped
                if (isRunningPlaying || isWalkingPlaying)
                {
                    Debug.Log("STOPPED");
                    SoundManager.instance.StopLoopSound();
                    isRunningPlaying = false;
                    isWalkingPlaying = false;
                }
            }
        }

        ////Vertical
        //float curSpeedX = 0; // Initialize with default value


        //if (canMove && Input.GetAxis("Vertical") != 0)
        //{
        //    //Debug.Log(isRunning);
        //    if (isRunning)
        //    {
        //        curSpeedX = runSpeed * Input.GetAxis("Vertical");

        //        if (!isRunningPlaying)
        //        {

        //            //Debug.Log("RUNNING");
        //            isRunningPlaying = true;
        //            isWalkingPlaying = false;

        //        }

        //    }
        //    else
        //    {
        //        curSpeedX = walkSpeed * Input.GetAxis("Vertical");

        //        if (!isWalkingPlaying)
        //        {

        //            //Debug.Log("WALKING");

        //            isRunningPlaying = false;
        //            isWalkingPlaying = true;

        //        }


        //    }
        //}

        ////Horizontal
        //float curSpeedY = 0; // Initialize with default value
        //if (canMove && Input.GetAxis("Horizontal") != 0)
        //{
        //    if (isRunning)
        //    {
        //        curSpeedY = runSpeed * Input.GetAxis("Horizontal");

        //        if (!isRunningPlaying)
        //        {


        //            //Debug.Log("RUNNING");
        //            isRunningPlaying = true;
        //            isWalkingPlaying = false;
        //        }
        //    }
        //    else
        //    {
        //        curSpeedY = walkSpeed * Input.GetAxis("Horizontal");

        //        if (!isWalkingPlaying)
        //        {


        //            //Debug.Log("WALKING");

        //            isRunningPlaying = false;
        //            isWalkingPlaying = true;
        //        }
        //    }
        //}

        //if (characterController.velocity.magnitude == 0)
        //{
        //    Debug.Log("STOPPED");
        //    isRunningPlaying = false;
        //    isWalkingPlaying = false;
        //}




        //float movementDirectionY = moveDirection.y;
        //moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        //moveDirection.y = movementDirectionY; //<- the fix, moved it  here
        #endregion

        #region Jumping

        if (characterController.isGrounded && !Input.GetButtonDown("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && canMove)
        {
            if (characterController.isGrounded || doubleJump)
            {
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

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);  //Prevents camera from rotating too far
            PLAYERCAMERA.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); //Rotate the cam
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);  //Rotates the player left and right

        }
        #endregion

        #region Crouch

        // Reference to the GameObject's Transform
        Transform myTransform = pivotParent.transform;

        // Get the current localScale
        Vector3 scale = myTransform.localScale;

        if (Input.GetButtonDown("Crouch") && characterController.isGrounded && isCrouching == false)
        {
            isCrouching = true;

            // Modify the Y-axis value
            scale.y = 0.6f; // Set the Y-axis scale to 2 (change this to your desired value)

            // Apply the new scale back to the Transform
            myTransform.localScale = scale;

        }

        else if (Input.GetButtonDown("Crouch") && isCrouching)
        {
            isCrouching = false;

            // Modify the Y-axis value
            scale.y = 1.0f; // Set the Y-axis scale to 2 (change this to your desired value)

            // Apply the new scale back to the Transform
            myTransform.localScale = scale;
        }

        #endregion

        #region Dash

        if (Input.GetMouseButton(1) && !isDashing)
        {
            StartDash();
        }

        #endregion

    }
    void ChangePivot(Transform target, Vector3 newPivot)
    {
        // Create a new empty parent GameObject
        GameObject pivotParent = new GameObject("PivotParent");
        pivotParent.transform.position = target.position + newPivot;

        // Reparent the target GameObject
        target.SetParent(pivotParent.transform, true);
    }

    void StartDash()
    {
        isDashing = true;
        dashDirection = camTransform.forward; // Dash forward
        dashDirection.y = 0; //Keep the dash horizontal
        dashDirection.Normalize(); // Normalize the direction vector for consistent movement
        StartCoroutine(DashMovement());
    }

    private IEnumerator DashMovement()
    {
        float startTime = Time.time;

        // Perform the dash for the specified duration
        while (Time.time < startTime + dashDuration)
        {
            // Move the character in the dash direction
            characterController.Move(dashDirection * dashSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }

        StartCoroutine(ResetDashAfterDelay(10));

    }

    IEnumerator ResetDashAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Stop dashing
        isDashing = false;
    }

}
