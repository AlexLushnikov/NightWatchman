using System;
using UniRx;
using UnityEngine;

namespace NightWatchman
{
    public class Core : ICore, IDisposable
    {
        private float SelectionTime = 1f;
        private float SelectDistance = 10f;
        
        private readonly LayerMask InteractableLayer = LayerMask.GetMask("Interactable");
        private ILevelService _levelService;
        private IPlayer _player;
        private CompositeDisposable _disposable = new();
        private Camera _camera;
        private Vector3 _screenCenter;

        private Interactable _current;
        private CoreView _coreView;

        private int _currentFound;
        private int _totalAnomalies;
        private IDisposable _timerDisposable;
        

        public Core(ILevelService levelService, IPlayer player, IViewsFactory viewsFactory)
        {
            _levelService = levelService;
            _player = player;
            _coreView = viewsFactory.GetCoreView();
            _coreView.Disable();
            
            _levelService.SpawnLevel();
            var spawnPoint = _levelService.CurrentEnvironment.SpawnPoint.position;
            _player.Spawn(spawnPoint);
            _camera = _player.Camera;
            _totalAnomalies = _levelService.CurrentLevel.AnomaliesCount;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            Observable.EveryUpdate().Subscribe(Update).AddTo(_disposable);
            var mouseDownStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0) && _current != null);

            var mouseUpStream = Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0) || _current == null);

            mouseDownStream
                .Subscribe(_ => StartTimer(SelectionTime))
                .AddTo(_disposable);

            mouseUpStream
                .Subscribe(_ => CancelTimer())
                .AddTo(_disposable);

            Prepare();
            StartLevel();
        }
        
        private void StartTimer(float time)
        {
            CancelTimer();

            var _timerProgress = 0f;

            _timerDisposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _timerProgress += Time.deltaTime / time;
                    _coreView.SetProgress(_timerProgress);
                    if (_timerProgress >= 1f)
                    {
                        _timerProgress = 1f;
                        TimerCompleted();
                        CancelTimer();
                    }
                })
                .AddTo(_disposable);
        }

        private void CancelTimer()
        {
            _timerDisposable?.Dispose();
            _coreView.SetProgress(0);
        }

        private void TimerCompleted()
        {
            if (_current.IsAnomaly)
            {
                _currentFound++;
                _coreView.SetCount(_currentFound, _totalAnomalies);
            }
            else
            {
                _coreView.ActivateNotAnomalyText();
            }
            
            _current.SetDifficulty(Difficulty.None);
            _current.ChangeState(InteractableState.Selected);
            _current = null;
            _coreView.ChangeTarget(false);
        }

        private void StartLevel()
        {
            _coreView.SetData(_currentFound, _totalAnomalies, 0);
            _coreView.Enable();
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
            
            if (Physics.Raycast(ray, out var hit, SelectDistance, InteractableLayer))
            {
                var interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (interactable && interactable.State is not InteractableState.Selected)
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
            _coreView.ChangeTarget(true);
        }

        private void DeselectObject()
        {
            _current.ChangeState(InteractableState.Default);
            _current = null;
            _coreView.ChangeTarget(false);
        }

        public void Dispose()
        {
            _timerDisposable?.Dispose();
            _disposable?.Dispose();
        }
    }
}