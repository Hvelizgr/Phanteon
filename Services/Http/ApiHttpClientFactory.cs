using Phanteon.Helpers;

namespace Phanteon.Services.Http
{
    /// <summary>
    /// Implementación del factory de HttpClient
    /// Centraliza la configuración del HttpClient para toda la aplicación
    /// </summary>
    public class ApiHttpClientFactory : IApiHttpClientFactory
    {
        private readonly HttpMessageHandler _messageHandler;

        public ApiHttpClientFactory()
        {
#if DEBUG
            // En desarrollo, aceptar certificados SSL auto-firmados
            _messageHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
#else
            // En producción, usar validación SSL estándar
            _messageHandler = new HttpClientHandler();
#endif
        }

        public HttpClient CreateClientForGet()
        {
            return CreateConfiguredClient();
        }

        public HttpClient CreateClientForPost()
        {
            return CreateConfiguredClient();
        }

        public HttpClient CreateClientForUpdate()
        {
            return CreateConfiguredClient();
        }

        public HttpClient CreateClientForDelete()
        {
            return CreateConfiguredClient();
        }

        public HttpMessageHandler CreateMessageHandler()
        {
            return _messageHandler;
        }

        /// <summary>
        /// Crea y configura un HttpClient con los valores base de la aplicación
        /// </summary>
        private HttpClient CreateConfiguredClient()
        {
            var client = new HttpClient(_messageHandler, disposeHandler: false)
            {
                BaseAddress = new Uri(ApiConfiguration.BaseUrl),
                Timeout = ApiConfiguration.Timeout
            };

            // Agregar headers comunes si son necesarios
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
    }
}
