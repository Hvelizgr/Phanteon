using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Api;
using Phanteon.Services.Navigation;
using System.Collections.ObjectModel;

namespace Phanteon.Features.Dispositivos.ViewModels
{
    public partial class DispositivosViewModel : ObservableObject
    {
        private readonly IDispositivosApi _dispositivosApi;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Dispositivo> dispositivos = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private string searchText = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private int dispositivosActivosCount;

        [ObservableProperty]
        private int dispositivosInactivosCount;

        public DispositivosViewModel(IDispositivosApi dispositivosApi, INavigationService navigationService)
        {
            _dispositivosApi = dispositivosApi;
            _navigationService = navigationService;
        }

        public async Task InitializeAsync()
        {
            await LoadDispositivosAsync();
        }

        [RelayCommand]
        private async Task LoadDispositivosAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                var dispositivos = await _dispositivosApi.GetDispositivosAsync();

                Dispositivos.Clear();
                foreach (var dispositivo in dispositivos)
                {
                    Dispositivos.Add(dispositivo);
                }

                // Actualizar contadores
                DispositivosActivosCount = Dispositivos.Count(d => d.Activo.Equals("Si", StringComparison.OrdinalIgnoreCase));
                DispositivosInactivosCount = Dispositivos.Count - DispositivosActivosCount;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar dispositivos: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefreshDispositivosAsync()
        {
            IsRefreshing = true;
            await LoadDispositivosAsync();
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task DispositivoSelectedAsync(Dispositivo dispositivo)
        {
            if (dispositivo == null)
                return;

            await _navigationService.NavigateToAsync($"DispositivoDetail?id={dispositivo.IdDispositivo}");
        }

        [RelayCommand]
        private async Task AddDispositivoAsync()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Agregar Dispositivo",
                    "Esta funcionalidad estará disponible próximamente.",
                    "OK");
            }
        }

        partial void OnSearchTextChanged(string value)
        {
            // Implementar búsqueda en tiempo real si es necesario
        }
    }
}
