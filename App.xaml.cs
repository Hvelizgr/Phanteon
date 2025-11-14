using Phanteon.Services.Storage;

namespace Phanteon
{
    public partial class App : Application
    {
        private readonly ISecureStorageService _secureStorageService;

        public App(ISecureStorageService secureStorageService)
        {
            InitializeComponent();
            _secureStorageService = secureStorageService;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();

            // Verificar si hay un usuario logueado
            Task.Run(async () =>
            {
                var userId = await _secureStorageService.GetAsync("user_id");

                // Si hay un usuario logueado, navegar a Main
                if (!string.IsNullOrEmpty(userId))
                {
                    await shell.GoToAsync("//Main");
                }
                // Si no, se queda en Login (página inicial del Shell)
            });

            return new Window(shell);
        }
    }
}