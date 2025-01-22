using System;
using UniRx;
using UnityEngine;

namespace NightWatchman
{
    public class MenuPresenter : IDisposable
    {
        private CompositeDisposable _disposable = new();
        private MenuView _view;
        
        public MenuPresenter(IViewsFactory viewsFactory)
        {
            _view = viewsFactory.GetMenuView();
            _view.Disable();

            Observable.EveryUpdate()
                .Where(_ => UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(EscapeClicked)
                .AddTo(_disposable);
        }

        private void EscapeClicked(long _)
        {
            if (_view.IsActive)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            _view.Enable();
        }

        private void HideMenu()
        {
            _view.Disable();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}