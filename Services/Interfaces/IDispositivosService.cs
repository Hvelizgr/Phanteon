using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    /// <summary>
    /// Servicio Refit para la API de Dispositivos
    /// Los métodos marcados con [FUTURO] están listos pero la API no los soporta todavía
    /// </summary>
    public interface IDispositivosService
    {
        /// <summary>
        /// Obtiene todos los dispositivos
        /// API: GET /api/dispositivos/getall
        /// </summary>
        [Get("/api/dispositivos/getall")]
        Task<List<Dispositivo>> GetAllDispositivosAsync();

        /// <summary>
        /// Obtiene un dispositivo por ID
        /// API: GET /api/dispositivos/getbyid/{id}
        /// </summary>
        [Get("/api/dispositivos/getbyid/{id}")]
        Task<Dispositivo> GetDispositivoByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo dispositivo
        /// API: POST /api/dispositivos/post
        /// </summary>
        [Post("/api/dispositivos/post")]
        Task<Dispositivo> CreateDispositivoAsync([Body] Dispositivo dispositivo);

        /// <summary>
        /// [FUTURO] Actualiza un dispositivo existente
        /// API: PUT /api/dispositivos/put/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Put("/api/dispositivos/put/{id}")]
        Task<Dispositivo> UpdateDispositivoAsync(int id, [Body] Dispositivo dispositivo);

        /// <summary>
        /// [FUTURO] Elimina un dispositivo por ID
        /// API: DELETE /api/dispositivos/delete/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Delete("/api/dispositivos/delete/{id}")]
        Task DeleteDispositivoAsync(int id);
    }
}
