using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace NightWatchman
{
    public class Core : ICore, IDisposable
    {
        private const string SelectButton = "Select";
        private readonly CoreView _coreView;

        private Interactable _current;

        private readonly CompositeDisposable _disposable = new();
        private readonly ILevelService _levelService;
        private readonly IPlayer _player;
        private readonly Selector _selector;
        private IDisposable _timerDisposable;
        
        private int _currentFound;
        private int _totalAnomalies;
        private int _mistakeCount;

        public Core(ILevelService levelService, IPlayer player, IViewsFactory viewsFactory)
        {
            _levelService = levelService;
            _player = player;
            _coreView = viewsFactory.GetCoreView();
            _coreView.Disable();

            //Cursor.lockState = CursorLockMode.Locked;

            _selector = new Selector(_player.Camera);
            _selector.Selected.Subscribe(SelectedChanged).AddTo(_disposable);

            var mouseDownStream = Observable.EveryUpdate()
                .Where(_ => SimpleInput.GetButtonDown(SelectButton) && _current != null);

            var mouseUpStream = Observable.EveryUpdate()
                .Where(_ => SimpleInput.GetButtonUp(SelectButton) || _current == null);

            mouseDownStream
                .Subscribe(_ => StartTimer(GameplaySettings.SelectionTime))
                .AddTo(_disposable);

            mouseUpStream
                .Subscribe(_ => CancelTimer())
                .AddTo(_disposable);

            StartLevel();
            StartDay();
        }

        private void SelectedChanged(Interactable interactable)
        {
            if (_current != null)
            {
                DeselectObject();
            }

            _current = interactable;
            if (_current != null)
            {
                SelectObject();
            }
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
            if (_current is Door)
            {
                ChangeCoreState().Forget();
                _current = null;
                return;
            }

            if (_current.IsAnomaly)
            {
                _currentFound++;
                _coreView.SetAnomalyCount(_currentFound, _totalAnomalies);
            }
            else
            {
                _mistakeCount++;
                _coreView.SetMistakeCount(_mistakeCount);
                _coreView.ActivateNotAnomalyText();
            }

            _current.SetDifficulty(Difficulty.None);
            _current.ChangeState(InteractableState.Selected);
            _current = null;
            _coreView.ChangeTarget(false);
        }

        private async UniTask ChangeCoreState()
        {
            await _coreView.EnableFade();
            StartNight();
            await UniTask.WaitForSeconds(1f);
            await _coreView.DisableFade();
        }

        private void StartLevel()
        {
            _levelService.SpawnLevel();
            var spawnPoint = _levelService.CurrentEnvironment.SpawnPoint.position;
            _player.Spawn(spawnPoint);
            _totalAnomalies = _levelService.CurrentLevel.AnomaliesCount;
            _currentFound = 0;
            _mistakeCount = 0;

            _coreView.Enable();
        }

        private void StartDay()
        {
            _levelService.CurrentEnvironment.SetupDay();
            _coreView.SetDayText();
        }

        private void StartNight()
        {
            _levelService.SetupNight();
            _coreView.SetNightText();
            _coreView.SetAnomalyCount(0, _totalAnomalies);
            _coreView.SetMistakeCount(_mistakeCount);
        }

        private void SelectObject()
        {
            _current.ChangeState(InteractableState.InProcess);
            _coreView.ChangeTarget(true);
        }

        private void DeselectObject()
        {
            _current.ChangeState(InteractableState.Default);
            _coreView.ChangeTarget(false);
        }
        
        public void Dispose()
        {
            _timerDisposable?.Dispose();
            _disposable?.Dispose();
        }
    }
}