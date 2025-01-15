using UnityEngine;

namespace NightWatchman
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";
        private const string MouseYAxis = "Mouse Y";
        private const string MouseXAxis = "Mouse X";
        private const string JumpAxis = "Jump";
        
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _mouseSensitivity = 2f;
        [SerializeField] private float _jumpForce = 200f;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private LayerMask _environmentMask;
        [SerializeField] private Camera _camera;

        private Vector3 _movementInput;
        private float _verticalRotation;
        private bool _canJump;

        public Camera Camera => _camera;
        
        private void Update()
        {
            HandleInput();
            HandleCameraRotation();
            HandleJump();
        }

        private void HandleJump()
        {
            if (Input.GetAxis(JumpAxis) > 0 && _canJump)
            {
                _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
                _canJump = false;
            }
        }
        
        private void HandleInput()
        {
            var moveX = Input.GetAxis(HorizontalAxis);
            var moveZ = Input.GetAxis(VerticalAxis);
            
            _movementInput = (transform.right * moveX + transform.forward * moveZ).normalized * _speed;
            transform.position += _movementInput * Time.deltaTime;
        }

        private void HandleCameraRotation()
        {
            var mouseX = Input.GetAxis(MouseXAxis) * _mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis(MouseYAxis) * _mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);
            _cameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }

        private void OnCollisionEnter(Collision other)
        {
            _canJump = _environmentMask == (_environmentMask | (1 << other.gameObject.layer)) ;
        }

        public void Spawn(Vector3 spawnPoint)
        {
            transform.position = new Vector3(spawnPoint.x, transform.position.y, spawnPoint.z);
        }
    }
}