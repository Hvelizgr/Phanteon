namespace Phanteon.Services.Navigation
{
    /// <summary>
    /// Servicio para navegación entre páginas
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navega a una ruta específica
        /// </summary>
        Task NavigateToAsync(string route);

        /// <summary>
        /// Navega a una ruta específica con parámetros
        /// </summary>
        Task NavigateToAsync(string route, IDictionary<string, object> parameters);

        /// <summary>
        /// Navega hacia atrás
        /// </summary>
        Task GoBackAsync();

        /// <summary>
        /// Navega a la raíz de la aplicación
        /// </summary>
        Task NavigateToRootAsync();
    }
}
