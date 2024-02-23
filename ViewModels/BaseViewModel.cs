using PropertyChanged;

namespace WTS.ViewModels
{
    /// <summary>
    /// BaseViewModel is an abstract class that provides a common foundation for all ViewModel classes.
    /// It includes support for INPC, property change notifications through the FODY nuget, which simplifies data binding and notifications for properties in MVVM architecture.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel
    {
        /// <summary>
        /// Creates a unique ID for each class instance that inherits from BaseViewModel. This allows for the identification of individual instances of classes, for example, using Debug.Writeline.
        /// </summary>
        public string InstanceID { get; set; } = Guid.NewGuid().ToString();
    }
}