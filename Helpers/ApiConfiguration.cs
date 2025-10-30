namespace Phanteon.Helpers
{
    public static class ApiConfiguration
    {
        // URL base de la API
        // Para desarrollo local en Android emulator, usa: https://10.0.2.2:7026
        // Para dispositivo físico en la misma red: https://[TU_IP_LOCAL]:7026
        // Para producción: https://tu-dominio.com
        public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";

        // Timeout para las peticiones HTTP
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
