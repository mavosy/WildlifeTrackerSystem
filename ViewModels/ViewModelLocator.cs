using Microsoft.Extensions.DependencyInjection;

namespace WTS.ViewModels
{
    /// <summary>
    /// Provides access to ViewModel instances using a given IServiceProvider.
    /// </summary>
    public class ViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator with a specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use for getting ViewModel instances.</param>
        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public WTSViewModel WTSViewModel => _serviceProvider.GetRequiredService<WTSViewModel>();
    }
}