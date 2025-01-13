using UnityEngine;

namespace NightWatchman
{
    public class CompositionRoot : MonoBehaviour
    {
        private static IComposition Composition = new ReleaseComposition();
        //private static IComposition Composition = new DebugComposition();

        private void OnDestroy()
        {
            Composition.Destroy();
        }

        public static IResourceManager GetResourceManager()
        {
            return Composition.GetResourceManager();
        }
    }
}