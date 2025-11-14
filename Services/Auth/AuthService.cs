using Phanteon.Services.Storage;
using Phanteon.Services.Navigation;

namespace Phanteon.Services.Auth
{
    /// <summary>
    /// Servicio de autenticación para gestionar el estado del usuario
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

    public class AuthService : IAuthService
    {
        private readonly ISecureStorageService _secureStorageService;
        private readonly INavigationService _navigationService;

        public AuthService(ISecureStorageService secureStorageService, INavigationService navigationService)
        {
            _secureStorageService = secureStorageService;
            _navigationService = navigationService;
        }

        public async Task<bool> IsUserLoggedInAsync()
        {
            var userId = await _secureStorageService.GetAsync("user_id");
            return !string.IsNullOrEmpty(userId);
        }

        public async Task<string?> GetCurrentUserIdAsync()
        {
            return await _secureStorageService.GetAsync("user_id");
        }

        public async Task<string?> GetCurrentUserNameAsync()
        {
            return await _secureStorageService.GetAsync("user_name");
        }

        public async Task<string?> GetCurrentUserEmailAsync()
        {
            return await _secureStorageService.GetAsync("user_email");
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            return await _secureStorageService.GetAsync("user_role");
        }

        public async Task LogoutAsync()
        {
            // Limpiar toda la información del usuario
            _secureStorageService.Remove("user_id");
            _secureStorageService.Remove("user_name");
            _secureStorageService.Remove("user_email");
            _secureStorageService.Remove("user_role");

            // Navegar a Login
            await _navigationService.NavigateToAsync("//Login");
        }
    }
}
