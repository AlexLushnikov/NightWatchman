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

        public Transform SpawnPoint => _spawnPoint;

        public void Init()
        {
            foreach (var interactable in _interactableObjects)
            {
                interactable.Init();
            }
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
    }
}