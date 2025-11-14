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
        [Get("/api/alertas")]
        Task<List<Alerta>> GetAlertasAsync();

        /// <summary>
        /// Obtiene alertas por dispositivo
        /// </summary>
        [Get("/api/alertas/dispositivo/{dispositivoId}")]
        Task<List<Alerta>> GetAlertasPorDispositivoAsync(int dispositivoId);

        /// <summary>
        /// Obtiene una alerta por ID
        /// </summary>
        [Get("/api/alertas/{id}")]
        Task<Alerta> GetAlertaAsync(int id);

        /// <summary>
        /// Crea una nueva alerta
        /// </summary>
        [Post("/api/alertas")]
        Task<Alerta> CreateAlertaAsync([Body] Alerta alerta);

        /// <summary>
        /// Marca una alerta como leída
        /// </summary>
        [Put("/api/alertas/{id}/marcar-leida")]
        Task MarcarAlertaLeidaAsync(int id);

        /// <summary>
        /// Elimina una alerta
        /// </summary>
        [Delete("/api/alertas/{id}")]
        Task DeleteAlertaAsync(int id);
    }
}
