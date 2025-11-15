using System.Globalization;

namespace Phanteon.Resources.Converters
{
    public class BoolToEyeIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue ? "\uf070" : "\uf06e"; // eye-slash : eye

            return "\uf06e"; // eye por defecto
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
