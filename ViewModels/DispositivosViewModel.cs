using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Phanteon.ViewModels
{
    public partial class DispositivosViewModel : ObservableObject
    {
        private readonly IDispositivosService _dispositivosService;

        [ObservableProperty]
        private ObservableCollection<Dispositivo> dispositivos = new();

        [ObservableProperty]
        private Dispositivo? dispositivoSeleccionado;

        [ObservableProperty]
        private bool estaCargando;

        [ObservableProperty]
        private string mensajeError = string.Empty;

        public DispositivosViewModel(IDispositivosService dispositivosService)
        {
            _dispositivosService = dispositivosService;
        }

        [RelayCommand]
        private async Task CargarDispositivosAsync()
        {
            try
            {
                EstaCargando = true;
                MensajeError = string.Empty;

                var lista = await _dispositivosService.GetAllDispositivosAsync();

                Dispositivos.Clear();
                foreach (var dispositivo in lista)
                {
                    Dispositivos.Add(dispositivo);
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al cargar dispositivos: {ex.Message}";
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task CrearDispositivoAsync(Dispositivo nuevoDispositivo)
        {
            try
            {
                EstaCargando = true;
                MensajeError = string.Empty;

                var resultado = await _dispositivosService.CreateDispositivoAsync(nuevoDispositivo);

                if (resultado != null)
                {
                    Dispositivos.Add(resultado);
                }
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al crear dispositivo: {ex.Message}";
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task ObtenerDispositivoPorIdAsync(int id)
        {
            try
            {
                EstaCargando = true;
                MensajeError = string.Empty;

                DispositivoSeleccionado = await _dispositivosService.GetDispositivoByIdAsync(id);
            }
            catch (Exception ex)
            {
                MensajeError = $"Error al obtener dispositivo: {ex.Message}";
                DispositivoSeleccionado = null;
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }
}
