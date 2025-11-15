using Phanteon.Services.Storage;

namespace Phanteon.Services.Auth
{
    /// <summary>
    /// Implementación de ISessionManager (SRP: Solo gestiona el almacenamiento de sesión)
    /// </summary>
    public class SessionManager : ISessionManager
    {
        private readonly ISecureStorageService _secureStorageService;

        private const string UserIdKey = "user_id";
        private const string UserNameKey = "user_name";
        private const string UserEmailKey = "user_email";
        private const string UserRoleKey = "user_role";

        public SessionManager(ISecureStorageService secureStorageService)
        {
            _secureStorageService = secureStorageService;
        }

        public async Task<bool> HasActiveSessionAsync()
        {
            var userId = await _secureStorageService.GetAsync(UserIdKey);
            return !string.IsNullOrEmpty(userId);
        }

        public async Task<string?> GetUserIdAsync()
        {
            return await _secureStorageService.GetAsync(UserIdKey);
        }

        public async Task<string?> GetUserNameAsync()
        {
            return await _secureStorageService.GetAsync(UserNameKey);
        }

        public async Task<string?> GetUserEmailAsync()
        {
            return await _secureStorageService.GetAsync(UserEmailKey);
        }

        public async Task<string?> GetUserRoleAsync()
        {
            return await _secureStorageService.GetAsync(UserRoleKey);
        }

        public async Task SaveSessionAsync(string userId, string userName, string userEmail, string userRole)
        {
            await _secureStorageService.SetAsync(UserIdKey, userId);
            await _secureStorageService.SetAsync(UserNameKey, userName);
            await _secureStorageService.SetAsync(UserEmailKey, userEmail);
            await _secureStorageService.SetAsync(UserRoleKey, userRole);
        }

        public Task ClearSessionAsync()
        {
            _secureStorageService.Remove(UserIdKey);
            _secureStorageService.Remove(UserNameKey);
            _secureStorageService.Remove(UserEmailKey);
            _secureStorageService.Remove(UserRoleKey);
            return Task.CompletedTask;
        }
    }
}
