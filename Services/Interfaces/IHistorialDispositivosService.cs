using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    /// <summary>
    /// Servicio Refit para la API de Historial de Dispositivos
    /// Los métodos marcados con [FUTURO] están listos pero la API no los soporta todavía
    /// </summary>
    public interface IHistorialDispositivosService
    {
        /// <summary>
        /// Obtiene todo el historial de dispositivos
        /// API: GET /api/historialdispositivos/getall
        /// </summary>
        [Get("/api/historialdispositivos/getall")]
        Task<List<HistorialDispositivo>> GetAllHistorialDispositivosAsync();

        /// <summary>
        /// Obtiene un registro de historial por ID
        /// API: GET /api/historialdispositivos/getbyid/{id}
        /// </summary>
        [Get("/api/historialdispositivos/getbyid/{id}")]
        Task<HistorialDispositivo> GetHistorialDispositivoByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo registro en el historial
        /// API: POST /api/historialdispositivos/post
        /// </summary>
        [Post("/api/historialdispositivos/post")]
        Task<HistorialDispositivo> CreateHistorialDispositivoAsync([Body] HistorialDispositivo historial);

        /// <summary>
        /// [FUTURO] Actualiza un registro de historial existente
        /// API: PUT /api/historialdispositivos/put/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Put("/api/historialdispositivos/put/{id}")]
        Task<HistorialDispositivo> UpdateHistorialDispositivoAsync(int id, [Body] HistorialDispositivo historial);

        /// <summary>
        /// [FUTURO] Elimina un registro de historial por ID
        /// API: DELETE /api/historialdispositivos/delete/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Delete("/api/historialdispositivos/delete/{id}")]
        Task DeleteHistorialDispositivoAsync(int id);
    }
}
