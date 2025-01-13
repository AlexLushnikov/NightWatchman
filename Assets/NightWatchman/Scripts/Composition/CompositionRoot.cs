using UnityEngine;

namespace Common
{
    public class CompositionRoot : MonoBehaviour
    {
        private static IComposition Composition = new ReleaseComposition();
        //private static IComposition Composition = new DebugComposition();

        private void OnDestroy()
        {
            Composition.Destroy();
        }
    }
}