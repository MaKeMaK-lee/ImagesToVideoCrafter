
using Makman.Visual.Core;
using System.Windows;
using ImagesToVideoCrafter_DesktopGUI.Core;

namespace ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {

        private INavigation _navigation;

        public INavigation Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public bool debugMode = false;
        public bool DebugMode
        {
            get => debugMode;
            set
            {
                debugMode = value;
                OnPropertyChanged(nameof(DebugMode));
                OnPropertyChanged(nameof(DebugNavigationVisibility));
            }
        }

        public Visibility DebugNavigationVisibility => debugMode ? Visibility.Visible : Visibility.Collapsed;

        public RelayCommand KeyDownF3Command { get; set; }

        //public RelayCommand NavigateToHomeCommand { get; set; } 

        public MainViewModel(INavigation navigationService)
        {

            Navigation = navigationService;
            //NavigateToHomeCommand = new RelayCommand(o =>
            //{
            //    Navigation.NavigateTo<HomeViewModel>();
            //}, o => true);

            KeyDownF3Command = new RelayCommand(o =>
            {
                if (DebugMode)
                    DebugMode = false;
                else
                    DebugMode = true;
            }, o => true);

            //Navigation.NavigateTo<MainViewModel>();
        }
    }
}
