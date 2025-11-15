using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Navigation;
using Phanteon.Services.Api;
using Phanteon.Models;
using System.Text.RegularExpressions;

namespace Phanteon.Features.Auth.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IUsuariosApi _usuariosApi;

        [ObservableProperty]
        private string nombreUsuario = string.Empty;

        [ObservableProperty]
        private string correo = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string confirmarPassword = string.Empty;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private bool showPassword;

        [ObservableProperty]
        private bool showConfirmPassword;

        public RegisterViewModel(INavigationService navigationService, IUsuariosApi usuariosApi)
        {
            _navigationService = navigationService;
            _usuariosApi = usuariosApi;
        }

        [RelayCommand]
        private async Task RegisterAsync()
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

            if (Password.Length < 6)
            {
                ErrorMessage = "La contraseña debe tener al menos 6 caracteres";
                return;
            }

            if (Password != ConfirmarPassword)
            {
                ErrorMessage = "Las contraseñas no coinciden";
                return;
            }

            IsLoading = true;

            try
            {
                // Crear usuario
                var nuevoUsuario = new Usuario
                {
                    NombreUsuario = NombreUsuario,
                    Correo = Correo,
                    PasswordHash = Password, // La API lo hasheará con BCrypt
                    Rol = "Usuario" // Rol por defecto
                };

                var resultado = await _usuariosApi.CreateUsuarioAsync(nuevoUsuario);

                // Navegar a la página de login (volver atrás)
                await Shell.Current.DisplayAlert("Éxito", "Usuario registrado correctamente. Por favor inicia sesión.", "OK");
                await _navigationService.GoBackAsync();
            }
            catch (Refit.ApiException apiEx)
            {
                // Error específico de la API
                var content = apiEx.Content ?? apiEx.Message;
                ErrorMessage = $"Error en la API: {content}";
            }
            catch (HttpRequestException httpEx)
            {
                // Error de conexión
                ErrorMessage = $"Error de conexión: {httpEx.Message}. Verifica que la API esté disponible.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al registrar: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task NavigateToLoginAsync()
        {
            await _navigationService.NavigateToAsync("Login");
        }

        [RelayCommand]
        private void ToggleShowPassword()
        {
            ShowPassword = !ShowPassword;
        }

        [RelayCommand]
        private void ToggleShowConfirmPassword()
        {
            ShowConfirmPassword = !ShowConfirmPassword;
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
