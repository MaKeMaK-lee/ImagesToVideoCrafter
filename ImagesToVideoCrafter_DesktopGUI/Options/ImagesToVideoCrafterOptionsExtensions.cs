using FFMediaToolkit.Encoding;
using ImagesToVideoCrafter_Options;

namespace ImagesToVideoCrafter_DesktopGUI.Options
{
    internal static class ImagesToVideoCrafterOptionsExtensions
    {
        public static ImagesToVideoCrafterOptionsObservable ToOptionsObservable(this ImagesToVideoCrafterOptions options) =>
            ImagesToVideoCrafterOptionsObservable.FromJson(
                options.GetJson())!;
    }
}
