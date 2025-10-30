# 📋 División de Tareas del Equipo

---

## 👥 Equipo (4 personas)

| Miembro | Código | 
|---------|--------|
| **Héctor Eduardo Véliz Girón** | 000108304 | 
| **Persona 1** | _________ | 
| **Persona 2** | _________ |
| **Persona 3** | _________ | 

---

## ✅ TAREAS COMPLETADAS 

### 🏗️ Infraestructura del Proyecto

- [x] **Inicialización del proyecto .NET MAUI**
  - Creación del proyecto base
  - Configuración de plataformas (Android, iOS, Windows)

- [x] **Configuración del repositorio Git**
  - Inicialización de Git
  - Configuración de .gitignore
  - Commits iniciales

- [x] **Instalación de paquetes NuGet**
  - CommunityToolkit.Maui (v12.2.0)
  - CommunityToolkit.Mvvm (v8.4.0)
  - Refit (v8.0.0)
  - Refit.HttpClientFactory (v8.0.0)
  - Newtonsoft.Json (v13.0.4)
  - Polly (v8.6.4)
  - Polly.Extensions.Http (v3.0.0)

### 📦 Modelos de Datos

Ubicación: `Models/`

- [x] **Usuario.cs**
  ```csharp
  - IdUsuario: int
  - NombreUsuario: string
  - Correo: string
  - PasswordHash: string
  - Rol: string
  ```

- [x] **Dispositivo.cs**
  ```csharp
  - IdDispositivo: int
  - SerialDispositivo: string
  - MAC: string
  - Firmware: string
  - Direccion: string
  - Latitud: double
  - Longitud: double
  - Registro: DateTime
  - Activo: string
  - UltimaVista: DateTime
  ```

- [x] **Alerta.cs**
  ```csharp
  - IdAlerta: int
  - IdDispositivo: int
  - TipoAlerta: string
  - Mensaje: string
  - FechaHora: DateTime
  - Estado: string
  ```

- [x] **HistorialDispositivo.cs**
  ```csharp
  - IdHistorial: int
  - IdDispositivo: int
  - Evento: string
  - FechaHora: DateTime
  - Detalles: string
  ```

### 🔌 Servicios de API (Refit)

Ubicación: `Services/Interfaces/`

- [x] **IDispositivosService.cs**
  - GetAllDispositivosAsync()
  - GetDispositivoByIdAsync(int id)
  - CreateDispositivoAsync(Dispositivo)

- [x] **IUsuariosService.cs**
  - GetAllUsuariosAsync()
  - GetUsuarioByIdAsync(int id)
  - CreateUsuarioAsync(Usuario)

- [x] **IAlertasService.cs**
  - GetAllAlertasAsync()
  - GetAlertaByIdAsync(int id)
  - CreateAlertaAsync(Alerta)

- [x] **IHistorialDispositivosService.cs**
  - GetAllHistorialAsync()
  - GetHistorialByIdAsync(int id)
  - CreateHistorialAsync(HistorialDispositivo)

### ⚙️ Configuración

- [x] **MauiProgram.cs**
  - Configuración de inyección de dependencias
  - Registro de servicios Refit
  - Configuración de HttpClient con:
    - BaseAddress desde ApiConfiguration
    - Timeout de 30 segundos
    - Manejo de certificados SSL en DEBUG
  - Registro de ViewModels y Pages existentes

- [x] **ApiConfiguration.cs** (`Helpers/`)
  - BaseUrl configurada: `https://10.0.2.2:7026`
  - Timeout: 30 segundos

### 🛠️ Helpers y Converters

Ubicación: `Helpers/`

- [x] **InvertedBoolConverter.cs**
  - Convierte true ↔ false
  - Uso: Deshabilitar botones mientras carga

- [x] **StringNotEmptyConverter.cs**
  - Retorna true si string NO está vacío
  - Uso: Mostrar mensajes de error condicionales

### 🎨 ViewModels Iniciales

- [x] **DispositivosViewModel.cs**
  - Propiedades: Dispositivos (ObservableCollection), EstaCargando
  - Comandos: CargarDispositivosCommand
  - Consume: IDispositivosService

- [x] **DiagnosticoViewModel.cs**
  - Propiedades: TotalDispositivos, DispositivosActivos, AlertasActivas, etc.
  - Comandos: ActualizarDashboardCommand
  - Consume: IDispositivosService, IAlertasService

### 📱 Páginas Iniciales

- [x] **DispositivosPage.xaml + .cs**
  - Lista de dispositivos con CollectionView
  - ActivityIndicator
  - ToolbarItem para actualizar

- [x] **DiagnosticoPage.xaml + .cs**
  - Dashboard con estadísticas
  - Tarjetas de resumen
  - Botón actualizar

---


### Tarea 1.1: LoginViewModel.cs

**Descripción:**
ViewModel para manejar el inicio de sesión de usuarios.

**Propiedades requeridas:**
```csharp
[ObservableProperty]
private string correo = string.Empty;

[ObservableProperty]
private string password = string.Empty;

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private string mensajeError = string.Empty;
```

**Comandos requeridos:**
```csharp
[RelayCommand]
private async Task IniciarSesion()
{
    // Validar correo y password
    // Llamar a IUsuariosService.GetAllUsuariosAsync()
    // Buscar usuario por correo
    // Verificar credenciales
    // Navegar a dashboard si es exitoso
}
```

**Servicio a inyectar:**
- `IUsuariosService`

**Validaciones a implementar:**
- Correo no vacío
- Correo con formato válido (contiene @)
- Password no vacío
- Password mínimo 6 caracteres

**Navegación:**
Si login exitoso: `await Shell.Current.GoToAsync("///diagnostico");`

**Archivo de referencia:**
Ver `DispositivosViewModel.cs` como ejemplo

---

### Tarea 1.2: AlertasViewModel.cs

**Descripción:**
ViewModel para listar y filtrar alertas del sistema.

**Propiedades requeridas:**
```csharp
[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasFiltradas = new();

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private string filtroSeleccionado = "Todas"; // "Todas", "Crítica", "Advertencia", "Info"
```

**Comandos requeridos:**
```csharp
[RelayCommand]
private async Task CargarAlertas()
{
    // Llamar a IAlertasService.GetAllAlertasAsync()
    // Llenar colección Alertas
}

[RelayCommand]
private async Task FiltrarPorTipo(string tipo)
{
    // Filtrar alertas según tipo
    // Actualizar AlertasFiltradas
}
```

**Servicio a inyectar:**
- `IAlertasService`

**Funcionalidad adicional:**
- Filtrar por tipo de alerta (Crítica, Advertencia, Info)
- Filtrar por estado (Nueva, Leída, Resuelta)
- Ordenar por fecha (más recientes primero)

---

### Tarea 1.3: DetalleDispositivoViewModel.cs

**Descripción:**
ViewModel para mostrar detalle completo de un dispositivo con su historial y alertas.

**Propiedades requeridas:**
```csharp
[ObservableProperty]
private Dispositivo? dispositivo;

[ObservableProperty]
private ObservableCollection<HistorialDispositivo> historial = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private int dispositivoId; // Parámetro de navegación
```

**Comandos requeridos:**
```csharp
[RelayCommand]
private async Task CargarDetalle()
{
    // 1. Cargar dispositivo por ID
    // 2. Cargar historial filtrado por IdDispositivo
    // 3. Cargar alertas filtradas por IdDispositivo
}

[RelayCommand]
private async Task ActualizarDatos()
{
    // Refrescar toda la información
}
```

**Servicios a inyectar:**
- `IDispositivosService`
- `IHistorialDispositivosService`
- `IAlertasService`

**Manejo de parámetros de navegación:**
```csharp
[QueryProperty(nameof(DispositivoId), "id")]
public partial class DetalleDispositivoViewModel : ObservableObject
{
    // ...
}
```

---

### Checklist Persona 1:

- [x] Crear LoginViewModel.cs
  - [ ] Propiedades con [ObservableProperty]
  - [ ] Comando IniciarSesionCommand
  - [ ] Validaciones de correo y password
  - [ ] Consumo de IUsuariosService
  - [ ] Navegación a dashboard

- [ ] Crear AlertasViewModel.cs
  - [ ] Propiedades para alertas y filtros
  - [ ] Comando CargarAlertasCommand
  - [ ] Comando FiltrarPorTipoCommand
  - [ ] Consumo de IAlertasService
  - [ ] Lógica de filtrado

- [ ] Crear DetalleDispositivoViewModel.cs
  - [ ] Propiedades para dispositivo, historial y alertas
  - [ ] Comando CargarDetalleCommand
  - [ ] QueryProperty para recibir ID
  - [ ] Consumo de 3 servicios
  - [ ] Manejo de errores

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<LoginViewModel>();`
  - [ ] `builder.Services.AddTransient<AlertasViewModel>();`
  - [ ] `builder.Services.AddTransient<DetalleDispositivoViewModel>();`

---

## 👤 PERSONA 2: Páginas XAML Faltantes

### Responsabilidades:
Crear 3 páginas XAML con sus code-behind que implementen las interfaces de usuario.

### Ubicación:
`Views/`

---

### Tarea 2.1: LoginPage.xaml + LoginPage.xaml.cs

**Descripción:**
Pantalla de inicio de sesión con formulario.

**Componentes XAML requeridos:**
```xml
- Entry para Correo (binding a Correo)
- Entry para Password (IsPassword="True", binding a Password)
- Button "Iniciar Sesión" (Command={Binding IniciarSesionCommand})
- Label para mensajes de error (binding a MensajeError)
- ActivityIndicator (binding a EstaCargando)
```

**Layout sugerido:**
```
- VerticalStackLayout centrado
- Logo o título de la app
- Entry de correo
- Entry de password
- Mensaje de error (condicional)
- Botón de login
- Indicador de carga
```

**Code-behind:**
```csharp
public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _viewModel;

    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}
```

**Converters a usar:**
- `InvertedBoolConverter` - Deshabilitar botón mientras carga
- `StringNotEmptyConverter` - Mostrar error solo si hay mensaje

---

### Tarea 2.2: AlertasPage.xaml + AlertasPage.xaml.cs

**Descripción:**
Lista de alertas con filtros por tipo.

**Componentes XAML requeridos:**
```xml
- ToolbarItem "Actualizar" (Command={Binding CargarAlertasCommand})
- Picker o Botones para filtros (Todas, Crítica, Advertencia, Info)
- CollectionView (ItemsSource={Binding AlertasFiltradas})
- ItemTemplate con:
  - TipoAlerta (con color según tipo)
  - Mensaje
  - FechaHora
  - Estado
- ActivityIndicator (binding a EstaCargando)
```

**Colores según tipo:**
- Crítica: Rojo (#DC3545)
- Advertencia: Amarillo (#FFC107)
- Info: Azul (#0D6EFD)

**Code-behind:**
```csharp
public partial class AlertasPage : ContentPage
{
    private readonly AlertasViewModel _viewModel;

    public AlertasPage(AlertasViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarAlertasCommand.ExecuteAsync(null);
    }
}
```

**Archivo de referencia:**
Ver `DispositivosPage.xaml` como ejemplo

---

### Tarea 2.3: DetalleDispositivoPage.xaml + DetalleDispositivoPage.xaml.cs

**Descripción:**
Página de detalle de un dispositivo específico con su información, historial y alertas.

**Componentes XAML requeridos:**

**Sección 1 - Información del Dispositivo:**
```xml
- SerialDispositivo (Label, negrita)
- MAC (Label)
- Firmware (Label)
- Dirección (Label)
- Estado Activo/Inactivo (Label con color)
- Fecha de Registro (Label)
- Última Vista (Label)
- Latitud/Longitud (Labels)
```

**Sección 2 - Historial:**
```xml
- CollectionView (ItemsSource={Binding Historial})
- ItemTemplate con:
  - Evento
  - FechaHora
  - Detalles
```

**Sección 3 - Alertas Activas:**
```xml
- CollectionView (ItemsSource={Binding Alertas})
- ItemTemplate con:
  - TipoAlerta (con color)
  - Mensaje
  - FechaHora
```

**Layout sugerido:**
- ScrollView con VerticalStackLayout
- Frame para información general
- Frame para historial
- Frame para alertas
- ActivityIndicator
- ToolbarItem "Actualizar"

---

### Checklist Persona 2:

- [ ] Crear LoginPage.xaml
  - [ ] Entry de correo
  - [ ] Entry de password
  - [ ] Button iniciar sesión
  - [ ] Label de error
  - [ ] ActivityIndicator
  - [ ] Bindings correctos

- [ ] Crear LoginPage.xaml.cs
  - [ ] Constructor con inyección de ViewModel
  - [ ] Asignar BindingContext

- [ ] Crear AlertasPage.xaml
  - [ ] ToolbarItem actualizar
  - [ ] Filtros por tipo
  - [ ] CollectionView con alertas
  - [ ] ItemTemplate con colores
  - [ ] ActivityIndicator

- [ ] Crear AlertasPage.xaml.cs
  - [ ] Constructor con inyección
  - [ ] Override OnAppearing

- [ ] Crear DetalleDispositivoPage.xaml
  - [ ] Sección de información
  - [ ] Sección de historial
  - [ ] Sección de alertas
  - [ ] ScrollView
  - [ ] ActivityIndicator

- [ ] Crear DetalleDispositivoPage.xaml.cs
  - [ ] Constructor con inyección
  - [ ] Override OnAppearing

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<LoginPage>();`
  - [ ] `builder.Services.AddTransient<AlertasPage>();`
  - [ ] `builder.Services.AddTransient<DetalleDispositivoPage>();`

---

## 👤 PERSONA 3: Navegación y Validaciones

### Responsabilidades:
Configurar el sistema de navegación completo y agregar validaciones en todos los ViewModels.

---

### Tarea 3.1: Configurar AppShell.xaml

**Descripción:**
Crear menú lateral (Flyout) con navegación a todas las páginas.

**Estructura requerida:**
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:views="clr-namespace:Phanteon.Views"
       x:Class="Phanteon.AppShell"
       FlyoutBehavior="Flyout">

    <!-- MenuItem 1: Dashboard -->
    <FlyoutItem Title="Dashboard" Icon="home.png">
        <ShellContent Route="diagnostico"
                     ContentTemplate="{DataTemplate views:DiagnosticoPage}"/>
    </FlyoutItem>

    <!-- MenuItem 2: Dispositivos -->
    <FlyoutItem Title="Dispositivos" Icon="devices.png">
        <ShellContent Route="dispositivos"
                     ContentTemplate="{DataTemplate views:DispositivosPage}"/>
    </FlyoutItem>

    <!-- MenuItem 3: Alertas -->
    <FlyoutItem Title="Alertas" Icon="alert.png">
        <ShellContent Route="alertas"
                     ContentTemplate="{DataTemplate views:AlertasPage}"/>
    </FlyoutItem>

    <!-- Páginas que NO aparecen en el menú -->
    <ShellContent Route="login"
                 ContentTemplate="{DataTemplate views:LoginPage}"
                 IsVisible="False"/>

    <ShellContent Route="detalleDispositivo"
                 ContentTemplate="{DataTemplate views:DetalleDispositivoPage}"
                 IsVisible="False"/>
</Shell>
```

---

### Tarea 3.2: Registrar Rutas en AppShell.xaml.cs

**Descripción:**
Registrar rutas para navegación programática.

```csharp
namespace Phanteon;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas para páginas de detalle
        Routing.RegisterRoute("detalleDispositivo", typeof(Views.DetalleDispositivoPage));
        Routing.RegisterRoute("login", typeof(Views.LoginPage));
    }
}
```

---

### Tarea 3.3: Configurar Navegación Inicial

**Descripción:**
Configurar qué página se muestra al iniciar la app.

**Opción A - Mostrar Login primero:**

En `App.xaml.cs`:
```csharp
public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();

        // Navegar a login al iniciar
        Shell.Current.GoToAsync("///login");
    }
}
```

**Opción B - Mostrar Dashboard directamente:**
```csharp
// No hacer nada, AppShell mostrará el primer FlyoutItem por defecto
```

---

### Tarea 3.4: Agregar Validaciones en ViewModels

**Descripción:**
Implementar validaciones completas en todos los ViewModels.

**En LoginViewModel:**
```csharp
[RelayCommand]
private async Task IniciarSesion()
{
    // Limpiar error previo
    MensajeError = string.Empty;

    // Validación 1: Correo no vacío
    if (string.IsNullOrWhiteSpace(Correo))
    {
        MensajeError = "El correo es requerido";
        return;
    }

    // Validación 2: Formato de correo
    if (!Correo.Contains("@") || !Correo.Contains("."))
    {
        MensajeError = "Formato de correo inválido";
        return;
    }

    // Validación 3: Password no vacío
    if (string.IsNullOrWhiteSpace(Password))
    {
        MensajeError = "La contraseña es requerida";
        return;
    }

    // Validación 4: Longitud mínima
    if (Password.Length < 6)
    {
        MensajeError = "La contraseña debe tener al menos 6 caracteres";
        return;
    }

    // Validación 5: Conectividad
    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
    {
        MensajeError = "No hay conexión a internet";
        await Toast.Make("Verifica tu conexión").Show();
        return;
    }

    // Intentar login con try-catch...
}
```

---

### Tarea 3.5: Implementar Manejo de Errores

**Descripción:**
Agregar try-catch y manejo de errores en todos los métodos async.

**Patrón a seguir:**
```csharp
[RelayCommand]
private async Task CargarDatos()
{
    EstaCargando = true;

    try
    {
        // Llamada a la API
        var datos = await _service.GetAllAsync();

        // Procesar datos
        foreach (var item in datos)
        {
            Coleccion.Add(item);
        }
    }
    catch (HttpRequestException ex)
    {
        // Error de red/conexión
        Console.WriteLine($"[Error HTTP] {ex.Message}");
        await Shell.Current.DisplayAlert(
            "Error de Conexión",
            "No se pudo conectar con el servidor. Verifica tu conexión a internet.",
            "OK");
    }
    catch (TaskCanceledException ex)
    {
        // Timeout
        Console.WriteLine($"[Timeout] {ex.Message}");
        await Shell.Current.DisplayAlert(
            "Tiempo Agotado",
            "La solicitud tardó demasiado tiempo. Intenta nuevamente.",
            "OK");
    }
    catch (Exception ex)
    {
        // Otros errores
        Console.WriteLine($"[Error General] {ex.Message}");
        await Shell.Current.DisplayAlert(
            "Error",
            $"Ocurrió un error inesperado: {ex.Message}",
            "OK");
    }
    finally
    {
        EstaCargando = false;
    }
}
```

---

### Tarea 3.6: Verificación de Conectividad

**Descripción:**
Agregar verificación de internet antes de llamadas API.

```csharp
using Microsoft.Maui.Networking;
using CommunityToolkit.Maui.Alerts;

// Antes de cualquier llamada API
if (Connectivity.NetworkAccess != NetworkAccess.Internet)
{
    await Toast.Make("No hay conexión a internet").Show();
    return;
}
```

---

### Checklist Persona 3:

- [ ] Configurar AppShell.xaml
  - [ ] FlyoutItem Dashboard
  - [ ] FlyoutItem Dispositivos
  - [ ] FlyoutItem Alertas
  - [ ] ShellContent Login (IsVisible=False)
  - [ ] ShellContent DetalleDispositivo (IsVisible=False)

- [ ] Configurar AppShell.xaml.cs
  - [ ] Registrar ruta "detalleDispositivo"
  - [ ] Registrar ruta "login"

- [ ] Configurar App.xaml.cs
  - [ ] Decidir página inicial (Login o Dashboard)

- [ ] Agregar validaciones en LoginViewModel
  - [ ] Validar correo no vacío
  - [ ] Validar formato de correo
  - [ ] Validar password no vacío
  - [ ] Validar longitud de password
  - [ ] Verificar conectividad

- [ ] Agregar manejo de errores en ViewModels
  - [ ] Try-catch en DispositivosViewModel
  - [ ] Try-catch en DiagnosticoViewModel
  - [ ] Try-catch en AlertasViewModel
  - [ ] Try-catch en LoginViewModel
  - [ ] Try-catch en DetalleDispositivoViewModel

- [ ] Verificación de conectividad
  - [ ] Usar Connectivity.NetworkAccess
  - [ ] Mostrar mensaje con Toast

- [ ] Probar navegación
  - [ ] Login → Dashboard
  - [ ] Dashboard → Dispositivos
  - [ ] Dispositivos → Detalle
  - [ ] Detalle → Volver atrás
  - [ ] Menú lateral funciona

---


## ✅ Criterios de Aceptación

Cada tarea se considera completada cuando:

1. ✅ El código compila sin errores ni warnings
2. ✅ Está registrado correctamente en MauiProgram.cs
3. ✅ Funciona correctamente (probado)
4. ✅ Tiene manejo de errores con try-catch
5. ✅ Tiene validaciones donde corresponde
6. ✅ Sigue el mismo estilo de código del proyecto
7. ✅ Está documentado con comentarios básicos

---

_Actualizado: 29/10/2024_
_Autor: Héctor Eduardo Véliz Girón_
