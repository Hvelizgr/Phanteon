using System.Globalization;

namespace Phanteon.Resources.Converters
{
    public class FiltroSelectedConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string currentFilter && parameter is string filterValue)
            {
                return currentFilter.Equals(filterValue, StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb("#512DA8") // Primary color cuando está seleccionado
                    : Color.FromArgb("#999999"); // Gris cuando no está seleccionado
            }

            return Color.FromArgb("#999999");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
