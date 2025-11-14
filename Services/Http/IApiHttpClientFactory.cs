namespace Phanteon.Services.Http
{
    /// <summary>
    /// Interfaz para el factory de HttpClient personalizado
    /// Proporciona métodos para crear clientes HTTP configurados
    /// </summary>
    public interface IApiHttpClientFactory
    {
        /// <summary>
        /// Crea un HttpClient configurado para solicitudes GET
        /// </summary>
        HttpClient CreateClientForGet();

        /// <summary>
        /// Crea un HttpClient configurado para solicitudes POST
        /// </summary>
        HttpClient CreateClientForPost();

        /// <summary>
        /// Crea un HttpClient configurado para solicitudes PUT
        /// </summary>
        HttpClient CreateClientForUpdate();

        /// <summary>
        /// Crea un HttpClient configurado para solicitudes DELETE
        /// </summary>
        HttpClient CreateClientForDelete();

        /// <summary>
        /// Crea un HttpMessageHandler configurado (para Refit)
        /// </summary>
        HttpMessageHandler CreateMessageHandler();
    }
}
