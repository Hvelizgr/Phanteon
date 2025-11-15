# Guía de Desarrollo Rápido - Phanteon

## Estructura del Proyecto

El proyecto utiliza una estructura profesional y escalable:

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

## Servicios Disponibles
- ✅ `IApiHttpClientFactory` - Factory de HttpClient
- ✅ `ISecureStorageService` - Almacenamiento seguro
- ✅ `INavigationService` - Navegación entre páginas
- ✅ `IDispositivosApi` - API de Dispositivos (Refit)
- ✅ `IUsuariosApi` - API de Usuarios (Refit)
- ✅ `IAlertasApi` - API de Alertas (Refit)

## Core Components
- `BaseViewModel` - ViewModel base con propiedades comunes
- `BoolToColorConverter` - Convierte bool a color
- `InvertedBoolConverter` - Invierte booleanos
- `StringNotEmptyConverter` - Verifica si string no está vacío
- `EventToCommandBehavior` - Convierte eventos en comandos

## Constants
- `ApiEndpoints` - Endpoints de la API
- `AppConstants` - Constantes generales
- `ErrorMessages` - Mensajes de error estandarizados

---

## Ejemplo: Módulo de Dispositivos

### Estructura
```
Features/Dispositivos/
├── DispositivosListPage.xaml
├── DispositivosListPage.xaml.cs
└── DispositivosListViewModel.cs
```

### ViewModel
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

### View (XAML)
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

### Code-behind
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

### Registro en MauiProgram.cs
```csharp
// ViewModels
builder.Services.AddTransient<DispositivosListViewModel>();

// Pages
builder.Services.AddTransient<DispositivosListPage>();
```

### Registro de Ruta en AppShell
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

---

## Recursos Adicionales

- [Documentación .NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- [Refit](https://github.com/reactiveui/refit)
- [Polly](https://github.com/App-vNext/Polly)

---

**Última actualización:** Noviembre 2025
