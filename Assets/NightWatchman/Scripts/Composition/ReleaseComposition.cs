using System;
using UnityEngine;

namespace NightWatchman
{
    public class ReleaseComposition : IComposition
    {
        private const string StoragePath = "NightWatchman/Storage";

        private IResourceManager _resourceManager;
        private IPlayer _player;
        private ILevelService _levelService;
        private ICore _core;
        private IUIRoot _uiRoot;
        private IViewsFactory _viewsFactory;
        private IInputHandler _inputHandler;
        private ISoundsService _soundsService;

        private MenuPresenter _menuPresenter;

        public void Destroy()
        {
            DisposeAll();
            _resourceManager = null;
            _player = null;
            _levelService = null;
            _core = null;
            _uiRoot = null;
            _viewsFactory = null;
            _menuPresenter = null;
            _inputHandler = null;
            _soundsService = null;
        }

        public IResourceManager GetResourceManager()
        {
            if (_resourceManager == null)
            {
                _resourceManager = new ResourceManager(StoragePath);
            }

            return _resourceManager;
        }

        public IPlayer GetPlayerController()
        {
            if (_player == null)
            {
                var resourceManager = GetResourceManager();
                _player = resourceManager.GetOrSpawnPrefab<Player>(EComponents.Player);
            }

            return _player;
        }

        public ILevelService GetLevelService()
        {
            if (_levelService == null)
            {
                _levelService = new LevelService();
            }

            return _levelService;
        }

        public ICore GetCore()
        {
            if (_core == null)
            {
                var levelService = GetLevelService();
                var playerController = GetPlayerController();
                var viewFactory = GetViewsFactory();
                _core = new Core(levelService, playerController, viewFactory);
            }

            return _core;
        }

        public IUIRoot GetUIRoot()
        {
            if (_uiRoot == null)
            {
                var resourceManager = GetResourceManager();
                _uiRoot = resourceManager.GetOrSpawnPrefab<UIRoot>(EComponents.UIRoot);
            }

            return _uiRoot;
        }

        public IViewsFactory GetViewsFactory()
        {
            if (_viewsFactory == null)
            {
                var resourceManager = GetResourceManager();
                var uiRoot = GetUIRoot();
                _viewsFactory = new ViewsFactory(resourceManager, uiRoot);
            }

            return _viewsFactory;
        }

        public MenuPresenter GetMenuPresenter()
        {
            if (_menuPresenter == null)
            {
                var viewFactory = GetViewsFactory();
                _menuPresenter = new MenuPresenter(viewFactory);
            }

            return _menuPresenter;
        }

        public IInputHandler GetInputHandler()
        {
            if (_inputHandler == null)
            {
                var resourceManager = GetResourceManager();
                _inputHandler = new InputHandler(resourceManager);
            }

            return _inputHandler;
        }

        public ISoundsService GetSoundsService()
        {
            if (_soundsService == null)
            {
                var resourceManager = GetResourceManager();
                _soundsService = resourceManager.GetOrSpawnPrefab<SoundsService>(EComponents.SoundsService);
            }

            return _soundsService;
        }

        private void DisposeAll()
        {
            (_resourceManager as IDisposable)?.Dispose();
            (_player as IDisposable)?.Dispose();
            (_levelService as IDisposable)?.Dispose();
            (_core as IDisposable)?.Dispose();
            (_uiRoot as IDisposable)?.Dispose();
            (_viewsFactory as IDisposable)?.Dispose();
            (_menuPresenter as IDisposable)?.Dispose();
            (_inputHandler as IDisposable)?.Dispose();
        }
    }
}