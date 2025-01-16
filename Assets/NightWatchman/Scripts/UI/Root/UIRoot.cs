using UnityEngine;

namespace NightWatchman
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Transform MainCanvas => _mainCanvas;
        [SerializeField] private Transform _mainCanvas;
    }
}