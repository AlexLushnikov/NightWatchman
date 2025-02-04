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
        private Dictionary<EComponents, GameObject> _spawnedComponents = new ();
        private Dictionary<EViews, BaseView> _views = new();

        public ResourceManager(string storagePath)
        {
            _storage = Resources.Load<Storage>(storagePath);
        }

        public T GetOrSpawnPrefab<T>(EComponents eComponent, bool dependsOnPlatform = false) where T : Component
        {
            if (!_spawnedComponents.TryGetValue(eComponent, out var go))
            {
                var prefab = GetPrefab<T>(dependsOnPlatform);
                if (prefab != null)
                {
                    go = Object.Instantiate(prefab);
                    _spawnedComponents.Add(eComponent, go);
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

        private GameObject GetPrefab<T>(bool dependsOnPlatform)
        {
            if (dependsOnPlatform)
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                return _storage.PCPrefabs.FirstOrDefault(x => x.TryGetComponent<T>(out var component));
#endif
                
#if UNITY_IOS || UNITY_ANDROID
                return _storage.MobilePrefabs.FirstOrDefault(x => x.TryGetComponent<T>(out var component));
#endif
                throw new UnityException($"Platform not supported");
            }
            else
            {
                return _storage.Prefabs.FirstOrDefault(x => x.TryGetComponent<T>(out var component));
            }
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