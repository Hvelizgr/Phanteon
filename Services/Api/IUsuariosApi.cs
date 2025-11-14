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
        [Get("/api/usuarios/getall")]
        Task<List<Usuario>> GetUsuariosAsync();

        /// <summary>
        /// Obtiene un usuario por ID
        /// </summary>
        [Get("/api/usuarios/getbyid/{id}")]
        Task<Usuario> GetUsuarioAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [Post("/api/usuarios/post")]
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

        /// <summary>
        /// Login de usuario
        /// </summary>
        [Post("/api/usuarios/login")]
        Task<Usuario> LoginAsync([Body] LoginRequest request);
    }

    /// <summary>
    /// Request para login
    /// </summary>
    public class LoginRequest
    {
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
