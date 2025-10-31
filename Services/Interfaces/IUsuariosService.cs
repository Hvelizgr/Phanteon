using Phanteon.Models;
using Refit;

namespace Phanteon.Services.Interfaces
{
    /// <summary>
    /// Servicio Refit para la API de Usuarios
    /// Los métodos marcados con [FUTURO] están listos pero la API no los soporta todavía
    /// </summary>
    public interface IUsuariosService
    {
        /// <summary>
        /// Obtiene todos los usuarios
        /// API: GET /api/usuarios/getall
        /// </summary>
        [Get("/api/usuarios/getall")]
        Task<List<Usuario>> GetAllUsuariosAsync();

        /// <summary>
        /// Obtiene un usuario por ID
        /// API: GET /api/usuarios/getbyid/{id}
        /// </summary>
        [Get("/api/usuarios/getbyid/{id}")]
        Task<Usuario> GetUsuarioByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario (registro)
        /// API: POST /api/usuarios/post
        /// </summary>
        [Post("/api/usuarios/post")]
        Task<Usuario> CreateUsuarioAsync([Body] Usuario usuario);

        /// <summary>
        /// [FUTURO] Actualiza un usuario existente
        /// API: PUT /api/usuarios/put/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Put("/api/usuarios/put/{id}")]
        Task<Usuario> UpdateUsuarioAsync(int id, [Body] Usuario usuario);

        /// <summary>
        /// [FUTURO] Elimina un usuario por ID
        /// API: DELETE /api/usuarios/delete/{id}
        /// NOTA: La API aún no implementa este endpoint
        /// </summary>
        [Delete("/api/usuarios/delete/{id}")]
        Task DeleteUsuarioAsync(int id);
    }
}
