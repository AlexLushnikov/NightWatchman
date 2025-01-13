using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NightWatchman
{
    public class ResourceManager : IResourceManager
    {
        private PrefabStorage _prefabStorage;
        private Dictionary<EPrefabs, GameObject> _spawnedObjects = new ();

        public ResourceManager()
        {
            _prefabStorage = Resources.Load<PrefabStorage>("NightWatchman/PrefabStorage");
        }

        public T GetPrefab<T>(EPrefabs ePrefab) where T : Component
        {
            if (!_spawnedObjects.TryGetValue(ePrefab, out var go))
            {
                var prefab = _prefabStorage.Prefabs.FirstOrDefault(x => x.Object.TryGetComponent<T>(out var component));
                if (prefab != null)
                {
                    go = Object.Instantiate(prefab.Object);
                    _spawnedObjects.Add(ePrefab, go);
                }
                else
                {
                    throw new UnityException($"Prefab with type {typeof(T)} not found");
                }
            }
            
            var component = go.GetComponent(typeof(T));
            if (component == null)
            {
                throw new UnityException($"{typeof(T)} type mismatch");
            }

            return (T)component;
        }
    }
}