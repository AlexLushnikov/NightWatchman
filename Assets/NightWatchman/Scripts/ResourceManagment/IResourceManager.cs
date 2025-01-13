using UnityEngine;

namespace NightWatchman
{
    public interface IResourceManager
    {
        T GetPrefab<T>(EPrefabs ePrefab) where T : Component;
    }
}