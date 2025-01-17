using System;
using UniRx;
using UnityEngine;

namespace NightWatchman
{
    public class Selector : IDisposable
    {
        private readonly LayerMask InteractableLayer = LayerMask.GetMask("Interactable");

        private ReactiveProperty<Interactable> _selected = new ();
        public ReadOnlyReactiveProperty<Interactable> Selected => _selected.ToReadOnlyReactiveProperty();
        
        private CompositeDisposable _disposable = new();
        private Camera _camera;
        private Vector3 _screenCenter;
        
        public Selector(Camera camera)
        {
            _camera = camera;
            _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            
            Observable.EveryUpdate().Subscribe(Update).AddTo(_disposable);
        }
        
        private void Update(long _)
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            var ray = _camera.ScreenPointToRay(_screenCenter);
            
            if (Physics.Raycast(ray, out var hit, GameplaySettings.SelectDistance, InteractableLayer))
            {
                var interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (interactable && interactable.State is not (InteractableState.Selected or InteractableState.None))
                {
                    if (_selected.Value == interactable)
                    {
                        return;
                    }

                    _selected.Value = interactable;
                }
            }
            else
            {
                if (_selected.Value != null)
                {
                    _selected.Value = null;
                }
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}