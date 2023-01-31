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

    void Awake(){
        canMove = true;
        playerCamera = GetComponentInChildren<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanMove(){
        return canMove;
    }
}
