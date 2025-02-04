using UnityEngine;

namespace NightWatchman
{
    public class ButtonInputMouse : MonoBehaviour
    {
        [SerializeField] private int _mouseButton;

        public SimpleInput.ButtonInput button = new();

        private void OnEnable()
        {
            button.StartTracking();
            SimpleInput.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            button.StopTracking();
            SimpleInput.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            button.value = Input.GetMouseButton(_mouseButton);
        }
    }
}