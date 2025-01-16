namespace NightWatchman
{
    public class ViewsFactory : IViewsFactory
    {
        private IResourceManager _resourceManager;
        private IUIRoot _uiRoot;
        
        public ViewsFactory(IResourceManager resourceManager, IUIRoot uiRoot)
        {
            _resourceManager = resourceManager;
            _uiRoot = uiRoot;
        }
        
        public CoreView GetCoreView()
        {
            var view = _resourceManager.GetOrSpawnView<CoreView>(EViews.CoreView);
            var canvasTransform = _uiRoot.MainCanvas;
            view.transform.SetParent(canvasTransform, false);
            return view;
        }
    }
}