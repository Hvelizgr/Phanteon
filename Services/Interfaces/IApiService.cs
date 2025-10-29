using System.Threading.Tasks;
using Refit;

namespace Phanteon.Services.Interfaces
{
    // Ejemplo mínimo de interfaz Refit
    public interface IApiService
    {
        [Get("/status")]
        Task<ApiStatusDto> GetStatusAsync();
    }

    public class ApiStatusDto
    {
        public string Message { get; set; }
    }
}