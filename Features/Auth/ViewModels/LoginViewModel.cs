using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Navigation;
using Phanteon.Services.Storage;
using Phanteon.Services.Api;
using Phanteon.Services.Auth;
using Phanteon.Models;
using System.Text.RegularExpressions;

namespace Phanteon.Features.Auth.ViewModels
{
    /// <summary>
    /// ViewModel para la página de login
    /// (SRP: Solo maneja la lógica de presentación del login)
    /// (DIP: Depende de abstracciones, no de implementaciones concretas)
    /// </summary>
    public partial class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly ISessionManager _sessionManager;
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
            ISessionManager sessionManager,
            IUsuariosApi usuariosApi)
        {
            _navigationService = navigationService;
            _secureStorageService = secureStorageService;
            _sessionManager = sessionManager;
            _usuariosApi = usuariosApi;

            // Cargar credenciales guardadas de forma segura
            Task.Run(async () => await LoadSavedCredentialsAsync());
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
                System.Diagnostics.Debug.WriteLine($"[LOGIN] Iniciando login para usuario: {NombreUsuario}");

                // Crear request de login
                var loginRequest = new LoginRequest
                {
                    NombreUsuario = NombreUsuario,
                    Correo = Correo,
                    Password = Password
                };

                System.Diagnostics.Debug.WriteLine($"[LOGIN] Llamando a API...");

                // Intentar login
                var usuario = await _usuariosApi.LoginAsync(loginRequest);

                System.Diagnostics.Debug.WriteLine($"[LOGIN] Login exitoso, usuario ID: {usuario?.IdUsuario}");

                if (usuario == null)
                {
                    ErrorMessage = "La API no devolvió información del usuario";
                    System.Diagnostics.Debug.WriteLine("[LOGIN] ERROR: Usuario null");
                    return;
                }

                // Guardar sesión del usuario (SRP: delegar a SessionManager)
                System.Diagnostics.Debug.WriteLine("[LOGIN] Guardando sesión...");
                await SaveSessionDataAsync(usuario);

                System.Diagnostics.Debug.WriteLine("[LOGIN] Navegando a Main...");

                // Navegar a la página principal - Cambiar el CurrentItem directamente
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("[LOGIN] Shell.Current existe: " + (Shell.Current != null));

                        if (Shell.Current != null && Shell.Current.Items.Count > 0)
                        {
                            var tabBar = Shell.Current.Items[0] as TabBar;
                            if (tabBar != null && tabBar.Items.Count > 0)
                            {
                                // Buscar el ShellContent de Main
                                var mainContent = tabBar.Items
                                    .SelectMany(section => section.Items)
                                    .FirstOrDefault(content => content.Route == "Main");

                                if (mainContent != null)
                                {
                                    System.Diagnostics.Debug.WriteLine("[LOGIN] Encontrado Main, cambiando CurrentItem...");
                                    mainContent.IsVisible = true; // Hacerlo visible
                                    Shell.Current.CurrentItem = tabBar;
                                    tabBar.CurrentItem = mainContent.Parent as ShellSection;
                                    System.Diagnostics.Debug.WriteLine("[LOGIN] Navegación exitosa");
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("[LOGIN] ERROR: No se encontró Main");
                                    ErrorMessage = "Error: Página Main no encontrada";
                                }
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("[LOGIN] ERROR: TabBar no encontrado o vacío");
                                ErrorMessage = "Error: Estructura Shell incorrecta";
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("[LOGIN] ERROR: Shell.Current es null o vacío");
                            ErrorMessage = "Error interno de navegación";
                        }
                    }
                    catch (Exception navEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"[LOGIN] ERROR: {navEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"[LOGIN] Stack: {navEx.StackTrace}");
                        ErrorMessage = $"Error: {navEx.Message}";
                    }
                });

                System.Diagnostics.Debug.WriteLine("[LOGIN] Proceso completado");
            }
            catch (Refit.ApiException apiEx)
            {
                // Error específico de la API
                var content = apiEx.Content ?? apiEx.Message;
                ErrorMessage = $"Error en la API: {content}";
                System.Diagnostics.Debug.WriteLine($"[LOGIN] API Exception: {apiEx.StatusCode} - {content}");
            }
            catch (HttpRequestException httpEx)
            {
                // Error de conexión
                ErrorMessage = $"Error de conexión: {httpEx.Message}. Verifica que la API esté disponible.";
                System.Diagnostics.Debug.WriteLine($"[LOGIN] HTTP Exception: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"[LOGIN] Exception: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[LOGIN] Stack: {ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
                System.Diagnostics.Debug.WriteLine($"[LOGIN] IsLoading = false");
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

        private async Task LoadSavedCredentialsAsync()
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

        /// <summary>
        /// Guarda los datos de sesión del usuario
        /// (SRP: Método privado que encapsula la lógica de guardar sesión)
        /// </summary>
        private async Task SaveSessionDataAsync(Usuario usuario)
        {
            try
            {
                // Guardar credenciales si "Recordarme" está activado
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

                // Guardar sesión del usuario usando SessionManager
                await _sessionManager.SaveSessionAsync(
                    usuario.IdUsuario.ToString(),
                    usuario.NombreUsuario ?? "",
                    usuario.Correo ?? "",
                    usuario.Rol ?? ""
                );
            }
            catch (InvalidOperationException storageEx)
            {
                // Si hay error de tamaño en SecureStorage, mostrar mensaje pero permitir continuar
                System.Diagnostics.Debug.WriteLine($"Advertencia al guardar en SecureStorage: {storageEx.Message}");

                // Intentar guardar solo lo esencial (ID del usuario)
                try
                {
                    _secureStorageService.RemoveAll();
                    await _sessionManager.SaveSessionAsync(
                        usuario.IdUsuario.ToString(),
                        usuario.NombreUsuario ?? "",
                        usuario.Correo ?? "",
                        usuario.Rol ?? ""
                    );
                }
                catch
                {
                    // Si ni siquiera podemos guardar el ID, continuar sin guardar nada
                    System.Diagnostics.Debug.WriteLine("No se pudo guardar información en SecureStorage");
                }
            }
        }
    }
}
