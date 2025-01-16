using UnityEngine;

namespace NightWatchman
{
    public class Starter : MonoBehaviour
    {
        private void Awake()
        {
            var resourceManager = CompositionRoot.GetResourceManager();
            var playerController = CompositionRoot.GetPlayerController();
            var levelService = CompositionRoot.GetLevelService();
            var core = CompositionRoot.GetCore();
            var uiRoot = CompositionRoot.GetUIRoot();
        }
    }
}