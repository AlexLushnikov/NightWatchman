using UnityEngine;

namespace NightWatchman
{
    public class Starter : MonoBehaviour
    {
        private async void Awake()
        {
            var resourceManager = CompositionRoot.GetResourceManager();
            resourceManager.GetPrefab<PlayerController>(EPrefabs.Player);
        }
    }
}