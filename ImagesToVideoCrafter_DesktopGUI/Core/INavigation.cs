
namespace ImagesToVideoCrafter_DesktopGUI.Core
{
    public interface INavigation
    {
        ViewModel? CurrentView { get; }

        public void AddNavigationChangedHandler(EventHandler handler);

        void NavigateTo<T>() where T : ViewModel;
    }
}
