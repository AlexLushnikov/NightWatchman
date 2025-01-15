using UnityEngine;

namespace NightWatchman
{
    public interface IResourceManager
    {
        T GetOrSpawnPrefab<T>(EPrefabs ePrefab) where T : Component;
    }
}