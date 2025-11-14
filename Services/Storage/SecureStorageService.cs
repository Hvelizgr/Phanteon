namespace Phanteon.Services.Storage
{
    /// <summary>
    /// Implementación del servicio de almacenamiento seguro usando SecureStorage de MAUI
    /// </summary>
    public class SecureStorageService : ISecureStorageService
    {
        // Límite de tamaño para SecureStorage (2KB es un límite seguro para la mayoría de plataformas)
        private const int MaxValueSize = 2048;

        public async Task SetAsync(string key, string value)
        {
            try
            {
                // Validar que el valor no sea nulo
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                // Validar tamaño del valor
                if (value.Length > MaxValueSize)
                {
                    throw new InvalidOperationException(
                        $"El valor para la clave '{key}' excede el tamaño máximo permitido " +
                        $"({value.Length} caracteres). Máximo: {MaxValueSize} caracteres. " +
                        $"SecureStorage no debe usarse para datos grandes.");
                }

                await SecureStorage.Default.SetAsync(key, value);
            }
            catch (Exception ex)
            {
                // Log error
                throw new InvalidOperationException($"Error al guardar en SecureStorage: {ex.Message}", ex);
            }
        }

        public async Task<string?> GetAsync(string key)
        {
            try
            {
                return await SecureStorage.Default.GetAsync(key);
            }
            catch (Exception ex)
            {
                // Log error
                throw new InvalidOperationException($"Error al leer de SecureStorage: {ex.Message}", ex);
            }
        }

        public bool Remove(string key)
        {
            try
            {
                return SecureStorage.Default.Remove(key);
            }
            catch (Exception ex)
            {
                // Log error
                throw new InvalidOperationException($"Error al eliminar de SecureStorage: {ex.Message}", ex);
            }
        }

        public void RemoveAll()
        {
            try
            {
                SecureStorage.Default.RemoveAll();
            }
            catch (Exception ex)
            {
                // Log error
                throw new InvalidOperationException($"Error al limpiar SecureStorage: {ex.Message}", ex);
            }
        }
    }
}
