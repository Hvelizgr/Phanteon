namespace Phanteon.Helpers
{
    public static class ApiConfiguration
    {
        // URL base de la API
        // Para local en Android emulator, usa: http://10.0.2.2:5094
        // Para dispositivo físico en la misma red: http://[TU_IP_LOCAL]:5094
        // Para Windows/Desktop: http://localhost:5094
        public static string BaseUrl { get; set; } = "http://10.0.2.2:5094";

        // Timeout para las peticiones HTTP
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
