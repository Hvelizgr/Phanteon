namespace Phanteon.Constants
{
    /// <summary>
    /// Constantes generales de la aplicación
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// Timeout para peticiones HTTP (en segundos)
        /// </summary>
        public const int HttpTimeoutSeconds = 30;

        /// <summary>
        /// Número máximo de reintentos para peticiones fallidas
        /// </summary>
        public const int MaxRetryAttempts = 3;

        /// <summary>
        /// Delay entre reintentos (en segundos)
        /// </summary>
        public const int RetryDelaySeconds = 2;

        // Storage Keys
        public static class StorageKeys
        {
            public const string AuthToken = "auth_token";
            public const string UserId = "user_id";
            public const string UserEmail = "user_email";
        }

        // Navigation Routes
        public static class Routes
        {
            public const string MainPage = "main";
            // TODO: Agregar más rutas aquí
        }
    }
}
