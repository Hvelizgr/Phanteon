using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    /// <summary>
    /// Servicio Refit para la API de Alertas
    /// Los métodos marcados con [FUTURO] están listos pero la API no los soporta todavía
    /// </summary>
    public interface IAlertasService
    {
        /// <summary>
        /// Obtiene todas las alertas
        /// API: GET /api/alertas/getall
        /// </summary>
        [Get("/api/alertas/getall")]
        Task<List<Alerta>> GetAllAlertasAsync();

        /// <summary>
        /// Obtiene una alerta por ID
        /// API: GET /api/alertas/getbyid/{id}
        /// </summary>
        [Get("/api/alertas/getbyid/{id}")]
        Task<Alerta> GetAlertaByIdAsync(int id);

        /// <summary>
        /// Crea una nueva alerta
        /// API: POST /api/alertas/post
        /// </summary>
        [Post("/api/alertas/post")]
        Task<Alerta> CreateAlertaAsync([Body] Alerta alerta);

        /// <summary>
        /// [FUTURO] Actualiza una alerta existente
        /// API: PUT /api/alertas/put/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Put("/api/alertas/put/{id}")]
        Task<Alerta> UpdateAlertaAsync(int id, [Body] Alerta alerta);

        /// <summary>
        /// [FUTURO] Elimina una alerta por ID
        /// API: DELETE /api/alertas/delete/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Delete("/api/alertas/delete/{id}")]
        Task DeleteAlertaAsync(int id);
    }
}
