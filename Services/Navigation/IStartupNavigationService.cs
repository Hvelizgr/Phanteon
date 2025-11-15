namespace Phanteon.Services.Navigation
{
    /// <summary>
    /// Servicio especializado para navegación durante el inicio de la aplicación
    /// (ISP: Interface Segregation - separamos la navegación de inicio de la navegación general)
    /// </summary>
    public interface IStartupNavigationService
    {
        Task NavigateToInitialPageAsync(Shell shell);
    }
}
