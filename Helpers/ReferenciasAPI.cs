namespace Phanteon.Helpers
{
    public class DISPOSITIVOS_REFERENCIA
    {
        // PROPIEDADES OBSERVABLES
        public const string DISPOSITIVOS = "Dispositivos";
        public const string DISPOSITIVO_SELECCIONADO = "DispositivoSeleccionado";
        public const string ESTA_CARGANDO = "EstaCargando";
        public const string MENSAJE_ERROR = "MensajeError";

        // COMANDOS
        public const string CARGAR_DISPOSITIVOS_COMMAND = "CargarDispositivosCommand";
        public const string OBTENER_DISPOSITIVO_POR_ID_COMMAND = "ObtenerDispositivoPorIdCommand";
        public const string CREAR_DISPOSITIVO_COMMAND = "CrearDispositivoCommand";
        public const string ACTUALIZAR_DISPOSITIVO_COMMAND = "ActualizarDispositivoCommand"; // [FUTURO]
        public const string ELIMINAR_DISPOSITIVO_COMMAND = "EliminarDispositivoCommand"; // [FUTURO]
    }

    public class ALERTAS_REFERENCIA
    {
        // PROPIEDADES OBSERVABLES
        public const string ALERTAS = "Alertas";
        public const string ALERTA_SELECCIONADA = "AlertaSeleccionada";
        public const string ESTA_CARGANDO = "EstaCargando";
        public const string MENSAJE_ERROR = "MensajeError";

        // COMANDOS
        public const string CARGAR_ALERTAS_COMMAND = "CargarAlertasCommand";
        public const string OBTENER_ALERTA_POR_ID_COMMAND = "ObtenerAlertaPorIdCommand";
        public const string CREAR_ALERTA_COMMAND = "CrearAlertaCommand";
        public const string ACTUALIZAR_ALERTA_COMMAND = "ActualizarAlertaCommand"; // [FUTURO]
        public const string ELIMINAR_ALERTA_COMMAND = "EliminarAlertaCommand"; // [FUTURO]
        public const string MARCAR_ALERTA_PROCESADA_COMMAND = "MarcarAlertaProcesadaCommand"; // [FUTURO]
    }

    public class HISTORIAL_DISPOSITIVOS_REFERENCIA
    {
        // PROPIEDADES OBSERVABLES
        public const string HISTORIALES = "Historiales";
        public const string HISTORIAL_SELECCIONADO = "HistorialSeleccionado";
        public const string ESTA_CARGANDO = "EstaCargando";
        public const string MENSAJE_ERROR = "MensajeError";

        // COMANDOS
        public const string CARGAR_HISTORIALES_COMMAND = "CargarHistorialesCommand";
        public const string OBTENER_HISTORIAL_POR_ID_COMMAND = "ObtenerHistorialPorIdCommand";
        public const string CREAR_HISTORIAL_COMMAND = "CrearHistorialCommand";
        public const string ACTUALIZAR_HISTORIAL_COMMAND = "ActualizarHistorialCommand"; // [FUTURO]
        public const string ELIMINAR_HISTORIAL_COMMAND = "EliminarHistorialCommand"; // [FUTURO]
        public const string OBTENER_HISTORIAL_POR_DISPOSITIVO_COMMAND = "ObtenerHistorialPorDispositivoCommand"; // [FUTURO]
    }

    public class USUARIOS_REFERENCIA
    {
        // PROPIEDADES OBSERVABLES
        public const string USUARIOS = "Usuarios";
        public const string USUARIO_SELECCIONADO = "UsuarioSeleccionado";
        public const string USUARIO_ACTUAL = "UsuarioActual";
        public const string ESTA_CARGANDO = "EstaCargando";
        public const string MENSAJE_ERROR = "MensajeError";

        // COMANDOS
        public const string CARGAR_USUARIOS_COMMAND = "CargarUsuariosCommand";
        public const string OBTENER_USUARIO_POR_ID_COMMAND = "ObtenerUsuarioPorIdCommand";
        public const string CREAR_USUARIO_COMMAND = "CrearUsuarioCommand";
        public const string ACTUALIZAR_USUARIO_COMMAND = "ActualizarUsuarioCommand"; // [FUTURO]
        public const string ELIMINAR_USUARIO_COMMAND = "EliminarUsuarioCommand"; // [FUTURO]
        public const string LOGOUT_COMMAND = "LogoutCommand";
    }
}
