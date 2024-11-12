
using ImagesToVideoCrafter_Core;
using ImagesToVideoCrafter_Options;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.Model
{
    public class Adapter : IAdapter
    {
        public Adapter()
        {
        }

        public Task<string> LaunchCraft(ImagesToVideoCrafterOptions? crafterOptions,
            Action<string>? printInfoAction,
            Action<string>? printWarningAction,
            Action<string>? printDebugAction)
        {
            var task = Task<string>.Run(() =>
            {
                var crafter = new Crafter(crafterOptions);
                string FullFileName = crafter.Craft(
                    printInfoAction: printInfoAction,
                    printWarningAction: printWarningAction,
                    printDebugAction: printDebugAction
                    );
                return FullFileName;
            });
            return task;
        }
    }
}
