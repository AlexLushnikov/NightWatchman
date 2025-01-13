namespace NightWatchman
{
    public class ReleaseComposition : IComposition
    {
        private IResourceManager _resourceManager;
        private IPlayerController _playerController;
        
        public void Destroy()
        {
            DisposeAll();
            _resourceManager = null;
            _playerController = null;
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
                _playerController = resourceManager.GetPrefab<PlayerController>(EPrefabs.Player);
            }

            return _playerController;
        }

        private void DisposeAll()
        {
        }
    }
}