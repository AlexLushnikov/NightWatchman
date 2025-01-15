namespace NightWatchman
{
    public interface IComposition
    {
        void Destroy();
        IResourceManager GetResourceManager();
        IPlayerController GetPlayerController();
        ILevelService GetLevelService();
        ICore GetCore();
    }
}