using Phanteon.Services.Navigation;

namespace Phanteon.Services.Auth
{
    /// <summary>
    /// Servicio de autenticación para gestionar operaciones de autenticación
    /// (SRP: Solo gestiona operaciones de autenticación, delegando sesión y navegación)
    /// (ISP: Interfaz segregada - solo operaciones de auth)
    /// </summary>
    public interface IAuthService
    {
        Task<bool> IsUserLoggedInAsync();
        Task<string?> GetCurrentUserIdAsync();
        Task<string?> GetCurrentUserNameAsync();
        Task<string?> GetCurrentUserEmailAsync();
        Task<string?> GetCurrentUserRoleAsync();
        Task LogoutAsync();
    }

    /// <summary>
    /// Implementación de IAuthService
    /// (DIP: Depende de abstracciones ISessionManager e INavigationService)
    /// (OCP: Abierto a extensión - podemos agregar más funcionalidad sin modificar código existente)
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ISessionManager _sessionManager;
        private readonly INavigationService _navigationService;

        public AuthService(ISessionManager sessionManager, INavigationService navigationService)
        {
            _sessionManager = sessionManager;
            _navigationService = navigationService;
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            return await _sessionManager.HasActiveSessionAsync();
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            return await _sessionManager.GetUserIdAsync();
        }

        public async Task<string?> GetCurrentUserNameAsync()
        {
            return await _sessionManager.GetUserNameAsync();
        }

        public async Task<string?> GetCurrentUserEmailAsync()
        {
            return await _sessionManager.GetUserEmailAsync();
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            return await _sessionManager.GetUserRoleAsync();
        }

        public async Task LogoutAsync()
        {
            // Limpiar sesión
            await _sessionManager.ClearSessionAsync();

            // Navegar a Login
            await _navigationService.NavigateToAsync("//Login");
        }
    }
}
