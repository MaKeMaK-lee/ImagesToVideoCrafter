using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using System.Windows.Threading;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        private double mainProgressPercent;
        public double MainProgressPercent
        {
            get => mainProgressPercent;
        }

        public void SetMainProgressPercent(int countReady, int countAll)
        {
            mainProgressPercent = ((double)countReady / countAll) * 100;
            OnPropertyChanged(nameof(MainProgressPercent));
        }

        private int windowHeight;
        public int WindowHeight
        {
            get => windowHeight;
            set
            {
                windowHeight = value;
                OnPropertyChanged(nameof(WindowHeight));
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

        public bool IsCheckedNavigateToHomeRadio => Navigation?.CurrentView?.GetType() == typeof(HomeViewModel) ? true : false;
        public bool IsCheckedNavigateToLogRadio => Navigation?.CurrentView?.GetType() == typeof(LogViewModel) ? true : false;

        public RelayCommand StartCraftingCommand { get; set; }
        public RelayCommand NavigateToHomeCommand { get; set; }
        public RelayCommand NavigateToLogCommand { get; set; }

        private void SetCommands()
        {
            NavigateToLogCommand = new RelayCommand(o =>
            {
                Navigation.NavigateTo<LogViewModel>();
            }, o => true);
            NavigateToHomeCommand = new RelayCommand(o =>
            {
                Navigation.NavigateTo<HomeViewModel>();
            }, o => true);
            StartCraftingCommand = new RelayCommand(o =>
            {
                Navigation.NavigateTo<LogViewModel>();
                _guiInstance.Craft();
            }, o => true);
        }

        public MainViewModel(INavigation navigationService, IAdapter adapter, IGuiInstance guiInstance)
        {
            GuiInstance = guiInstance;
            Navigation = navigationService;

            Navigation.AddNavigationChangedHandler((o, e) =>
            {
                OnPropertyChanged(nameof(IsCheckedNavigateToHomeRadio));
                OnPropertyChanged(nameof(IsCheckedNavigateToLogRadio));

            });

            SetCommands();

            GuiInstance.AddProgressCountUpdateAction(SetMainProgressPercent);



            Navigation.NavigateTo<HomeViewModel>();
        }
    }
}
