namespace NightWatchman
{
    public interface IViewsFactory
    {
        CoreView GetCoreView();
        MenuView GetMenuView();
    }
}