using System.Globalization;

namespace Phanteon.Resources.Converters
{
    /// <summary>
    /// Convierte un string a bool (true si no está vacío)
    /// Uso: <Label IsVisible="{Binding ErrorMessage, Converter={StaticResource StringNotEmptyConverter}}" />
    /// </summary>
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
