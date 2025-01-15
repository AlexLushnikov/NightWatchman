using UnityEngine;

namespace NightWatchman
{
    public class ReleaseComposition : IComposition
    {
        private IResourceManager _resourceManager;
        private IPlayerController _playerController;
        private ILevelService _levelService;
        private ICore _core;
        
        public void Destroy()
        {
            DisposeAll();
            _resourceManager = null;
            _playerController = null;
            _levelService = null;
            _core = null;
        }

        public IResourceManager GetResourceManager()
        {
            if (_resourceManager == null)
            {
                _resourceManager = new ResourceManager();
            }

            return _resourceManager;
        }

        public IPlayerController GetPlayerController()
        {
            if (_playerController == null)
            {
                var resourceManager = GetResourceManager();
                _playerController = resourceManager.GetOrSpawnPrefab<PlayerController>(EPrefabs.Player);
            }

            return _playerController;
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
                _core = new Core(levelService, playerController);
            }

            return _core;
        }

        private void DisposeAll()
        {
        }
    }
}