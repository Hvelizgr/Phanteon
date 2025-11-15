using System.Globalization;

namespace Phanteon.Resources.Converters
{
    public class SeveridadToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string severidad)
            {
                return severidad.ToLower() switch
                {
                    "alta" or "high" or "critica" or "critical" => Color.FromArgb("#F44336"), // Rojo
                    "media" or "medium" or "moderada" => Color.FromArgb("#FF9800"), // Naranja
                    "baja" or "low" => Color.FromArgb("#4CAF50"), // Verde
                    _ => Color.FromArgb("#2196F3") // Azul (info)
                };
            }

            return Color.FromArgb("#999999"); // Gris por defecto
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
