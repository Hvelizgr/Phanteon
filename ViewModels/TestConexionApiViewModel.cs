using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Interfaces;

namespace Phanteon.ViewModels
{
    /// <summary>
    /// ViewModel SOLO para testing de conexión con la API
    /// NO usar en producción - Solo para verificar que la API responde
    /// </summary>
    public partial class TestConexionApiViewModel : BaseViewModel
    {
        private readonly IDispositivosService _dispositivosService;
        private readonly IAlertasService _alertasService;
        private readonly IUsuariosService _usuariosService;
        private readonly IHistorialDispositivosService _historialService;

        #region Propiedades de resultado de pruebas

        [ObservableProperty]
        private string resultadoPruebaDispositivos = "No probado";

        [ObservableProperty]
        private string resultadoPruebaAlertas = "No probado";

        [ObservableProperty]
        private string resultadoPruebaUsuarios = "No probado";

        [ObservableProperty]
        private string resultadoPruebaHistorial = "No probado";

        [ObservableProperty]
        private string estadoConexion = "❓ No verificado";

        [ObservableProperty]
        private bool conexionExitosa = false;

        #endregion

        #region Constructor

        public TestConexionApiViewModel(
            IDispositivosService dispositivosService,
            IAlertasService alertasService,
            IUsuariosService usuariosService,
            IHistorialDispositivosService historialService)
        {
            _dispositivosService = dispositivosService;
            _alertasService = alertasService;
            _usuariosService = usuariosService;
            _historialService = historialService;
            Titulo = "Test Conexión API";
        }

        #endregion

        #region Comandos de prueba

        /// <summary>
        /// Prueba todas las conexiones de la API
        /// </summary>
        [RelayCommand]
        private async Task ProbarTodasLasConexionesAsync()
        {
            EstaCargando = true;
            LimpiarError();
            ConexionExitosa = false;

            await ProbarDispositivosAsync();
            await ProbarAlertasAsync();
            await ProbarUsuariosAsync();
            await ProbarHistorialAsync();

            // Evaluar resultado general
            if (ResultadoPruebaDispositivos.Contains("✅") &&
                ResultadoPruebaAlertas.Contains("✅") &&
                ResultadoPruebaUsuarios.Contains("✅") &&
                ResultadoPruebaHistorial.Contains("✅"))
            {
                EstadoConexion = "✅ API funcionando correctamente";
                ConexionExitosa = true;
            }
            else
            {
                EstadoConexion = "❌ Algunos endpoints fallaron";
                ConexionExitosa = false;
            }

            EstaCargando = false;
        }

        /// <summary>
        /// Prueba endpoint de dispositivos
        /// </summary>
        [RelayCommand]
        private async Task ProbarDispositivosAsync()
        {
            try
            {
                ResultadoPruebaDispositivos = "⏳ Probando...";
                var dispositivos = await _dispositivosService.GetAllDispositivosAsync();
                ResultadoPruebaDispositivos = $"✅ OK ({dispositivos.Count} registros)";
            }
            catch (Exception ex)
            {
                ResultadoPruebaDispositivos = $"❌ Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Prueba endpoint de alertas
        /// </summary>
        [RelayCommand]
        private async Task ProbarAlertasAsync()
        {
            try
            {
                ResultadoPruebaAlertas = "⏳ Probando...";
                var alertas = await _alertasService.GetAllAlertasAsync();
                ResultadoPruebaAlertas = $"✅ OK ({alertas.Count} registros)";
            }
            catch (Exception ex)
            {
                ResultadoPruebaAlertas = $"❌ Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Prueba endpoint de usuarios
        /// </summary>
        [RelayCommand]
        private async Task ProbarUsuariosAsync()
        {
            try
            {
                ResultadoPruebaUsuarios = "⏳ Probando...";
                var usuarios = await _usuariosService.GetAllUsuariosAsync();
                ResultadoPruebaUsuarios = $"✅ OK ({usuarios.Count} registros)";
            }
            catch (Exception ex)
            {
                ResultadoPruebaUsuarios = $"❌ Error: {ex.Message}";
            }
        }

        /// <summary>
        /// Prueba endpoint de historial
        /// </summary>
        [RelayCommand]
        private async Task ProbarHistorialAsync()
        {
            try
            {
                ResultadoPruebaHistorial = "⏳ Probando...";
                var historial = await _historialService.GetAllHistorialDispositivosAsync();
                ResultadoPruebaHistorial = $"✅ OK ({historial.Count} registros)";
            }
            catch (Exception ex)
            {
                ResultadoPruebaHistorial = $"❌ Error: {ex.Message}";
            }
        }

        #endregion
    }
}
