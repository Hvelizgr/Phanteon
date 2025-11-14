using Phanteon.Features.Auth;
using Phanteon.Features.Main;
using Phanteon.Features.Profile;

namespace Phanteon
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registrar rutas de navegación
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("Register", typeof(RegisterPage));
            Routing.RegisterRoute("Main", typeof(MainPage));
            Routing.RegisterRoute("Profile", typeof(ProfilePage));
        }
    }
}
