namespace Phanteon.Helpers
{
    public static class ApiConfiguration
    {
        // URL base de la API desplegada en Azure
        // API de producción en Azure Web Services
        public static string BaseUrl { get; set; } = "https://webapidevices-bnekc4e9a8dtarep.eastus2-01.azurewebsites.net";

        // Timeout para las peticiones HTTP
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
