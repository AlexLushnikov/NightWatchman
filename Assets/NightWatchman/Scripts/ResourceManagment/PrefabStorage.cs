using System;
using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    [CreateAssetMenu(fileName = "PrefabStorage", menuName = "ScriptableObjects/PrefabStorage", order = 1)]
    public class PrefabStorage : ScriptableObject
    {
        [SerializeField] private List<Prefab> _prefabs;
        public List<Prefab> Prefabs => _prefabs;
    }
    
    
    [Serializable]
    public class Prefab
    {
        public EPrefabs EPrefab;
        public GameObject Object;
    }
}