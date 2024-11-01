using ImagesToVideoCrafter_DesktopGUI.Core;
using ImagesToVideoCrafter_DesktopGUI.MVVM.View;
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

            services.AddSingleton<MainView>(provider => new MainView
            {
                DataContext = provider.GetRequiredService<MainViewModel>(),
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<Func<Type, ViewModel>>
                (serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));
            services.AddSingleton<INavigation, Navigation>();





            _serviceProvider = services.BuildServiceProvider();




            Startup += Application_Startup;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainView>();


            mainWindow.Show();
        }
    }

}
