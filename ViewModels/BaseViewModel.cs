using CommunityToolkit.Mvvm.ComponentModel;

namespace Phanteon.ViewModels
{
    /// <summary>
    /// ViewModel base con propiedades comunes para todos los ViewModels
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Indicador de si el ViewModel está ejecutando una operación
        /// Usar en XAML: IsRunning="{Binding EstaCargando}"
        /// </summary>
        [ObservableProperty]
        private bool estaCargando;

        /// <summary>
        /// Mensaje de error para mostrar al usuario
        /// Usar en XAML: Text="{Binding MensajeError}"
        /// </summary>
        [ObservableProperty]
        private string mensajeError = string.Empty;

        /// <summary>
        /// Título de la página
        /// Usar en XAML: Title="{Binding Titulo}"
        /// </summary>
        [ObservableProperty]
        private string titulo = string.Empty;

        /// <summary>
        /// Indica si hay un mensaje de error
        /// </summary>
        public bool TieneError => !string.IsNullOrWhiteSpace(MensajeError);

        /// <summary>
        /// Limpia el mensaje de error
        /// </summary>
        protected void LimpiarError()
        {
            MensajeError = string.Empty;
        }

        /// <summary>
        /// Establece un mensaje de error
        /// </summary>
        protected void EstablecerError(string mensaje)
        {
            MensajeError = mensaje;
        }

        /// <summary>
        /// Maneja excepciones y establece el mensaje de error apropiado
        /// </summary>
        protected void ManejarError(Exception ex, string operacion)
        {
            if (ex is HttpRequestException)
            {
                MensajeError = $"Error de conexión al {operacion}. Verifica tu conexión a internet.";
            }
            else if (ex is TaskCanceledException)
            {
                MensajeError = $"Tiempo de espera agotado al {operacion}.";
            }
            else
            {
                MensajeError = $"Error al {operacion}: {ex.Message}";
            }
        }
    }
}
