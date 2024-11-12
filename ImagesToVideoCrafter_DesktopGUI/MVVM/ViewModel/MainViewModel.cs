using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using System.Windows.Threading;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        public int LogMaxHeight => WindowHeight - contentControlMinHeight - 20;

        private int contentControlMinHeight;
        public int ContentControlMinHeight
        {
            get => contentControlMinHeight;
            set
            {
                contentControlMinHeight = value;
                OnPropertyChanged(nameof(ContentControlMinHeight));
                OnPropertyChanged(nameof(LogMaxHeight));
            }
        }

        private int windowHeight;
        public int WindowHeight
        {
            get => windowHeight;
            set
            {
                windowHeight = value;
                OnPropertyChanged(nameof(WindowHeight));
                OnPropertyChanged(nameof(LogMaxHeight));
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

        private INavigation _navigation;
        public INavigation Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        private IGuiInstance _guiInstance;
        public IGuiInstance GuiInstance
        {
            get => _guiInstance;
            set
            {
                _guiInstance = value;
                OnPropertyChanged(nameof(GuiInstance));
            }
        }

        public Action<string> LogAppendTextAction { get; set; }

        public RelayCommand StartCraftingCommand { get; set; }
        public RelayCommand NavigateToHomeCommand { get; set; }
        private void SetCommands()
        {
            NavigateToHomeCommand = new RelayCommand(o =>
            {
                Navigation.NavigateTo<HomeViewModel>();
            }, o => true);
            StartCraftingCommand = new RelayCommand(o =>
            {
                _guiInstance.Craft();
            }, o => true);
        }
        public MainViewModel(INavigation navigationService, IAdapter adapter, IGuiInstance guiInstance, Dispatcher dispatcher)
        {
            CurrentDispatcher = dispatcher;
            GuiInstance = guiInstance;
            Navigation = navigationService;

            ContentControlMinHeight = 145;

            SetCommands();
            GuiInstance.AddLogAction((o, s) =>
            {
                CurrentDispatcher.Invoke(() =>
                {
                    LogAppendTextAction?.Invoke('\n' + s);
                });
            });

            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
