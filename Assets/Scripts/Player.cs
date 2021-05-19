using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private Vector3 velocity;
    [SerializeField] private bool _groundedPlayer;
    [SerializeField] private float playerSpeed  = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f; 
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private bool _canDoubleJump;
    private float _yV;
    
    // Start is called before the first frame update
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Player is missing controller");
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && velocity.y < 0)
        {
            velocity.y = 0;
        }

        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        _controller.Move(move * Time.deltaTime * playerSpeed);
        
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * -3.0f * gravityValue));
        }
        
        velocity.y += gravityValue * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);

    }
}
