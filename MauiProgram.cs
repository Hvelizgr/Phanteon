using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Phanteon.Helpers;
using Phanteon.Services.Interfaces;
using Phanteon.ViewModels;
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

#if DEBUG
            // Configurar HttpClient para aceptar certificados SSL de desarrollo
            var httpClientHandler = new System.Net.Http.HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
#endif

            // Registrar servicios de Refit con la URL base de la API
            builder.Services
                .AddRefitClient<IDispositivosService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                })
#if DEBUG
                .ConfigurePrimaryHttpMessageHandler(() => httpClientHandler)
#endif
                ;

            builder.Services
                .AddRefitClient<IAlertasService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                })
#if DEBUG
                .ConfigurePrimaryHttpMessageHandler(() => new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                })
#endif
                ;

            builder.Services
                .AddRefitClient<IUsuariosService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                })
#if DEBUG
                .ConfigurePrimaryHttpMessageHandler(() => new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                })
#endif
                ;

            builder.Services
                .AddRefitClient<IHistorialDispositivosService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
                    c.Timeout = ApiConfiguration.Timeout;
                })
#if DEBUG
                .ConfigurePrimaryHttpMessageHandler(() => new System.Net.Http.HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                })
#endif
                ;

            // Registrar ViewModels
            builder.Services.AddTransient<DispositivosViewModel>();
            builder.Services.AddTransient<DiagnosticoViewModel>();

            // Registrar Páginas
            builder.Services.AddTransient<Views.DispositivosPage>();
            builder.Services.AddTransient<Views.DiagnosticoPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
