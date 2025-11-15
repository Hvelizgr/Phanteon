# 📋 Phanteon Cheat Sheet

> Referencia rápida para desarrolladores - Todo lo que necesitas en un solo lugar

---

## 🚀 Comandos de Inicio Rápido

### Setup Inicial
```bash
# Clonar y configurar
git clone https://github.com/Hvelizgr/Phanteon.git
cd Phanteon
git checkout ControllerBD
git checkout -b feature/tu-nombre-tarea

# Restaurar y ejecutar
dotnet restore
dotnet build
dotnet run
```

### Backend (DevicesAPI)
```bash
# En otra terminal
cd DevicesAPI
dotnet run
# Debe mostrar: "Now listening on: https://localhost:7026"
```

---

## 🏗️ Estructura de Archivos

### Crear un Nuevo Módulo
```
Features/
└── MiModulo/
    ├── MiModuloPage.xaml
    ├── MiModuloPage.xaml.cs
    └── MiModuloViewModel.cs
```

### Registrar en MauiProgram.cs
```csharp
// ViewModels
builder.Services.AddTransient<MiModuloViewModel>();

// Pages
builder.Services.AddTransient<MiModuloPage>();
```

### Registrar Ruta en AppShell.xaml.cs
```csharp
public AppShell()
{
    InitializeComponent();
    Routing.RegisterRoute("mimodulo", typeof(Features.MiModulo.MiModuloPage));
}
```

---

## 💻 Código de ViewModel

### Template Básico
```csharp
using Phanteon.Core.ViewModels;
using Phanteon.Services.Api;
using Phanteon.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Phanteon.Features.MiModulo
{
    public partial class MiModuloViewModel : BaseViewModel
    {
        private readonly IMiApi _api;

        public MiModuloViewModel(IMiApi api)
        {
            _api = api;
            Titulo = "Mi Módulo";
        }

        [ObservableProperty]
        private ObservableCollection<MiModelo> items = new();

        [ObservableProperty]
        private MiModelo itemSeleccionado;

        [RelayCommand]
        private async Task CargarAsync()
        {
            if (EstaCargando) return;

            try
            {
                EstaCargando = true;
                LimpiarError();

                var data = await _api.GetAllAsync();

                Items.Clear();
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex, "cargar datos");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task SeleccionarAsync(MiModelo item)
        {
            if (item == null) return;

            ItemSeleccionado = item;
            // Navegar o hacer algo con el item
        }

        [RelayCommand]
        private async Task EliminarAsync(MiModelo item)
        {
            if (item == null) return;

            try
            {
                EstaCargando = true;
                await _api.DeleteAsync(item.Id);
                Items.Remove(item);
            }
            catch (Exception ex)
            {
                ManejarError(ex, "eliminar");
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }
}
```

---

## 📄 Código XAML

### Page Template
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:Phanteon.Features.MiModulo"
             xmlns:models="clr-namespace:Phanteon.Models"
             x:Class="Phanteon.Features.MiModulo.MiModuloPage"
             x:DataType="vm:MiModuloViewModel"
             Title="{Binding Titulo}">

    <Grid RowDefinitions="Auto,*" Padding="20">

        <!-- Indicador de carga -->
        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"
                          Grid.Row="0"/>

        <!-- Mensaje de error -->
        <Label Text="{Binding MensajeError}"
               IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}"
               TextColor="Red"
               Grid.Row="0"/>

        <!-- Contenido principal -->
        <RefreshView Command="{Binding CargarCommand}"
                    IsRefreshing="{Binding EstaCargando}"
                    Grid.Row="1">

            <CollectionView ItemsSource="{Binding Items}">
                <CollectionView.ItemTemplate>
                    <DataTemplate x:DataType="models:MiModelo">
                        <Frame Padding="10" Margin="0,5">
                            <Grid ColumnDefinitions="*,Auto">
                                <!-- Contenido del item -->
                                <Label Text="{Binding Nombre}"
                                       FontSize="16"
                                       Grid.Column="0"/>

                                <!-- Botón de acción -->
                                <Button Text="Ver"
                                        Command="{Binding Source={RelativeSource AncestorType={x:Type vm:MiModuloViewModel}}, Path=SeleccionarCommand}"
                                        CommandParameter="{Binding .}"
                                        Grid.Column="1"/>
                            </Grid>
                        </Frame>
                    </DataTemplate>
                </CollectionView.ItemTemplate>

                <!-- Empty view -->
                <CollectionView.EmptyView>
                    <VerticalStackLayout VerticalOptions="Center"
                                        HorizontalOptions="Center">
                        <Label Text="No hay datos"
                               FontSize="18"
                               TextColor="Gray"/>
                    </VerticalStackLayout>
                </CollectionView.EmptyView>
            </CollectionView>
        </RefreshView>
    </Grid>
</ContentPage>
```

### Code-Behind Template
```csharp
namespace Phanteon.Features.MiModulo;

public partial class MiModuloPage : ContentPage
{
    public MiModuloPage(MiModuloViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is MiModuloViewModel vm)
        {
            await vm.CargarCommand.ExecuteAsync(null);
        }
    }
}
```

---

## 🔌 Servicios API (Refit)

### Definir Interface
```csharp
// Services/Api/IMiApi.cs
using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Api
{
    public interface IMiApi
    {
        [Get("/api/mirecurso")]
        Task<List<MiModelo>> GetAllAsync();

        [Get("/api/mirecurso/{id}")]
        Task<MiModelo> GetByIdAsync(int id);

        [Post("/api/mirecurso")]
        Task<MiModelo> CreateAsync([Body] MiModelo modelo);

        [Put("/api/mirecurso/{id}")]
        Task<MiModelo> UpdateAsync(int id, [Body] MiModelo modelo);

        [Delete("/api/mirecurso/{id}")]
        Task DeleteAsync(int id);

        // Con ApiResponse para mejor manejo
        [Get("/api/mirecurso/{id}")]
        Task<ApiResponse<MiModelo>> GetByIdWithResponseAsync(int id);
    }
}
```

### Registrar en MauiProgram.cs
```csharp
builder.Services.AddRefitClient<IMiApi>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(ApiConfiguration.BaseUrl);
        c.Timeout = ApiConfiguration.Timeout;
    })
#if DEBUG
    .ConfigurePrimaryHttpMessageHandler(() => httpClientHandler)
#endif
    ;
```

---

## 🎨 Bindings XAML Comunes

### Text
```xml
<Label Text="{Binding Nombre}"/>
<Label Text="{Binding Fecha, StringFormat='{0:dd/MM/yyyy}'}"/>
<Label Text="{Binding Precio, StringFormat='${0:N2}'}"/>
```

### Visibility
```xml
<Label IsVisible="{Binding EstaCargando}"/>
<Label IsVisible="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}"/>
<Label IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}"/>
```

### Commands
```xml
<Button Text="Cargar" Command="{Binding CargarCommand}"/>
<Button Text="Guardar" Command="{Binding GuardarCommand}" CommandParameter="{Binding Item}"/>
```

### Collections
```xml
<CollectionView ItemsSource="{Binding Items}">
    <CollectionView.ItemTemplate>
        <DataTemplate x:DataType="models:MiModelo">
            <!-- Tu diseño aquí -->
        </DataTemplate>
    </CollectionView.ItemTemplate>
</CollectionView>
```

---

## 🔄 Git Workflow

### Commits
```bash
# Formato: tipo: descripción breve
git commit -m "feat: Agregar módulo de alertas"
git commit -m "fix: Corregir bug en login"
git commit -m "docs: Actualizar README"
git commit -m "refactor: Mejorar estructura de viewmodels"
```

### Actualizar desde ControllerBD
```bash
git checkout ControllerBD
git pull origin ControllerBD
git checkout feature/tu-rama
git merge ControllerBD
git push origin feature/tu-rama
```

### Resolver Conflictos
```bash
# 1. Haz merge (aparecen conflictos)
git merge ControllerBD

# 2. Abre archivos con conflicto
# Busca <<<<<<, ====== y >>>>>>

# 3. Edita y elimina marcadores

# 4. Marca como resuelto
git add .
git commit -m "merge: Resolver conflictos desde ControllerBD"
```

---

## 🌐 Navegación

### Navegar a otra página
```csharp
// Simple
await Shell.Current.GoToAsync("nombreRuta");

// Con parámetros
await Shell.Current.GoToAsync($"nombreRuta?id={itemId}");

// Con múltiples parámetros
await Shell.Current.GoToAsync($"nombreRuta?id={id}&nombre={nombre}");

// Atrás
await Shell.Current.GoToAsync("..");
```

### Recibir parámetros
```csharp
[QueryProperty(nameof(ItemId), "id")]
public partial class DetalleViewModel : BaseViewModel
{
    [ObservableProperty]
    private int itemId;

    partial void OnItemIdChanged(int value)
    {
        _ = CargarDetalleCommand.ExecuteAsync(null);
    }
}
```

---

## 🛠️ Utilidades

### Converters Disponibles
```xml
<!-- En Resources/Styles/Converters.xaml -->
<conv:BoolToColorConverter x:Key="BoolToColorConverter"/>
<conv:InvertedBoolConverter x:Key="InvertedBoolConverter"/>
<conv:StringNotEmptyConverter x:Key="StringNotEmptyConverter"/>
```

### BaseViewModel Propiedades
```csharp
// Heredando de BaseViewModel tienes:
EstaCargando         // bool - Indica si está cargando
MensajeError         // string - Mensaje de error actual
Titulo              // string - Título de la página
```

### BaseViewModel Métodos
```csharp
LimpiarError()                              // Limpia mensaje de error
EstablecerError(string mensaje)            // Establece un error
ManejarError(Exception ex, string accion)  // Maneja excepción y muestra mensaje
```

---

## 🔧 Configuración

### URLs de API
```csharp
// Helpers/ApiConfiguration.cs

// Emulador Android (default)
public static string BaseUrl = "https://10.0.2.2:7026";

// Windows Desktop
public static string BaseUrl = "https://localhost:7026";

// Dispositivo físico (reemplazar IP)
public static string BaseUrl = "https://192.168.1.XXX:7026";
```

---

## ❌ Errores Comunes

| Error | Solución Rápida |
|-------|----------------|
| `Connection refused` | Verificar que DevicesAPI esté corriendo |
| `Cannot resolve service` | Registrar en MauiProgram.cs |
| `ObservableProperty not found` | `using CommunityToolkit.Mvvm.ComponentModel;` |
| `RelayCommand not found` | `using CommunityToolkit.Mvvm.Input;` |
| `Binding not found` | Verificar `x:DataType` en XAML |
| `SSL Certificate failed` | Ya solucionado en modo DEBUG |

---

## 📚 Links Rápidos

- **[Guía de Inicio](02_Empezar_Aqui.md)** - Setup en 5 minutos
- **[Arquitectura](08_Arquitectura.md)** - Estructura del proyecto
- **[Configuración API](09_Configuracion_Servicios.md)** - Setup de Refit
- **[Solución Problemas](06_Solucion_Problemas.md)** - Errores y soluciones
- **[Guía Git](07_Como_Hacer_Commits.md)** - Commits y workflow

---

## 💡 Tips Útiles

### Performance
```csharp
// Cargar datos en paralelo
var task1 = _api1.GetAsync();
var task2 = _api2.GetAsync();
await Task.WhenAll(task1, task2);
```

### Validaciones
```csharp
[ObservableProperty]
[NotifyCanExecuteChangedFor(nameof(GuardarCommand))]
private string nombre;

[RelayCommand(CanExecute = nameof(PuedeGuardar))]
private async Task GuardarAsync()
{
    // ...
}

private bool PuedeGuardar() => !string.IsNullOrWhiteSpace(Nombre);
```

### Refresh
```xml
<RefreshView Command="{Binding CargarCommand}"
            IsRefreshing="{Binding EstaCargando}">
    <CollectionView ItemsSource="{Binding Items}"/>
</RefreshView>
```

---

**Última actualización:** 14 de noviembre de 2025
