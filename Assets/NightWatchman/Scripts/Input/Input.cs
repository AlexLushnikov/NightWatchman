using System;
using SimpleInputNamespace;
using UnityEngine;

namespace NightWatchman
{
    public class Input : MonoBehaviour
    {
        private void Awake()
        {
            SimpleInput.TrackUnityInput = false;
        }
    }
}