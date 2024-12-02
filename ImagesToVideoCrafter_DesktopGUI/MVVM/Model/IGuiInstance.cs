using ImagesToVideoCrafter_DesktopGUI.Options;
using ImagesToVideoCrafter_Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.Model
{
    public interface IGuiInstance : IDisposable
    {
        public void Craft();
        public ImagesToVideoCrafterOptionsObservable CrafterOptions { get; }
        public void AddLogAction(Action<string> action);
        public void AddProgressCountUpdateAction(Action<int, int> action);
    }
}
