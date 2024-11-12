using ImagesToVideoCrafter_Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImagesToVideoCrafter_DesktopGUI.Options
{
    public class ImagesToVideoCrafterOptionsObservable : ImagesToVideoCrafterOptions, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public static new ImagesToVideoCrafterOptionsObservable? FromJson(string jsonString)
        {
            return JsonSerializer.Deserialize<ImagesToVideoCrafterOptionsObservable>(jsonString);
        }
    }
}
