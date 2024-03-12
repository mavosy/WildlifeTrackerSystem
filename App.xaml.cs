using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WTS.Services;
using WTS.Services.Interfaces;
using WTS.Validators;
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

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            var viewModelLocator = new ViewModelLocator(ServiceProvider);
            Resources.Add("ViewModelLocator", viewModelLocator);

            var wtsView = new WTSView();
            wtsView.Show();

            base.OnStartup(e);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IAnimalManager, AnimalManager>();
            services.AddTransient<GeneralAnimalValidator>();

            services.AddTransient<WTSViewModel>();
        }
    }
}