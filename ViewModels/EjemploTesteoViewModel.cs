using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Phanteon.ViewModels
{
    public partial class EjemploTesteoViewModel : BaseViewModel
    {
        private readonly IDispositivosService _dispositivosService;

        #region Propiedades Observables

        [ObservableProperty]
        private ObservableCollection<Dispositivo> dispositivos = new();

        [ObservableProperty]
        private Dispositivo? dispositivoSeleccionado;

        #endregion

        #region Constructor con Inyección de Dependencias

        public EjemploTesteoViewModel(IDispositivosService dispositivosService)
        {
            _dispositivosService = dispositivosService;
            Titulo = "Ejemplo Testeo - Dispositivos";
        }

        #endregion

        #region EJEMPLO 1: Método básico con manejo de errores


        [RelayCommand]
        private async Task CargarDispositivosAsync()
        {
            try
            {
                EstaCargando = true;
                LimpiarError();

                var lista = await _dispositivosService.GetAllDispositivosAsync();

                Dispositivos.Clear();
                foreach (var dispositivo in lista)
                {
                    Dispositivos.Add(dispositivo);
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex, "cargar dispositivos");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        #endregion

        #region EJEMPLO 2: Método con parámetro

        [RelayCommand]
        private async Task ObtenerDispositivoPorIdAsync(int id)
        {
            try
            {
                EstaCargando = true;
                LimpiarError();

                DispositivoSeleccionado = await _dispositivosService.GetDispositivoByIdAsync(id);
            }
            catch (Exception ex)
            {
                ManejarError(ex, "obtener dispositivo");
                DispositivoSeleccionado = null;
            }
            finally
            {
                EstaCargando = false;
            }
        }

        #endregion

        #region EJEMPLO 3: Crear registro

        [RelayCommand]
        private async Task CrearDispositivoAsync(Dispositivo nuevoDispositivo)
        {
            try
            {
                EstaCargando = true;
                LimpiarError();

                var resultado = await _dispositivosService.CreateDispositivoAsync(nuevoDispositivo);

                if (resultado != null)
                {
                    Dispositivos.Add(resultado);
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex, "crear dispositivo");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        #endregion

        #region Métodos auxiliares de filtrado

   
        public ObservableCollection<Dispositivo> ObtenerDispositivosActivos()
        {
            return new ObservableCollection<Dispositivo>(
                Dispositivos.Where(d => d.Activo?.ToLower() == "true" || d.Activo == "1" || d.Activo?.ToLower() == "activo")
            );
        }

        public ObservableCollection<Dispositivo> BuscarDispositivos(string busqueda)
        {
            busqueda = busqueda.ToLower();
            return new ObservableCollection<Dispositivo>(
                Dispositivos.Where(d =>
                    d.SerialDispositivo.ToLower().Contains(busqueda) ||
                    d.MAC.ToLower().Contains(busqueda)
                )
            );
        }

        #endregion
    }

    #region EJEMPLO DE TESTEO: Clase Fake para pruebas

    public class FakeDispositivosService : IDispositivosService
    {
        // Datos de prueba en memoria
        private List<Dispositivo> _dispositivosFake = new List<Dispositivo>
        {
            new Dispositivo
            {
                IdDispositivo = 1,
                SerialDispositivo = "TEST001",
                MAC = "00:11:22:33:44:55",
                Firmware = "v1.0.0",
                Direccion = "Calle Falsa 123",
                Latitud = 14.6349,
                Longitud = -90.5069,
                Registro = DateTime.Now.AddDays(-30),
                Activo = "true",
                UltimaVista = DateTime.Now
            },
            new Dispositivo
            {
                IdDispositivo = 2,
                SerialDispositivo = "TEST002",
                MAC = "AA:BB:CC:DD:EE:FF",
                Firmware = "v1.2.1",
                Direccion = "Avenida Test 456",
                Latitud = 14.5833,
                Longitud = -90.5167,
                Registro = DateTime.Now.AddDays(-15),
                Activo = "false",
                UltimaVista = DateTime.Now.AddDays(-5)
            }
        };

        public Task<List<Dispositivo>> GetAllDispositivosAsync()
        {
            // Simulamos delay de red
            return Task.FromResult(_dispositivosFake);
        }

        public Task<Dispositivo> GetDispositivoByIdAsync(int id)
        {
            var dispositivo = _dispositivosFake.FirstOrDefault(d => d.IdDispositivo == id);
            return Task.FromResult(dispositivo ?? new Dispositivo());
        }

        public Task<Dispositivo> CreateDispositivoAsync(Dispositivo dispositivo)
        {
            dispositivo.IdDispositivo = _dispositivosFake.Count + 1;
            _dispositivosFake.Add(dispositivo);
            return Task.FromResult(dispositivo);
        }

        public Task<Dispositivo> UpdateDispositivoAsync(int id, Dispositivo dispositivo)
        {
            var index = _dispositivosFake.FindIndex(d => d.IdDispositivo == id);
            if (index != -1)
            {
                _dispositivosFake[index] = dispositivo;
            }
            return Task.FromResult(dispositivo);
        }

        public Task DeleteDispositivoAsync(int id)
        {
            _dispositivosFake.RemoveAll(d => d.IdDispositivo == id);
            return Task.CompletedTask;
        }
    }

    #endregion
}
