using ImagesToVideoCrafter_Options;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.Model
{
    public interface IAdapter
    {
        public Task<string> LaunchCraft(ImagesToVideoCrafterOptions? crafterOptions,
            Action<string>? printInfoAction,
            Action<string>? printWarningAction,
            Action<string>? printDebugAction);
    }
}
