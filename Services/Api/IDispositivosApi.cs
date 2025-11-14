using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Api
{
    /// <summary>
    /// API de Dispositivos usando Refit
    /// </summary>
    public interface IDispositivosApi
    {
        /// <summary>
        /// Obtiene todos los dispositivos
        /// </summary>
        [Get("/api/dispositivos/getall")]
        Task<List<Dispositivo>> GetDispositivosAsync();

        /// <summary>
        /// Obtiene un dispositivo por ID
        /// </summary>
        [Get("/api/dispositivos/getbyid/{id}")]
        Task<Dispositivo> GetDispositivoAsync(int id);

        /// <summary>
        /// Crea un nuevo dispositivo
        /// </summary>
        [Post("/api/dispositivos/post")]
        Task<Dispositivo> CreateDispositivoAsync([Body] Dispositivo dispositivo);

        /// <summary>
        /// Actualiza un dispositivo existente
        /// </summary>
        [Put("/api/dispositivos/{id}")]
        Task<Dispositivo> UpdateDispositivoAsync(int id, [Body] Dispositivo dispositivo);

        /// <summary>
        /// Elimina un dispositivo
        /// </summary>
        [Delete("/api/dispositivos/{id}")]
        Task DeleteDispositivoAsync(int id);

        /// <summary>
        /// Obtiene el historial de un dispositivo
        /// </summary>
        [Get("/api/dispositivos/{id}/historial")]
        Task<List<HistorialDispositivo>> GetHistorialAsync(int id);
    }
}
