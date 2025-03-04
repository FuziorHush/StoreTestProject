using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : IUpdateContext
{
    private float _moveSpeed = 3f;
    private float _turnRate = 60f;
    public float _gravity = -9.81f;
    public float _groundCheckDistance = 0.1f;

    private Vector2 _inputVector;
    private Vector2 _lookVector;
    private bool _isGrounded;
    private Vector3 _velocity;

    private Transform _playerTransform;
    private Transform _cameraTransform;
    private CharacterController _characterController;
    private IInputHandler _inputHandler;
    public LayerMask _groundLM;

    public void Init(IInputHandler inputHandler, Transform playerTransform, Transform cameraTransform, CharacterController characterController, LayerMask groundLM)
    {
        _playerTransform = playerTransform;
        _cameraTransform = cameraTransform;
        _characterController = characterController;
        _inputHandler = inputHandler;
        _groundLM = groundLM;
    }

    public void OnUpdate()
    {
        _isGrounded = Physics.CheckSphere(_playerTransform.position, _groundCheckDistance, _groundLM);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }

        _inputVector = _inputHandler.GetMovement();
        _characterController.Move((_playerTransform.right * _inputVector.x + _playerTransform.forward * _inputVector.y) * _moveSpeed * Time.deltaTime);

        if (!_isGrounded)
            _characterController.Move(_playerTransform.up * _gravity * Time.deltaTime);

        _lookVector = _inputHandler.GetLookVector();
        _playerTransform.Rotate(Vector3.up * _lookVector.x * _turnRate * Time.deltaTime);
        _cameraTransform.Rotate(-Vector3.right * _lookVector.y * _turnRate * Time.deltaTime);
    }
}
