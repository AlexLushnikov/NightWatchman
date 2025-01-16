using System;
using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    [CreateAssetMenu(fileName = "Storage", menuName = "ScriptableObjects/Storage", order = 1)]
    public class Storage : ScriptableObject
    {
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private List<BaseView> _views;
        public List<GameObject> Prefabs => _prefabs;
        public List<BaseView> Views => _views;
    }
}