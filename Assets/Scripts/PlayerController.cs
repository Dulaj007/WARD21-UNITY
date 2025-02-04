using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCam;



    public bool isMoving { get; private set; }
    public bool isCrouching { get; private set; }
    public bool isRunning { get; private set; }
    public bool isWalking { get; private set; }

    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float jumpPower = 0f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 75f;
    public float cameraRotationSmooth = 5f;

    public AudioSource woodFootstepSources;



  

    public float crouchCameraHeight = 1f;
    public float crouchTransitionSpeed = 5f;

    public int ZoomFOV = 35;
    public int initialFOV;
    public float cameraZoomSmooth = 1;

    private bool isFootstepCoroutineRunning = false;
    private bool isZoomed = false;
    public bool canMove = true;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float rotationY = 0;
    private float initialCameraYPos;
    private float targetCameraYPos;
      public float headBobAmount = 0.05f;
    public float headBobSpeed = 10f;
    private float headBobTimer = 0;


    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        initialCameraYPos = playerCam.transform.localPosition.y;
        targetCameraYPos = initialCameraYPos;


    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
        HandleZoom();
        HandleFootsteps();
        HandleCrouch();
        HandleHeadBobbing();

    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            rotationY += Input.GetAxis("Mouse X") * lookSpeed;

            Quaternion targetRotationX = Quaternion.Euler(rotationX, 0, 0);
            Quaternion targetRotationY = Quaternion.Euler(0, rotationY, 0);

            playerCam.transform.localRotation = Quaternion.Slerp(playerCam.transform.localRotation, targetRotationX, Time.deltaTime * cameraRotationSmooth);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationY, Time.deltaTime * cameraRotationSmooth);
        }
    }

    void HandleZoom()
    {
        if (Input.GetButtonDown("Zoom"))
        {
            isZoomed = true;
        }
        if (Input.GetButtonUp("Zoom"))
        {
            isZoomed = false;
        }

        float targetFOV = isZoomed ? ZoomFOV : initialFOV;
        playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, targetFOV, Time.deltaTime * cameraZoomSmooth);
    }

    void HandleFootsteps()
    {
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        isCrouching = Input.GetKey(KeyCode.LeftControl);
        isRunning = Input.GetKey(KeyCode.LeftShift) && isMoving;

        if (isMoving && !isCrouching)
        {
            if (!isWalking && !isFootstepCoroutineRunning)
            {
                isWalking = true;
                StartCoroutine(PlayFootstepSounds(1.3f / (isRunning ? runSpeed : walkSpeed)));
            }
        }
        else
        {
            isWalking = false;
        }
    }

    IEnumerator PlayFootstepSounds(float footstepDelay)
    {
        isFootstepCoroutineRunning = true;
        while (isWalking)
        {
            if (woodFootstepSources)
            {
              
              

                woodFootstepSources.pitch = Random.Range(0.9f, 1.1f);
                woodFootstepSources.Play();

                yield return new WaitForSeconds(footstepDelay);
            }
            else
            {
                yield break;
            }
        }
        isFootstepCoroutineRunning = false;
    }



    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            targetCameraYPos = initialCameraYPos - crouchCameraHeight;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            targetCameraYPos = initialCameraYPos;
        }

        Vector3 currentCamPos = playerCam.transform.localPosition;
        currentCamPos.y = Mathf.Lerp(currentCamPos.y, targetCameraYPos, Time.deltaTime * crouchTransitionSpeed);
        playerCam.transform.localPosition = currentCamPos;
    }
       void HandleHeadBobbing()
    {
        if (isMoving && characterController.isGrounded && !isCrouching)
        {
            headBobTimer += Time.deltaTime * headBobSpeed;
            float bobOffset = Mathf.Sin(headBobTimer) * headBobAmount;
            playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x, initialCameraYPos + bobOffset, playerCam.transform.localPosition.z);
        }
        else
        {
            headBobTimer = 0;
            playerCam.transform.localPosition = new Vector3(playerCam.transform.localPosition.x, Mathf.Lerp(playerCam.transform.localPosition.y, initialCameraYPos, Time.deltaTime * 10f), playerCam.transform.localPosition.z);
        }
    }


}
