namespace NightWatchman
{
    public interface IComposition
    {
        void Destroy();
        IResourceManager GetResourceManager();
        IPlayer GetPlayerController();
        ILevelService GetLevelService();
        ICore GetCore();
        IUIRoot GetUIRoot();
        IViewsFactory GetViewsFactory();
    }
}