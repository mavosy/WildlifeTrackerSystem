using CommunityToolkit.Mvvm.Messaging;
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
            services.AddSingleton<IMessenger, StrongReferenceMessenger>();

            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDialogService, DialogService>();
            services.AddTransient<IAnimalManager, AnimalManager>();
            services.AddSingleton<IFoodManager, FoodManager>();
            services.AddTransient<GeneralAnimalValidator>();

            services.AddTransient<WTSViewModel>();
            services.AddTransient<FoodItemViewModel>();
        }
    }
}