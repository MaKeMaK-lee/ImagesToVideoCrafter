
namespace ImagesToVideoCrafter_DesktopGUI.Core
{
    public class Navigation : ObservableObject, INavigation
    {
        private ViewModel? _currentView;

        private readonly Func<Type, ViewModel> _viewModelFactory;

        private event EventHandler? OnNavigationChanged;

        public void AddNavigationChangedHandler(EventHandler handler)
        {
            OnNavigationChanged += handler;
        }

        public ViewModel? CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public Navigation(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
            OnNavigationChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
