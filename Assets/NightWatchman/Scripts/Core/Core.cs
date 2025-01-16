using System;
using UniRx;
using UnityEngine;

namespace NightWatchman
{
    public class Core : ICore, IDisposable
    {
        private readonly LayerMask InteractableLayer = LayerMask.GetMask("Interactable");
        private ILevelService _levelService;
        private IPlayerController _playerController;
        private CompositeDisposable _disposable = new();
        private Camera _camera;
        private Vector3 _screenCenter;

        private float _selectDistance = 10f;
        private Interactable _current;

        public Core(ILevelService levelService, IPlayerController playerController)
        {
            _levelService = levelService;
            _playerController = playerController;
            
            _levelService.SpawnLevel();
            var spawnPoint = _levelService.CurrentEnvironment.SpawnPoint.position;
            _playerController.Spawn(spawnPoint);
            _camera = _playerController.Camera;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            Observable.EveryUpdate().Subscribe(Update).AddTo(_disposable);

            Prepare();
        }

        private void Prepare()
        {
            _screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            
            _levelService.CurrentEnvironment.Init();
            _levelService.SetupAnomaly();
        }

        private void Update(long _)
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            var ray = _camera.ScreenPointToRay(_screenCenter);
            
            if (Physics.Raycast(ray, out var hit, _selectDistance, InteractableLayer))
            {
                var interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (interactable)
                {
                    if (_current == interactable)
                    {
                        return;
                    }
                    
                    if (_current != null)
                    {
                        DeselectObject();
                    }

                    _current = interactable;
                    SelectObject();
                }
            }
            else
            {
                if (_current != null)
                {
                    DeselectObject();
                }
            }
        }

        private void SelectObject()
        {
            _current.ChangeState(InteractableState.InProcess);
        }

        private void DeselectObject()
        {
            _current.ChangeState(InteractableState.Default);
            _current = null;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}