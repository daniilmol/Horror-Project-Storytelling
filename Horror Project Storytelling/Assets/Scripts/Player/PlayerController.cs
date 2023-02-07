using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;
    
    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDir;
    private Vector2 curInput;

    private float rotationX = 0;

    private bool canMove;


    //Collect soul -Mike
    //public RaycastHit rayHit;
    //public float collectRange;
    //public LayerMask isSoul;

    [Header("Interaction")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    [SerializeField] private bool canInteract = true;
    private Interactable currentInteractable;

    // collect soul end

    void Awake(){
        canMove = true;
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
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
            ApplyFinalMovements();
        }

        if (canInteract)
        {
            //HandleCollect();
            HandleInteractionCheck();
            HandleInteractionInput();
        }
    }

    private void HandleMovementInput(){
        curInput = new Vector2(walkSpeed * Input.GetAxisRaw("Vertical"), walkSpeed * Input.GetAxisRaw("Horizontal"));
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

    // for collect soul shard - Mike
    private void HandleCollect()
    {
        if (Input.GetKeyDown(KeyCode.E)) // press E to collect for alpha
        {
            // collect if soul within collect range
            //if (Physics.Raycast(transform.position, transform.forward, out rayHit, collectRange, isSoul))
            //{
            //    //Debug.Log("collect");
            //    rayHit.transform.gameObject.SetActive(false); // change it to Destroy(rayHit.transform.gameObject) later

            //    // trigger effect/event


            //}
        }
    }

    private void HandleInteractionCheck()
    {
        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
        {
            if(hit.collider.gameObject.layer == 6 && 
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
    // collect soul end
}
