
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
            Action<int, int>? setProgressCountAction,
            Action<string>? printInfoAction,
            Action<string>? printWarningAction,
            Action<string>? printDebugAction)
        {
            var task = Task<string>.Run(() =>
            {
                var crafter = new Crafter(crafterOptions);
                string FullFileName = crafter.Craft(
                    setProgressCountAction: setProgressCountAction,
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
