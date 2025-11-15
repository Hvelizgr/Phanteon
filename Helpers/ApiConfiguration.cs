namespace Phanteon.Helpers
{
    /// <summary>
    /// Configuración centralizada de la API REST
    /// </summary>
    public static class ApiConfiguration
    {
        /// <summary>
        /// URL base de la API desplegada en Azure
        /// </summary>
        public static string BaseUrl { get; set; } = "https://webapidevices-bnekc4e9a8dtarep.eastus2-01.azurewebsites.net";

        /// <summary>
        /// Timeout para peticiones HTTP (30 segundos)
        /// </summary>
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
