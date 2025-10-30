using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    public interface IAlertasService
    {
        [Get("/api/alertas/getall")]
        Task<List<Alerta>> GetAllAlertasAsync();

        [Get("/api/alertas/getbyid/{id}")]
        Task<Alerta> GetAlertaByIdAsync(int id);

        [Post("/api/alertas/post")]
        Task<Alerta> CreateAlertaAsync([Body] Alerta alerta);
    }
}
