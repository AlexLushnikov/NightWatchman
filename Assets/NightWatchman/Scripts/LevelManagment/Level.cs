using System.Collections.Generic;
using UnityEngine;

namespace NightWatchman
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 2)]
    public class Level : ScriptableObject
    {
        public int Id;
        public int AnomaliesCount;
        public int EasyCount;
        public int MediumCount;
        public int HardCount;
        public List<EInteractableIds> AnomaliesIds;
        public Environment Environment;
    }
}