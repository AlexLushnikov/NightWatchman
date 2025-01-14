using System;
using UnityEngine;

namespace NightWatchman
{
    public class Interactable : MonoBehaviour
    {
        public EInteractableIds ID => _id;
        public InteractableState State { get; private set; }
        public Difficulty Difficulty { get; private set; }

        public bool IsAnomaly => Difficulty != Difficulty.None;
        
        [SerializeField] private GameObject _none;
        [SerializeField] private GameObject _easy;
        [SerializeField] private GameObject _medium;
        [SerializeField] private GameObject _hard;
        
        [SerializeField] private EInteractableIds _id;
        
        private GameObject _currentObject;

        private void Awake()
        {
            SetData(Difficulty.None);
            _easy.SetActive(false);
            _medium.SetActive(false);
            _hard.SetActive(false);
        }
        
        public void SetData(Difficulty difficulty)
        {
            Difficulty = difficulty;
            GameObject data = difficulty switch
            {
                Difficulty.None => _none,
                Difficulty.Easy => _easy,
                Difficulty.Medium => _medium,
                Difficulty.Hard => _hard,
                _ => throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null)
            };

            _currentObject?.SetActive(false);

            _currentObject = data;
            _currentObject.SetActive(true);
        }
        
        public void ChangeState(InteractableState state)
        {
            State = state;
            //change graphics
        }
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