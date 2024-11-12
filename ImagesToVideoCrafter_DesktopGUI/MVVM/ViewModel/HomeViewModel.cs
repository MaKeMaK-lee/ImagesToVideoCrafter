using ImagesToVideoCrafter_Core;
using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using ImagesToVideoCrafter_DesktopGUI.Options;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel
{
    public class HomeViewModel : Core.ViewModel
    {
        private IGuiInstance _guiInstance;

        public ImagesToVideoCrafterOptionsObservable Options => _guiInstance.CrafterOptions;

        private void SetCommands()
        {
        }
        public HomeViewModel(INavigation navigationService, IAdapter adapter, IGuiInstance guiInstance)
        {
            _guiInstance = guiInstance;

            SetCommands();
        }


    }
}
