using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canHeadbob = true;
    [SerializeField] private bool canFootstep = true;
    [SerializeField] private bool canUseFlashlight = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode flashlightKey = KeyCode.F;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.01f;
    [SerializeField] private float sprintBobSpeed = 20f;
    [SerializeField] private float sprintBobAmount = 0.03f;
    private float defaultYPos = 0;
    private float defaultZPos = 0;
    private float timer;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float sprintStepMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepsAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;
    
    private Camera playerCamera;
    private CharacterController characterController;
    private Light flashlight;

    private Vector3 moveDir;
    private Vector2 curInput;

    private float rotationX = 0;

    private bool canMove;
    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool isUsingFlashlight;


    //Collect soul
    [Header("Interaction")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    [SerializeField] private bool canInteract = true;
    private Interactable currentInteractable;

    
    void Awake(){
        canMove = true;
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
       // flashlight = GameObject.Find("Flashlight").GetComponent<Light>();
        defaultYPos = playerCamera.transform.localPosition.y;
       // defaultZPos = flashlight.transform.localPosition.z;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove){
            //print(canMove);
            HandleMovementInput();
            HandleMouseLook();
            //HandleInputs();
            ApplyFinalMovements();
        }
        if(canHeadbob){
            HandleHeadbob();
        }
        if(canFootstep){
            HandleFootsteps();
        }
        if (canInteract)
        {
            HandleInteractionCheck();
            HandleInteractionInput();
        }
    }

    private void HandleInputs()
    {
       if (Input.GetKeyDown(flashlightKey) && canUseFlashlight)
       {
           isUsingFlashlight = !isUsingFlashlight;
       }
       if (isUsingFlashlight)
       {
           flashlight.intensity = 2;
       }
       else
       {
           flashlight.intensity = 0;
       }
    }

    private void HandleFootsteps(){
        if(!characterController.isGrounded){
            return;
        } 
        if(curInput == Vector2.zero) {
            return;
        }
        footstepTimer -= Time.deltaTime;
        if(footstepTimer <= 0){
            if(Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3)){
                switch(hit.collider.tag){
                    case "Footsteps/WOOD":
                        //Debug.Log("hi");
                    footstepsAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                    break;
                    case "Footsteps/METAL":
                    footstepsAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                    break;
                    case "Footsteps/GRASS":
                    footstepsAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                    break;
                    default://default sound effect, footstep sound that's not any other specific sound
                    break;
                }
            }
            footstepTimer = GetCurrentOffset;
        }
    }

    private void HandleHeadbob(){
        if(!characterController.isGrounded){
            return;
        }
        if(Mathf.Abs(moveDir.x) > 0.1f || Mathf.Abs(moveDir.z) > 0.1f){
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, 
            defaultYPos + Mathf.Sin(timer) * (isSprinting ? sprintBobAmount : walkBobAmount), playerCamera.transform.localPosition.z);
        }

    }

    private void HandleMovementInput(){
        curInput = new Vector2((isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"), (isSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal"));
        float movDirY = moveDir.y;
        moveDir = (transform.TransformDirection(Vector3.forward) * curInput.x) + (transform.TransformDirection(Vector3.right) * curInput.y);
        moveDir.y = movDirY;
    }

    private void HandleMouseLook(){
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void ApplyFinalMovements(){
        if(!characterController.isGrounded){
            moveDir.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDir * Time.deltaTime);
    }

    public bool CanMove(){
        return canMove;
    }

    // for collect soul shard
    private void HandleInteractionCheck()
    {
        if (Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if (hit.collider.gameObject.layer == 6 &&
                (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                {
                    currentInteractable.OnFocus();
                }
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
        else
        {

        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint),
            out RaycastHit hit, interactionDistance, interactionLayer))      // press E to collect for alpha
        {
            currentInteractable.OnInteract();
        }
    }
    
}
