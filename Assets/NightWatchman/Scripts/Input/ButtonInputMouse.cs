using UnityEngine;

namespace NightWatchman
{
    public class ButtonInputMouse : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private int mouseButton;
#pragma warning restore 0649

        public SimpleInput.ButtonInput button = new SimpleInput.ButtonInput();

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
            button.value = Input.GetMouseButton(mouseButton);
        }
    }
}