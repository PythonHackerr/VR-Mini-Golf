using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using System.Collections.Generic;


[RequireComponent(typeof(CharacterController))]
public class VRJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController cc;
    [SerializeField] private LayerMask groundLayers;

    private float gravity = Physics.gravity.y;
    private Vector3 movement;

    private bool isGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, groundLayers);
    }


    void Update()
    {
        bool _isGrounded = isGrounded();

        if (jumpButton.action.WasPressedThisFrame() && _isGrounded)
        {
            Jump();
        }

        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);
    }


    void Jump()
    {
        Debug.Log("Jump");
        movement.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
    }
}
