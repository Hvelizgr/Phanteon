# 🎨 Mockups y Ejemplos de Páginas XAML

> Guía visual y código de ejemplo para las páginas XAML del proyecto Phanteon


---

## 📑 Índice

1. [Página 1: LoginPage](#página-1-loginpage)
2. [Página 2: DispositivosPage](#página-2-dispositivospage-ya-implementada)
3. [Página 3: AlertasPage](#página-3-alertaspage)
4. [Página 4: DetalleDispositivoPage](#página-4-detalledispositivopage)
5. [Página 5: DiagnosticoPage](#página-5-diagnosticopage-ya-implementada)
6. [Estilos Comunes](#estilos-comunes)
7. [Navegación entre Páginas](#navegación-entre-páginas)

---

## Página 1: LoginPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│                                          │
│          [LOGO PHANTEON]                 │
│     Sistema de Monitoreo IoT             │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ Correo Electrónico                 │  │
│  │ usuario@ejemplo.com                │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ Contraseña                         │  │
│  │ ••••••••                           │  │
│  └────────────────────────────────────┘  │
│                                          │
│  [ ] Recordar mi sesión                  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │       INICIAR SESIÓN               │  │
│  └────────────────────────────────────┘  │
│                                          │
│       ¿Olvidaste tu contraseña?          │
│                                          │
│  [ActivityIndicator] Iniciando sesión... │
│  [Label] Error: Credenciales inválidas   │
│                                          │
└──────────────────────────────────────────┘


### Componentes UI

- **Entry** para correo (Keyboard: Email)
- **Entry** para contraseña (IsPassword: true)
- **CheckBox** para "Recordar sesión"
- **Button** para "Iniciar Sesión"
- **Label** para errores
- **ActivityIndicator** para loading

```

### Code-Behind (LoginPage.xaml.cs)

```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private string correo = string.Empty;

[ObservableProperty]
private string password = string.Empty;

[ObservableProperty]
private bool recordarSesion;

[ObservableProperty]
private string correoError = string.Empty;

[ObservableProperty]
private string passwordError = string.Empty;

[ObservableProperty]
private bool hasCorreoError;

[ObservableProperty]
private bool hasPasswordError;

[ObservableProperty]
private string errorMessage = string.Empty;

[ObservableProperty]
private bool hasError;

[ObservableProperty]
private bool isBusy;

[RelayCommand]
private async Task Login() { }

[RelayCommand]
private async Task OlvidoPassword() { }
```

### Endpoints Usados

- `POST /api/usuarios/login` (debes agregarlo al backend)
- `GET /api/usuarios/getbyid/{id}`

---

## DispositivosPage

Esta página ya fue implementada por Héctor. Revisa el archivo `Views/DispositivosPage.xaml` como referencia.

### Características

- CollectionView con lista de dispositivos
- RefreshView para pull-to-refresh
- Estado vacío cuando no hay dispositivos
- Indicador de carga
- Navegación a DetalleDispositivoPage

---

## Página 3: AlertasPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│  Alertas                          [Filtro]│
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🔴 CRÍTICA                         │  │
│  │ Dispositivo DEV-001 Desconectado   │  │
│  │ Hace 5 minutos                     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🟡 ADVERTENCIA                     │  │
│  │ Temperatura alta en DEV-003        │  │
│  │ Hace 15 minutos                    │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🟢 INFORMACIÓN                     │  │
│  │ Actualización de firmware DEV-005  │  │
│  │ Hace 1 hora                        │  │
│  └────────────────────────────────────┘  │
│                                          │
│  [Pull to refresh]                       │
│                                          │
└──────────────────────────────────────────┘
```

### Registrar el Converter en App.xaml

```xml
<Application.Resources>
    <ResourceDictionary>
        <helpers:AlertaColorConverter x:Key="AlertaColorConverter"/>
    </ResourceDictionary>
</Application.Resources>
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasFiltradas = new();

[ObservableProperty]
private List<string> tiposAlerta = new() { "Todas", "Crítica", "Advertencia", "Información" };

[ObservableProperty]
private string tipoAlertaSeleccionado = "Todas";

[ObservableProperty]
private bool isRefreshing;

[RelayCommand]
private async Task CargarAlertas() { }

[RelayCommand]
private async Task Refresh() { }
```

### Endpoints Usados

- `GET /api/alertas/getall`
- `GET /api/alertas/getbyid/{id}`

---

## Página 4: DetalleDispositivoPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│  ← DEV-001                        [Editar]│
│                                          │
│  ┌────────────────────────────────────┐  │
│  │  Estado: ACTIVO                 🟢 │  │
│  │  Última vista: Hace 2 minutos      │  │
│  └────────────────────────────────────┘  │
│                                          │
│  INFORMACIÓN GENERAL                     │
│  ┌────────────────────────────────────┐  │
│  │  Serial: DEV-001                   │  │
│  │  MAC: 00:1A:2B:3C:4D:5E           │  │
│  │  Firmware: v2.1.5                  │  │
│  │  Dirección: Av. Principal #123     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  UBICACIÓN                               │
│  ┌────────────────────────────────────┐  │
│  │  [MAPA]                            │  │
│  │  Lat: -12.0464                     │  │
│  │  Lon: -77.0428                     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ALERTAS RECIENTES (3)                   │
│  • Temperatura alta - Hace 1h            │
│  • Actualización firmware - Hace 2d      │
│  • Conexión restaurada - Hace 1w         │
│                                          │
│  HISTORIAL                               │
│  [Ver historial completo →]             │
│                                          │
└──────────────────────────────────────────┘
```


### Code-Behind (DetalleDispositivoPage.xaml.cs)

```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

public partial class DetalleDispositivoPage : ContentPage
{
    public DetalleDispositivoPage(DetalleDispositivoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DetalleDispositivoViewModel vm)
        {
            await vm.CargarDetallesAsync();
        }
    }
}
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private Dispositivo dispositivo = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasRecientes = new();

[ObservableProperty]
private bool tieneAlertas;

[ObservableProperty]
private string estadoTexto = string.Empty;

[ObservableProperty]
private string estadoEmoji = string.Empty;

[ObservableProperty]
private string ultimaVistaTexto = string.Empty;

[ObservableProperty]
private int historialCount;

[RelayCommand]
private async Task CargarDetalles() { }

[RelayCommand]
private async Task AbrirMapa() { }

[RelayCommand]
private async Task VerHistorialCompleto() { }

[RelayCommand]
private async Task Editar() { }

[RelayCommand]
private async Task Eliminar() { }
```

### Endpoints Usados

- `GET /api/dispositivos/getbyid/{id}`
- `GET /api/alertas/getall` (filtrar por IdDispositivo)
- `GET /api/historial/getall` (filtrar por IdDispositivo)

---

##  DiagnosticoPage 

Esta página ya fue implementada por Héctor. Revisa el archivo `Views/DiagnosticoPage.xaml` como referencia.

---

## Estilos Comunes

### Colores (App.xaml)

```xml
<Application.Resources>
    <ResourceDictionary>

        <!-- Colors -->
        <Color x:Key="Primary">#512BD4</Color>
        <Color x:Key="Secondary">#DFD8F7</Color>
        <Color x:Key="Tertiary">#2B0B98</Color>

        <Color x:Key="White">White</Color>
        <Color x:Key="Black">Black</Color>

        <Color x:Key="Gray100">#F5F5F5</Color>
        <Color x:Key="Gray200">#E8E8E8</Color>
        <Color x:Key="Gray300">#CCCCCC</Color>
        <Color x:Key="Gray400">#999999</Color>
        <Color x:Key="Gray500">#666666</Color>
        <Color x:Key="Gray600">#4D4D4D</Color>
        <Color x:Key="Gray700">#333333</Color>
        <Color x:Key="Gray800">#1A1A1A</Color>
        <Color x:Key="Gray900">#0D0D0D</Color>

        <Color x:Key="Success">#28A745</Color>
        <Color x:Key="Warning">#FFC107</Color>
        <Color x:Key="Danger">#DC3545</Color>
        <Color x:Key="Info">#17A2B8</Color>

    </ResourceDictionary>
</Application.Resources>
```

### Estilos de Texto

```xml
<!-- Title Styles -->
<Style x:Key="PageTitleStyle" TargetType="Label">
    <Setter Property="FontSize" Value="24"/>
    <Setter Property="FontAttributes" Value="Bold"/>
    <Setter Property="TextColor" Value="{StaticResource Gray900}"/>
</Style>

<!-- Subtitle Styles -->
<Style x:Key="SubtitleStyle" TargetType="Label">
    <Setter Property="FontSize" Value="18"/>
    <Setter Property="FontAttributes" Value="Bold"/>
    <Setter Property="TextColor" Value="{StaticResource Gray700}"/>
</Style>

<!-- Body Styles -->
<Style x:Key="BodyStyle" TargetType="Label">
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="TextColor" Value="{StaticResource Gray700}"/>
</Style>

<!-- Caption Styles -->
<Style x:Key="CaptionStyle" TargetType="Label">
    <Setter Property="FontSize" Value="12"/>
    <Setter Property="TextColor" Value="{StaticResource Gray600}"/>
</Style>
```

---

## Navegación entre Páginas

### Registrar Rutas en AppShell.xaml.cs

```csharp
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas para navegación
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        Routing.RegisterRoute(nameof(Views.DispositivosPage), typeof(Views.DispositivosPage));
        Routing.RegisterRoute(nameof(Views.AlertasPage), typeof(Views.AlertasPage));
        Routing.RegisterRoute(nameof(Views.DetalleDispositivoPage), typeof(Views.DetalleDispositivoPage));
        Routing.RegisterRoute(nameof(Views.DiagnosticoPage), typeof(Views.DiagnosticoPage));
    }
}
```

### Ejemplo de Navegación desde ViewModel

```csharp
// Navegar a DetalleDispositivoPage pasando el ID
[RelayCommand]
private async Task VerDetalle(int idDispositivo)
{
    await Shell.Current.GoToAsync($"{nameof(DetalleDispositivoPage)}?id={idDispositivo}");
}

// Navegar atrás
[RelayCommand]
private async Task Volver()
{
    await Shell.Current.GoToAsync("..");
}

// Navegar a página raíz
[RelayCommand]
private async Task IrAInicio()
{
    await Shell.Current.GoToAsync("//DispositivosPage");
}
```

### Recibir Parámetros en ViewModel

```csharp
[QueryProperty(nameof(IdDispositivo), "id")]
public partial class DetalleDispositivoViewModel : ObservableObject
{
    private int _idDispositivo;

    public int IdDispositivo
    {
        get => _idDispositivo;
        set
        {
            _idDispositivo = value;
            // Cargar detalles cuando se asigna el ID
            _ = CargarDetallesAsync();
        }
    }
}
```

---



## Recursos Adicionales

- **[Microsoft MAUI Docs](https://learn.microsoft.com/en-us/dotnet/maui/)** - Documentación oficial
- **[XAML Controls](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/)** - Lista de controles disponibles
- **[Data Binding](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/)** - Guía de binding
- **[Shell Navigation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation)** - Navegación con Shell

---

**Siguiente:** [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Volver al índice:** [README.md](README.md)
