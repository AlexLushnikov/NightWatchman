using System;
using UnityEngine;

namespace NightWatchman
{
    public class InputHandler : IInputHandler
    {
        public event Action OnJump;
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnRotate;
        
        private const string MoveForward = "MoveForward";
        private const string MoveRight = "MoveRight";
        private const string RotateVertical = "RotateVertical";
        private const string RotateHorizontal = "RotateHorizontal";
        private const string Jump = "Jump";

        public InputHandler(IResourceManager resourceManager)
        {
            var input = resourceManager.GetOrSpawnPrefab<Input>(EPrefabs.Input, true);
            SimpleInput.OnUpdate += InputUpdate;
        }
        
        private void InputUpdate()
        {
            HandleMove();
            HandleRotation();
            HandleJump();
        }
        
        private void HandleJump()
        {
            var jumpAxis = SimpleInput.GetButtonDown(Jump);
            if (jumpAxis)
            {
                OnJump?.Invoke();
            }
        }
        
        private void HandleMove()
        {
            var moveX = SimpleInput.GetAxis(MoveRight);
            var moveZ = SimpleInput.GetAxis(MoveForward);
            OnMove?.Invoke(new Vector2(moveX, moveZ));
        }

        private void HandleRotation()
        {
            var mouseX = SimpleInput.GetAxis(RotateVertical);
            var mouseY = SimpleInput.GetAxis(RotateHorizontal);
            OnRotate?.Invoke(new Vector2(mouseX, mouseY));
        }

        public void Dispose()
        {
            SimpleInput.OnUpdate -= InputUpdate;
        }
    }
}