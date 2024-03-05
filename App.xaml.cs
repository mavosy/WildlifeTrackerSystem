using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WTS.Services;
using WTS.Services.Interfaces;
using WTS.ViewModels;
using WTS.Views;

namespace WTS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();

            services.AddTransient<WTSViewModel>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var wtsView = new WTSView
            {
                DataContext = ServiceProvider.GetRequiredService<WTSViewModel>()
            };
            wtsView.Show();

            base.OnStartup(e);
        }
    }
}