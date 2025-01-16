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

        public void Destroy()
        {
            DisposeAll();
            _resourceManager = null;
            _player = null;
            _levelService = null;
            _core = null;
            _uiRoot = null;
            _viewsFactory = null;
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
                _player = resourceManager.GetOrSpawnPrefab<Player>(EPrefabs.Player);
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
                _uiRoot = resourceManager.GetOrSpawnPrefab<UIRoot>(EPrefabs.UIRoot);
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

        private void DisposeAll()
        {
        }
    }
}