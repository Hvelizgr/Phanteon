using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    public interface IHistorialDispositivosService
    {
        [Get("/api/historialdispositivos/getall")]
        Task<List<HistorialDispositivo>> GetAllHistorialDispositivosAsync();

        [Get("/api/historialdispositivos/getbyid/{id}")]
        Task<HistorialDispositivo> GetHistorialDispositivoByIdAsync(int id);

        [Post("/api/historialdispositivos/post")]
        Task<HistorialDispositivo> CreateHistorialDispositivoAsync([Body] HistorialDispositivo historial);
    }
}
