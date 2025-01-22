using System;
using UnityEngine;

namespace NightWatchman
{
    public interface IInputHandler : IDisposable
    {
        public event Action<float> OnJump;
        public event Action<Vector2> OnMove;
        public event Action<Vector2> OnRotate;
    }
}