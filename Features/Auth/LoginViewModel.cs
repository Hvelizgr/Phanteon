using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Navigation;
using Phanteon.Services.Storage;
using Phanteon.Services.Api;
using System.Text.RegularExpressions;

namespace Phanteon.Features.Auth
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly IUsuariosApi _usuariosApi;

        [ObservableProperty]
        private string nombreUsuario = string.Empty;

        [ObservableProperty]
        private string correo = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool recordarMe;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private bool showPassword;

        public LoginViewModel(
            INavigationService navigationService,
            ISecureStorageService secureStorageService,
            IUsuariosApi usuariosApi)
        {
            _navigationService = navigationService;
            _secureStorageService = secureStorageService;
            _usuariosApi = usuariosApi;

            // Cargar credenciales guardadas si existen
            LoadSavedCredentials();
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            ErrorMessage = null;

            // Validaciones
            if (string.IsNullOrWhiteSpace(NombreUsuario))
            {
                ErrorMessage = "El nombre de usuario es requerido";
                return;
            }

            if (string.IsNullOrWhiteSpace(Correo))
            {
                ErrorMessage = "El correo es requerido";
                return;
            }

            if (!IsValidEmail(Correo))
            {
                ErrorMessage = "El correo no es válido";
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "La contraseña es requerida";
                return;
            }

            IsLoading = true;

            try
            {
                // Crear request de login
                var loginRequest = new LoginRequest
                {
                    NombreUsuario = NombreUsuario,
                    Correo = Correo,
                    Password = Password
                };

                // Intentar login
                var usuario = await _usuariosApi.LoginAsync(loginRequest);

                // Guardar credenciales si "Recordarme" está activado
                try
                {
                    if (RecordarMe)
                    {
                        await _secureStorageService.SetAsync("saved_email", Correo);
                        await _secureStorageService.SetAsync("remember_me", "true");
                    }
                    else
                    {
                        _secureStorageService.Remove("saved_email");
                        _secureStorageService.Remove("remember_me");
                    }

                    // Guardar información del usuario logueado
                    await _secureStorageService.SetAsync("user_id", usuario.IdUsuario.ToString());
                    await _secureStorageService.SetAsync("user_name", usuario.NombreUsuario ?? "");
                    await _secureStorageService.SetAsync("user_email", usuario.Correo ?? "");
                    await _secureStorageService.SetAsync("user_role", usuario.Rol ?? "");
                }
                catch (InvalidOperationException storageEx)
                {
                    // Si hay error de tamaño en SecureStorage, mostrar mensaje pero permitir continuar
                    System.Diagnostics.Debug.WriteLine($"Advertencia al guardar en SecureStorage: {storageEx.Message}");

                    // Intentar guardar solo lo esencial (ID del usuario)
                    try
                    {
                        _secureStorageService.RemoveAll();
                        await _secureStorageService.SetAsync("user_id", usuario.IdUsuario.ToString());
                    }
                    catch
                    {
                        // Si ni siquiera podemos guardar el ID, continuar sin guardar nada
                        System.Diagnostics.Debug.WriteLine("No se pudo guardar información en SecureStorage");
                    }
                }

                // Navegar a la página principal
                await _navigationService.NavigateToAsync("Main");
            }
            catch (Refit.ApiException apiEx)
            {
                // Error específico de la API
                var content = apiEx.Content ?? apiEx.Message;
                ErrorMessage = $"Error en l" +
                    $"a API: {content}";
            }
            catch (HttpRequestException httpEx)
            {
                // Error de conexión
                ErrorMessage = $"Error de conexión: {httpEx.Message}. Verifica que la API esté disponible.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToRegisterAsync()
        {
            await _navigationService.NavigateToAsync("Register");
        }

        [RelayCommand]
        private async Task ForgotPasswordAsync()
        {
            await Shell.Current.DisplayAlert(
                "Recuperar Contraseña",
                "Por favor contacta al administrador para recuperar tu contraseña.",
                "OK");
        }

        [RelayCommand]
        private void ToggleShowPassword()
        {
            ShowPassword = !ShowPassword;
        }

        private async void LoadSavedCredentials()
        {
            try
            {
                var rememberMe = await _secureStorageService.GetAsync("remember_me");
                if (rememberMe == "true")
                {
                    var savedEmail = await _secureStorageService.GetAsync("saved_email");
                    if (!string.IsNullOrEmpty(savedEmail))
                    {
                        Correo = savedEmail;
                        RecordarMe = true;
                    }
                }
            }
            catch
            {
                // Ignorar errores al cargar credenciales guardadas
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}
