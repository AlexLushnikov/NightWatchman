using System;
using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private List<Prefab> _prefabs;
        private Dictionary<EPrefabs, GameObject> _spawnedObjects;

        // public T GetPrefab<T>(EPrefabs prefab)
        // {
        //     if (_spawnedObjects.ContainsKey(prefab))
        //     {
        //         
        //     }
        // }
    }

    [Serializable]
    public class Prefab
    {
        public EPrefabs _prefab;
        public GameObject _object;
    }
}