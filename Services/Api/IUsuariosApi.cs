using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Api
{
    /// <summary>
    /// API de Usuarios usando Refit
    /// </summary>
    public interface IUsuariosApi
    {
        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        [Get("/api/usuarios")]
        Task<List<Usuario>> GetUsuariosAsync();

        /// <summary>
        /// Obtiene un usuario por ID
        /// </summary>
        [Get("/api/usuarios/{id}")]
        Task<Usuario> GetUsuarioAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [Post("/api/usuarios")]
        Task<Usuario> CreateUsuarioAsync([Body] Usuario usuario);

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        [Put("/api/usuarios/{id}")]
        Task<Usuario> UpdateUsuarioAsync(int id, [Body] Usuario usuario);

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        [Delete("/api/usuarios/{id}")]
        Task DeleteUsuarioAsync(int id);
    }
}
