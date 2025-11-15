using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Phanteon.Helpers;
using Phanteon.Services.Http;
using Phanteon.Services.Storage;
using Phanteon.Services.Navigation;
using Phanteon.Services.Auth;
using Phanteon.Features.Main.Views;
using Phanteon.Features.Main.ViewModels;
using Phanteon.Features.Auth.Views;
using Phanteon.Features.Auth.ViewModels;
using Phanteon.Features.Profile.Views;
using Phanteon.Features.Profile.ViewModels;
using Phanteon.Features.Dispositivos.Views;
using Phanteon.Features.Dispositivos.ViewModels;
using Phanteon.Features.Alertas.Views;
using Phanteon.Features.Alertas.ViewModels;
using Phanteon.Services.Api;
using Refit;

namespace Phanteon
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    // Font Awesome Icons
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                });

            // ========== Servicios Core (aplicando SOLID) ==========
            // Singleton: Servicios que viven durante toda la aplicación
            builder.Services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();
            builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            // Servicios de autenticación y sesión (SRP, DIP, ISP aplicados)
            builder.Services.AddSingleton<ISessionManager, SessionManager>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IStartupNavigationService, StartupNavigationService>();

            // ========== ViewModels ==========
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<DispositivosViewModel>();
            builder.Services.AddTransient<DispositivoDetailViewModel>();
            builder.Services.AddTransient<AlertasViewModel>();

            // ========== Pages ==========
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<DispositivosPage>();
            builder.Services.AddTransient<DispositivoDetailPage>();
            builder.Services.AddTransient<AlertasPage>();

            // ========== APIs con Refit ==========
            builder.Services.AddRefitClient<IDispositivosApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                });

            builder.Services.AddRefitClient<IUsuariosApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                });

            builder.Services.AddRefitClient<IAlertasApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        // Ejemplo de política de reintentos para Refit
        // private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        // {
        //     return HttpPolicyExtensions
        //         .HandleTransientHttpError()
        //         .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        // }
    }
}
