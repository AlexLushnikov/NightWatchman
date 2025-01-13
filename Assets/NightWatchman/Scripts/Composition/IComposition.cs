namespace NightWatchman
{
    public interface IComposition
    {
        void Destroy();
        IResourceManager GetResourceManager();
    }
}