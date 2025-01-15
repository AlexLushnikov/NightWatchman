using System;
using UnityEngine;

namespace NightWatchman
{
    public class Interactable : MonoBehaviour, IInteractable
    {
        private readonly Color OutlineColor = new (1f, 0.42f, 0f);
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
        private Outline _outline;

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
            
            _easy.SetActive(false);
            _medium.SetActive(false);
            _hard.SetActive(false);
        }

        public void Init()
        {
            SetData(Difficulty.None);
            CreateOutline();
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
        
        private void CreateOutline()
        {
            if (_outline == null)
            {
                _outline = gameObject.AddComponent<Outline>();
                _outline.OutlineColor = OutlineColor;
                _outline.OutlineMode = Outline.Mode.OutlineVisible;
                _outline.enabled = false;
            }
        }
        
        public void SetData(Difficulty difficulty)
        {
            Difficulty = difficulty;
            var data = difficulty switch
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
            if (State == state)
            {
                return;
            }
            
            State = state;
            _outline.enabled = State is InteractableState.Selected or InteractableState.InProcess;
            _outline.OutlineMode = Outline.Mode.OutlineVisible;
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