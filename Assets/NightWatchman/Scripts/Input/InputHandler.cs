using System;
using UnityEngine;

namespace NightWatchman
{
    public class InputHandler : IInputHandler
    {
        public event Action<float> OnJump;
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnRotate;
        
        private const string VerticalAxis = "Vertical";
        private const string HorizontalAxis = "Horizontal";
        private const string MouseYAxis = "Mouse Y";
        private const string MouseXAxis = "Mouse X";
        private const string JumpAxis = "Jump";

        public InputHandler(IResourceManager resourceManager)
        {
            var input = resourceManager.GetOrSpawnPrefab<Input>(EPrefabs.Input, true);
            SimpleInput.OnUpdate += InputUpdate;
        }
        
        private void InputUpdate()
        {
            HandleInput();
            HandleCameraRotation();
            HandleJump();
        }
        
        private void HandleJump()
        {
            var jumpAxis = SimpleInput.GetAxis(JumpAxis);
            OnJump?.Invoke(jumpAxis);
        }
        
        private void HandleInput()
        {
            var moveX = SimpleInput.GetAxis(HorizontalAxis);
            var moveZ = SimpleInput.GetAxis(VerticalAxis);
        }

        private void HandleCameraRotation()
        {
            var mouseX = SimpleInput.GetAxis(MouseXAxis);
            var mouseY = SimpleInput.GetAxis(MouseYAxis);
        }

        public void Dispose()
        {
            SimpleInput.OnUpdate -= InputUpdate;
        }
    }
}