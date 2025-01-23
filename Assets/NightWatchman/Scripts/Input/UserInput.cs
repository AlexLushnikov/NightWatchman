using UnityEngine;

namespace NightWatchman
{
    public class UserInput : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Cursor.lockState = CursorLockMode.Locked;
#endif

            SimpleInput.TrackUnityInput = false;
        }
    }
}