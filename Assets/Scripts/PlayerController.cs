using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Action<float> OnVelocityChanged;
    public Action OnPlayerJump;
    public Action OnPlayerLand;

    [SerializeField] private IInputDevice joystick = null;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float jumpHeight = 5;

    private Rigidbody playerRigidbody = null;
    private bool isGrounded = true;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<Joystick>();
        Subscribe();
    }

    private void Update()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.parent.TryGetComponent<Floor>(out Floor floor))
        {
            isGrounded = true;
            OnPlayerLand?.Invoke();
        }
    }

    private void RotatePlayer()
    {
        if (joystick.GetHorizontalInput() != 0 || joystick.GetVerticalInput() != 0)
            transform.rotation = Quaternion.LookRotation(new Vector3(joystick.GetHorizontalInput(), 0, joystick.GetVerticalInput()));
    }

    private void MovePlayer()
    {
        transform.position += new Vector3(joystick.GetHorizontalInput(), 0, joystick.GetVerticalInput()) * Time.deltaTime * moveSpeed;
    }

    private void ResetVelocity()
    {
        OnVelocityChanged?.Invoke(0);
    }

    private void UpdateVelocity()
    {
        var velocity = Mathf.Abs(joystick.GetHorizontalInput()) + Mathf.Abs(joystick.GetVerticalInput());
        OnVelocityChanged?.Invoke(velocity);
    }

    private void Jump()
    {
        if (!isGrounded)
            return;
        isGrounded = false;
        playerRigidbody.AddForce(new Vector3(joystick.GetHorizontalInput() * jumpForce, jumpHeight, joystick.GetVerticalInput() * jumpForce), ForceMode.Impulse);
        OnPlayerJump?.Invoke();
    }

    private void Subscribe()
    {
        joystick.OnStartInput += UpdateVelocity;
        joystick.OnEndInput += ResetVelocity;
        joystick.OnEndInput += Jump;
    }

    private void Unsubscribe()
    {
        joystick.OnStartInput -= UpdateVelocity;
        joystick.OnEndInput -= ResetVelocity;
        joystick.OnEndInput -= Jump;
    }
}