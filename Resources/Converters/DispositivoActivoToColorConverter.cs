using System.Globalization;

namespace Phanteon.Resources.Converters
{
    public class DispositivoActivoToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string activo)
            {
                return activo.Equals("Si", StringComparison.OrdinalIgnoreCase) ||
                       activo.Equals("Activo", StringComparison.OrdinalIgnoreCase)
                    ? Color.FromArgb("#4CAF50") // Verde para activo
                    : Color.FromArgb("#F44336"); // Rojo para inactivo
            }

            return Color.FromArgb("#999999"); // Gris por defecto
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
