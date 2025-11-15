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
    /// <summary>
    /// Configuración principal de la aplicación MAUI
    /// Registra servicios, ViewModels, Pages y APIs
    /// </summary>
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

                    // Font Awesome para iconos
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
                });

            // Servicios core - Singleton para persistencia durante toda la app
            builder.Services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();
            builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            // Servicios de autenticación y manejo de sesión
            builder.Services.AddSingleton<ISessionManager, SessionManager>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IStartupNavigationService, StartupNavigationService>();

            // ViewModels - Transient para nueva instancia cada vez
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<DispositivosViewModel>();
            builder.Services.AddTransient<DispositivoDetailViewModel>();
            builder.Services.AddTransient<AlertasViewModel>();

            // Pages - Transient para inyección de ViewModels
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<DispositivosPage>();
            builder.Services.AddTransient<DispositivoDetailPage>();
            builder.Services.AddTransient<AlertasPage>();

            // APIs con Refit - Cliente HTTP tipado para consumo de API REST
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

        // Política de reintentos con Polly (opcional, descomentado si se necesita)
        // private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        // {
        //     return HttpPolicyExtensions
        //         .HandleTransientHttpError()
        //         .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        // }
    }
}
