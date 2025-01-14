using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 2)]
    public class LevelsData : ScriptableObject
    {
        public List<Level> Levels;
    }
}