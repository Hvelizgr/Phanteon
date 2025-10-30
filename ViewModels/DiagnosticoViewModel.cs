using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Phanteon.Helpers;
using Phanteon.Services.Interfaces;

namespace Phanteon.ViewModels
{
    public class DiagnosticoViewModel : INotifyPropertyChanged
    {
        private readonly IDispositivosService _dispositivosService;
        private readonly IAlertasService _alertasService;

        private string _urlApi;
        private string _estadoConexion;
        private string _mensajeEstado;
        private string _resultados;

        public DiagnosticoViewModel(
            IDispositivosService dispositivosService,
            IAlertasService alertasService)
        {
            _dispositivosService = dispositivosService;
            _alertasService = alertasService;

            _urlApi = ApiConfiguration.BaseUrl;
            _estadoConexion = "Sin probar";
            _mensajeEstado = "Presiona 'Probar Conexión' para verificar el estado de la API";
            _resultados = "Esperando pruebas...";
        }

        public string UrlApi
        {
            get => _urlApi;
            set
            {
                _urlApi = value;
                OnPropertyChanged();
            }
        }

        public string EstadoConexion
        {
            get => _estadoConexion;
            set
            {
                _estadoConexion = value;
                OnPropertyChanged();
            }
        }

        public string MensajeEstado
        {
            get => _mensajeEstado;
            set
            {
                _mensajeEstado = value;
                OnPropertyChanged();
            }
        }

        public string Resultados
        {
            get => _resultados;
            set
            {
                _resultados = value;
                OnPropertyChanged();
            }
        }

        public async Task ProbarConexionAsync()
        {
            try
            {
                EstadoConexion = "Probando...";
                MensajeEstado = "Intentando conectar con la API...";
                Resultados = "Realizando prueba de conexión...";

                // Intentar obtener dispositivos como prueba de conexión
                var dispositivos = await _dispositivosService.GetAllDispositivosAsync();

                if (dispositivos != null)
                {
                    EstadoConexion = "✓ Conectado";
                    MensajeEstado = $"Conexión exitosa. Se encontraron {dispositivos.Count} dispositivos.";

                    var sb = new StringBuilder();
                    sb.AppendLine("=== PRUEBA DE CONEXIÓN EXITOSA ===");
                    sb.AppendLine($"URL: {UrlApi}");
                    sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    sb.AppendLine($"Dispositivos encontrados: {dispositivos.Count}");
                    sb.AppendLine();

                    if (dispositivos.Count > 0)
                    {
                        sb.AppendLine("Primeros dispositivos:");
                        foreach (var dispositivo in dispositivos.Take(3))
                        {
                            sb.AppendLine($"- ID: {dispositivo.IdDispositivo}, Serial: {dispositivo.SerialDispositivo}");
                        }
                    }

                    Resultados = sb.ToString();
                }
                else
                {
                    EstadoConexion = "⚠ Advertencia";
                    MensajeEstado = "Conexión establecida pero no se obtuvieron datos.";
                    Resultados = "La API respondió pero no devolvió dispositivos.";
                }
            }
            catch (HttpRequestException ex)
            {
                EstadoConexion = "✗ Error de Red";
                MensajeEstado = "No se pudo conectar con la API. Verifica que esté ejecutándose.";

                var sb = new StringBuilder();
                sb.AppendLine("=== ERROR DE CONEXIÓN ===");
                sb.AppendLine($"URL: {UrlApi}");
                sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine();
                sb.AppendLine("Mensaje de error:");
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine("Posibles causas:");
                sb.AppendLine("1. La API no está ejecutándose");
                sb.AppendLine("2. La URL es incorrecta");
                sb.AppendLine("3. Firewall bloqueando la conexión");

                if (UrlApi.Contains("10.0.2.2"))
                {
                    sb.AppendLine();
                    sb.AppendLine("NOTA: Estás usando 10.0.2.2 (emulador Android)");
                    sb.AppendLine("Verifica que la API esté corriendo en localhost:5000");
                }

                Resultados = sb.ToString();
            }
            catch (TaskCanceledException ex)
            {
                EstadoConexion = "✗ Timeout";
                MensajeEstado = "La petición tardó demasiado tiempo.";

                var sb = new StringBuilder();
                sb.AppendLine("=== TIMEOUT ===");
                sb.AppendLine($"URL: {UrlApi}");
                sb.AppendLine($"Timeout configurado: {ApiConfiguration.Timeout.TotalSeconds} segundos");
                sb.AppendLine();
                sb.AppendLine("La API no respondió a tiempo.");
                sb.AppendLine("Verifica que la API esté funcionando correctamente.");

                Resultados = sb.ToString();
            }
            catch (Exception ex)
            {
                EstadoConexion = "✗ Error Desconocido";
                MensajeEstado = $"Error inesperado: {ex.GetType().Name}";

                var sb = new StringBuilder();
                sb.AppendLine("=== ERROR INESPERADO ===");
                sb.AppendLine($"URL: {UrlApi}");
                sb.AppendLine($"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine();
                sb.AppendLine("Tipo de error:");
                sb.AppendLine(ex.GetType().Name);
                sb.AppendLine();
                sb.AppendLine("Mensaje:");
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine("Stack Trace:");
                sb.AppendLine(ex.StackTrace);

                Resultados = sb.ToString();
            }
        }

        public async Task ObtenerDispositivosAsync()
        {
            try
            {
                EstadoConexion = "Obteniendo dispositivos...";
                MensajeEstado = "Consultando endpoint de dispositivos...";

                var dispositivos = await _dispositivosService.GetAllDispositivosAsync();

                if (dispositivos != null && dispositivos.Count > 0)
                {
                    EstadoConexion = $"✓ Éxito - {dispositivos.Count} dispositivos";
                    MensajeEstado = "Dispositivos obtenidos correctamente.";

                    var sb = new StringBuilder();
                    sb.AppendLine("=== DISPOSITIVOS ===");
                    sb.AppendLine($"Total: {dispositivos.Count}");
                    sb.AppendLine($"Endpoint: {UrlApi}/api/dispositivos/getall");
                    sb.AppendLine();

                    foreach (var dispositivo in dispositivos)
                    {
                        sb.AppendLine($"ID: {dispositivo.IdDispositivo}");
                        sb.AppendLine($"  Serial: {dispositivo.SerialDispositivo}");
                        sb.AppendLine($"  MAC: {dispositivo.MAC}");
                        sb.AppendLine($"  Firmware: {dispositivo.Firmware}");
                        sb.AppendLine($"  Activo: {dispositivo.Activo}");
                        sb.AppendLine($"  Dirección: {dispositivo.Direccion}");
                        sb.AppendLine($"  Coordenadas: {dispositivo.Latitud}, {dispositivo.Longitud}");
                        sb.AppendLine($"  Registro: {dispositivo.Registro:yyyy-MM-dd HH:mm:ss}");
                        sb.AppendLine($"  Última Vista: {dispositivo.UltimaVista:yyyy-MM-dd HH:mm:ss}");
                        sb.AppendLine();
                    }

                    Resultados = sb.ToString();
                }
                else
                {
                    EstadoConexion = "⚠ Sin datos";
                    MensajeEstado = "No hay dispositivos registrados.";
                    Resultados = "La API respondió correctamente pero no hay dispositivos en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                EstadoConexion = "✗ Error";
                MensajeEstado = "Error al obtener dispositivos.";
                Resultados = $"Error: {ex.Message}";
            }
        }

        public async Task ObtenerAlertasAsync()
        {
            try
            {
                EstadoConexion = "Obteniendo alertas...";
                MensajeEstado = "Consultando endpoint de alertas...";

                var alertas = await _alertasService.GetAllAlertasAsync();

                if (alertas != null && alertas.Count > 0)
                {
                    EstadoConexion = $"✓ Éxito - {alertas.Count} alertas";
                    MensajeEstado = "Alertas obtenidas correctamente.";

                    var sb = new StringBuilder();
                    sb.AppendLine("=== ALERTAS ===");
                    sb.AppendLine($"Total: {alertas.Count}");
                    sb.AppendLine($"Endpoint: {UrlApi}/api/alertas/getall");
                    sb.AppendLine();

                    foreach (var alerta in alertas)
                    {
                        sb.AppendLine($"ID: {alerta.IdAlert}");
                        sb.AppendLine($"  Tipo Evento: {alerta.TipoEvento}");
                        sb.AppendLine($"  Severidad: {alerta.Severidad}");
                        sb.AppendLine($"  Marca Tiempo: {alerta.MarcaTiempo:yyyy-MM-dd HH:mm:ss}");
                        sb.AppendLine($"  Hora Almacenado: {alerta.HoraAlmacenado:yyyy-MM-dd HH:mm:ss}");
                        sb.AppendLine($"  Procesado: {alerta.Procesado}");
                        sb.AppendLine($"  Carga Útil: {alerta.CargaUtil}");
                        sb.AppendLine($"  Dispositivo ID: {alerta.IdDispositivo}");
                        sb.AppendLine();
                    }

                    Resultados = sb.ToString();
                }
                else
                {
                    EstadoConexion = "⚠ Sin datos";
                    MensajeEstado = "No hay alertas registradas.";
                    Resultados = "La API respondió correctamente pero no hay alertas en la base de datos.";
                }
            }
            catch (Exception ex)
            {
                EstadoConexion = "✗ Error";
                MensajeEstado = "Error al obtener alertas.";
                Resultados = $"Error: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
