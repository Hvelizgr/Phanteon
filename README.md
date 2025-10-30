# 📱 Phanteon - Aplicación .NET MAUI

> 🎓 **Proyecto Universitario**: Aplicación multiplataforma desarrollada con .NET 9 y MAUI
>
> 📚 **Conceptos clave**: Arquitectura MVVM | Inyección de Dependencias | APIs Seguras | Buenas Prácticas

---

## 👥 Integrantes del Equipo

| Nombre Completo | Código | Rol |
|----------------|---------|-----|
| Héctor Eduardo Véliz Girón | 000108304 | Desarrollador Principal |
| _Nombre Completo_ | _Código_ | _Rol_ |
| _Nombre Completo_ | _Código_ | _Rol_ |

**Fecha de Entrega:** _____/_____/_____
**Docente:** _________________________________
**Curso:** _________________________________

---

## 📑 Índice

1. [¿Por Dónde Empezar?](#-por-dónde-empezar) ⭐ **¡EMPIEZA AQUÍ!**
2. [Descripción General](#-descripción-general)
3. [Requisitos del Sistema](#-requisitos-del-sistema)
4. [Instalación y Configuración](#-instalación-y-configuración)
5. [Arquitectura MVVM](#-arquitectura-mvvm-explicada)
6. [Paquetes NuGet Explicados](#-paquetes-nuget-explicados)
7. [Estructura del Proyecto](#-estructura-del-proyecto)
8. [Guías Paso a Paso](#-guías-paso-a-paso)
9. [Ejemplos de Código](#-ejemplos-de-código-completos)
10. [Comandos Útiles](#-comandos-útiles)
11. [Solución de Problemas](#-solución-de-problemas)

---

## 🎯 ¿Por Dónde Empezar?

### 📖 Si eres nuevo en el proyecto, sigue estos pasos:

1. **Lee primero**: [Descripción General](#-descripción-general) - entiende qué hace el proyecto
2. **Instala lo necesario**: [Requisitos del Sistema](#-requisitos-del-sistema) - prepara tu computadora
3. **Configura el proyecto**: [Instalación y Configuración](#-instalación-y-configuración) - descarga y ejecuta
4. **Entiende la arquitectura**: [Arquitectura MVVM](#-arquitectura-mvvm-explicada) - aprende cómo está organizado
5. **Estudia los paquetes**: [Paquetes NuGet](#-paquetes-nuget-explicados) - conoce las herramientas que usamos
6. **Practica con ejemplos**: [Guías Paso a Paso](#-guías-paso-a-paso) - crea tus primeros componentes

### 🎓 Conceptos que debes conocer antes:

- **C#**: Lenguaje de programación que usamos
- **XAML**: Lenguaje para diseñar interfaces (similar a HTML)
- **Git**: Control de versiones para trabajar en equipo
- **Arquitectura MVVM**: Patrón de diseño que separa la lógica de la interfaz

> 💡 **Tip**: No te preocupes si no conoces todo, ¡este documento te enseñará!

---

## 🎯 Descripción General

### Objetivo
Desarrollar una aplicación multiplataforma usando .NET MAUI que implemente:
- Arquitectura MVVM limpia
- Inyección de dependencias
- Consumo seguro de APIs REST
- Almacenamiento seguro de credenciales

### Características
- ✅ Arquitectura MVVM con separación de responsabilidades
- ✅ Inyección de dependencias nativa de .NET
- ✅ Consumo de APIs REST con Refit
- ✅ Almacenamiento seguro con SecureStorage
- ✅ Manejo de errores resiliente con Polly

### Plataformas Soportadas
- 📱 **Android** (API 21+)
- 🍎 **iOS** (14.0+)
- 💻 **Windows** (10.0.17763+)
- 🍏 **macOS** (10.15+)

---

## 💻 Requisitos del Sistema

### Software Necesario

| Herramienta | Versión | Propósito |
|-------------|---------|-----------|
| Visual Studio 2022 | 17.8+ | IDE principal |
| .NET SDK | 9.0+ | Framework |
| Android SDK | API 34 | Desarrollo Android |
| Xcode | 15.0+ (macOS) | Desarrollo iOS |

### Verificar Instalación
```bash
# Verificar .NET MAUI
dotnet workload list

# Instalar si no está presente
dotnet workload install maui
```

---

---

## 📦 Paquetes NuGet Explicados

> 🎓 **¿Qué es NuGet?** Es como una "tienda de herramientas" donde descargamos librerías (código ya hecho) que nos facilitan el desarrollo.

### 1️⃣ CommunityToolkit.Maui (v12.2.0)

**¿Para qué sirve?**
Proporciona componentes visuales y funcionalidades extra para MAUI que no vienen incluidas por defecto.

**¿Cuándo lo usamos?**
- Cuando necesitamos alerts, toasts, popups
- Para validadores de formularios
- Cuando queremos animaciones predefinidas

**Ejemplo de uso:**
```csharp
// En MauiProgram.cs
builder.UseMauiCommunityToolkit(); // ← Activar el toolkit

// En tu ViewModel
using CommunityToolkit.Maui.Alerts;

// Mostrar un toast (mensaje temporal)
await Toast.Make("¡Datos guardados correctamente!").Show();
```

**Instalación:**
```bash
dotnet add package CommunityToolkit.Maui --version 12.2.0
```

---

### 2️⃣ CommunityToolkit.Mvvm (v8.4.0) ⭐ **MUY IMPORTANTE**

**¿Para qué sirve?**
Simplifica enormemente la implementación del patrón MVVM. Genera código automáticamente para nosotros usando "Source Generators".

**¿Qué nos ahorra?**
- No necesitamos escribir `INotifyPropertyChanged` manualmente
- Los comandos se crean con un simple atributo
- Reduce el código repetitivo en un 70%

**Ejemplo ANTES (sin Toolkit):**
```csharp
// Código largo y tedioso 😫
private string nombre;
public string Nombre
{
    get => nombre;
    set
    {
        if (nombre != value)
        {
            nombre = value;
            OnPropertyChanged(nameof(Nombre));
        }
    }
}
```

**Ejemplo DESPUÉS (con Toolkit):**
```csharp
// ¡Una sola línea! 🎉
[ObservableProperty]
private string nombre;

// El toolkit genera automáticamente:
// - La propiedad pública "Nombre"
// - La notificación de cambios
```

**Ejemplo completo de ViewModel:**
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

public partial class LoginViewModel : ObservableObject
{
    // Propiedades observables (generan notificaciones automáticas)
    [ObservableProperty]
    private string usuario;

    [ObservableProperty]
    private string contraseña;

    [ObservableProperty]
    private bool estaCargando;

    // Comando para el botón de login
    [RelayCommand]
    private async Task IniciarSesion()
    {
        EstaCargando = true;

        // Tu lógica aquí...
        await Task.Delay(2000); // Simula llamada a API

        EstaCargando = false;
    }
}
```

**Instalación:**
```bash
dotnet add package CommunityToolkit.Mvvm --version 8.4.0
```

---

### 3️⃣ Refit (v8.0.0) + Refit.HttpClientFactory (v8.0.0)

**¿Para qué sirve?**
Convierte llamadas a APIs REST en simples interfaces de C#. No necesitas escribir `HttpClient` manualmente.

**¿Cuándo lo usamos?**
- Para consumir APIs (GET, POST, PUT, DELETE)
- Cuando queremos que las llamadas HTTP sean fáciles de leer y mantener

**Ejemplo SIN Refit (tedioso):**
```csharp
// Mucho código manual 😫
var client = new HttpClient();
var response = await client.GetAsync("https://api.ejemplo.com/usuarios");
var json = await response.Content.ReadAsStringAsync();
var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
```

**Ejemplo CON Refit (simple):**
```csharp
// 1. Define la interfaz (contrato de la API)
public interface IUsuariosApi
{
    [Get("/usuarios")]
    Task<List<Usuario>> ObtenerUsuarios();

    [Get("/usuarios/{id}")]
    Task<Usuario> ObtenerUsuario(int id);

    [Post("/usuarios")]
    Task<Usuario> CrearUsuario([Body] Usuario usuario);

    [Put("/usuarios/{id}")]
    Task ActualizarUsuario(int id, [Body] Usuario usuario);

    [Delete("/usuarios/{id}")]
    Task EliminarUsuario(int id);
}

// 2. Registrar en MauiProgram.cs
builder.Services
    .AddRefitClient<IUsuariosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.ejemplo.com"));

// 3. Usar en tu ViewModel
public class UsuariosViewModel
{
    private readonly IUsuariosApi _api;

    public UsuariosViewModel(IUsuariosApi api)
    {
        _api = api; // ← Inyección de dependencias
    }

    [RelayCommand]
    private async Task CargarUsuarios()
    {
        var usuarios = await _api.ObtenerUsuarios(); // ← ¡Así de fácil!
    }
}
```

**Instalación:**
```bash
dotnet add package Refit --version 8.0.0
dotnet add package Refit.HttpClientFactory --version 8.0.0
```

---

### 4️⃣ Newtonsoft.Json (v13.0.4)

**¿Para qué sirve?**
Convierte objetos de C# a JSON y viceversa (serialización/deserialización).

**¿Cuándo lo usamos?**
- Para guardar/leer datos en formato JSON
- Al trabajar con APIs que devuelven JSON
- Para almacenar configuraciones

**Ejemplo:**
```csharp
using Newtonsoft.Json;

// Clase de C#
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
}

// Convertir objeto a JSON (Serializar)
var producto = new Producto { Id = 1, Nombre = "Laptop", Precio = 1200.50m };
string json = JsonConvert.SerializeObject(producto);
// Resultado: {"Id":1,"Nombre":"Laptop","Precio":1200.50}

// Convertir JSON a objeto (Deserializar)
string jsonRecibido = "{\"Id\":2,\"Nombre\":\"Mouse\",\"Precio\":25.99}";
Producto productoNuevo = JsonConvert.DeserializeObject<Producto>(jsonRecibido);
```

**Instalación:**
```bash
dotnet add package Newtonsoft.Json --version 13.0.4
```

---

### 5️⃣ Polly (v8.6.4) + Polly.Extensions.Http (v3.0.0)

**¿Para qué sirve?**
Hace que las llamadas HTTP sean más resistentes a fallos. Si una petición falla, puede reintentar automáticamente.

**¿Cuándo lo usamos?**
- Cuando la conexión a internet puede fallar
- Para reintentar automáticamente si una API no responde
- Para evitar que la app se "rompa" por errores de red

**Ejemplo de configuración:**
```csharp
using Polly;
using Polly.Extensions.Http;

// En MauiProgram.cs
builder.Services
    .AddRefitClient<IUsuariosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.ejemplo.com"))
    .AddPolicyHandler(GetRetryPolicy()); // ← Agregar política de reintentos

// Política: reintentar 3 veces si falla
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError() // Maneja errores 5xx y 408
        .WaitAndRetryAsync(
            retryCount: 3,
            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            onRetry: (outcome, timespan, retryAttempt, context) =>
            {
                Console.WriteLine($"Reintento {retryAttempt} después de {timespan.TotalSeconds}s");
            });
}
```

**Qué hace este código:**
1. Si la API falla, espera 2 segundos y reintenta
2. Si falla de nuevo, espera 4 segundos y reintenta
3. Si falla de nuevo, espera 8 segundos y reintenta
4. Si falla una 4ta vez, lanza el error

**Instalación:**
```bash
dotnet add package Polly --version 8.6.4
dotnet add package Polly.Extensions.Http --version 3.0.0
```

---

### 6️⃣ Microsoft.Maui.Essentials (Incluido en MAUI)

**¿Para qué sirve?**
Proporciona acceso a funcionalidades nativas del dispositivo de forma multiplataforma.

**Funcionalidades principales:**
- **SecureStorage**: Almacenar contraseñas de forma segura
- **Connectivity**: Verificar si hay internet
- **Geolocation**: Obtener ubicación GPS
- **DeviceInfo**: Info del dispositivo
- **Preferences**: Guardar configuraciones simples

**Ejemplo 1: SecureStorage (Guardar contraseñas de forma segura)**
```csharp
using Microsoft.Maui.Storage;

// Guardar token de autenticación
await SecureStorage.SetAsync("auth_token", "ABC123XYZ789");

// Leer token guardado
string token = await SecureStorage.GetAsync("auth_token");

// Eliminar token (ej: al cerrar sesión)
SecureStorage.Remove("auth_token");
```

**Ejemplo 2: Connectivity (Verificar internet)**
```csharp
using Microsoft.Maui.Networking;

// Verificar si hay conexión
if (Connectivity.NetworkAccess == NetworkAccess.Internet)
{
    // Hay internet, hacer llamada a API
    await _api.ObtenerDatos();
}
else
{
    // No hay internet, mostrar mensaje
    await Toast.Make("No hay conexión a internet").Show();
}
```

**Ejemplo 3: Preferences (Guardar configuraciones)**
```csharp
using Microsoft.Maui.Storage;

// Guardar preferencia
Preferences.Set("tema_oscuro", true);
Preferences.Set("idioma", "es");
Preferences.Set("tamaño_fuente", 14);

// Leer preferencia
bool temaOscuro = Preferences.Get("tema_oscuro", false); // false = valor por defecto
string idioma = Preferences.Get("idioma", "en");
int tamañoFuente = Preferences.Get("tamaño_fuente", 12);
```

**¿Cómo funciona SecureStorage internamente?**
- **Android**: Usa EncryptedSharedPreferences (cifrado AES-256)
- **iOS/macOS**: Usa Keychain (almacén seguro de Apple)
- **Windows**: Usa Data Protection API (DPAPI)

> ⚠️ **IMPORTANTE**: SecureStorage NO requiere instalación adicional, viene incluido en MAUI.

---

## 📊 Resumen de Paquetes NuGet

| Paquete | ¿Para qué? | ¿Cuándo usarlo? |
|---------|------------|-----------------|
| **CommunityToolkit.Maui** | Componentes visuales extra | Toasts, Alerts, Popups |
| **CommunityToolkit.Mvvm** | Simplificar MVVM | Siempre (en todos los ViewModels) |
| **Refit** | Llamadas a APIs fáciles | Consumo de REST APIs |
| **Newtonsoft.Json** | Convertir JSON ↔ C# | Trabajar con datos JSON |
| **Polly** | Reintentos automáticos | APIs que pueden fallar |
| **MAUI Essentials** | Funciones del dispositivo | Almacenamiento, GPS, Conectividad |

---

## 🏗️ Arquitectura MVVM Explicada

### ¿Qué es MVVM?

**MVVM** significa **Model-View-ViewModel**. Es un patrón de diseño que separa tu aplicación en 3 capas para que sea más fácil de mantener y probar.

> 🎓 **Analogía**: Imagina un restaurante:
> - **View** (Vista) = El mesero que interactúa con el cliente
> - **ViewModel** = El gerente que coordina todo
> - **Model** = La cocina y el almacén donde se preparan los platillos

### Las 3 Capas de MVVM

```
┌─────────────────────────────────────────────────────────────┐
│                    👁️ VIEW (Vista)                          │
│                 (Views/MainPage.xaml)                       │
│                                                             │
│  📱 Lo que el usuario VE y TOCA                             │
│  - Botones, Textos, Imágenes, Formularios                  │
│  - Definida en XAML (similar a HTML)                       │
│  - NO tiene lógica de negocio (solo muestra datos)         │
│                                                             │
│  Ejemplo: <Button Text="Iniciar Sesión"                    │
│                   Command="{Binding IniciarSesionCommand}"/>│
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ 🔗 DATA BINDING
                       │ (Conexión automática de datos)
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                  🧠 VIEW MODEL                              │
│              (ViewModels/LoginViewModel.cs)                 │
│                                                             │
│  🎮 COORDINADOR entre Vista y Servicios                     │
│  - Propiedades que la Vista puede ver (ej: Usuario)        │
│  - Comandos que ejecutan acciones (ej: LoginCommand)       │
│  - Lógica de presentación (ej: validar formularios)        │
│  - NO sabe de botones ni XAML                              │
│                                                             │
│  Ejemplo: [ObservableProperty] string usuario;             │
│           [RelayCommand] async Task IniciarSesion()         │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ 💉 INYECCIÓN DE DEPENDENCIAS
                       │ (El ViewModel pide servicios)
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                   ⚙️ SERVICES (Servicios)                   │
│        (Services/Implementations/AuthService.cs)            │
│                                                             │
│  🔧 LÓGICA DE NEGOCIO                                       │
│  - Llamadas a APIs (con Refit)                             │
│  - Guardar/Leer datos (SecureStorage)                      │
│  - Cálculos complejos                                       │
│  - NO sabe de vistas ni ViewModels                         │
│                                                             │
│  Ejemplo: Task<Usuario> IniciarSesion(string user, pass)   │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       │ 📦 USA
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                    📋 MODELS (Modelos)                      │
│                  (Models/Usuario.cs)                        │
│                                                             │
│  📊 DATOS (solo propiedades, sin lógica)                    │
│  - Clases simples que representan datos                    │
│  - DTOs para transferir datos de la API                    │
│                                                             │
│  Ejemplo: public class Usuario { ... }                     │
└─────────────────────────────────────────────────────────────┘
```

---

### 📖 Ejemplo Completo: Sistema de Login

Vamos a ver cómo se conecta todo con un ejemplo real de login.

#### 1️⃣ **MODEL** (Modelo de datos)

```csharp
// Models/Usuario.cs
namespace Phanteon.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }

    // DTO para enviar credenciales
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
    }
}
```

#### 2️⃣ **SERVICE** (Lógica de negocio)

```csharp
// Services/Interfaces/IAuthService.cs
public interface IAuthService
{
    Task<Usuario> IniciarSesion(string usuario, string contraseña);
    Task CerrarSesion();
    Task<bool> EstaAutenticado();
}

// Services/Implementations/AuthService.cs
using Phanteon.Models;
using Microsoft.Maui.Storage;

public class AuthService : IAuthService
{
    private readonly IAuthApi _authApi; // API con Refit

    public AuthService(IAuthApi authApi)
    {
        _authApi = authApi;
    }

    public async Task<Usuario> IniciarSesion(string usuario, string contraseña)
    {
        // 1. Llamar a la API
        var request = new LoginRequest
        {
            Usuario = usuario,
            Contraseña = contraseña
        };

        var usuarioAutenticado = await _authApi.Login(request);

        // 2. Guardar token de forma segura
        await SecureStorage.SetAsync("auth_token", usuarioAutenticado.Token);
        await SecureStorage.SetAsync("user_id", usuarioAutenticado.Id.ToString());

        return usuarioAutenticado;
    }

    public async Task CerrarSesion()
    {
        SecureStorage.Remove("auth_token");
        SecureStorage.Remove("user_id");
    }

    public async Task<bool> EstaAutenticado()
    {
        var token = await SecureStorage.GetAsync("auth_token");
        return !string.IsNullOrEmpty(token);
    }
}
```

#### 3️⃣ **VIEWMODEL** (Lógica de presentación)

```csharp
// ViewModels/LoginViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Alerts;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    // Constructor (recibe servicios por inyección de dependencias)
    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    // Propiedades que la Vista puede ver y modificar
    [ObservableProperty]
    private string usuario = string.Empty;

    [ObservableProperty]
    private string contraseña = string.Empty;

    [ObservableProperty]
    private bool estaCargando = false;

    [ObservableProperty]
    private string mensajeError = string.Empty;

    // Comando que el botón ejecutará
    [RelayCommand]
    private async Task IniciarSesion()
    {
        // Validaciones
        if (string.IsNullOrWhiteSpace(Usuario))
        {
            MensajeError = "El usuario es requerido";
            return;
        }

        if (string.IsNullOrWhiteSpace(Contraseña))
        {
            MensajeError = "La contraseña es requerida";
            return;
        }

        try
        {
            EstaCargando = true;
            MensajeError = string.Empty;

            // Llamar al servicio
            var usuarioAutenticado = await _authService.IniciarSesion(Usuario, Contraseña);

            // Mostrar mensaje de éxito
            await Toast.Make($"¡Bienvenido {usuarioAutenticado.NombreUsuario}!").Show();

            // Navegar a la página principal (lo veremos más adelante)
            await Shell.Current.GoToAsync("///MainPage");
        }
        catch (Exception ex)
        {
            MensajeError = "Usuario o contraseña incorrectos";
        }
        finally
        {
            EstaCargando = false;
        }
    }
}
```

#### 4️⃣ **VIEW** (Interfaz visual)

```xml
<!-- Views/LoginPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:Phanteon.ViewModels"
             x:Class="Phanteon.Views.LoginPage"
             x:DataType="viewmodel:LoginViewModel">

    <VerticalStackLayout Padding="20" Spacing="20">

        <Label Text="Iniciar Sesión"
               FontSize="32"
               FontAttributes="Bold"
               HorizontalOptions="Center"/>

        <!-- Campo de Usuario -->
        <Entry Placeholder="Usuario"
               Text="{Binding Usuario}"
               IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}"/>

        <!-- Campo de Contraseña -->
        <Entry Placeholder="Contraseña"
               IsPassword="True"
               Text="{Binding Contraseña}"
               IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}"/>

        <!-- Mensaje de Error -->
        <Label Text="{Binding MensajeError}"
               TextColor="Red"
               IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}"/>

        <!-- Botón de Login -->
        <Button Text="Iniciar Sesión"
                Command="{Binding IniciarSesionCommand}"
                IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}"/>

        <!-- Indicador de Carga -->
        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"/>

    </VerticalStackLayout>
</ContentPage>
```

```csharp
// Views/LoginPage.xaml.cs (Code-Behind mínimo)
namespace Phanteon.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; // Conectar con el ViewModel
    }
}
```

#### 5️⃣ **Registrar en MauiProgram.cs**

```csharp
// MauiProgram.cs
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit();

        // Registrar API (Refit)
        builder.Services
            .AddRefitClient<IAuthApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://tu-api.com"));

        // Registrar Servicios
        builder.Services.AddSingleton<IAuthService, AuthService>();

        // Registrar ViewModels
        builder.Services.AddTransient<LoginViewModel>();

        // Registrar Pages
        builder.Services.AddTransient<LoginPage>();

        return builder.Build();
    }
}
```

---

### 🔄 Flujo de Datos en MVVM

Cuando el usuario presiona el botón "Iniciar Sesión":

1. **View** → Ejecuta el comando `IniciarSesionCommand` del **ViewModel**
2. **ViewModel** → Lee las propiedades `Usuario` y `Contraseña`
3. **ViewModel** → Llama al método `IniciarSesion()` del **Service**
4. **Service** → Hace la petición HTTP a la API con **Refit**
5. **Service** → Recibe el **Model** (Usuario) de la API
6. **Service** → Guarda el token en **SecureStorage**
7. **Service** → Devuelve el **Model** al **ViewModel**
8. **ViewModel** → Actualiza sus propiedades (ej: `EstaCargando = false`)
9. **View** → Se actualiza automáticamente gracias al **Data Binding**

---

### ✅ Ventajas de MVVM

| Ventaja | Explicación |
|---------|-------------|
| **Separación de responsabilidades** | Cada capa tiene su función clara |
| **Facilita testing** | Puedes probar el ViewModel sin la Vista |
| **Reutilización** | Servicios pueden usarse en múltiples ViewModels |
| **Mantenibilidad** | Cambios en la UI no afectan la lógica |
| **Trabajo en equipo** | Varios desarrolladores pueden trabajar en paralelo |

---

### 🎯 Principios SOLID (Buenas Prácticas)

| Principio | ¿Qué significa? | Ejemplo en nuestro código |
|-----------|-----------------|---------------------------|
| **S**ingle Responsibility | Cada clase una sola responsabilidad | `AuthService` solo maneja autenticación |
| **O**pen/Closed | Abierto a extensión, cerrado a modificación | Usamos interfaces (`IAuthService`) |
| **L**iskov Substitution | Podemos reemplazar interfaces | Podemos usar `MockAuthService` para testing |
| **I**nterface Segregation | Interfaces pequeñas y específicas | `IAuthService` solo tiene métodos de auth |
| **D**ependency Inversion | Depender de abstracciones | ViewModel depende de `IAuthService`, no `AuthService` |

---

## 📂 Estructura del Proyecto

```
Phanteon/
├── 📁 Views/                       ← INTERFAZ (Lo que el usuario ve)
│   ├── MainPage.xaml              → Diseño visual en XAML
│   └── MainPage.xaml.cs           → Code-behind (mínimo)
│
├── 📁 ViewModels/                  ← LÓGICA DE PRESENTACIÓN
│   └── BaseViewModel.cs           → Clase base para ViewModels
│   └── MainViewModel.cs           → ViewModel específico
│
├── 📁 Models/                      ← DATOS (Clases simples)
│   └── Usuario.cs                 → Modelo de datos
│   └── Producto.cs
│
├── 📁 Services/                    ← LÓGICA DE NEGOCIO
│   ├── 📁 Interfaces/             ← Contratos (¿QUÉ hace?)
│   │   ├── IAuthService.cs        → Interface de autenticación
│   │   └── IProductoService.cs    → Interface de productos
│   │
│   └── 📁 Implementations/        ← Implementaciones (¿CÓMO lo hace?)
│       ├── AuthService.cs         → Implementación de auth
│       └── ProductoService.cs     → Implementación de productos
│
├── 📁 Helpers/                     ← UTILIDADES
│   ├── Converters/                → Conversores para XAML
│   │   └── BoolToColorConverter.cs
│   └── Extensions/                → Métodos de extensión
│
├── 📁 Resources/                   ← RECURSOS VISUALES
│   ├── Styles/
│   │   ├── Colors.xaml            → Paleta de colores
│   │   └── Styles.xaml            → Estilos globales
│   ├── Images/                    → Imágenes de la app
│   └── Fonts/                     → Fuentes personalizadas
│
├── 📁 Platforms/                   ← CÓDIGO ESPECÍFICO DE PLATAFORMA
│   ├── Android/
│   │   └── AndroidManifest.xml    → Permisos y config Android
│   ├── iOS/
│   │   └── Info.plist             → Config iOS
│   ├── Windows/
│   ├── MacCatalyst/
│   └── Tizen/
│
├── 📄 App.xaml                     ← Configuración global de la app
├── 📄 AppShell.xaml                ← Navegación y rutas
├── 📄 MauiProgram.cs               ← ⭐ CONFIGURACIÓN E INYECCIÓN DE DEPENDENCIAS
└── 📄 Phanteon.csproj              ← Paquetes NuGet y configuración
```

### 📖 ¿Para qué sirve cada carpeta?

| Carpeta | ¿Qué va aquí? | Ejemplo |
|---------|---------------|---------|
| **Views/** | Interfaces visuales (XAML) | LoginPage.xaml, HomePage.xaml |
| **ViewModels/** | Lógica de presentación | LoginViewModel.cs |
| **Models/** | Clases de datos | Usuario.cs, Producto.cs |
| **Services/Interfaces/** | Contratos de servicios | IAuthService.cs |
| **Services/Implementations/** | Implementación de servicios | AuthService.cs |
| **Helpers/** | Código reutilizable | Converters, Extensions |
| **Resources/** | Imágenes, estilos, fuentes | logo.png, Colors.xaml |

---

## 📝 Guías Paso a Paso

### 🎯 Guía 1: Crear un nuevo Model (Modelo de Datos)

Los modelos son clases simples que representan datos.

**Paso 1:** Crear archivo en la carpeta `Models/`

```csharp
// Models/Producto.cs
namespace Phanteon.Models
{
    /// <summary>
    /// Representa un producto en el sistema
    /// </summary>
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string ImagenUrl { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
```

**Paso 2:** (Opcional) Crear DTOs para la API

```csharp
// Models/DTOs/ProductoDto.cs
namespace Phanteon.Models.DTOs
{
    /// <summary>
    /// DTO para crear un producto (no incluye Id)
    /// </summary>
    public class CrearProductoDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
```

> 💡 **Tip**: Los DTOs (Data Transfer Objects) son versiones simplificadas de los modelos para enviar/recibir de la API.

---

### 🎯 Guía 2: Crear un Service (Servicio)

Los servicios contienen la lógica de negocio.

**Paso 1:** Crear la interfaz en `Services/Interfaces/`

```csharp
// Services/Interfaces/IProductoService.cs
using Phanteon.Models;

namespace Phanteon.Services.Interfaces
{
    public interface IProductoService
    {
        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        Task<List<Producto>> ObtenerProductos();

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        Task<Producto> ObtenerProductoPorId(int id);

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        Task<Producto> CrearProducto(Producto producto);

        /// <summary>
        /// Elimina un producto
        /// </summary>
        Task<bool> EliminarProducto(int id);
    }
}
```

**Paso 2:** Crear la implementación en `Services/Implementations/`

```csharp
// Services/Implementations/ProductoService.cs
using Phanteon.Models;
using Phanteon.Services.Interfaces;

namespace Phanteon.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoApi _productoApi; // API con Refit

        public ProductoService(IProductoApi productoApi)
        {
            _productoApi = productoApi;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            try
            {
                // Llamar a la API
                var productos = await _productoApi.GetProductos();
                return productos;
            }
            catch (Exception ex)
            {
                // Manejar errores
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
                return new List<Producto>();
            }
        }

        public async Task<Producto> ObtenerProductoPorId(int id)
        {
            return await _productoApi.GetProducto(id);
        }

        public async Task<Producto> CrearProducto(Producto producto)
        {
            return await _productoApi.CreateProducto(producto);
        }

        public async Task<bool> EliminarProducto(int id)
        {
            await _productoApi.DeleteProducto(id);
            return true;
        }
    }
}
```

**Paso 3:** Crear la API interface con Refit

```csharp
// Services/Api/IProductoApi.cs
using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Api
{
    public interface IProductoApi
    {
        [Get("/productos")]
        Task<List<Producto>> GetProductos();

        [Get("/productos/{id}")]
        Task<Producto> GetProducto(int id);

        [Post("/productos")]
        Task<Producto> CreateProducto([Body] Producto producto);

        [Delete("/productos/{id}")]
        Task DeleteProducto(int id);
    }
}
```

**Paso 4:** Registrar en `MauiProgram.cs`

```csharp
// MauiProgram.cs
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();

    // Registrar API con Refit
    builder.Services
        .AddRefitClient<IProductoApi>()
        .ConfigureHttpClient(c =>
            c.BaseAddress = new Uri("https://tu-api.com/api"));

    // Registrar Servicio
    builder.Services.AddSingleton<IProductoService, ProductoService>();

    return builder.Build();
}
```

---

### 🎯 Guía 3: Crear un ViewModel

**Paso 1:** Crear archivo en `ViewModels/`

```csharp
// ViewModels/ProductosViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Phanteon.ViewModels
{
    public partial class ProductosViewModel : ObservableObject
    {
        private readonly IProductoService _productoService;

        // Constructor con inyección de dependencias
        public ProductosViewModel(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // Colección observable de productos
        [ObservableProperty]
        private ObservableCollection<Producto> productos = new();

        // Indicador de carga
        [ObservableProperty]
        private bool estaCargando = false;

        // Producto seleccionado
        [ObservableProperty]
        private Producto productoSeleccionado;

        // Comando para cargar productos
        [RelayCommand]
        private async Task CargarProductos()
        {
            EstaCargando = true;

            try
            {
                var listaProductos = await _productoService.ObtenerProductos();

                Productos.Clear();
                foreach (var producto in listaProductos)
                {
                    Productos.Add(producto);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    "Error",
                    $"No se pudieron cargar los productos: {ex.Message}",
                    "OK");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        // Comando para eliminar producto
        [RelayCommand]
        private async Task EliminarProducto(int id)
        {
            bool confirmacion = await Shell.Current.DisplayAlert(
                "Confirmar",
                "¿Estás seguro de eliminar este producto?",
                "Sí",
                "No");

            if (confirmacion)
            {
                await _productoService.EliminarProducto(id);
                await CargarProductos(); // Recargar lista
            }
        }
    }
}
```

**Paso 2:** Registrar en `MauiProgram.cs`

```csharp
// Registrar ViewModel
builder.Services.AddTransient<ProductosViewModel>();
```

---

### 🎯 Guía 4: Crear una View (Página)

**Paso 1:** Crear archivo XAML en `Views/`

```xml
<!-- Views/ProductosPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:Phanteon.ViewModels"
             x:Class="Phanteon.Views.ProductosPage"
             x:DataType="viewmodel:ProductosViewModel"
             Title="Productos">

    <!-- Toolbar con botón de recargar -->
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Recargar"
                    Command="{Binding CargarProductosCommand}"/>
    </ContentPage.ToolbarItems>

    <Grid>
        <!-- Lista de productos -->
        <CollectionView ItemsSource="{Binding Productos}"
                       SelectionMode="Single"
                       SelectedItem="{Binding ProductoSeleccionado}">
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="models:Producto">
                    <SwipeView>
                        <!-- Acción de deslizar para eliminar -->
                        <SwipeView.RightItems>
                            <SwipeItems>
                                <SwipeItem Text="Eliminar"
                                          BackgroundColor="Red"
                                          Command="{Binding Source={RelativeSource AncestorType={x:Type viewmodel:ProductosViewModel}}, Path=EliminarProductoCommand}"
                                          CommandParameter="{Binding Id}"/>
                            </SwipeItems>
                        </SwipeView.RightItems>

                        <!-- Contenido del item -->
                        <Frame Padding="10" Margin="10">
                            <Grid ColumnDefinitions="Auto,*,Auto">
                                <!-- Imagen -->
                                <Image Source="{Binding ImagenUrl}"
                                      WidthRequest="60"
                                      HeightRequest="60"
                                      Aspect="AspectFill"/>

                                <!-- Info -->
                                <VerticalStackLayout Grid.Column="1" Padding="10,0">
                                    <Label Text="{Binding Nombre}"
                                          FontSize="18"
                                          FontAttributes="Bold"/>
                                    <Label Text="{Binding Descripcion}"
                                          FontSize="14"
                                          TextColor="Gray"/>
                                </VerticalStackLayout>

                                <!-- Precio -->
                                <Label Grid.Column="2"
                                      Text="{Binding Precio, StringFormat='${0:F2}'}"
                                      FontSize="20"
                                      FontAttributes="Bold"
                                      VerticalOptions="Center"/>
                            </Grid>
                        </Frame>
                    </SwipeView>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>

        <!-- Indicador de carga -->
        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"
                          HorizontalOptions="Center"
                          VerticalOptions="Center"/>
    </Grid>
</ContentPage>
```

**Paso 2:** Crear code-behind

```csharp
// Views/ProductosPage.xaml.cs
namespace Phanteon.Views;

public partial class ProductosPage : ContentPage
{
    private readonly ProductosViewModel _viewModel;

    public ProductosPage(ProductosViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Cargar datos cuando la página aparece
        await _viewModel.CargarProductosCommand.ExecuteAsync(null);
    }
}
```

**Paso 3:** Registrar en `MauiProgram.cs`

```csharp
// Registrar Page
builder.Services.AddTransient<ProductosPage>();
```

---

### 🎯 Guía 5: Configurar Navegación

**En AppShell.xaml:**

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:views="clr-namespace:Phanteon.Views"
       x:Class="Phanteon.AppShell">

    <!-- Tabs principales -->
    <TabBar>
        <ShellContent Title="Inicio"
                     Icon="home.png"
                     ContentTemplate="{DataTemplate views:MainPage}"/>

        <ShellContent Title="Productos"
                     Icon="cart.png"
                     Route="productos"
                     ContentTemplate="{DataTemplate views:ProductosPage}"/>
    </TabBar>
</Shell>
```

**Navegar desde código:**

```csharp
// Navegar a otra página
await Shell.Current.GoToAsync("///productos");

// Navegar con parámetros
await Shell.Current.GoToAsync($"detalles?id={productoId}");

// Volver atrás
await Shell.Current.GoToAsync("..");
```

---

## ⚙️ Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone https://github.com/tu-usuario/Phanteon.git
cd Phanteon
```

### 2. Restaurar Paquetes
```bash
dotnet restore
```

### 3. Compilar el Proyecto
```bash
# Compilación general
dotnet build

# Compilación para Android
dotnet build -f net9.0-android
```

### 4. Ejecutar la Aplicación
```bash
# Android
dotnet build -t:Run -f net9.0-android

# iOS (solo macOS)
dotnet build -t:Run -f net9.0-ios

# Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

---

## 🛠️ Comandos Útiles

### Gestión del Proyecto
```bash
# Limpiar solución
dotnet clean

# Restaurar paquetes
dotnet restore

# Compilar sin ejecutar
dotnet build

# Reconstruir completamente
dotnet clean && dotnet build
```

### Gestión de Paquetes
```bash
# Ver paquetes instalados
dotnet list package

# Agregar nuevo paquete
dotnet add package NombrePaquete --version X.X.X

# Actualizar paquete
dotnet add package NombrePaquete
```

### Debugging
```bash
# Ver logs detallados
dotnet build -v detailed

# Ver diagnósticos
dotnet build /bl
```

---
<<<<<<< HEAD

## 📝 Configuración de MauiProgram.cs

El archivo `MauiProgram.cs` es el punto de entrada para configurar servicios e inyección de dependencias:

```csharp
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

        // Registrar servicios
        builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();

        // Registrar ViewModels
        builder.Services.AddTransient<MainViewModel>();

        // Registrar Views
        builder.Services.AddTransient<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
```

---

---

## 💻 Ejemplos de Código Completos

### 📱 Ejemplo 1: MauiProgram.cs Completo

Este es el archivo más importante donde se configura todo.

```csharp
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Phanteon.Views;
using Phanteon.ViewModels;
using Phanteon.Services.Interfaces;
using Phanteon.Services.Implementations;
using Phanteon.Services.Api;
using Refit;
using Polly;
using Polly.Extensions.Http;

namespace Phanteon
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit() // ← Activar Community Toolkit
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
            // CONFIGURAR APIs CON REFIT Y POLLY
            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

            // API de Autenticación
            builder.Services
                .AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri("https://tu-api.com/api"))
                .AddPolicyHandler(GetRetryPolicy());

            // API de Productos
            builder.Services
                .AddRefitClient<IProductoApi>()
                .ConfigureHttpClient(c =>
                    c.BaseAddress = new Uri("https://tu-api.com/api"))
                .AddPolicyHandler(GetRetryPolicy());

            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
            // REGISTRAR SERVICIOS (Singleton = una instancia para toda la app)
            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IProductoService, ProductoService>();

            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
            // REGISTRAR VIEWMODELS (Transient = nueva instancia cada vez)
            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<ProductosViewModel>();
            builder.Services.AddTransient<MainViewModel>();

            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
            // REGISTRAR PAGES (Transient = nueva instancia cada vez)
            // ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ProductosPage>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /// <summary>
        /// Política de reintentos: intenta 3 veces con espera exponencial
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError() // Errores 5xx y 408
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2s, 4s, 8s
                    onRetry: (outcome, timespan, retryAttempt, context) =>
                    {
#if DEBUG
                        Console.WriteLine($"[Refit] Reintento {retryAttempt} después de {timespan.TotalSeconds}s");
#endif
                    });
        }
    }
}
```

---

### 📱 Ejemplo 2: BaseViewModel (Clase base para todos los ViewModels)

```csharp
// ViewModels/BaseViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;

namespace Phanteon.ViewModels
{
    /// <summary>
    /// Clase base para todos los ViewModels con funcionalidades comunes
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool estaCargando;

        [ObservableProperty]
        private string titulo = string.Empty;

        [ObservableProperty]
        private string mensajeError = string.Empty;

        /// <summary>
        /// Método auxiliar para manejar errores de forma consistente
        /// </summary>
        protected void ManejarError(Exception ex, string mensajePersonalizado = "Ocurrió un error")
        {
            MensajeError = mensajePersonalizado;
            Console.WriteLine($"[Error] {ex.Message}");
#if DEBUG
            Console.WriteLine($"[StackTrace] {ex.StackTrace}");
#endif
        }

        /// <summary>
        /// Muestra un alert al usuario
        /// </summary>
        protected async Task MostrarAlerta(string titulo, string mensaje)
        {
            await Shell.Current.DisplayAlert(titulo, mensaje, "OK");
        }

        /// <summary>
        /// Muestra un diálogo de confirmación
        /// </summary>
        protected async Task<bool> MostrarConfirmacion(string titulo, string mensaje)
        {
            return await Shell.Current.DisplayAlert(titulo, mensaje, "Sí", "No");
        }
    }
}
```

>>>>>>> 25ca9c43c61af9e74cf038a6dee521a1c6d1cde6
---

### 📱 Ejemplo 3: Helpers - Converters para XAML

Los converters transforman datos en XAML (ej: true → "Visible", false → "Hidden")

```csharp
// Helpers/Converters/BoolToInverseConverter.cs
using System.Globalization;

namespace Phanteon.Helpers.Converters
{
    /// <summary>
    /// Invierte un valor booleano (true → false, false → true)
    /// Útil para deshabilitar botones mientras carga
    /// </summary>
    public class BoolToInverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;

            return false;
        }
    }
}
```

```csharp
// Helpers/Converters/StringNotEmptyConverter.cs
using System.Globalization;

namespace Phanteon.Helpers.Converters
{
    /// <summary>
    /// Devuelve true si el string NO está vacío
    /// Útil para mostrar mensajes de error solo cuando existen
    /// </summary>
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                return !string.IsNullOrWhiteSpace(str);

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
```

**Registrar converters en App.xaml:**

```xml
<!-- App.xaml -->
<?xml version="1.0" encoding="UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:converters="clr-namespace:Phanteon.Helpers.Converters"
             x:Class="Phanteon.App">
    <Application.Resources>
        <ResourceDictionary>
            <!-- Registrar Converters -->
            <converters:BoolToInverseConverter x:Key="InvertedBoolConverter"/>
            <converters:StringNotEmptyConverter x:Key="StringNotEmptyConverter"/>

            <!-- Colores -->
            <Color x:Key="Primary">#512BD4</Color>
            <Color x:Key="Secondary">#DFD8F7</Color>
            <Color x:Key="Tertiary">#2B0B98</Color>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

---

<<<<<<< HEAD
### 📱 Ejemplo 4: Servicio completo con manejo de errores

```csharp
// Services/Implementations/AuthService.cs
using Phanteon.Models;
using Phanteon.Services.Interfaces;
using Phanteon.Services.Api;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Networking;
using CommunityToolkit.Maui.Alerts;

namespace Phanteon.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IAuthApi _authApi;

        public AuthService(IAuthApi authApi)
        {
            _authApi = authApi;
        }

        public async Task<Usuario> IniciarSesion(string usuario, string contraseña)
        {
            // 1. Verificar conectividad
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                throw new Exception("No hay conexión a internet");
            }

            try
            {
                // 2. Preparar request
                var request = new LoginRequest
                {
                    Usuario = usuario,
                    Contraseña = contraseña
                };

                // 3. Llamar a la API (Polly manejará reintentos automáticamente)
                var usuarioAutenticado = await _authApi.Login(request);

                // 4. Guardar token de forma segura
                await SecureStorage.SetAsync("auth_token", usuarioAutenticado.Token);
                await SecureStorage.SetAsync("user_id", usuarioAutenticado.Id.ToString());
                await SecureStorage.SetAsync("username", usuarioAutenticado.NombreUsuario);

                return usuarioAutenticado;
            }
            catch (Refit.ApiException apiEx)
            {
                // Errores de la API (401, 404, 500, etc.)
                if (apiEx.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Usuario o contraseña incorrectos");
                }
                else if (apiEx.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Error en el servidor. Intenta más tarde");
                }
                else
                {
                    throw new Exception($"Error de API: {apiEx.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Otros errores (red, timeout, etc.)
                Console.WriteLine($"[AuthService] Error: {ex.Message}");
                throw new Exception("Error al iniciar sesión. Verifica tu conexión");
            }
        }

        public async Task CerrarSesion()
        {
            // Limpiar todo el almacenamiento seguro
            SecureStorage.Remove("auth_token");
            SecureStorage.Remove("user_id");
            SecureStorage.Remove("username");

            await Toast.Make("Sesión cerrada correctamente").Show();
        }

        public async Task<bool> EstaAutenticado()
        {
            try
            {
                var token = await SecureStorage.GetAsync("auth_token");
                return !string.IsNullOrEmpty(token);
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> ObtenerTokenActual()
        {
            return await SecureStorage.GetAsync("auth_token") ?? string.Empty;
        }
    }
}
```

---

## 🐛 Solución de Problemas

### ❌ Error: "No se puede resolver el servicio IProductoService"

**Causa:** No registraste el servicio en `MauiProgram.cs`

**Solución:**
```csharp
// En MauiProgram.cs
builder.Services.AddSingleton<IProductoService, ProductoService>();
```

---

### ❌ Error: "Namespace 'Phanteon' not found"

**Causa:** El namespace no coincide con la estructura de carpetas

**Solución:**
1. Verifica que el namespace en el archivo coincida con la carpeta
2. Ejemplo: Si el archivo está en `ViewModels/`, el namespace debe ser `Phanteon.ViewModels`

```csharp
// Correcto ✅
namespace Phanteon.ViewModels
{
    public class LoginViewModel { }
}

// Incorrecto ❌
namespace Phanteon
{
    public class LoginViewModel { }
}
```

---

### ❌ Error: ObservableProperty no genera la propiedad pública

**Causa:** La clase no es `partial` o no hereda de `ObservableObject`

**Solución:**
```csharp
// Correcto ✅
public partial class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string usuario;
}

// Incorrecto ❌ (falta 'partial')
public class LoginViewModel : ObservableObject
{
    [ObservableProperty]
    private string usuario;
}
```

---

### ❌ Error XA0129 (Android): "Error al implementar archivos `.__override__`"

**Causa:** Problema con compresión de ensamblados en Android

**Solución:** Agregar en `Phanteon.csproj`:
```xml
<PropertyGroup Condition="'$(TargetFramework)' == 'net9.0-android'">
    <AndroidUseAssemblyStore>false</AndroidUseAssemblyStore>
    <AndroidEnableAssemblyCompression>false</AndroidEnableAssemblyCompression>
</PropertyGroup>
```

---

### ❌ SecureStorage lanza excepción en Android

**Causa:** Permisos faltantes o emulador sin cifrado

**Solución 1:** Agregar permisos en `Platforms/Android/AndroidManifest.xml`
```xml
<uses-permission android:name="android.permission.INTERNET" />
```

**Solución 2:** Manejar con try-catch
```csharp
try
{
    await SecureStorage.SetAsync("token", "ABC123");
}
catch (Exception ex)
{
    Console.WriteLine($"SecureStorage error: {ex.Message}");
    // Usar Preferences como fallback
    Preferences.Set("token", "ABC123");
}
```

---

### ❌ Error: "Refit.ApiException: Connection refused"

**Causa:** La API no está disponible o la URL es incorrecta

**Solución:**
1. Verifica que la API esté corriendo
2. En Android, usa `10.0.2.2` en lugar de `localhost`:
```csharp
// Para Android (emulador)
#if ANDROID
    c.BaseAddress = new Uri("http://10.0.2.2:5000/api");
#else
    c.BaseAddress = new Uri("http://localhost:5000/api");
#endif
```

---

### ❌ Error: "Binding context is null"

**Causa:** No asignaste el ViewModel al `BindingContext` de la página

**Solución:**
```csharp
// En el code-behind de la página
public ProductosPage(ProductosViewModel viewModel)
{
    InitializeComponent();
    BindingContext = viewModel; // ← ¡Importante!
}
```

---

### ❌ La app no actualiza cuando cambio una propiedad

**Causa:** La propiedad no es `[ObservableProperty]` o falta `OnPropertyChanged()`

**Solución con Toolkit (recomendado):**
```csharp
[ObservableProperty]
private string nombre; // ← Genera notificaciones automáticamente
```

**Solución manual (no recomendado):**
```csharp
private string _nombre;
public string Nombre
{
    get => _nombre;
    set
    {
        if (_nombre != value)
        {
            _nombre = value;
            OnPropertyChanged(); // ← Notificar cambio
        }
    }
}
```

---

### 🔍 Comandos útiles para debugging

```bash
# Ver logs detallados de compilación
dotnet build -v detailed

# Limpiar y reconstruir (soluciona el 80% de problemas)
dotnet clean && dotnet build

# Ver paquetes instalados
dotnet list package

# Restaurar paquetes NuGet
dotnet restore

# Ver errores de Refit/Source Generators
dotnet build /p:EmitCompilerGeneratedFiles=true
```

---

## 📚 Referencias y Recursos de Aprendizaje

### 📖 Documentación Oficial

| Recurso | Descripción | Link |
|---------|-------------|------|
| **.NET MAUI** | Documentación oficial de MAUI | [Docs](https://learn.microsoft.com/dotnet/maui/) |
| **CommunityToolkit.Mvvm** | Guía del MVVM Toolkit | [Docs](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) |
| **Refit** | Cliente HTTP declarativo | [GitHub](https://github.com/reactiveui/refit) |
| **Polly** | Resiliencia y reintentos | [GitHub](https://github.com/App-vNext/Polly) |
| **SecureStorage** | Almacenamiento seguro | [Docs](https://learn.microsoft.com/dotnet/maui/platform-integration/storage/secure-storage) |

### 🎓 Tutoriales y Cursos (Gratuitos)

1. **Microsoft Learn - .NET MAUI**
   - [Crear tu primera app MAUI](https://learn.microsoft.com/training/modules/build-mobile-and-desktop-apps/)
   - [MVVM en MAUI](https://learn.microsoft.com/training/modules/use-mvvm-pattern-xamarin-forms/)
   - [Consumir APIs REST](https://learn.microsoft.com/training/modules/consume-rest-services/)

2. **YouTube - Canales recomendados**
   - **James Montemagno** (Desarrollador de Microsoft MAUI)
   - **.NET** (Canal oficial)
   - **Coding Droplets** (Tutoriales en español)

3. **Blogs y Artículos**
   - [Blog oficial de .NET](https://devblogs.microsoft.com/dotnet/)
   - [MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)

### 🛠️ Herramientas útiles

| Herramienta | ¿Para qué sirve? | Link |
|-------------|------------------|------|
| **Visual Studio 2022** | IDE principal para MAUI | [Descargar](https://visualstudio.microsoft.com/) |
| **Postman** | Probar APIs REST | [Descargar](https://www.postman.com/) |
| **DB Browser SQLite** | Ver bases de datos SQLite | [Descargar](https://sqlitebrowser.org/) |
| **Git** | Control de versiones | [Descargar](https://git-scm.com/) |
| **GitHub Desktop** | Git visual (más fácil) | [Descargar](https://desktop.github.com/) |

---

## ✅ Checklist de Calidad del Proyecto

Usa esta lista para verificar que tu proyecto cumple con las buenas prácticas:

### 📋 Código

- [ ] El proyecto compila sin errores ni warnings
- [ ] Todos los paquetes NuGet están actualizados
- [ ] No hay código comentado (elimínalo)
- [ ] Los nombres de variables están en español (según el estándar del proyecto)
- [ ] Los métodos públicos tienen comentarios XML `///`

### 🏗️ Arquitectura

- [ ] Se sigue el patrón MVVM correctamente
- [ ] Las Views solo tienen XAML (mínimo code-behind)
- [ ] Los ViewModels usan `[ObservableProperty]` y `[RelayCommand]`
- [ ] Los Services están en carpetas Interfaces/ e Implementations/
- [ ] Todos los servicios están registrados en `MauiProgram.cs`
- [ ] Se usa inyección de dependencias (no `new Service()`)

### 🔒 Seguridad

- [ ] Las contraseñas se guardan en `SecureStorage`, no en `Preferences`
- [ ] No hay credenciales hardcodeadas en el código
- [ ] Los tokens de API se almacenan de forma segura
- [ ] Se valida entrada del usuario antes de enviar a la API

### 🎨 Interfaz

- [ ] La app se ve bien en diferentes tamaños de pantalla
- [ ] Hay indicadores de carga (`ActivityIndicator`) en operaciones largas
- [ ] Los botones se deshabilitan mientras se procesa
- [ ] Hay mensajes de error claros para el usuario
- [ ] La navegación funciona correctamente

### 🧪 Manejo de Errores

- [ ] Todos los métodos async tienen `try-catch`
- [ ] Se muestra un mensaje amigable al usuario en caso de error
- [ ] Los errores se loguean en consola para debugging
- [ ] Se maneja la falta de conexión a internet

### 📝 Documentación

- [ ] El README está actualizado
- [ ] Se documentaron los servicios y sus métodos
- [ ] Hay comentarios explicando lógica compleja
- [ ] Se incluyeron los nombres de todos los integrantes del equipo

---

## 🎯 Consejos para el Trabajo en Equipo

### 🔀 Usando Git correctamente

```bash
# 1. Antes de empezar a trabajar, actualiza tu rama
git pull origin master

# 2. Crea una rama para tu feature
git checkout -b feature/login-page

# 3. Haz commits frecuentes con mensajes descriptivos
git add .
git commit -m "Agregar página de login con validación"

# 4. Sube tus cambios
git push origin feature/login-page

# 5. Crea un Pull Request en GitHub para revisión
```

### 📋 División de tareas recomendada

| Rol | Responsabilidades | Archivos principales |
|-----|-------------------|---------------------|
| **Desarrollador 1** | Models y Services | `Models/`, `Services/` |
| **Desarrollador 2** | ViewModels | `ViewModels/` |
| **Desarrollador 3** | Views (XAML) | `Views/`, `Resources/` |
| **Desarrollador 4** | Testing y documentación | `Tests/`, `README.md` |

> 💡 **Tip**: Trabajen en archivos diferentes para evitar conflictos en Git

### 🚫 Errores comunes a evitar

1. **No hacer commits gigantes**: Haz commits pequeños y frecuentes
2. **No comentar código**: Elimínalo, Git guarda el historial
3. **No hardcodear valores**: Usa configuración o constantes
4. **No mezclar español e inglés**: Elige uno y sé consistente
5. **No ignorar warnings**: Siempre arregla los warnings

---

## 🎓 Glosario de Términos

Para que todos en el equipo hablen el mismo idioma:

| Término | Significado | Ejemplo |
|---------|-------------|---------|
| **API** | Application Programming Interface - Servicio web que devuelve datos | La API de usuarios devuelve una lista de usuarios |
| **DTO** | Data Transfer Object - Objeto para transferir datos | `LoginDto` con usuario y contraseña |
| **Binding** | Conexión automática entre Vista y ViewModel | `Text="{Binding Usuario}"` |
| **Command** | Acción que ejecuta un botón | `Command="{Binding LoginCommand}"` |
| **Observable** | Propiedad que notifica cuando cambia | `[ObservableProperty] string nombre` |
| **Dependency Injection** | El sistema provee las dependencias automáticamente | Constructor recibe `IAuthService` |
| **Async/Await** | Código asíncrono (no bloquea la UI) | `await _api.Login()` |
| **Token** | Código secreto para autenticación | "ABC123XYZ789" guardado en SecureStorage |
| **Endpoint** | URL específica de una API | `/api/usuarios` |
| **JSON** | Formato de datos en texto | `{"id":1,"nombre":"Juan"}` |

---

## 🚀 Próximos Pasos (Después de completar lo básico)

1. **Implementar base de datos local**
   - Usar SQLite para datos offline
   - Sincronizar con la API cuando hay internet

2. **Agregar autenticación biométrica**
   - Huella dactilar / Face ID
   - Usar MAUI Essentials

3. **Implementar notificaciones push**
   - Firebase Cloud Messaging
   - Notificaciones locales

4. **Mejorar la UI**
   - Animaciones con Community Toolkit
   - Temas claro/oscuro
   - Personalización

5. **Testing**
   - Unit Tests para ViewModels
   - Integration Tests para Services

---

## 📊 Criterios de Evaluación (Ejemplo)

| Criterio | Puntos | Qué evaluar |
|----------|--------|-------------|
| **Arquitectura MVVM** | 25% | Separación correcta de capas |
| **Funcionalidad** | 30% | La app funciona sin errores |
| **Código limpio** | 20% | Nombres claros, sin código repetido |
| **Documentación** | 15% | README completo y comentarios |
| **Presentación** | 10% | Demostración del proyecto |

---

=======
>>>>>>> 25ca9c43c61af9e74cf038a6dee521a1c6d1cde6
## 📄 Licencia

Este proyecto es de uso educativo para fines académicos.

---

## 🤝 Contribuciones

Si encuentras un error o quieres mejorar algo:

1. Haz un fork del repositorio
2. Crea una rama para tu mejora
3. Haz un commit con tus cambios
4. Abre un Pull Request

---

## 📞 Contacto y Soporte

**Dudas o problemas con el proyecto:**
- Consultar con el equipo en reuniones semanales
- Revisar la sección de [Solución de Problemas](#-solución-de-problemas)
- Buscar en [Stack Overflow](https://stackoverflow.com/questions/tagged/.net-maui)

---

## 🏆 Reconocimientos

- **Microsoft** por .NET MAUI
- **Comunidad .NET** por las herramientas open-source
- **Nuestro profesor** por la guía y apoyo

---

**📅 Última actualización:** Octubre 2025
**📌 Versión del proyecto:** 1.0.0
**🎓 Curso:** _[Nombre del curso]_
**🏫 Universidad:** _[Nombre de la universidad]_

---

<div align="center">

### ⭐ Si este README te ayudó, dale una estrella al repositorio

**¡Hecho con dedicación por el equipo Phanteon!** 🚀

</div>
