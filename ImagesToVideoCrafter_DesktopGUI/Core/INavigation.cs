
namespace ImagesToVideoCrafter_DesktopGUI.Core
{
    public interface INavigation
    {
        ViewModel? CurrentView { get; }

        void NavigateTo<T>() where T : ViewModel;
    }
}
