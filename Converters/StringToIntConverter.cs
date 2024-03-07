using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WTS.Converters
{
    /// <summary>
    /// Converts between string and int types for data binding.
    /// </summary>
    public class StringToIntConverter : IValueConverter
    {
        /// <summary>
        /// Converts an int value to a string.
        /// </summary>
        /// <returns>String representation of the int value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return value?.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception($"The number could not be converted back to string. Error: {ex}");
            }
        }

        /// <summary>
        /// Converts a string back to an int value.
        /// </summary>
        /// <returns>The int value parsed from the string or UnsetValue for an invalid input.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string input)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return targetType == typeof(int?) ? null : 0;
                }

                if (int.TryParse(input, NumberStyles.Any, culture, out int intResult))
                {
                    return intResult;
                }
                return DependencyProperty.UnsetValue;
            }
            return null;
        }

    }
}