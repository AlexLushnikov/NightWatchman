using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NightWatchman
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroupPanel _canvasGroupPanel;
        public bool IsActive { get; private set; }
        
        public virtual void Enable()
        {
            _canvasGroupPanel.SetVisible(true);
            IsActive = true;
        }

        public virtual void Disable()
        {
            _canvasGroupPanel.SetVisible(false);
            IsActive = false;
        }
    }
}