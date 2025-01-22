using UnityEngine;

namespace NightWatchman
{
    public interface IResourceManager
    {
        T GetOrSpawnPrefab<T>(EPrefabs ePrefab, bool dependsOnPlatform = false) where T : Component;
        T GetOrSpawnView<T>(EViews eView) where T : BaseView;
    }
}