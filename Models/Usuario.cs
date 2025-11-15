namespace Phanteon.Models
{
    /// <summary>
    /// Modelo de usuario del sistema
    /// Sincronizado con la API externa
    /// </summary>
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
