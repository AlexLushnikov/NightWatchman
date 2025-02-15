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

        public static IPlayer GetPlayerController()
        {
            return Composition.GetPlayerController();
        }

        public static ILevelService GetLevelService()
        {
            return Composition.GetLevelService();
        }

        public static ICore GetCore()
        {
            return Composition.GetCore();
        }

        public static IUIRoot GetUIRoot()
        {
            return Composition.GetUIRoot();
        }

        public static IViewsFactory GetViewsFactory()
        {
            return Composition.GetViewsFactory();
        }

        public static MenuPresenter GetMenuPresenter()
        {
            return Composition.GetMenuPresenter();
        }

        public static IInputHandler GetInputHandler()
        {
            return Composition.GetInputHandler();
        }

        public static ISoundsService GetSoundsService()
        {
            return Composition.GetSoundsService();
        }
    }
}