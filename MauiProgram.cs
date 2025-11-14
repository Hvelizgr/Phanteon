using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Phanteon.Helpers;
using Phanteon.Services.Http;
using Phanteon.Services.Storage;
using Phanteon.Services.Navigation;
using Phanteon.Features.Main;

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

            // ========== ViewModels ==========
            builder.Services.AddTransient<MainViewModel>();

            // ========== Pages ==========
            builder.Services.AddTransient<MainPage>();

            // TODO: Cuando crees servicios API con Refit, regístralos aquí
            // Ejemplo:
            // builder.Services.AddRefitClient<IDispositivosApi>()
            //     .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl))
            //     .AddPolicyHandler(GetRetryPolicy());

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
