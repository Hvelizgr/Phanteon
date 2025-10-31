using Phanteon.Services.Interfaces;

namespace Phanteon.Services.Implementations
{
    /// <summary>
    /// Servicio de almacenamiento seguro usando MAUI SecureStorage
    /// SecureStorage utiliza APIs nativas del sistema:
    /// - Android: EncryptedSharedPreferences (API 23+) o KeyStore
    /// </summary>
    public class SecureStorageService : ISecureStorageService
    {
        /// <summary>
        /// Verifica si existe una clave en el almacenamiento seguro
        /// </summary
        /// <param name="key">Clave a verificar</param>
        /// <returns>True si existe, false si no</returns>
        public bool HasKey(string key)
        {
            try
            {
                var value = SecureStorage.GetAsync(key).Result;
                return !string.IsNullOrEmpty(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene un valor del almacenamiento seguro
        /// </summary>
        /// <param name="key">Clave del valor</param>
        /// <returns>Valor almacenado o null si no existe</returns>
        public string? Get(string key)
        {
            try
            {
                return SecureStorage.GetAsync(key).Result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Guarda un valor de forma segura
        /// </summary>
        /// <param name="key">Clave para el valor</param>
        /// <param name="value">Valor a guardar</param>
        public void Set(string key, string value)
        {
            try
            {
                SecureStorage.SetAsync(key, value).Wait();
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                System.Diagnostics.Debug.WriteLine($"Error al guardar en SecureStorage: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un valor del almacenamiento seguro
        /// </summary>
        /// <param name="key">Clave del valor a eliminar</param>
        public void Remove(string key)
        {
            try
            {
                SecureStorage.Remove(key);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al eliminar de SecureStorage: {ex.Message}");
            }
        }
    }
}
