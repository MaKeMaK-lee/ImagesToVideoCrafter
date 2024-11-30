using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using ImagesToVideoCrafter_DesktopGUI.MVVM.Model;
using ImagesToVideoCrafter_DesktopGUI.MVVM.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace ImagesToVideoCrafter_DesktopGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<LogViewModel>(provider => new LogViewModel(
                provider.GetRequiredService<INavigation>(),
                provider.GetRequiredService<IAdapter>(),
                provider.GetRequiredService<IGuiInstance>(),
                Dispatcher
                ));
            services.AddSingleton<Func<Type, ViewModel>>
                (serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<INavigation, Navigation>();

            services.AddSingleton<IAdapter, Adapter>();

            services.AddSingleton<IGuiInstance, GuiInstance>();





            _serviceProvider = services.BuildServiceProvider();




            Startup += Application_Startup;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
        }
    }

}
