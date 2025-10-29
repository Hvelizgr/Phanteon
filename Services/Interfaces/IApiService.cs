using System.Threading.Tasks;
using Refit;

namespace Phanteon.Services.Interfaces
{
    // Ejemplo m�nimo de interfaz Refit
    public interface IApiService
    {
        [Get("/status")]
        Task<ApiStatusDto> GetStatusAsync();
    }

    public class ApiStatusDto
    {
        public string Message { get; set; } = string.Empty;
    }
}