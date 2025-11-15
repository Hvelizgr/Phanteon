using Phanteon.Services.Auth;

namespace Phanteon.Services.Navigation
{
    /// <summary>
    /// Implementación del servicio de navegación inicial
    /// (SRP: Solo se encarga de decidir y ejecutar la navegación inicial)
    /// (DIP: Depende de abstracción ISessionManager, no de implementación concreta)
    /// </summary>
    public class StartupNavigationService : IStartupNavigationService
    {
        private readonly ISessionManager _sessionManager;

        public StartupNavigationService(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public async Task NavigateToInitialPageAsync(Shell shell)
        {
            try
            {
                var hasActiveSession = await _sessionManager.HasActiveSessionAsync();

                if (hasActiveSession)
                {
                    await shell.GoToAsync("//Main");
                }
                else
                {
                    await shell.GoToAsync("//Login");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[StartupNav] Error: {ex.Message}");
                // En caso de error, navegar a Login por seguridad
                try
                {
                    await shell.GoToAsync("//Login");
                }
                catch
                {
                    // Fallback silencioso
                }
            }
        }
    }
}
