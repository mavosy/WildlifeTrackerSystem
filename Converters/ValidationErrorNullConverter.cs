using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WTS.Converters
{
    /// <summary>
    /// Converts a collection of validation errors to the content of the first error, or null if no errors.
    /// </summary>
    public class ValidationErrorNullConverter : IValueConverter
    {
        /// <summary>
        /// Converts the first validation error in the collection to its error content, or returns null if there are no errors.
        /// </summary>
        /// <returns>The error content of the first validation error, or null if no errors exist.</returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReadOnlyObservableCollection<ValidationError> errors && errors.Count > 0)
            {
                return errors[0].ErrorContent;
            }
            return null;
        }

        /// <summary>
        /// Conversion back is not implemented and will throw NotImplementedException.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}