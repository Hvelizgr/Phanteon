namespace Phanteon.Services.Auth
{
    /// <summary>
    /// Gestiona el estado de la sesión del usuario (SRP: Una sola responsabilidad - gestionar sesión)
    /// </summary>
    public interface ISessionManager
    {
        Task<bool> HasActiveSessionAsync();
        Task<string?> GetUserIdAsync();
        Task<string?> GetUserNameAsync();
        Task<string?> GetUserEmailAsync();
        Task<string?> GetUserRoleAsync();
        Task SaveSessionAsync(string userId, string userName, string userEmail, string userRole);
        Task ClearSessionAsync();
    }
}
