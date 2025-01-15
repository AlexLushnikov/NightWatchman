using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace NightWatchman
{
    public class LevelService : ILevelService
    {
        private int _currentLevelId;
        private LevelsData _data;
        private Environment _currentEnvironment;
        private Level _currentLevel;

        public Level CurrentLevel => _currentLevel;
        public Environment CurrentEnvironment => _currentEnvironment;

        public LevelService()
        {
            _data = Resources.Load<LevelsData>("NightWatchman/Levels/LevelsData");
        }

        public void SpawnLevel()
        {
            if (_data.Levels.Count < _currentLevelId)
            {
                Debug.LogError("No more levels");
                return;
            }

            if (_currentEnvironment != null)
            {
                Object.Destroy(_currentEnvironment.gameObject);
            }

            _currentLevel = _data.Levels[_currentLevelId];
            _currentEnvironment = Object.Instantiate(_currentLevel.Environment);
        }

        public void SetupAnomaly()
        {
            var tempList = new List<EInteractableIds>(_currentLevel.AnomaliesIds);
            var easyObjects = SelectRandomObjects(tempList, _currentLevel.EasyCount);
            var mediumObjects = SelectRandomObjects(tempList, _currentLevel.MediumCount);
            var hardObjects = SelectRandomObjects(tempList, _currentLevel.HardCount);

            ActivateAnomaly(easyObjects, Difficulty.Easy);
            ActivateAnomaly(mediumObjects, Difficulty.Medium);
            ActivateAnomaly(hardObjects, Difficulty.Hard);
        }

        private void ActivateAnomaly(List<EInteractableIds> easyObjects, Difficulty difficulty)
        {
            foreach (var id in easyObjects)
            {
                _currentEnvironment.ActivateAnomaly(id, difficulty);
            }
        }

        private List<EInteractableIds> SelectRandomObjects(List<EInteractableIds> ids, int objectCount)
        {
            if (objectCount > ids.Count)
            {
                Debug.LogError("Objects in source list not enough");
                return null;
            }
            
            var resultList = new List<EInteractableIds>();

            for (var i = 0; i < objectCount; i++)
            {
                var randomIndex = Random.Range(0, ids.Count);

                resultList.Add(ids[randomIndex]);
                
                ids.Remove(ids[randomIndex]);
            }

            return resultList;
        }

        public void FinishLevel()
        {
            if (_currentEnvironment != null)
            {
                Object.Destroy(_currentEnvironment.gameObject);
                _currentEnvironment = null;
            }

            _currentLevel = null;
        }
    }
}
