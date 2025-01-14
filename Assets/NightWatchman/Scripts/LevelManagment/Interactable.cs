using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NightWatchman
{
    public class Interactable : MonoBehaviour
    {
        public EInteractableIds ID => _id;
        public InteractableState State { get; private set; }
        public Difficulty Difficulty { get; private set; }

        public bool IsAnomaly => Difficulty != Difficulty.None;
        
        [SerializeField] private List<DifficultyData> _data;
        [SerializeField] private EInteractableIds _id;
        
        private DifficultyData _currentData;

        private void Start()
        {
            SetData(Difficulty.Easy);
        }
        
        public void SetData(Difficulty difficulty)
        {
            Difficulty = difficulty;
            var data = _data.FirstOrDefault(x => x.Difficulty == difficulty);
            if (data == null)
            {
                throw new UnityException($"Data with type {difficulty} not found");
            }

            _currentData?.GameObject.SetActive(false);

            _currentData = data;
            _currentData.GameObject.SetActive(true);
        }
        
        public void ChangeState(InteractableState state)
        {
            State = state;
            //change graphics
        }
    }

    [Serializable]
    public class DifficultyData
    {
        public Difficulty Difficulty;
        public GameObject GameObject;
    }

    public enum InteractableState
    {
        Default,
        InProcess,
        Selected,
        Disable
    }

    public enum Difficulty
    {
        None,
        Easy,
        Medium,
        Hard
    }
}