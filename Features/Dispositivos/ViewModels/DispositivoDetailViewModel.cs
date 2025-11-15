using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Api;
using Phanteon.Services.Navigation;

namespace Phanteon.Features.Dispositivos.ViewModels
{
    [QueryProperty(nameof(DispositivoId), "id")]
    public partial class DispositivoDetailViewModel : ObservableObject
    {
        private readonly IDispositivosApi _dispositivosApi;
        private readonly IAlertasApi _alertasApi;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private int dispositivoId;

        [ObservableProperty]
        private Dispositivo? dispositivo;

        [ObservableProperty]
        private List<Alerta> alertasRecientes = new();

        [ObservableProperty]
        private List<HistorialDispositivo> historial = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private int alertasCount;

        public DispositivoDetailViewModel(
            IDispositivosApi dispositivosApi,
            IAlertasApi alertasApi,
            INavigationService navigationService)
        {
            _dispositivosApi = dispositivosApi;
            _alertasApi = alertasApi;
            _navigationService = navigationService;
        }

        public async Task InitializeAsync()
        {
            await LoadDispositivoAsync();
        }

        private async Task LoadDispositivoAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                // Cargar dispositivo
                Dispositivo = await _dispositivosApi.GetDispositivoAsync(DispositivoId);

                // Cargar alertas recientes
                var alertas = await _alertasApi.GetAlertasPorDispositivoAsync(DispositivoId);
                AlertasRecientes = alertas.OrderByDescending(a => a.MarcaTiempo).Take(5).ToList();
                AlertasCount = alertas.Count;

                // Cargar historial
                try
                {
                    Historial = await _dispositivosApi.GetHistorialAsync(DispositivoId);
                }
                catch
                {
                    // Si no hay historial disponible, continuar sin error
                    Historial = new List<HistorialDispositivo>();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar dispositivo: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task VerTodasAlertasAsync()
        {
            await _navigationService.NavigateToAsync($"Alertas?dispositivoId={DispositivoId}");
        }

        [RelayCommand]
        private async Task EditarDispositivoAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Editar Dispositivo",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        [RelayCommand]
        private async Task EliminarDispositivoAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                var confirmed = await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Eliminar Dispositivo",
                    "¿Estás seguro de que deseas eliminar este dispositivo?",
                    "Sí",
                    "No");

                if (confirmed)
                {
                    try
                    {
                        IsLoading = true;
                        await _dispositivosApi.DeleteDispositivoAsync(DispositivoId);
                        await _navigationService.GoBackAsync();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = $"Error al eliminar: {ex.Message}";
                    }
                    finally
                    {
                        IsLoading = false;
                    }
                }
            }
        }

        [RelayCommand]
        private async Task AbrirMapaAsync()
        {
            if (Dispositivo != null)
            {
                try
                {
                    var location = new Location(Dispositivo.Latitud, Dispositivo.Longitud);
                    var options = new MapLaunchOptions { Name = Dispositivo.SerialDispositivo };
                    await Map.Default.OpenAsync(location, options);
                }
                catch (Exception ex)
                {
                    if (Application.Current?.Windows.Count > 0)
                    {
                        await Application.Current.Windows[0].Page!.DisplayAlert(
                            "Error",
                            $"No se pudo abrir el mapa: {ex.Message}",
                            "OK");
                    }
                }
            }
        }

        partial void OnDispositivoIdChanged(int value)
        {
            if (value > 0)
            {
                _ = LoadDispositivoAsync();
            }
        }
    }
}
