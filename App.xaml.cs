using Phanteon.Services.Navigation;

namespace Phanteon
{
    /// <summary>
    /// Aplicación principal MAUI
    /// Maneja la inicialización y navegación inicial basada en sesión del usuario
    /// </summary>
    public partial class App : Application
    {
        private readonly IStartupNavigationService _startupNavigationService;

        public App(IStartupNavigationService startupNavigationService)
        {
            InitializeComponent();
            _startupNavigationService = startupNavigationService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Navegación inicial con verificación de sesión
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(500);

                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            if (Windows.Count > 0 && Windows[0].Page is Shell shell)
                            {
                                await _startupNavigationService.NavigateToInitialPageAsync(shell);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error navegando: {ex}");
                        }
                    });
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error en inicialización: {ex}");
                }
            });
        }
    }
}