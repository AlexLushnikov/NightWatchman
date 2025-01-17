using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NightWatchman
{
    public class Environment : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private List<Interactable> _interactableObjects;
        [SerializeField] private Door _door;
        
        public Transform SpawnPoint => _spawnPoint;

        public void SetupDay()
        {
            foreach (var interactable in _interactableObjects)
            {
                interactable.Init();
            }
            
            _door.ChangeState(InteractableState.Default);
        }

        public void ActivateAnomaly(EInteractableIds id, Difficulty difficulty)
        {
            var interactableObject = _interactableObjects.FirstOrDefault(x => x.ID == id);
            if (interactableObject == null)
            {
                Debug.LogError($"Objects with id {id} not found!");
            }
            else
            {
                interactableObject.SetDifficulty(difficulty);
            }
        }

        public void SetupNight()
        {
            foreach (var interactable in _interactableObjects)
            {
                interactable.ChangeState(InteractableState.Default);
            }
        }
    }
}