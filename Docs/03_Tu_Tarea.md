# 📋 División de Tareas del Equipo

---

## 👥 Equipo (4 personas)

| Miembro | Código | Tareas Asignadas |
|---------|--------|------------------|
| **Héctor Eduardo Véliz Girón** | 000108304 | ✅ **YA COMPLETADAS** (Ver abajo) + LoginPage con su ViewModel |
| **Persona 1** | _________ | `Features/Alertas/` completo (AlertasPage.xaml + AlertasViewModel.cs) |
| **Persona 2** | _________ | `Features/Dispositivos/DispositivoDetail/` completo |
| **Persona 3** | _________ | `Features/Dispositivos/DispositivosList/` completo + Navegación |

**⚠️ ESTRATEGIA:** Cada persona trabaja en su propia carpeta Feature para evitar conflictos.

**📐 NUEVA ESTRUCTURA:** El proyecto ahora usa **Feature-based Architecture**.
- Las Views y ViewModels van juntos en `Features/{NombreModulo}/`
- Ver **[08_Arquitectura.md](08_Arquitectura.md)** para más detalles

---

## ✅ TAREAS COMPLETADAS (Héctor)

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

### 📡 Capa de Comunicación con API Externa

**⚠️ NOTA IMPORTANTE:** La API es un **repositorio externo** (de @epinto17). Aquí solo se configuró el **consumo** desde Phanteon.

**Ubicación de la API:** https://github.com/epinto17/DevicesAPI

#### 📦 Modelos de Datos (sincronizados con la API)

Ubicación: `Models/`

Estos modelos **reflejan** la estructura de datos de la API externa para poder consumirla.

- [x] **Usuario.cs** - Modelo para autenticación y usuarios
- [x] **Dispositivo.cs** - Modelo para dispositivos IoT
- [x] **Alerta.cs** - Modelo para alertas del sistema
- [x] **HistorialDispositivo.cs** - Modelo para eventos de dispositivos

#### 🔌 Interfaces de Servicios API (Refit)

**Nueva Ubicación:** `Services/Api/`

Estas interfaces definen **cómo consumir** los endpoints de la API externa usando Refit.

- [x] **IDispositivosApi.cs**
  - GetDispositivosAsync()
  - GetDispositivoAsync(int id)
  - CreateDispositivoAsync(Dispositivo)
  - UpdateDispositivoAsync(int id, Dispositivo)
  - DeleteDispositivoAsync(int id)
  - GetHistorialAsync(int id)

- [x] **IUsuariosApi.cs**
  - GetUsuariosAsync()
  - GetUsuarioAsync(int id)
  - CreateUsuarioAsync(Usuario)
  - UpdateUsuarioAsync(int id, Usuario)
  - DeleteUsuarioAsync(int id)

- [x] **IAlertasApi.cs**
  - GetAlertasAsync()
  - GetAlertasPorDispositivoAsync(int dispositivoId)
  - GetAlertaAsync(int id)
  - CreateAlertaAsync(Alerta)
  - MarcarAlertaLeidaAsync(int id)
  - DeleteAlertaAsync(int id)

#### 🔧 Servicios Core

**Ubicación:** `Services/{Categoría}/`

- [x] **Http/** - `ApiHttpClientFactory.cs` - Factory de HttpClient
- [x] **Storage/** - `SecureStorageService.cs` - Almacenamiento seguro
- [x] **Navigation/** - `NavigationService.cs` - Navegación entre páginas

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

### 🧩 Core Components

**Ubicación:** `Core/`

- [x] **ViewModels/BaseViewModel.cs** - Clase base con EstaCargando, MensajeError, etc.
- [x] **Converters/** - 3 converters XAML listos:
  - `BoolToColorConverter.cs` - bool → Color
  - `InvertedBoolConverter.cs` - !bool
  - `StringNotEmptyConverter.cs` - string → bool
- [x] **Behaviors/EventToCommandBehavior.cs** - Convierte eventos en comandos
- [x] **Controls/** - Para controles personalizados futuros

### 🎯 Features Iniciales

**Ubicación:** `Features/`

- [x] **Main/** - Página principal
  - `MainPage.xaml + .xaml.cs` - Vista
  - `MainViewModel.cs` - ViewModel con MVVM completo
- [x] **Shared/** - Para componentes compartidos entre features

**⚠️ NOTA IMPORTANTE:**
- Todas las nuevas features deben seguir este patrón
- Cada feature agrupa su View + ViewModel en la misma carpeta
- Ver **[08_Arquitectura.md](08_Arquitectura.md)** para más detalles

### 📐 Constants

**Ubicación:** `Constants/`

- [x] **ApiEndpoints.cs** - Endpoints de la API
- [x] **AppConstants.cs** - Constantes generales (timeouts, storage keys, rutas)
- [x] **ErrorMessages.cs** - Mensajes de error estandarizados

---

## ⏳ TAREAS PENDIENTES

---

## 👤 Persona: Héctor (Tarea Adicional)

### Responsabilidad:
Crear LoginPage completo con su ViewModel para autenticación.

### Nueva Ubicación (Feature-based):
- `Features/Auth/LoginViewModel.cs`
- `Features/Auth/LoginPage.xaml` + `LoginPage.xaml.cs`

---

### Tarea: LoginPage + LoginViewModel

**Descripción:**
Página de inicio de sesión con formulario y lógica de autenticación.

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
Ver `EjemploTesteoViewModel.cs` como ejemplo completo

**Componentes XAML requeridos (LoginPage.xaml):**
```xml
- Entry para Correo (binding a Correo)
- Entry para Password (IsPassword="True", binding a Password)
- Button "Iniciar Sesión" (Command={Binding IniciarSesionCommand})
- Label para mensajes de error (binding a MensajeError)
- ActivityIndicator (binding a EstaCargando)
```

**Converters a usar:**
- `InvertedBoolConverter` - Deshabilitar botón mientras carga
- `StringNotEmptyConverter` - Mostrar error solo si hay mensaje

---

## 👤 Persona 1

### Responsabilidad:
Crear AlertasPage completo con su ViewModel para listar y filtrar alertas.

### Ubicación:
- `ViewModels/AlertasViewModel.cs`
- `Views/AlertasPage.xaml` + `AlertasPage.xaml.cs`

---

### Tarea: AlertasPage + AlertasViewModel

**Descripción:**
Página de alertas con lista filtrable y lógica de negocio.

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

**Componentes XAML requeridos (AlertasPage.xaml):**
```xml
- ToolbarItem "Actualizar" (Command={Binding CargarAlertasCommand})
- Picker o Botones para filtros (Todas, Crítica, Advertencia, Info)
- CollectionView (ItemsSource={Binding AlertasFiltradas})
- ItemTemplate con TipoAlerta, Mensaje, FechaHora, Estado
- ActivityIndicator (binding a EstaCargando)
```

**Colores según tipo:**
- Crítica: Rojo (#DC3545)
- Advertencia: Amarillo (#FFC107)
- Info: Azul (#0D6EFD)

**Archivo de referencia:**
Ver `EjemploTesteoViewModel.cs` como ejemplo

---

## 👤 Persona 2

### Responsabilidad:
Crear DetalleDispositivoPage completo con su ViewModel para mostrar información detallada.

### Ubicación:
- `ViewModels/DetalleDispositivoViewModel.cs`
- `Views/DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`

---

### Tarea: DetalleDispositivoPage + DetalleDispositivoViewModel

**Descripción:**
Página de detalle de dispositivo con información completa, historial y alertas asociadas.

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

**Componentes XAML requeridos (DetalleDispositivoPage.xaml):**

**Sección 1 - Información del Dispositivo:**
```xml
- SerialDispositivo, MAC, Firmware, Dirección
- Estado Activo/Inactivo (Label con color)
- Fecha de Registro, Última Vista
- Latitud/Longitud
```

**Sección 2 - Historial:**
```xml
- CollectionView (ItemsSource={Binding Historial})
- ItemTemplate con Evento, FechaHora, Detalles
```

**Sección 3 - Alertas Activas:**
```xml
- CollectionView (ItemsSource={Binding Alertas})
- ItemTemplate con TipoAlerta (con color), Mensaje, FechaHora
```

**Layout sugerido:**
- ScrollView con VerticalStackLayout
- Frame para cada sección
- ActivityIndicator
- ToolbarItem "Actualizar"

**Archivo de referencia:**
Ver `EjemploTesteoViewModel.cs` como ejemplo

---

## 👤 Persona 3

### Responsabilidad:
Crear DispositivosPage y DispositivosViewModel COMPLETOS desde cero con funcionalidad real, más configurar navegación y validaciones para todo el proyecto.

### Ubicación:
- `ViewModels/DispositivosViewModel.cs` (CREAR desde cero)
- `Views/DispositivosPage.xaml` + `DispositivosPage.xaml.cs` (CREAR desde cero)
- `AppShell.xaml` + `AppShell.xaml.cs`
- `App.xaml.cs`

**⚠️ NOTA:** Existe un TestConexionApiViewModel.cs que es SOLO para pruebas de conexión. NO lo uses como base, créalo desde cero siguiendo el patrón de EjemploTesteoViewModel.cs

---

### Tarea 3.1: DispositivosPage + DispositivosViewModel (Implementación Real)

**Descripción:**
Crear una página completa que muestre:
- Lista de dispositivos conectados
- Detalles de cada dispositivo (Serial, MAC, Firmware, Estado)
- Navegación al detalle completo del dispositivo
- Búsqueda y filtrado de dispositivos
- Actualización de la lista (pull-to-refresh)

**Propiedades sugeridas:**
```csharp
[ObservableProperty]
private ObservableCollection<Dispositivo> dispositivos = new();

[ObservableProperty]
private Dispositivo? dispositivoSeleccionado;

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private string filtro = string.Empty; // Para búsqueda
```

**Comandos sugeridos:**
```csharp
[RelayCommand]
private async Task CargarDispositivos();

[RelayCommand]
private async Task IrADetalle(int dispositivoId);

[RelayCommand]
private async Task BuscarDispositivos(string termino);
```

**Componentes XAML:**
- SearchBar para filtrar dispositivos
- CollectionView con lista de dispositivos
- TapGestureRecognizer para navegar a detalle
- RefreshView para actualizar
- ActivityIndicator

---

### Checklist Persona: Héctor (Tarea adicional):

- [ ] Crear LoginViewModel.cs
  - [ ] Propiedades con [ObservableProperty]
  - [ ] Comando IniciarSesionCommand
  - [ ] Validaciones de correo y password
  - [ ] Consumo de IUsuariosService
  - [ ] Navegación a dashboard

- [ ] Crear LoginPage.xaml + LoginPage.xaml.cs
  - [ ] Entry de correo
  - [ ] Entry de password
  - [ ] Button iniciar sesión
  - [ ] Label de error
  - [ ] ActivityIndicator
  - [ ] Bindings correctos

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<LoginViewModel>();`
  - [ ] `builder.Services.AddTransient<LoginPage>();`

---

### Checklist Persona 1:

- [ ] Crear AlertasViewModel.cs
  - [ ] Propiedades para alertas y filtros
  - [ ] Comando CargarAlertasCommand
  - [ ] Comando FiltrarPorTipoCommand
  - [ ] Consumo de IAlertasService
  - [ ] Lógica de filtrado

- [ ] Crear AlertasPage.xaml + AlertasPage.xaml.cs
  - [ ] ToolbarItem actualizar
  - [ ] Filtros por tipo
  - [ ] CollectionView con alertas
  - [ ] ItemTemplate con colores
  - [ ] ActivityIndicator
  - [ ] Override OnAppearing en code-behind

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<AlertasViewModel>();`
  - [ ] `builder.Services.AddTransient<AlertasPage>();`

---

### Checklist Persona 2:

- [ ] Crear DetalleDispositivoViewModel.cs
  - [ ] Propiedades para dispositivo, historial y alertas
  - [ ] Comando CargarDetalleCommand
  - [ ] QueryProperty para recibir ID
  - [ ] Consumo de 3 servicios (Dispositivos, Historial, Alertas)
  - [ ] Manejo de errores

- [ ] Crear DetalleDispositivoPage.xaml + DetalleDispositivoPage.xaml.cs
  - [ ] Sección de información del dispositivo
  - [ ] Sección de historial
  - [ ] Sección de alertas activas
  - [ ] ScrollView completo
  - [ ] ActivityIndicator
  - [ ] ToolbarItem "Actualizar"
  - [ ] Override OnAppearing en code-behind

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<DetalleDispositivoViewModel>();`
  - [ ] `builder.Services.AddTransient<DetalleDispositivoPage>();`

---

### Checklist Persona 3:

- [ ] CREAR DispositivosViewModel.cs desde cero
  - [ ] Propiedades con [ObservableProperty]
  - [ ] Comando CargarDispositivosCommand
  - [ ] Comando IrADetalleCommand
  - [ ] Comando BuscarDispositivosCommand
  - [ ] Consumo de IDispositivosService
  - [ ] Lógica de búsqueda/filtrado
  - [ ] Manejo de errores

- [ ] CREAR DispositivosPage.xaml + DispositivosPage.xaml.cs desde cero
  - [ ] SearchBar para filtrar dispositivos
  - [ ] RefreshView con pull-to-refresh
  - [ ] CollectionView con lista de dispositivos
  - [ ] ItemTemplate mostrando: Serial, MAC, Estado, Firmware
  - [ ] TapGestureRecognizer para navegar a detalle
  - [ ] ActivityIndicator
  - [ ] Override OnAppearing en code-behind

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<DispositivosViewModel>();`
  - [ ] `builder.Services.AddTransient<DispositivosPage>();`

---

### Tarea 3.3: Configurar AppShell.xaml

**Descripción:**
Configurar menú lateral (Flyout) con navegación a todas las páginas.

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

### Tarea 3.4: Registrar Rutas en AppShell.xaml.cs

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

### Tarea 3.5: Configurar Navegación Inicial

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

### Tarea 3.6: Agregar Validaciones en ViewModels

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

### Tarea 3.7: Implementar Manejo de Errores

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

### Tarea 3.8: Verificación de Conectividad

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

### Checklist Final Persona 3:

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
---

S
