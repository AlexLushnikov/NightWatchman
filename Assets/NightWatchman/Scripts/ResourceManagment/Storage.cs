using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace NightWatchman
{
    [CreateAssetMenu(fileName = "Storage", menuName = "ScriptableObjects/Storage", order = 1)]
    public class Storage : ScriptableObject
    {
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private List<BaseView> _views;
        [SerializeField] private List<GameObject> _pcPrefabs;
        [SerializeField] private List<GameObject> _mobilePrefabs;
        
        public List<GameObject> Prefabs => _prefabs;
        public List<BaseView> Views => _views;

        public List<GameObject> PCPrefabs => _pcPrefabs;
        public List<GameObject> MobilePrefabs => _mobilePrefabs;
    }
}