using UnityEngine;

namespace NightWatchman
{
    public interface IResourceManager
    {
        T GetOrSpawnPrefab<T>(EComponents eComponent, bool dependsOnPlatform = false) where T : Component;
        T GetOrSpawnView<T>(EViews eView) where T : BaseView;
    }
}