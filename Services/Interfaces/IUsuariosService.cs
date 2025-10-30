using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    public interface IUsuariosService
    {
        [Get("/api/usuarios/getall")]
        Task<List<Usuario>> GetAllUsuariosAsync();

        [Get("/api/usuarios/getbyid/{id}")]
        Task<Usuario> GetUsuarioByIdAsync(int id);

        [Post("/api/usuarios/post")]
        Task<Usuario> CreateUsuarioAsync([Body] Usuario usuario);
    }
}
