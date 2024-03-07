using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace WTS.Converters
{
    /// <summary>
    /// Converts validation errors to a string for display purposes.
    /// </summary>
    public class FirstValidationErrorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the first or all validation errors to a string.
        /// </summary>
        /// <returns>String representation of the first or concatenated validation errors.</returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ReadOnlyObservableCollection<ValidationError> errors || !errors.Any())
                return string.Empty;

            bool showAllErrors = Equals(parameter, "All");
            return showAllErrors
                   ? string.Join("\n", errors.Select(e => e.ErrorContent.ToString()))
                   : errors[0].ErrorContent.ToString();
        }

        /// <summary>
        /// Throws NotSupportedException as conversion back is not supported.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Conversion back is not supported.");
        }
    }
}