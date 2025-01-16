using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NightWatchman
{
    public abstract class BaseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroupPanel _canvasGroupPanel;
        
        public virtual void Enable()
        {
            _canvasGroupPanel.SetVisible(true);
        }

        public virtual void Disable()
        {
            _canvasGroupPanel.SetVisible(true);
        }
    }
}