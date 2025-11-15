using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Api
{
    /// <summary>
    /// API de Alertas usando Refit
    /// </summary>
    public interface IAlertasApi
    {
        /// <summary>
        /// Obtiene todas las alertas
        /// </summary>
        [Get("/api/alertas/getall")]
        Task<List<Alerta>> GetAlertasAsync();

        /// <summary>
        /// Obtiene alertas por dispositivo (filtrando en el cliente)
        /// </summary>
        [Get("/api/alertas/getall")]
        Task<List<Alerta>> GetAlertasPorDispositivoAsync(int dispositivoId);

        /// <summary>
        /// Obtiene una alerta por ID
        /// </summary>
        [Get("/api/alertas/getbyid/{id}")]
        Task<Alerta> GetAlertaAsync(int id);

        /// <summary>
        /// Crea una nueva alerta
        /// </summary>
        [Post("/api/alertas/post")]
        Task<Alerta> CreateAlertaAsync([Body] Alerta alerta);

        /// <summary>
        /// Marca una alerta como leída (no implementado en backend aún)
        /// </summary>
        [Put("/api/alertas/{id}/marcar-leida")]
        Task MarcarAlertaLeidaAsync(int id);

        /// <summary>
        /// Elimina una alerta (no implementado en backend aún)
        /// </summary>
        [Delete("/api/alertas/{id}")]
        Task DeleteAlertaAsync(int id);
    }
}
