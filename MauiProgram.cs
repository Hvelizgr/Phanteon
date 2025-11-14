using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Phanteon.Helpers;
using Phanteon.Services.Http;
using Phanteon.Services.Storage;
using Phanteon.Services.Navigation;
using Phanteon.Services.Auth;
using Phanteon.Features.Main;
using Phanteon.Features.Auth;
using Phanteon.Features.Profile;
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
                });

            // ========== Servicios Core ==========
            builder.Services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();
            builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();

            // ========== ViewModels ==========
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();

            // ========== Pages ==========
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<ProfilePage>();

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
