namespace Phanteon.Services.Storage
{
    /// <summary>
    /// Servicio para almacenamiento seguro de datos sensibles
    /// </summary>
    public interface ISecureStorageService
    {
        /// <summary>
        /// Guarda un valor de manera segura
        /// </summary>
        Task SetAsync(string key, string value);

        /// <summary>
        /// Obtiene un valor almacenado de manera segura
        /// </summary>
        Task<string?> GetAsync(string key);

        /// <summary>
        /// Elimina un valor del almacenamiento seguro
        /// </summary>
        bool Remove(string key);

        /// <summary>
        /// Elimina todos los valores del almacenamiento seguro
        /// </summary>
        void RemoveAll();
    }
}
