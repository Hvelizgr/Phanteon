using Phanteon.Features.Auth.Views;
using Phanteon.Features.Main.Views;
using Phanteon.Features.Profile.Views;
using Phanteon.Features.Dispositivos.Views;
using Phanteon.Features.Alertas.Views;

namespace Phanteon
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas de navegación
            // Ruta de registro (Login y Main ya están definidos en XAML)
            Routing.RegisterRoute("Register", typeof(RegisterPage));

            // Rutas hijas (relativas para navegación push)
            Routing.RegisterRoute("Main/Profile", typeof(ProfilePage));
            Routing.RegisterRoute("Main/Dispositivos", typeof(DispositivosPage));
            Routing.RegisterRoute("Main/Alertas", typeof(AlertasPage));
            Routing.RegisterRoute("Main/Dispositivos/DispositivoDetail", typeof(DispositivoDetailPage));

            // Mantener rutas cortas para compatibilidad
            Routing.RegisterRoute("Profile", typeof(ProfilePage));
            Routing.RegisterRoute("Dispositivos", typeof(DispositivosPage));
            Routing.RegisterRoute("DispositivoDetail", typeof(DispositivoDetailPage));
            Routing.RegisterRoute("Alertas", typeof(AlertasPage));
        }
    }
}
