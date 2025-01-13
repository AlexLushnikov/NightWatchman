namespace NightWatchman
{
    public class ReleaseComposition : IComposition
    {
        private IResourceManager _resourceManager;
        public void Destroy()
        {
            DisposeAll();
            _resourceManager = null;
        }

        public IResourceManager GetResourceManager()
        {
            if (_resourceManager == null)
            {
                _resourceManager = new ResourceManager();
            }

            return _resourceManager;
        }

        private void DisposeAll()
        {
        }
    }
}