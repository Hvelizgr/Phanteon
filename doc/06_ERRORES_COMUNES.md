# 🐛 Errores Comunes y Soluciones

> Documentación de problemas encontrados durante el desarrollo y sus soluciones

---

## 🔴 ERRORES DE CONEXIÓN CON LA API

### ❌ Error: "Connection refused" o "No connection could be made"

**Síntomas:**
```
System.Net.Http.HttpRequestException: Connection refused
Unable to connect to https://localhost:7026
```

**Causas posibles:**
1. El backend no está corriendo
2. URL incorrecta en `ApiConfiguration.cs`
3. Puerto bloqueado por firewall

**Soluciones:**

#### Solución 1: Verificar que el backend esté corriendo

```bash
# Navegar a la carpeta del backend
cd DevicesAPI

# Ejecutar
dotnet run
```

**Salida esperada:**
```
Now listening on: https://localhost:7026
Application started. Press Ctrl+C to shut down.
```

#### Solución 2: Verificar URL según plataforma

Editar `Helpers/ApiConfiguration.cs`:

```csharp
// Para EMULADOR Android (10.0.2.2 apunta a localhost del PC)
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";

// Para WINDOWS Desktop
public static string BaseUrl { get; set; } = "https://localhost:7026";

// Para DISPOSITIVO Android físico (misma red WiFi)
public static string BaseUrl { get; set; } = "https://192.168.1.XXX:7026";
```

**Encontrar tu IP local:**
```bash
# Windows
ipconfig
# Busca "Dirección IPv4"

# Linux/Mac
ifconfig
```

#### Solución 3: Permitir conexiones en el firewall (solo dispositivo físico)

```bash
# Windows PowerShell (como Administrador)
netsh advfirewall firewall add rule name="DevicesAPI" dir=in action=allow protocol=TCP localport=7026
```

---

### ❌ Error: "SSL Certificate validation failed" o "The SSL connection could not be established"

**Síntomas:**
```
System.Net.Http.HttpRequestException: The SSL connection could not be established
AuthenticationException: The remote certificate is invalid
```

**Causa:**
El backend usa un certificado SSL de desarrollo autofirmado que no es confiable.

**Solución: YA IMPLEMENTADA ✅**

Esta solución ya está aplicada en `MauiProgram.cs` líneas 27-31:

```csharp
#if DEBUG
// Configurar HttpClient para aceptar certificados SSL de desarrollo
var httpClientHandler = new System.Net.Http.HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};
#endif
```

Y se aplica en cada registro de servicio:

```csharp
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
```

**⚠️ Importante:**
- Esto **solo aplica en modo DEBUG**
- En **Release** (producción), los certificados se validan correctamente
- NUNCA uses esto en producción con APIs públicas

---

### ❌ Error: "Timeout" o "The operation was canceled"

**Síntomas:**
```
TaskCanceledException: A task was canceled
The request was canceled due to the configured HttpClient.Timeout
```

**Causa:**
La petición tardó más de 30 segundos (timeout configurado).

**Soluciones:**

#### Solución 1: Aumentar el timeout (temporal)

Editar `Helpers/ApiConfiguration.cs`:

```csharp
public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60); // Aumentar a 60 segundos
```

#### Solución 2: Optimizar la consulta en el backend

Si una consulta tarda mucho, puede haber problema en:
- Tabla sin índices
- Consulta mal optimizada
- Demasiados datos

#### Solución 3: Implementar paginación

En lugar de traer todos los datos:

```csharp
// Mal (puede ser lento con muchos registros)
var todos = await _service.GetAllDispositivosAsync();

// Mejor (traer solo lo necesario)
var recientes = await _service.GetAllDispositivosAsync();
var ultimos10 = recientes.OrderByDescending(d => d.Registro).Take(10).ToList();
```

---

## 🔴 ERRORES DE BASE DE DATOS

### ❌ Error: "Unable to connect to database" en el backend

**Síntomas:**
```
SqlException: A network-related or instance-specific error occurred
Cannot open database "DevicesDB"
Login failed for user
```

**Soluciones:**

#### Solución 1: Verificar que SQL Server esté corriendo

**Windows:**
1. Win + R → `services.msc`
2. Buscar "SQL Server (MSSQLSERVER)"
3. Estado debe ser "Running"
4. Si no está corriendo: Click derecho → Start

#### Solución 2: Verificar cadena de conexión

Editar `DevicesAPI/appsettings.Development.json`:

**Opción A - Windows Authentication:**
```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Opción B - SQL Server Express:**
```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost\\SQLEXPRESS;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Opción C - Usuario/Contraseña:**
```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=DevicesDB;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;"
  }
}
```

#### Solución 3: Crear la base de datos

```bash
cd DevicesAPI
dotnet ef database update
```

---

### ❌ Error: "No such table" o "Invalid object name"

**Causa:**
Las migraciones no se aplicaron correctamente.

**Solución:**

```bash
cd DevicesAPI

# Ver migraciones disponibles
dotnet ef migrations list

# Aplicar migraciones
dotnet ef database update

# Si hay problemas, eliminar BD y recrear
dotnet ef database drop
dotnet ef database update
```

---

## 🔴 ERRORES DE CONFIGURACIÓN

### ❌ Error: "Cannot resolve service for type IXXXService"

**Síntomas:**
```
InvalidOperationException: Unable to resolve service for type 'Phanteon.Services.Interfaces.IDispositivosService'
```

**Causa:**
El servicio no está registrado en `MauiProgram.cs`.

**Solución:**

Verificar que el servicio esté registrado en `MauiProgram.cs`:

```csharp
// Servicios de API (ya registrados)
builder.Services.AddRefitClient<IDispositivosService>()...
builder.Services.AddRefitClient<IAlertasService>()...
builder.Services.AddRefitClient<IUsuariosService>()...
builder.Services.AddRefitClient<IHistorialDispositivosService>()...

// Si agregas nuevos ViewModels:
builder.Services.AddTransient<TuViewModel>();

// Si agregas nuevas Pages:
builder.Services.AddTransient<TuPage>();
```

---

### ❌ Error: "Port 7026 already in use"

**Síntomas:**
```
IOException: Failed to bind to address https://127.0.0.1:7026
EADDRINUSE: address already in use
```

**Causa:**
Otra instancia del backend está corriendo o el puerto está ocupado.

**Soluciones:**

#### Solución 1: Cerrar instancia anterior

```bash
# Windows
taskkill /F /IM dotnet.exe

# Linux/Mac
killall dotnet
```

#### Solución 2: Cambiar puerto

Editar `DevicesAPI/Properties/launchSettings.json`:

```json
{
  "profiles": {
    "https": {
      "applicationUrl": "https://localhost:7027;http://localhost:5001"
    }
  }
}
```

Y actualizar en Phanteon (`ApiConfiguration.cs`):
```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7027";
```

---

## 🔴 ERRORES DE COMPILACIÓN

### ❌ Error: "ObservableProperty does not exist in the current context"

**Causa:**
Falta el using o la clase no es `partial`.

**Solución:**

```csharp
// 1. Agregar using
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

// 2. Asegurar que la clase sea partial
public partial class DispositivosViewModel : ObservableObject
{
    [ObservableProperty]  // ← Ahora funcionará
    private string nombre;
}
```

---

### ❌ Error: "RelayCommand does not exist"

**Solución:**

```csharp
using CommunityToolkit.Mvvm.Input;

public partial class MiViewModel : ObservableObject
{
    [RelayCommand]  // ← Funciona con el using correcto
    private async Task MiMetodo()
    {
        // ...
    }
}
```

---

## 🔴 ERRORES EN RUNTIME

### ❌ Error: "Binding not found" o propiedades no se actualizan

**Causa:**
- `BindingContext` no asignado
- Propiedad no es `ObservableProperty`
- Error en el nombre del binding en XAML

**Soluciones:**

#### Solución 1: Verificar BindingContext

```csharp
// En el code-behind de la página
public DispositivosPage(DispositivosViewModel viewModel)
{
    InitializeComponent();
    BindingContext = viewModel; // ← Importante
}
```

#### Solución 2: Verificar propiedades observables

```csharp
// ❌ Mal (no notifica cambios)
public string Nombre { get; set; }

// ✅ Bien
[ObservableProperty]
private string nombre;
```

#### Solución 3: Verificar nombres en XAML

```xml
<!-- El nombre debe coincidir con la propiedad generada -->
<Label Text="{Binding Nombre}"/> <!-- ← Mayúscula inicial -->
```

---

### ❌ Error: "Command not found" o botón no ejecuta comando

**Causa:**
El comando no está generado correctamente.

**Solución:**

```csharp
// 1. Asegurar que el método sea async Task
[RelayCommand]
private async Task CargarDatos() // ← async Task
{
    // ...
}

// 2. En XAML, el comando se llama automáticamente con "Command" al final
<Button Text="Cargar" Command="{Binding CargarDatosCommand}"/>
<!--                                                ^^^^^^^ -->
```

---

## 🔴 ERRORES DE NAVEGACIÓN

### ❌ Error: "Route not found" al navegar

**Causa:**
La ruta no está registrada en `AppShell.xaml.cs`.

**Solución:**

```csharp
// En AppShell.xaml.cs
public AppShell()
{
    InitializeComponent();

    // Registrar rutas
    Routing.RegisterRoute("detalleDispositivo", typeof(Views.DetalleDispositivoPage));
    Routing.RegisterRoute("login", typeof(Views.LoginPage));
}
```

---

### ❌ Error: QueryProperty no recibe el parámetro

**Solución:**

```csharp
// En el ViewModel
[QueryProperty(nameof(DispositivoId), "id")]
//                     ^^^^^^^^^^^^^^^  ^^^
//                     Nombre de la      Nombre del
//                     propiedad         parámetro en URL

public partial class DetalleDispositivoViewModel : ObservableObject
{
    [ObservableProperty]
    private int dispositivoId;

    // Al cambiar dispositivoId, cargar datos
    partial void OnDispositivoIdChanged(int value)
    {
        _ = CargarDetalleCommand.ExecuteAsync(null);
    }
}

// Al navegar
await Shell.Current.GoToAsync($"detalleDispositivo?id={dispositivoId}");
//                                                  ^^
//                                                  Debe coincidir
```

---

## 🔴 ERRORES DE RED

### ❌ Error: "No hay conexión a internet"

**Solución:**

Verificar conectividad antes de llamar API:

```csharp
using Microsoft.Maui.Networking;
using CommunityToolkit.Maui.Alerts;

[RelayCommand]
private async Task CargarDatos()
{
    // Verificar conexión
    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
    {
        await Toast.Make("No hay conexión a internet").Show();
        return;
    }

    // Continuar con la llamada
    var datos = await _service.GetAllAsync();
}
```

---

## 🛠️ Herramientas de Debugging

### Ver logs en tiempo real:

**Visual Studio:**
- Output → Debug
- View → Other Windows → Device Log (para Android)

**Ver peticiones HTTP:**
```csharp
// Agregar en MauiProgram.cs
#if DEBUG
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Trace);
#endif
```

### Probar endpoints directamente:

**Postman/Insomnia:**
```
GET https://localhost:7026/api/dispositivos/getall
GET https://localhost:7026/api/alertas/getall
```

**Navegador:**
```
https://localhost:7026/api/dispositivos/getall
```

---