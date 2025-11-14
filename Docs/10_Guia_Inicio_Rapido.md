# Guía Rápida - Phanteon

## Estructura del Proyecto Actualizada

Tu proyecto ahora tiene una estructura profesional y escalable:

```
Phanteon/
├── Features/               # Módulos por funcionalidad
│   ├── Main/              # Página principal
│   └── Shared/            # Componentes compartidos
│
├── Core/                  # Componentes reutilizables
│   ├── ViewModels/       # BaseViewModel
│   ├── Converters/       # Value Converters
│   ├── Behaviors/        # Behaviors XAML
│   └── Controls/         # Controles personalizados
│
├── Services/              # Servicios de la aplicación
│   ├── Http/             # HttpClient Factory
│   ├── Api/              # Interfaces Refit (IDispositivosApi, etc.)
│   ├── Storage/          # SecureStorage
│   └── Navigation/       # Navegación
│
├── Models/                # Modelos de datos
├── Constants/             # Constantes (endpoints, mensajes, etc.)
├── Helpers/               # Utilidades
└── Data/                  # Repositorios y DB local
```

## Archivos Importantes Creados

### 1. Documentación
- `ARCHITECTURE.md` - Arquitectura detallada del proyecto
- `SERVICES_SETUP.md` - Guía de configuración de servicios API
- `QUICK_START.md` - Esta guía

### 2. Servicios Listos para Usar
- ✅ `IApiHttpClientFactory` - Factory de HttpClient
- ✅ `ISecureStorageService` - Almacenamiento seguro
- ✅ `INavigationService` - Navegación entre páginas
- ✅ `IDispositivosApi` - API de Dispositivos (Refit)
- ✅ `IUsuariosApi` - API de Usuarios (Refit)
- ✅ `IAlertasApi` - API de Alertas (Refit)

### 3. Core Components
- ✅ `BaseViewModel` - ViewModel base con propiedades comunes
- ✅ `BoolToColorConverter` - Convierte bool a color
- ✅ `InvertedBoolConverter` - Invierte booleanos
- ✅ `StringNotEmptyConverter` - Verifica si string no está vacío
- ✅ `EventToCommandBehavior` - Convierte eventos en comandos

### 4. Constants
- ✅ `ApiEndpoints` - Endpoints de la API
- ✅ `AppConstants` - Constantes generales
- ✅ `ErrorMessages` - Mensajes de error estandarizados

## Próximos Pasos Recomendados

### 1. Configurar la URL de tu API

Actualiza `Helpers/ApiConfiguration.cs`:

```csharp
public static class ApiConfiguration
{
    public static string BaseUrl => "https://tu-api.com";  // ← Cambiar aquí
    public static TimeSpan Timeout => TimeSpan.FromSeconds(30);
}
```

### 2. Registrar Servicios API (Opcional)

Si quieres usar los servicios API con Refit, descomentar en `MauiProgram.cs`:

```csharp
// Ejemplo de registro de API:
builder.Services.AddRefitClient<IDispositivosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl));

builder.Services.AddRefitClient<IUsuariosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl));

builder.Services.AddRefitClient<IAlertasApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl));
```

Ver `SERVICES_SETUP.md` para más opciones (con Polly, autenticación, etc.)

### 3. Actualizar Endpoints de API

Edita `Constants/ApiEndpoints.cs` con las rutas de tu API:

```csharp
public static class ApiEndpoints
{
    public const string Dispositivos = "/api/dispositivos";
    public const string Usuarios = "/api/usuarios";
    public const string Alertas = "/api/alertas";
}
```

### 4. Crear Tu Primera Feature

#### Ejemplo: Módulo de Dispositivos

1. **Crear estructura**:
```
Features/Dispositivos/
├── DispositivosListPage.xaml
├── DispositivosListPage.xaml.cs
└── DispositivosListViewModel.cs
```

2. **ViewModel** (`DispositivosListViewModel.cs`):
```csharp
using Phanteon.Core.ViewModels;
using Phanteon.Services.Api;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Phanteon.Features.Dispositivos
{
    public partial class DispositivosListViewModel : BaseViewModel
    {
        private readonly IDispositivosApi _api;

        [ObservableProperty]
        private ObservableCollection<Dispositivo> dispositivos = new();

        public DispositivosListViewModel(IDispositivosApi api)
        {
            _api = api;
            Titulo = "Dispositivos";
        }

        [RelayCommand]
        private async Task CargarAsync()
        {
            try
            {
                EstaCargando = true;
                var items = await _api.GetDispositivosAsync();

                Dispositivos.Clear();
                foreach (var item in items)
                    Dispositivos.Add(item);
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
    }
}
```

3. **View** (`DispositivosListPage.xaml`):
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:Phanteon.Features.Dispositivos"
             x:Class="Phanteon.Features.Dispositivos.DispositivosListPage"
             x:DataType="vm:DispositivosListViewModel"
             Title="{Binding Titulo}">

    <Grid RowDefinitions="Auto,*">
        <!-- Loading indicator -->
        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"
                          Grid.Row="0" />

        <!-- Error message -->
        <Label Text="{Binding MensajeError}"
               IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}"
               TextColor="Red"
               Grid.Row="0" />

        <!-- Lista de dispositivos -->
        <CollectionView ItemsSource="{Binding Dispositivos}"
                       Grid.Row="1">
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="models:Dispositivo">
                    <Frame Margin="10,5" Padding="10">
                        <VerticalStackLayout>
                            <Label Text="{Binding Nombre}" FontSize="18" FontAttributes="Bold" />
                            <Label Text="{Binding Descripcion}" />
                        </VerticalStackLayout>
                    </Frame>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>
    </Grid>

</ContentPage>
```

4. **Code-behind** (`DispositivosListPage.xaml.cs`):
```csharp
namespace Phanteon.Features.Dispositivos
{
    public partial class DispositivosListPage : ContentPage
    {
        public DispositivosListPage(DispositivosListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Cargar datos cuando la página aparece
            if (BindingContext is DispositivosListViewModel vm)
            {
                await vm.CargarCommand.ExecuteAsync(null);
            }
        }
    }
}
```

5. **Registrar en `MauiProgram.cs`**:
```csharp
// ViewModels
builder.Services.AddTransient<DispositivosListViewModel>();

// Pages
builder.Services.AddTransient<DispositivosListPage>();
```

6. **Agregar ruta en `AppShell.xaml`**:
```xml
<ShellContent
    Title="Dispositivos"
    ContentTemplate="{DataTemplate dispositivos:DispositivosListPage}"
    Route="Dispositivos" />
```

## Usando los Converters

Los converters ya están registrados en `App.xaml` y listos para usar:

```xml
<!-- Mostrar label solo si hay error -->
<Label Text="{Binding MensajeError}"
       IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}" />

<!-- Deshabilitar botón mientras está cargando -->
<Button Text="Guardar"
        IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}" />

<!-- Cambiar color según estado -->
<Label Text="{Binding Estado}"
       TextColor="{Binding EstaActivo, Converter={StaticResource BoolToColorConverter}}" />
```

## Usando BaseViewModel

Todos tus ViewModels deberían heredar de `BaseViewModel`:

```csharp
public partial class MiViewModel : BaseViewModel
{
    // Ya tienes disponible:
    // - EstaCargando (bool)
    // - MensajeError (string)
    // - Titulo (string)
    // - TieneError (bool)
    // - ManejarError(Exception, string)
    // - LimpiarError()
    // - EstablecerError(string)
}
```

## Patrones de Código Comunes

### 1. Llamada a API con manejo de errores
```csharp
[RelayCommand]
private async Task GuardarAsync()
{
    try
    {
        EstaCargando = true;
        LimpiarError();

        await _api.CreateDispositivoAsync(NuevoDispositivo);

        // Navegar atrás o mostrar mensaje
    }
    catch (Exception ex)
    {
        ManejarError(ex, "guardar dispositivo");
    }
    finally
    {
        EstaCargando = false;
    }
}
```

### 2. Navegación con parámetros
```csharp
var parameters = new Dictionary<string, object>
{
    { "DispositivoId", dispositivo.Id }
};

await _navigation.NavigateToAsync("dispositivodetail", parameters);
```

### 3. Almacenamiento seguro
```csharp
// Guardar
await _secureStorage.SetAsync(AppConstants.StorageKeys.AuthToken, token);

// Leer
var token = await _secureStorage.GetAsync(AppConstants.StorageKeys.AuthToken);

// Eliminar
_secureStorage.Remove(AppConstants.StorageKeys.AuthToken);
```

## Limpieza Pendiente

Puedes eliminar las siguientes carpetas/archivos antiguos si ya no los necesitas:

- `Views/Main/` (ya movido a `Features/Main/`)
- `ViewModels/` (ya movido a `Core/ViewModels/`)
- `Services/Interfaces/` y `Services/Implementations/` (ya movidos a subcarpetas en `Services/`)

## Recursos Adicionales

- [Documentación .NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- [Refit](https://github.com/reactiveui/refit)
- [Polly](https://github.com/App-vNext/Polly)

## Verificar Build

Para verificar que todo compila correctamente:

```bash
dotnet build
```

El proyecto debería compilar sin errores ✅
