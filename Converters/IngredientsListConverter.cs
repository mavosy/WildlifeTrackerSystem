using System.Globalization;
using System.Windows.Data;
using WTS.Services;

namespace WTS.Converters
{
    public class IngredientsListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ListManager<string> listManager)
            {
                return string.Join(", ", listManager.ToStringList());
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
