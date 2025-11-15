using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Api;
using System.Collections.ObjectModel;

namespace Phanteon.Features.Alertas.ViewModels
{
    [QueryProperty(nameof(DispositivoId), "dispositivoId")]
    public partial class AlertasViewModel : ObservableObject
    {
        private readonly IAlertasApi _alertasApi;

        [ObservableProperty]
        private int? dispositivoId;

        [ObservableProperty]
        private ObservableCollection<Alerta> alertas = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool isRefreshing;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private string filtroSeveridad = "Todas";

        [ObservableProperty]
        private int alertasAltasCount;

        [ObservableProperty]
        private int alertasMediasCount;

        [ObservableProperty]
        private int alertasBajasCount;

        [ObservableProperty]
        private int alertasProcesadasCount;

        private List<Alerta> _todasLasAlertas = new();

        public AlertasViewModel(IAlertasApi alertasApi)
        {
            _alertasApi = alertasApi;
        }

        public async Task InitializeAsync()
        {
            await LoadAlertasAsync();
        }

        [RelayCommand]
        private async Task LoadAlertasAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            ErrorMessage = string.Empty;

            try
            {
                // Obtener todas las alertas desde la API
                var alertas = await _alertasApi.GetAlertasAsync();

                // Filtrar por dispositivo si se especificó un ID
                if (DispositivoId.HasValue && DispositivoId.Value > 0)
                {
                    alertas = alertas.Where(a => a.IdDispositivo == DispositivoId.Value).ToList();
                }

                _todasLasAlertas = alertas.OrderByDescending(a => a.MarcaTiempo).ToList();

                ActualizarEstadisticas();
                AplicarFiltro();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar alertas: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task RefreshAlertasAsync()
        {
            IsRefreshing = true;
            await LoadAlertasAsync();
            IsRefreshing = false;
        }

        [RelayCommand]
        private void FiltrarPorSeveridad(string severidad)
        {
            FiltroSeveridad = severidad;
            AplicarFiltro();
        }

        private void AplicarFiltro()
        {
            Alertas.Clear();

            var alertasFiltradas = FiltroSeveridad.Equals("Todas", StringComparison.OrdinalIgnoreCase)
                ? _todasLasAlertas
                : _todasLasAlertas.Where(a => a.Severidad.Equals(FiltroSeveridad, StringComparison.OrdinalIgnoreCase));

            foreach (var alerta in alertasFiltradas)
            {
                Alertas.Add(alerta);
            }
        }

        private void ActualizarEstadisticas()
        {
            AlertasAltasCount = _todasLasAlertas.Count(a =>
                a.Severidad.Equals("Alta", StringComparison.OrdinalIgnoreCase) ||
                a.Severidad.Equals("High", StringComparison.OrdinalIgnoreCase) ||
                a.Severidad.Equals("Critica", StringComparison.OrdinalIgnoreCase));

            AlertasMediasCount = _todasLasAlertas.Count(a =>
                a.Severidad.Equals("Media", StringComparison.OrdinalIgnoreCase) ||
                a.Severidad.Equals("Medium", StringComparison.OrdinalIgnoreCase));

            AlertasBajasCount = _todasLasAlertas.Count(a =>
                a.Severidad.Equals("Baja", StringComparison.OrdinalIgnoreCase) ||
                a.Severidad.Equals("Low", StringComparison.OrdinalIgnoreCase));

            AlertasProcesadasCount = _todasLasAlertas.Count(a => a.Procesado);
        }

        [RelayCommand]
        private async Task MarcarComoLeidaAsync(Alerta alerta)
        {
            try
            {
                await _alertasApi.MarcarAlertaLeidaAsync(alerta.IdAlert);
                alerta.Procesado = true;
                ActualizarEstadisticas();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al marcar alerta: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task EliminarAlertaAsync(Alerta alerta)
        {
            if (Application.Current?.Windows.Count > 0)
            {
                var confirmed = await Application.Current.Windows[0].Page!.DisplayAlert(
                    "Eliminar Alerta",
                    "¿Estás seguro de que deseas eliminar esta alerta?",
                    "Sí",
                    "No");

                if (confirmed)
                {
                    try
                    {
                        await _alertasApi.DeleteAlertaAsync(alerta.IdAlert);
                        _todasLasAlertas.Remove(alerta);
                        Alertas.Remove(alerta);
                        ActualizarEstadisticas();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = $"Error al eliminar: {ex.Message}";
                    }
                }
            }
        }
    }
}
