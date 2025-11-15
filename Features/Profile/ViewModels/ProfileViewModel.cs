using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Auth;
using Phanteon.Services.Storage;

namespace Phanteon.Features.Profile.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly ISecureStorageService _secureStorageService;

        [ObservableProperty]
        private string userName = string.Empty;

        [ObservableProperty]
        private string userEmail = string.Empty;

        [ObservableProperty]
        private string userRole = string.Empty;

        [ObservableProperty]
        private string userId = string.Empty;

        [ObservableProperty]
        private string userInitials = string.Empty;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private int daysActive;

        [ObservableProperty]
        private int devicesCount;

        [ObservableProperty]
        private int sessionsCount;

        [ObservableProperty]
        private string appVersion = string.Empty;

        public ProfileViewModel(IAuthService authService, ISecureStorageService secureStorageService)
        {
            _authService = authService;
            _secureStorageService = secureStorageService;

            // Obtener versión de la app
            AppVersion = $"Versión {AppInfo.VersionString} (Build {AppInfo.BuildString})";

            // Cargar datos del usuario
            LoadUserData();
        }

        private async void LoadUserData()
        {
            IsLoading = true;

            try
            {
                var id = await _authService.GetCurrentUserIdAsync();
                var name = await _authService.GetCurrentUserNameAsync();
                var email = await _authService.GetCurrentUserEmailAsync();
                var role = await _authService.GetCurrentUserRoleAsync();

                UserId = id ?? "N/A";
                UserName = name ?? "Usuario";
                UserEmail = email ?? "email@ejemplo.com";
                UserRole = role ?? "Usuario";

                // Calcular iniciales
                UserInitials = GetInitials(UserName);

                // Cargar estadísticas (datos de ejemplo - puedes conectarlos con tu API)
                await LoadStatisticsAsync();
            }
            catch (Exception ex)
            {
                UserName = "Error al cargar";
                UserEmail = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadStatisticsAsync()
        {
            try
            {

                var registrationDateString = await _secureStorageService.GetAsync("RegistrationDate");

                if (!string.IsNullOrEmpty(registrationDateString) && DateTime.TryParse(registrationDateString, out var registrationDate))
                {
                    DaysActive = (DateTime.Now - registrationDate).Days;
                }
                else
                {
                    await _secureStorageService.SetAsync("RegistrationDate", DateTime.Now.ToString("O"));
                    DaysActive = 0;
                }

                // TODO: Reemplazar con datos reales de tu API
                DevicesCount = 3;
                SessionsCount = 24;
            }
            catch
            {
                // Valores por defecto en caso de error
                DaysActive = 0;
                DevicesCount = 0;
                SessionsCount = 0;
            }
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            var confirmed = false;

            if (Application.Current?.Windows.Count > 0)
            {
                confirmed = await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Cerrar Sesión",
                    "¿Estás seguro de que deseas cerrar sesión?",
                    "Sí",
                    "No");
            }

            if (confirmed)
            {
                IsLoading = true;
                await _authService.LogoutAsync();
            }
        }

        [RelayCommand]
        private async Task EditProfileAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Editar Perfil",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Cambiar Contraseña",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        [RelayCommand]
        private async Task ViewSettingsAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Configuración",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        [RelayCommand]
        private async Task ViewNotificationsAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Notificaciones",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        private string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "U";

            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return parts[0].Substring(0, Math.Min(2, parts[0].Length)).ToUpper();

            return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
        }
    }
}
