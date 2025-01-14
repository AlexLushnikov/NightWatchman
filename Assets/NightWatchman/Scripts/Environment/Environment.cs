using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NightWatchman
{
    public class Environment : MonoBehaviour
    {
        [SerializeField] private List<Interactable> _interactableObjects;

        public void ActivateAnomaly(EInteractableIds id, Difficulty difficulty)
        {
            var interactableObject = _interactableObjects.FirstOrDefault(x => x.ID == id);
            if (interactableObject == null)
            {
                Debug.LogError($"Objects with id {id} not found!");
            }
            else
            {
                interactableObject.SetData(difficulty);
            }
        }
    }
}