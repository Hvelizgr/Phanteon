using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    public interface IDispositivosService
    {
        [Get("/api/dispositivos/getall")]
        Task<List<Dispositivo>> GetAllDispositivosAsync();

        [Get("/api/dispositivos/getbyid/{id}")]
        Task<Dispositivo> GetDispositivoByIdAsync(int id);

        [Post("/api/dispositivos/post")]
        Task<Dispositivo> CreateDispositivoAsync([Body] Dispositivo dispositivo);
    }
}
