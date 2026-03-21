using System;
using PurrNet;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundCheckDistance = 0.2f;

    [Header("Look Settings")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float maxLookAngle = 80f;

    [Header("References")] 
    [SerializeField] private LookAtTransform bodyLookAtScript;
    [SerializeField] private Transform cameraPivot, bodyTf;
    
    private CharacterController characterController;
    private Vector3 velocity, moveDirection;
    private float verticalRotation, horizontalRotation;
    private bool controllerActive;

    protected override void OnSpawned()
    {
        base.OnSpawned();

        enabled = isOwner;
        
        if (!isOwner)
            Destroy(cameraPivot.gameObject);
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();

        if (cameraPivot == null)
            enabled = false;
    }

    private void Update()
    {
        if (!controllerActive)
            return;
        
        HandleMovement();
        HandleRotation();
        HandleBodyRotation();
    }

    public void SetActivity(bool state)
    {
        controllerActive = state;
    }

    private void HandleMovement()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = InputManager.instance.inputActionAsset.FindAction("Move").ReadValue<Vector2>().x;
        float vertical = InputManager.instance.inputActionAsset.FindAction("Move").ReadValue<Vector2>().y;

        moveDirection = cameraPivot.right * horizontal + cameraPivot.forward * vertical;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);
        moveDirection.y = 0;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        characterController.Move(moveDirection * currentSpeed * Time.deltaTime);

        if (InputManager.instance.inputActionAsset.FindAction("Jump").IsPressed() && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float mouseX = InputManager.instance.inputActionAsset.FindAction("Look").ReadValue<Vector2>().x * lookSensitivity;
        float mouseY = InputManager.instance.inputActionAsset.FindAction("Look").ReadValue<Vector2>().y * lookSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        
        horizontalRotation += mouseX;
        
        cameraPivot.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    private void HandleBodyRotation()
    {
        Vector3 lookVector = transform.position + moveDirection * 99f;
        
        if (moveDirection != Vector3.zero)
            bodyLookAtScript.LookAtPosition(lookVector);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.03f, Vector3.down, groundCheckDistance);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.03f, Vector3.down * groundCheckDistance);
    }
#endif
}