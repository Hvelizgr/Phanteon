# Configuración de Servicios API

## Registro de Servicios Refit

### Opción 1: Básico (Sin reintentos)

```csharp
using Phanteon.Services.Api;
using Refit;

// En MauiProgram.CreateMauiApp()
// Después de registrar los servicios core...

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
```

### Opción 2: Con Polly (Recomendado)

```csharp
using Phanteon.Services.Api;
using Phanteon.Constants;
using Refit;
using Polly;
using Polly.Extensions.Http;

// En MauiProgram.CreateMauiApp()

// ========== APIs con Refit y Polly ==========
var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        AppConstants.MaxRetryAttempts,
        retryAttempt => TimeSpan.FromSeconds(AppConstants.RetryDelaySeconds * retryAttempt)
    );

builder.Services.AddRefitClient<IDispositivosApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
        c.Timeout = ApiConfiguration.Timeout;
    })
    .AddPolicyHandler(retryPolicy);

builder.Services.AddRefitClient<IUsuariosApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
        c.Timeout = ApiConfiguration.Timeout;
    })
    .AddPolicyHandler(retryPolicy);

builder.Services.AddRefitClient<IAlertasApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
        c.Timeout = ApiConfiguration.Timeout;
    })
    .AddPolicyHandler(retryPolicy);
```

### Opción 3: Con Factory Personalizado

```csharp
using Phanteon.Services.Api;
using Phanteon.Services.Http;
using Refit;

// En MauiProgram.CreateMauiApp()

var httpClientFactory = builder.Services.BuildServiceProvider()
    .GetRequiredService<IApiHttpClientFactory>();

builder.Services.AddRefitClient<IDispositivosApi>()
    .ConfigurePrimaryHttpMessageHandler(() => httpClientFactory.CreateMessageHandler());

builder.Services.AddRefitClient<IUsuariosApi>()
    .ConfigurePrimaryHttpMessageHandler(() => httpClientFactory.CreateMessageHandler());

builder.Services.AddRefitClient<IAlertasApi>()
    .ConfigurePrimaryHttpMessageHandler(() => httpClientFactory.CreateMessageHandler());
```

## Uso en ViewModels

### Ejemplo: ViewModel con API de Dispositivos

```csharp
using Phanteon.Core.ViewModels;
using Phanteon.Services.Api;
using Phanteon.Services.Navigation;
using Phanteon.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Phanteon.Features.Dispositivos
{
    public partial class DispositivosListViewModel : BaseViewModel
    {
        private readonly IDispositivosApi _dispositivosApi;
        private readonly INavigationService _navigation;

        [ObservableProperty]
        private ObservableCollection<Dispositivo> dispositivos = new();

        public DispositivosListViewModel(
            IDispositivosApi dispositivosApi,
            INavigationService navigation)
        {
            _dispositivosApi = dispositivosApi;
            _navigation = navigation;

            Titulo = "Dispositivos";
        }

        [RelayCommand]
        private async Task CargarDispositivosAsync()
        {
            if (EstaCargando)
                return;

            try
            {
                EstaCargando = true;
                LimpiarError();

                var items = await _dispositivosApi.GetDispositivosAsync();

                Dispositivos.Clear();
                foreach (var item in items)
                {
                    Dispositivos.Add(item);
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex, "cargar dispositivos");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task SeleccionarDispositivoAsync(Dispositivo dispositivo)
        {
            if (dispositivo == null)
                return;

            var parameters = new Dictionary<string, object>
            {
                { "DispositivoId", dispositivo.Id }
            };

            await _navigation.NavigateToAsync("dispositivodetail", parameters);
        }

        [RelayCommand]
        private async Task EliminarDispositivoAsync(Dispositivo dispositivo)
        {
            if (dispositivo == null)
                return;

            try
            {
                EstaCargando = true;

                await _dispositivosApi.DeleteDispositivoAsync(dispositivo.Id);
                Dispositivos.Remove(dispositivo);
            }
            catch (Exception ex)
            {
                ManejarError(ex, "eliminar dispositivo");
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }
}
```

### Ejemplo: ViewModel con múltiples APIs

```csharp
public partial class DashboardViewModel : BaseViewModel
{
    private readonly IDispositivosApi _dispositivosApi;
    private readonly IAlertasApi _alertasApi;
    private readonly IUsuariosApi _usuariosApi;

    public DashboardViewModel(
        IDispositivosApi dispositivosApi,
        IAlertasApi alertasApi,
        IUsuariosApi usuariosApi)
    {
        _dispositivosApi = dispositivosApi;
        _alertasApi = alertasApi;
        _usuariosApi = usuariosApi;
    }

    [RelayCommand]
    private async Task CargarDashboardAsync()
    {
        try
        {
            EstaCargando = true;

            // Cargar datos en paralelo
            var dispositivosTask = _dispositivosApi.GetDispositivosAsync();
            var alertasTask = _alertasApi.GetAlertasAsync();
            var usuariosTask = _usuariosApi.GetUsuariosAsync();

            await Task.WhenAll(dispositivosTask, alertasTask, usuariosTask);

            // Procesar resultados
            var dispositivos = await dispositivosTask;
            var alertas = await alertasTask;
            var usuarios = await usuariosTask;

            // Actualizar UI...
        }
        catch (Exception ex)
        {
            ManejarError(ex, "cargar dashboard");
        }
        finally
        {
            EstaCargando = false;
        }
    }
}
```

## Headers Personalizados

### Agregar Token de Autenticación

```csharp
using Refit;

public interface IDispositivosApi
{
    [Get("/api/dispositivos")]
    [Headers("Authorization: Bearer")]
    Task<List<Dispositivo>> GetDispositivosAsync([Header("Authorization")] string token);
}

// Uso:
var token = await _secureStorage.GetAsync(AppConstants.StorageKeys.AuthToken);
var dispositivos = await _dispositivosApi.GetDispositivosAsync($"Bearer {token}");
```

### Usando DelegatingHandler (Recomendado para tokens)

```csharp
// Crear AuthHeaderHandler.cs en Services/Http/

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ISecureStorageService _storage;

    public AuthHeaderHandler(ISecureStorageService storage)
    {
        _storage = storage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _storage.GetAsync(AppConstants.StorageKeys.AuthToken);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

// Registrar en MauiProgram.cs:
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddRefitClient<IDispositivosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl))
    .AddHttpMessageHandler<AuthHeaderHandler>();
```

## Manejo de Respuestas

### Con ApiResponse (Recomendado para mayor control)

```csharp
using Refit;

public interface IDispositivosApi
{
    [Get("/api/dispositivos/{id}")]
    Task<ApiResponse<Dispositivo>> GetDispositivoAsync(int id);
}

// Uso:
var response = await _dispositivosApi.GetDispositivoAsync(id);

if (response.IsSuccessStatusCode)
{
    var dispositivo = response.Content;
    // Usar dispositivo...
}
else
{
    // Manejar error según código de estado
    switch (response.StatusCode)
    {
        case HttpStatusCode.NotFound:
            EstablecerError("Dispositivo no encontrado");
            break;
        case HttpStatusCode.Unauthorized:
            EstablecerError("No autorizado");
            break;
        default:
            EstablecerError($"Error: {response.StatusCode}");
            break;
    }
}
```

---

**Última actualización:** Noviembre 2025
