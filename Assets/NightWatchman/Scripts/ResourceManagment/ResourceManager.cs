using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NightWatchman
{
    public class ResourceManager : IResourceManager
    {
        private Storage _storage;
        private Dictionary<EPrefabs, GameObject> _spawnedObjects = new ();
        private Dictionary<EViews, BaseView> _views = new();

        public ResourceManager(string storagePath)
        {
            _storage = Resources.Load<Storage>(storagePath);
        }

        public T GetOrSpawnPrefab<T>(EPrefabs ePrefab) where T : Component
        {
            if (!_spawnedObjects.TryGetValue(ePrefab, out var go))
            {
                var prefab = _storage.Prefabs.FirstOrDefault(x => x.TryGetComponent<T>(out var component));
                if (prefab != null)
                {
                    go = Object.Instantiate(prefab);
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
        
        public T GetOrSpawnView<T>(EViews eView) where T : BaseView
        {
            if (!_views.TryGetValue(eView, out var go))
            {
                var view = _storage.Views.FirstOrDefault(x => x.TryGetComponent<T>(out var component));
                if (view != null)
                {
                    go = Object.Instantiate(view);
                    _views.Add(eView, go);
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