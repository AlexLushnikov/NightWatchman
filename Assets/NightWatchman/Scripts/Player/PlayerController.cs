using System;
using UnityEngine;

namespace NightWatchman
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _jumpForce = 200f;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private LayerMask _environmentMask;

        private IInputHandler _inputHandler;

        private Vector3 _movementInput;
        private float _verticalRotation;
        private bool _canJump;

        private void Awake()
        {
            _inputHandler = CompositionRoot.GetInputHandler();
            
            _inputHandler.OnJump += HandleJump;
            _inputHandler.OnMove += HandleMove;
            _inputHandler.OnRotate += HandleRotation;
        }

        private void HandleJump()
        {
            if (_canJump)
            {
                _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
                _canJump = false;
            }
        }
        
        private void HandleMove(Vector2 value)
        {
            var moveX = value.x;
            var moveZ = value.y;
            
            _movementInput = (transform.right * moveX + transform.forward * moveZ).normalized * _speed;
            transform.position += _movementInput * Time.deltaTime;
        }

        private void HandleRotation(Vector2 value)
        {
            var mouseX = value.x * _mouseSensitivity * Time.deltaTime;
            var mouseY = value.y * _mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void OnCollisionEnter(Collision other)
        {
            _canJump = _environmentMask == (_environmentMask | (1 << other.gameObject.layer)) ;
        }
    }
}