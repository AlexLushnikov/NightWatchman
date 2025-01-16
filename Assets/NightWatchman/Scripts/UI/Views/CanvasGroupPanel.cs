using System;
using UnityEngine;

namespace NightWatchman
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupPanel : MonoBehaviour
    {
        public event Action<bool> ActiveChanged;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
        }

        public void SetVisible(bool isActive)
        {
            Init();

            _canvasGroup.alpha = isActive ? 1f : 0f;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
            ActiveChanged?.Invoke(isActive);
        }
    }
}