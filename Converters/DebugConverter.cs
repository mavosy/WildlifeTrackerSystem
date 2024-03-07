using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace WTS.Converters
{
    /// <summary>
    /// A converter used for debugging purposes that triggers a debugger break.
    /// </summary>
    public class DebugConverter : IValueConverter
    {
        /// <summary>
        /// Triggers a debugger break and returns the passed value.
        /// </summary>
        /// <returns>The original value passed to the method.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }

        /// <summary>
        /// Triggers a debugger break and returns the passed value.
        /// </summary>
        /// <returns>The original value passed to the method.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debugger.Break();
            return value;
        }
    }
}