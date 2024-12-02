using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using System.Windows.Threading;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel
{
    public class LogViewModel : Core.ViewModel
    {
        private IGuiInstance _guiInstance;

        public Action<string> LogAppendTextAction { get; set; }

        private Dispatcher _currentDispatcher;
        public Dispatcher CurrentDispatcher
        {
            get => _currentDispatcher;
            set
            {
                _currentDispatcher = value;
                OnPropertyChanged(nameof(CurrentDispatcher));
            }
        }

        private string logText;
        public string LogText
        {
            get => logText;
            set
            {
                logText = value;
                OnPropertyChanged(nameof(LogText));
            }
        }

        private void SetCommands()
        {

        }

        public LogViewModel(INavigation navigationService, IAdapter adapter, IGuiInstance guiInstance, Dispatcher dispatcher)
        {
            CurrentDispatcher = dispatcher;
            _guiInstance = guiInstance;

            SetCommands();

            _guiInstance.AddLogAction(s =>
            {
                CurrentDispatcher.Invoke(() =>
                {
                    LogAppendTextAction?.Invoke('\n' + s);
                });
            });
        }

    }
}
