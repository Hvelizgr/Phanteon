using System;
using System.Net.Http;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Maui;
using Refit;
using Refit.NewtonsoftJson;
using Polly;
using Polly.Extensions.Http;
using Phanteon.Services.Interfaces;
using Phanteon.Services.Implementations;
using Phanteon.ViewModels;
        
namespace Phanteon
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
