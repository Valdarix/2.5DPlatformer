using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    private CharacterController _controller;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    [SerializeField] private float playerSpeed  = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f; 
    [SerializeField] private float gravityValue = -9.81f;
    private bool _canDoubleJump;
    
    // Start is called before the first frame update
    private void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Player is missing controller");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
            _canDoubleJump = true;
        }
        
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        _controller.Move(move * (Time.deltaTime * playerSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        else if (Input.GetButtonDown("Jump") && _canDoubleJump)
        {
            _playerVelocity.y += Mathf.Sqrt(jumpHeight/2 * -3.0f * gravityValue);
            _canDoubleJump = false;
        }

        _playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

   
}
