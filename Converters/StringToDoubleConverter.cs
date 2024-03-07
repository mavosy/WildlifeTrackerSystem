using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WTS.Converters
{
    /// <summary>
    /// Converts between string and double types for data binding.
    /// </summary>
    public class StringToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Converts a double value to a string.
        /// </summary>
        /// <returns>String representation of the double value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"Conversion error in the data binding, from double to string. Error: {ex}");
            }
        }

        /// <summary>
        /// Converts a string back to a double value.
        /// </summary>
        /// <returns>The double value parsed from the string or UnsetValue (which triggers FluentValidation instead) for an invalid input.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return targetType == typeof(double?) ? null : 0;
                }

                if (double.TryParse(input, NumberStyles.Any, culture, out double doubleResult))
                {
                    return doubleResult;
                }
                return DependencyProperty.UnsetValue;
            }
            return null;
        }
    }
}