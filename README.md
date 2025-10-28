# 📱 Phanteon - Proyecto .NET MAUI

> Aplicación multiplataforma desarrollada con .NET 9 y MAUI  
> Arquitectura limpia | MVVM | Inyección de Dependencias | APIs Seguras

---

## 👥 Integrantes del Equipo

| Nombre Completo | Código |
|----------------|---------|
| Héctor Eduardo Véliz Girón | 000108304 | Desarrollador Principal |
| | |
| | |
| | |

**Fecha de Entrega:** _____/_____/_____  
**Docente:** _________________________________  
**Curso:** _________________________________

---

## 📑 Índice de Contenidos

1. [Descripción General](#-descripción-general)
2. [Requisitos del Sistema](#-requisitos-del-sistema)
3. [Tecnologías Utilizadas](#-tecnologías-utilizadas)
4. [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
5. [Estructura de Carpetas](#-estructura-de-carpetas)
6. [Configuración Inicial](#-configuración-inicial)
7. [Patrones de Diseño](#-patrones-de-diseño)
8. [Ejemplos de Código](#-ejemplos-de-código)
9. [Guía de Uso](#-guía-de-uso)
10. [Solución de Problemas](#-solución-de-problemas)
11. [Referencias](#-referencias)

---

## 🎯 Descripción General

### Objetivo del Proyecto
Desarrollar una aplicación multiplataforma que implemente arquitectura limpia, patrones MVVM, inyección de dependencias y consumo seguro de APIs REST, siguiendo los principios SOLID y las mejores prácticas de desarrollo móvil.

### Características Principales
- ✅ **Arquitectura MVVM** con separación de responsabilidades
- ✅ **Inyección de dependencias** nativa de .NET
- ✅ **Consumo de APIs REST** con Refit
- ✅ **Almacenamiento seguro** de credenciales
- ✅ **Manejo de errores resiliente** con Polly

### Plataformas Soportadas
- 📱 **Android** (API 21+)
- 🍎 **iOS** (14.0+)
- 💻 **Windows** (10.0.17763+)
- 🍏 **macOS** (10.15+)

---

## 💻 Requisitos del Sistema

### Software Necesario

| Herramienta | Versión Mínima | Propósito |
|-------------|----------------|-----------|
| Visual Studio 2022 | 17.8+ | IDE principal |
| .NET SDK | 9.0+ | Framework de desarrollo |
| Android SDK | API 34 | Desarrollo Android |
| Xcode | 15.0+ (macOS) | Desarrollo iOS |

### Instalación de Cargas de Trabajo
```bash
# Verificar instalación de .NET MAUI
dotnet workload list

# Instalar carga de trabajo MAUI si no está presente
dotnet workload install maui
```

---

## 🛠️ Tecnologías Utilizadas

### Paquetes NuGet Principales

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| **CommunityToolkit.Mvvm** | 8.4.0 | Implementación simplificada de MVVM con source generators |
| **Refit** | 8.0.0 | Cliente HTTP declarativo para APIs REST |
| **Newtonsoft.Json** | 13.0.4 | Serialización/deserialización JSON flexible |
| **Polly** | 8.6.4 | Políticas de resiliencia y manejo de fallos |
| **Microsoft.Maui.Essentials** | Incluido | APIs nativas (SecureStorage, Connectivity, etc.) |

### Propósito de Cada Tecnología

#### 1. CommunityToolkit.Mvvm
**¿Qué problema resuelve?**  
Elimina el código repetitivo (boilerplate) en ViewModels mediante source generators.
```csharp
// ❌ Antes: Código manual extenso
private string nombre;
public string Nombre 
{ 
    get => nombre; 
    set 
    { 
        if (nombre != value)
        {
            nombre = value;
            OnPropertyChanged();
        }
    }
}

// ✅ Ahora: Generación automática
[ObservableProperty]
private string nombre;
```

#### 2. SecureStorage (MAUI Essentials)
**¿Qué problema resuelve?**  
Almacena información sensible (tokens, contraseñas) de forma segura usando APIs nativas:
- **Android:** EncryptedSharedPreferences
- **iOS:** Keychain
- **Windows:** Data Protection API
```csharp
// Guardar token de autenticación de forma segura
await SecureStorage.SetAsync("auth_token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...");

// Recuperar token
string token = await SecureStorage.GetAsync("auth_token");

// Eliminar credenciales
SecureStorage.Remove("auth_token");
```

#### 3. Refit
**¿Qué problema resuelve?**  
Convierte interfaces en clientes HTTP automáticamente, reduciendo el código manual.
```csharp
// ✅ Definición simple de API
public interface IApiUsuarios
{
    [Get("/usuarios/{id}")]
    Task ObtenerUsuario(int id);
    
    [Post("/usuarios")]
    Task CrearUsuario([Body] Usuario usuario);
}

// ❌ Sin Refit necesitarías escribir todo esto:
// var httpClient = new HttpClient();
// var response = await httpClient.GetAsync($"https://api.com/usuarios/{id}");
// var content = await response.Content.ReadAsStringAsync();
// var usuario = JsonConvert.DeserializeObject(content);
```

#### 4. Polly
**¿Qué problema resuelve?**  
Implementa políticas de resiliencia para manejar fallos transitorios en llamadas HTTP.
```csharp
// Política de reintentos con backoff exponencial
var politicaReintentos = Policy
    .Handle()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: intentos => TimeSpan.FromSeconds(Math.Pow(2, intentos)),
        onRetry: (excepcion, tiempoEspera, intento, contexto) =>
        {
            Console.WriteLine($"Reintento {intento} después de {tiempoEspera.TotalSeconds}s");
        });
```

---

## 🏗️ Arquitectura del Proyecto

### Diagrama de Capas
```
┌─────────────────────────────────────────┐
│           CAPA DE PRESENTACIÓN          │
│  (Views + ViewModels + XAML)            │
│  - MainPage.xaml                        │
│  - MainViewModel.cs                     │
└─────────────┬───────────────────────────┘
              │ Binding & Commands
              ▼
┌─────────────────────────────────────────┐
│        CAPA DE LÓGICA DE NEGOCIO        │
│  (Services + Interfaces)                │
│  - IApiService, IAutenticacionService   │
│  - ApiService, AutenticacionService     │
└─────────────┬───────────────────────────┘
              │ HTTP/REST
              ▼
┌─────────────────────────────────────────┐
│           CAPA DE DATOS                 │
│  (Models + DTOs + Repositorios)         │
│  - Usuario.cs, Producto.cs              │
│  - SecureStorage (persistencia local)   │
└─────────────────────────────────────────┘
```

### Principios SOLID Aplicados

| Principio | Implementación en Phanteon |
|-----------|----------------------------|
| **S** - Single Responsibility | Cada clase tiene una única responsabilidad: ViewModels solo manejan UI, Services solo consumen APIs |
| **O** - Open/Closed | Extensible mediante herencia/interfaces sin modificar código existente |
| **L** - Liskov Substitution | Interfaces pueden ser sustituidas por mocks en testing sin romper la lógica |
| **I** - Interface Segregation | Interfaces pequeñas y específicas (`IApiUsuarios`, `IApiProductos`) en lugar de una grande |
| **D** - Dependency Inversion | Dependemos de abstracciones (`IApiService`) no de implementaciones concretas |

---

## 📂 Estructura de Carpetas
```
Phanteon/
├── Views/                          # Páginas XAML y code-behind
│   ├── MainPage.xaml
│   ├── MainPage.xaml.cs
│   ├── LoginPage.xaml
│   └── LoginPage.xaml.cs
│
├── ViewModels/                     # Lógica de presentación
│   ├── MainViewModel.cs
│   ├── LoginViewModel.cs
│   └── BaseViewModel.cs
│
├── Models/                         # DTOs y modelos de dominio
│   ├── Usuario.cs
│   ├── Producto.cs
│   └── RespuestaApi.cs
│
├── Services/
│   ├── Interfaces/                 # Contratos de servicios
│   │   ├── IApiService.cs
│   │   ├── IAutenticacionService.cs
│   │   └── IAlmacenamientoSeguro.cs
│   │
│   └── Implementations/            # Implementaciones concretas
│       ├── ApiService.cs
│       ├── AutenticacionService.cs
│       └── AlmacenamientoSeguroService.cs
│
├── Helpers/                        # Utilidades y extensiones
│   ├── Converters/
│   │   └── BoolToColorConverter.cs
│   ├── Extensions/
│   │   └── StringExtensions.cs
│   └── Policies/
│       └── PoliticasHttp.cs
│
├── Resources/
│   ├── Styles/                     # Estilos XAML
│   │   ├── Colors.xaml
│   │   └── Styles.xaml
│   ├── Images/                     # Recursos gráficos
│   └── Fonts/                      # Tipografías personalizadas
│
├── Scripts/                        # Scripts de automatización
│   └── create_folders.ps1
│
└── Tests/                          # Pruebas unitarias
    ├── ViewModelTests/
    └── ServiceTests/
```

### Responsabilidades de Cada Carpeta

#### Views
- **Contenido:** Archivos XAML y *code-behind* mínimo  
- **Regla:** **NO** debe contener lógica de negocio  
- **Ejemplo:** Solo *bindings* a propiedades del ViewModel  

```xml
<ContentPage x:DataType="vm:MainViewModel">
    <StackLayout>
        <Label Text="{Binding Titulo}" FontSize="24" />
        <Button Text="Cargar" Command="{Binding CargarCommand}" />
    </StackLayout>
</ContentPage>
```

#### 🎮 ViewModels
- **Contenido:** Lógica de presentación, comandos, propiedades observables
- **Regla:** Inyectar servicios por constructor (Dependency Injection)
- **Herramientas:** Usar `[ObservableProperty]` y `[RelayCommand]`

#### 📦 Models
- **Contenido:** Tipos simples, DTOs, entidades
- **Regla:** Solo propiedades, sin lógica compleja
- **Uso:** Transferencia de datos entre capas

#### 🔧 Services
- **Interfaces:** Contratos que definen qué hace el servicio
- **Implementations:** Lógica real, orquestación de Refit/Polly/SecureStorage

#### 🛠️ Helpers
- **Converters:** Conversión de datos para binding XAML
- **Extensions:** Métodos de extensión para tipos existentes
- **Policies:** Políticas de Polly centralizadas

---

## ⚙️ Configuración Inicial

### 1. Instalación de Paquetes NuGet
```bash
# Navegar a la carpeta del proyecto
cd Phanteon

# Instalar CommunityToolkit.Mvvm para MVVM simplificado
dotnet add package CommunityToolkit.Mvvm --version 8.4.0

# Instalar Refit para consumo de APIs
dotnet add package Refit --version 8.0.0

# Instalar Newtonsoft.Json para serialización
dotnet add package Newtonsoft.Json --version 13.0.4

# Instalar Polly para resiliencia
dotnet add package Polly --version 8.6.4
```

**Nota:** `SecureStorage` ya viene incluido en `Microsoft.Maui.Essentials`, no requiere instalación adicional.

### 2. Configuración de MauiProgram.cs
```csharp
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Refit;
using Polly;
using Polly.Extensions.Http;
using Phanteon.Services.Interfaces;
using Phanteon.Services.Implementations;
using Phanteon.ViewModels;
using Phanteon.Views;

namespace Phanteon;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ========================================
        // CONFIGURACIÓN DE SERVICIOS
        // ========================================

        // 1. Servicios de almacenamiento seguro
        builder.Services.AddSingleton();

        // 2. Política de reintentos con Polly
        var politicaReintentos = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: intentos => TimeSpan.FromSeconds(Math.Pow(2, intentos)),
                onRetry: (resultado, tiempoEspera, intento, contexto) =>
                {
                    Console.WriteLine($"Reintento {intento} después de {tiempoEspera.TotalSeconds}s");
                });

        // 3. Configuración de Refit con políticas de Polly
        builder.Services
            .AddRefitClient(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.ejemplo.com"))
            .AddPolicyHandler(politicaReintentos);

        // 4. Otros servicios
        builder.Services.AddSingleton();

        // ========================================
        // REGISTRO DE VIEWMODELS
        // ========================================
        builder.Services.AddTransient();
        builder.Services.AddTransient();

        // ========================================
        // REGISTRO DE VIEWS
        // ========================================
        builder.Services.AddTransient();
        builder.Services.AddTransient();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
```

### 3. Crear Script de Carpetas (PowerShell)
```powershell
# Scripts/create_folders.ps1

# Script para crear la estructura de carpetas del proyecto Phanteon
param(
    [string]$rutaBase = "."
)

Write-Host "🚀 Creando estructura de carpetas para Phanteon..." -ForegroundColor Cyan

# Definir carpetas a crear
$carpetas = @(
    "Views",
    "ViewModels",
    "Models",
    "Services/Interfaces",
    "Services/Implementations",
    "Helpers/Converters",
    "Helpers/Extensions",
    "Helpers/Policies",
    "Resources/Styles",
    "Resources/Images",
    "Resources/Fonts",
    "Tests/ViewModelTests",
    "Tests/ServiceTests"
)

# Crear cada carpeta
foreach ($carpeta in $carpetas) {
    $rutaCompleta = Join-Path $rutaBase $carpeta
    
    if (-not (Test-Path $rutaCompleta)) {
        New-Item -Path $rutaCompleta -ItemType Directory -Force | Out-Null
        
        # Crear archivo .gitkeep para mantener la carpeta en git
        $archivoGitkeep = Join-Path $rutaCompleta ".gitkeep"
        New-Item -Path $archivoGitkeep -ItemType File -Force | Out-Null
        
        Write-Host "✅ Creada: $carpeta" -ForegroundColor Green
    }
    else {
        Write-Host "⚠️  Ya existe: $carpeta" -ForegroundColor Yellow
    }
}

Write-Host ""
Write-Host "✨ Estructura de carpetas creada exitosamente" -ForegroundColor Green
```

**Ejecución:**
```powershell
# Desde la raíz del proyecto
.\Scripts\create_folders.ps1
```

---

## 🎨 Patrones de Diseño

### 1. Patrón MVVM (Model-View-ViewModel)

#### Flujo de Comunicación
```
┌──────────┐         ┌──────────────┐         ┌─────────┐
│   View   │◄────────│  ViewModel   │◄────────│  Model  │
│  (XAML)  │ Binding │   (Lógica)   │ Consume │  (DTO)  │
└──────────┘         └──────────────┘         └─────────┘
     │                      │
     │ Commands             │ INotifyPropertyChanged
     └──────────────────────┘
```

#### Implementación Práctica

**Model (Models/Usuario.cs):**
```csharp
namespace Phanteon.Models;

/// 
/// Modelo que representa un usuario del sistema
/// 
public class Usuario
{
    /// 
    /// Identificador único del usuario
    /// 
    public int Id { get; set; }
    
    /// 
    /// Nombre completo del usuario
    /// 
    public string NombreCompleto { get; set; }
    
    /// 
    /// Correo electrónico del usuario
    /// 
    public string CorreoElectronico { get; set; }
    
    /// 
    /// Número de teléfono del usuario
    /// 
    public string Telefono { get; set; }
    
    /// 
    /// Fecha de registro del usuario en el sistema
    /// 
    public DateTime FechaRegistro { get; set; }
    
    /// 
    /// Indica si el usuario está activo
    /// 
    public bool EstaActivo { get; set; }
}
```

**ViewModel (ViewModels/MainViewModel.cs):**
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Interfaces;
using Phanteon.Models;
using System.Collections.ObjectModel;

namespace Phanteon.ViewModels;

/// 
/// ViewModel principal de la aplicación
/// Maneja la lógica de presentación de la página principal
/// 
public partial class MainViewModel : ObservableObject
{
    // ========================================
    // INYECCIÓN DE DEPENDENCIAS
    // ========================================
    private readonly IApiService _apiService;
    private readonly ILogger _logger;

    /// 
    /// Constructor que recibe las dependencias necesarias
    /// 
    public MainViewModel(IApiService apiService, ILogger logger)
    {
        _apiService = apiService;
        _logger = logger;
        
        // Inicializar colecciones
        Usuarios = new ObservableCollection();
    }

    // ========================================
    // PROPIEDADES OBSERVABLES
    // ========================================
    
    /// 
    /// Título de la página principal
    /// 
    [ObservableProperty]
    private string titulo = "Lista de Usuarios";

    /// 
    /// Mensaje de bienvenida dinámico
    /// 
    [ObservableProperty]
    private string mensajeBienvenida = "Bienvenido a Phanteon";

    /// 
    /// Indica si se están cargando datos
    /// 
    [ObservableProperty]
    private bool estaCargando;

    /// 
    /// Colección de usuarios para mostrar en la UI
    /// 
    public ObservableCollection Usuarios { get; }

    // ========================================
    // COMANDOS
    // ========================================
    
    /// 
    /// Comando para cargar la lista de usuarios desde la API
    /// 
    [RelayCommand]
    private async Task CargarUsuariosAsync()
    {
        try
        {
            EstaCargando = true;
            _logger.LogInformation("Iniciando carga de usuarios...");

            // Llamada a la API mediante el servicio
            var usuarios = await _apiService.ObtenerUsuariosAsync();

            // Limpiar lista actual
            Usuarios.Clear();

            // Agregar nuevos usuarios a la colección observable
            foreach (var usuario in usuarios)
            {
                Usuarios.Add(usuario);
            }

            MensajeBienvenida = $"Se cargaron {usuarios.Count} usuarios exitosamente";
            _logger.LogInformation($"Usuarios cargados: {usuarios.Count}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar usuarios");
            MensajeBienvenida = "❌ Error al cargar los datos. Intenta nuevamente.";
        }
        finally
        {
            EstaCargando = false;
        }
    }

    /// 
    /// Comando para seleccionar un usuario
    /// 
    [RelayCommand]
    private async Task SeleccionarUsuario(Usuario usuario)
    {
        if (usuario == null) return;

        _logger.LogInformation($"Usuario seleccionado: {usuario.NombreCompleto}");
        
        // Navegar a página de detalles (ejemplo)
        // await Shell.Current.GoToAsync($"detalles?usuarioId={usuario.Id}");
    }
}
```

<!-- Views/MainPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:Phanteon.ViewModels"
             x:Class="Phanteon.Views.MainPage"
             x:DataType="vm:MainViewModel"
             Title="{Binding Titulo}"
             BackgroundColor="{StaticResource BackgroundColor}">

    <ScrollView>
        <VerticalStackLayout Spacing="20" Padding="20">

            <!-- Título de la página -->
            <Label Text="{Binding Titulo}"
                   FontSize="28"
                   FontAttributes="Bold"
                   HorizontalOptions="Center"
                   TextColor="{StaticResource PrimaryTextColor}" />

            <!-- Indicador de carga -->
            <ActivityIndicator IsRunning="{Binding EstaCargando}"
                               Color="{StaticResource AccentColor}"
                               IsVisible="{Binding EstaCargando}" />

            <!-- Lista de usuarios -->
            <CollectionView ItemsSource="{Binding Usuarios}"
                            IsVisible="{Binding !EstaCargando}">
                <CollectionView.ItemTemplate>
                    <DataTemplate>
                        <Grid Padding="12" ColumnDefinitions="*,Auto">
                            <!-- Nombre del usuario -->
                            <VerticalStackLayout Grid.Column="0">
                                <Label Text="{Binding NombreCompleto}"
                                       FontAttributes="Bold"
                                       FontSize="16" />
                                <Label Text="{Binding CorreoElectronico}"
                                       FontSize="14"
                                       TextColor="{StaticResource SecondaryTextColor}" />
                            </VerticalStackLayout>

                            <!-- Estado activo -->
                            <Frame Grid.Column="1"
                                   BackgroundColor="{Binding EstaActivo, Converter={StaticResource BoolToColorConverter}}"
                                   CornerRadius="8"
                                   Padding="8"
                                   HasShadow="False">
                                <Label Text="{Binding EstaActivo, Converter={StaticResource BoolToStatusConverter}}"
                                       TextColor="White"
                                       FontSize="12"
                                       FontAttributes="Bold" />
                            </Frame>
                        </Grid>
                    </DataTemplate>
                </CollectionView.ItemTemplate>
            </CollectionView>

            <!-- Botón de acción -->
            <Button Text="Cargar Usuarios"
                    Command="{Binding CargarUsuariosAsyncCommand}"
                    BackgroundColor="{StaticResource PrimaryColor}"
                    TextColor="White"
                    CornerRadius="12"
                    FontAttributes="Bold"
                    HorizontalOptions="Center"
                    Padding="20,12" />

        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

**Code-Behind (Views/MainPage.xaml.cs):**
```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

/// 
/// Página principal de la aplicación
/// 
public partial class MainPage : ContentPage
{
    /// 
    /// Constructor que recibe el ViewModel por inyección de dependencias
    /// 
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        
        // Asignar el ViewModel recibido por DI
        BindingContext = viewModel;
    }
}
```

### 2. Patrón Repository con Refit

#### Interface de API (Services/Interfaces/IApiService.cs):
```csharp
using Refit;
using Phanteon.Models;

namespace Phanteon.Services.Interfaces;

/// 
/// Interfaz que define los endpoints de la API REST
/// Refit genera automáticamente la implementación
/// 
public interface IApiService
{
    /// 
    /// Obtiene la lista completa de usuarios
    /// 
    /// Lista de usuarios
    [Get("/api/usuarios")]
    Task<List> ObtenerUsuariosAsync();

    /// 
    /// Obtiene un usuario específico por su ID
    /// 
    /// Identificador del usuario
    [Get("/api/usuarios/{id}")]
    Task ObtenerUsuarioPorIdAsync(int id);

    /// 
    /// Crea un nuevo usuario
    /// 
    /// Datos del usuario a crear
    [Post("/api/usuarios")]
    Task CrearUsuarioAsync([Body] Usuario usuario);

    /// 
    /// Actualiza un usuario existente
    /// 
    /// ID del usuario a actualizar
    /// Nuevos datos del usuario
    [Put("/api/usuarios/{id}")]
    Task ActualizarUsuarioAsync(int id, [Body] Usuario usuario);

    /// 
    /// Elimina un usuario
    /// 
    /// ID del usuario a eliminar
    [Delete("/api/usuarios/{id}")]
    Task EliminarUsuarioAsync(int id);

    /// 
    /// Busca usuarios por nombre
    /// 
    /// Término de búsqueda
    [Get("/api/usuarios/buscar")]
    Task<List> BuscarUsuariosAsync([Query] string termino);
}
```

### 3. Patrón Adapter para SecureStorage

#### Interface (Services/Interfaces/IAlmacenamientoSeguro.cs):
```csharp
namespace Phanteon.Services.Interfaces;

/// 
/// Abstracción del almacenamiento seguro para facilitar testing
/// 
public interface IAlmacenamientoSeguro
{
    /// 
    /// Guarda un valor de forma segura
    /// 
    Task GuardarAsync(string clave, string valor);

    /// 
    /// Obtiene un valor almacenado de forma segura
    /// 
    Task ObtenerAsync(string clave);

    /// 
    /// Elimina un valor específico
    /// 
    bool Eliminar(string clave);

    /// 
    /// Limpia todo el almacenamiento seguro
    /// 
    void LimpiarTodo();

    /// 
    /// Verifica si existe una clave
    /// 
    Task ExisteAsync(string clave);
}
```

#### Implementación (Services/Implementations/AlmacenamientoSeguroService.cs):
```csharp
using Microsoft.Maui.Storage;
using Phanteon.Services.Interfaces;

namespace Phanteon.Services.Implementations;

/// 
/// Implementación del almacenamiento seguro usando SecureStorage de MAUI
/// 
public class AlmacenamientoSeguroService : IAlmacenamientoSeguro
{
    private readonly ILogger _logger;

    public AlmacenamientoSeguroService(ILogger logger)
    {
        _logger = logger;
    }

    /// 
    /// Guarda un valor de forma segura (encriptado)
    /// 
    public async Task GuardarAsync(string clave, string valor)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave no puede estar vacía", nameof(clave));

            await SecureStorage.SetAsync(clave, valor);
            _logger.LogInformation($"Valor guardado exitosamente para la clave: {clave}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al guardar valor para la clave: {clave}");
            throw;
        }
    }

    /// 
    /// Obtiene un valor almacenado
    /// 
    public async Task ObtenerAsync(string clave)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("La clave no puede estar vacía", nameof(clave));

            var valor = await SecureStorage.GetAsync(clave);
            
            if (valor == null)
                _logger.LogWarning($"No se encontró valor para la clave: {clave}");
            
            return valor;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al obtener valor para la clave: {clave}");
            throw;
        }
    }

    /// 
    /// Elimina un valor específico
    /// 
    public bool Eliminar(string clave)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(clave))
                return false;

            SecureStorage.Remove(clave);
            _logger.LogInformation($"Valor eliminado para la clave: {clave}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error al eliminar valor para la clave: {clave}");
            return false;
        }
    }

    /// 
    /// Limpia todo el almacenamiento seguro
    /// 
    public void LimpiarTodo()
    {
        try
        {
            SecureStorage.RemoveAll();
            _logger.LogInformation("Almacenamiento seguro limpiado completamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al limpiar el almacenamiento seguro");
            throw;
        }
    }

    /// 
    /// Verifica si existe una clave en el almacenamiento
    /// 
    public async Task ExisteAsync(string clave)
    {
        try
        {
            var valor = await ObtenerAsync(clave);
            return !string.IsNullOrEmpty(valor);
        }
        catch
        {
            return false;
        }
    }
}
```

### 4. Políticas de Resiliencia con Polly

#### Centralización de Políticas (Helpers/Policies/PoliticasHttp.cs):
```csharp
using Polly;
using Polly.Extensions.Http;

namespace Phanteon.Helpers.Policies;

/// 
/// Políticas de resiliencia centralizadas para llamadas HTTP
/// 
public static class PoliticasHttp
{
    /// 
    /// Política de reintentos con backoff exponencial
    /// Maneja errores transitorios de red (timeout, 5xx, etc.)
    /// 
    public static IAsyncPolicy ObtenerPoliticaReintentos()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // Maneja 5xx y errores de red
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests) // 429
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: intentos => TimeSpan.FromSeconds(Math.Pow(2, intentos)),
                onRetry: (resultado, tiempoEspera, intento, contexto) =>
                {
                    Console.WriteLine(
                        $"⚠️ Reintento {intento} después de {tiempoEspera.TotalSeconds}s. " +
                        $"Razón: {resultado.Exception?.Message ?? resultado.Result.StatusCode.ToString()}"
                    );
                });
    }

    /// 
    /// Política de circuit breaker para evitar sobrecarga
    /// Abre el circuito después de 5 fallos consecutivos
    /// 
    public static IAsyncPolicy ObtenerPoliticaCircuitBreaker()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (resultado, duracion) =>
                {
                    Console.WriteLine($"🔴 Circuit breaker ABIERTO por {duracion.TotalSeconds}s");
                },
                onReset: () =>
                {
                    Console.WriteLine("🟢 Circuit breaker CERRADO - Conexión restaurada");
                });
    }

    /// 
    /// Política de timeout para evitar esperas indefinidas
    /// 
    public static IAsyncPolicy ObtenerPoliticaTimeout()
    {
        return Policy
            .TimeoutAsync(
                timeout: TimeSpan.FromSeconds(30),
                onTimeoutAsync: (contexto, tiempoEspera, tarea) =>
                {
                    Console.WriteLine($"⏱️ Timeout después de {tiempoEspera.TotalSeconds}s");
                    return Task.CompletedTask;
                });
    }

    /// 
    /// Política combinada (wrap): timeout + reintentos + circuit breaker
    /// 
    public static IAsyncPolicy ObtenerPoliticaCompleta()
    {
        var timeout = ObtenerPoliticaTimeout();
        var reintentos = ObtenerPoliticaReintentos();
        var circuitBreaker = ObtenerPoliticaCircuitBreaker();

        // El orden importa: primero timeout, luego reintentos, finalmente circuit breaker
        return Policy.WrapAsync(circuitBreaker, reintentos, timeout);
    }
}
```

---

## 💡 Ejemplos de Código

### Ejemplo 1: Servicio de Autenticación

#### Interface (Services/Interfaces/IAutenticacionService.cs):
```csharp
using Phanteon.Models;

namespace Phanteon.Services.Interfaces;

/// 
/// Interfaz para el servicio de autenticación
/// 
public interface IAutenticacionService
{
    /// 
    /// Inicia sesión con credenciales del usuario
    /// 
    Task IniciarSesionAsync(string correo, string contrasena);
    
    /// 
    /// Cierra la sesión actual
    /// 
    Task CerrarSesionAsync();
    
    /// 
    /// Verifica si el usuario está autenticado
    /// 
    Task EstaAutenticadoAsync();
    
    /// 
    /// Obtiene el token de autenticación actual
    /// 
    Task ObtenerTokenAsync();
}
```

#### Implementación (Services/Implementations/AutenticacionService.cs):
```csharp
using Phanteon.Services.Interfaces;
using System.Text.Json;

namespace Phanteon.Services.Implementations;

/// 
/// Servicio que maneja la autenticación de usuarios
/// 
public class AutenticacionService : IAutenticacionService
{
    private readonly IAlmacenamientoSeguro _almacenamientoSeguro;
    private readonly IApiService _apiService;
    private readonly ILogger _logger;

    // Claves para SecureStorage
    private const string CLAVE_TOKEN = "auth_token";
    private const string CLAVE_USUARIO = "usuario_actual";

    public AutenticacionService(
        IAlmacenamientoSeguro almacenamientoSeguro,
        IApiService apiService,
        ILogger logger)
    {
        _almacenamientoSeguro = almacenamientoSeguro;
        _apiService = apiService;
        _logger = logger;
    }

    /// 
    /// Inicia sesión y guarda el token de forma segura
    /// 
    public async Task IniciarSesionAsync(string correo, string contrasena)
    {
        try
        {
            _logger.LogInformation($"Intentando iniciar sesión para: {correo}");

            // Validar entrada
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                _logger.LogWarning("Credenciales vacías");
                return false;
            }

            // Llamar a la API de autenticación (aquí simularemos la respuesta)
            // var respuesta = await _apiService.AutenticarAsync(correo, contrasena);
            
            // SIMULACIÓN: Generar token ficticio
            var tokenSimulado = $"Bearer {Guid.NewGuid():N}";
            
            // Guardar token de forma segura
            await _almacenamientoSeguro.GuardarAsync(CLAVE_TOKEN, tokenSimulado);
            await _almacenamientoSeguro.GuardarAsync(CLAVE_USUARIO, correo);

            _logger.LogInformation("Inicio de sesión exitoso");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error durante el inicio de sesión");
            return false;
        }
    }

    /// 
    /// Cierra sesión eliminando las credenciales almacenadas
    /// 
    public async Task CerrarSesionAsync()
    {
        try
        {
            _logger.LogInformation("Cerrando sesión...");
            
            _almacenamientoSeguro.Eliminar(CLAVE_TOKEN);
            _almacenamientoSeguro.Eliminar(CLAVE_USUARIO);

            _logger.LogInformation("Sesión cerrada exitosamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cerrar sesión");
            throw;
        }
    }

    /// 
    /// Verifica si el usuario está autenticado
    /// 
    public async Task EstaAutenticadoAsync()
    {
        try
        {
            var token = await _almacenamientoSeguro.ObtenerAsync(CLAVE_TOKEN);
            return !string.IsNullOrEmpty(token);
        }
        catch
        {
            return false;
        }
    }

    /// 
    /// Obtiene el token de autenticación actual
    /// 
    public async Task ObtenerTokenAsync()
    {
        return await _almacenamientoSeguro.ObtenerAsync(CLAVE_TOKEN);
    }
}
```

### Ejemplo 2: Converter para XAML

#### Bool a Color Converter (Helpers/Converters/BoolToColorConverter.cs):
```csharp
using System.Globalization;

namespace Phanteon.Helpers.Converters;

/// 
/// Convierte un valor booleano a Color (Verde si true, Rojo si false)
/// Uso en XAML: TextColor="{Binding EstaActivo, Converter={StaticResource BoolToColorConverter}}"
/// 
public class BoolToColorConverter : IValueConverter
{
    /// 
    /// Convierte de bool a Color
    /// 
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool esVerdadero)
        {
            return esVerdadero ? Colors.Green : Colors.Red;
        }
        
        return Colors.Gray;
    }

    /// 
    /// Conversión inversa (no implementada)
    /// 
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

#### Registro en App.xaml:
```xml


    
        
            
            
            
            
            #512BD4
            #DFD8F7
        
    

```

### Ejemplo 3: Extension Methods

#### String Extensions (Helpers/Extensions/StringExtensions.cs):
```csharp
namespace Phanteon.Helpers.Extensions;

/// 
/// Métodos de extensión para strings
/// 
public static class StringExtensions
{
    /// 
    /// Valida si un string es un correo electrónico válido
    /// 
    public static bool EsCorreoValido(this string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            return false;

        try
        {
            var direccion = new System.Net.Mail.MailAddress(correo);
            return direccion.Address == correo;
        }
        catch
        {
            return false;
        }
    }

    /// 
    /// Trunca un string a una longitud máxima
    /// 
    public static string Truncar(this string texto, int longitudMaxima, string sufijo = "...")
    {
        if (string.IsNullOrEmpty(texto) || texto.Length <= longitudMaxima)
            return texto;

        return texto.Substring(0, longitudMaxima) + sufijo;
    }

    /// 
    /// Capitaliza la primera letra de cada palabra
    /// 
    public static string CapitalizarPalabras(this string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return texto;

        var palabras = texto.Split(' ');
        for (int i = 0; i < palabras.Length; i++)
        {
            if (palabras[i].Length > 0)
            {
                palabras[i] = char.ToUpper(palabras[i][0]) + palabras[i].Substring(1).ToLower();
            }
        }

        return string.Join(" ", palabras);
    }
}
```

**Uso de Extensions:**
```csharp
// En un ViewModel o servicio
string correo = "usuario@ejemplo.com";
if (correo.EsCorreoValido())
{
    // Correo válido
}

string textoLargo = "Este es un texto muy largo que necesita ser truncado";
string textoCorto = textoLargo.Truncar(20); // "Este es un texto muy..."

string nombre = "juan pérez";
string nombreFormateado = nombre.CapitalizarPalabras(); // "Juan Pérez"
```

---

## 📖 Guía de Uso

### 1. Crear un Nuevo ViewModel
```csharp
// 1. Crear archivo: ViewModels/ProductosViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Interfaces;
using Phanteon.Models;

namespace Phanteon.ViewModels;

/// 
/// ViewModel para la gestión de productos
/// 
public partial class ProductosViewModel : ObservableObject
{
    private readonly IApiService _apiService;

    public ProductosViewModel(IApiService apiService)
    {
        _apiService = apiService;
    }

    /// 
    /// Nombre del producto
    /// 
    [ObservableProperty]
    private string nombreProducto;

    /// 
    /// Precio del producto
    /// 
    [ObservableProperty]
    private decimal precio;

    /// 
    /// Comando para guardar un producto
    /// 
    [RelayCommand]
    private async Task GuardarProducto()
    {
        // Lógica para guardar
    }
}

// 2. Registrar en MauiProgram.cs
builder.Services.AddTransient();

// 3. Crear la vista y vincularla
```

### 2. Consumir una API con Refit
```csharp
// 1. Definir el modelo (Models/Producto.cs)
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal Precio { get; set; }
}

// 2. Definir el endpoint en IApiService
[Get("/api/productos")]
Task<List> ObtenerProductosAsync();

// 3. Usar en el ViewModel
var productos = await _apiService.ObtenerProductosAsync();
```

### 3. Navegar Entre Páginas
```csharp
// Navegación simple
await Shell.Current.GoToAsync("//detalles");

// Navegación con parámetros
await Shell.Current.GoToAsync($"detalles?productoId={producto.Id}");

// Recibir parámetros en el ViewModel
[QueryProperty(nameof(ProductoId), "productoId")]
public partial class DetallesViewModel : ObservableObject
{
    [ObservableProperty]
    private int productoId;
}
```

### 4. Mostrar Alertas y Diálogos
```csharp
// Alerta simple
await Application.Current.MainPage.DisplayAlert(
    "Éxito", 
    "Los datos se guardaron correctamente", 
    "OK");

// Confirmación
bool confirmar = await Application.Current.MainPage.DisplayAlert(
    "Confirmar",
    "¿Deseas eliminar este elemento?",
    "Sí",
    "No");

if (confirmar)
{
    // Usuario confirmó
}

// Prompt (input del usuario)
string resultado = await Application.Current.MainPage.DisplayPromptAsync(
    "Ingresa tu nombre",
    "¿Cómo te llamas?",
    placeholder: "Tu nombre aquí");
```

---

## 🐛 Solución de Problemas

### Problema 1: "Refit no genera implementación"

**Síntoma:** Errores de compilación indicando que `IApiService` no tiene implementación.

**Solución:**
```csharp
// Verificar que esté registrado correctamente en MauiProgram.cs
builder.Services
    .AddRefitClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.ejemplo.com"));
```

### Problema 2: "SecureStorage lanza excepción en Android"

**Síntoma:** `PlatformNotSupportedException` al usar SecureStorage.

**Solución:**
```csharp
// Envolver en try-catch y verificar disponibilidad
try
{
    await SecureStorage.SetAsync("clave", "valor");
}
catch (Exception ex)
{
    // Fallback: usar Preferences (menos seguro)
    Preferences.Set("clave", "valor");
}
```

### Problema 3: "ObservableProperty no se genera"

**Síntoma:** Error de compilación `[ObservableProperty]` no reconocido.

**Solución:**
1. Verificar que el paquete esté instalado: `CommunityToolkit.Mvvm`
2. La clase debe ser `partial`
3. Heredar de `ObservableObject`
4. Recompilar el proyecto (**Build > Rebuild Solution**)

### Problema 4: "Políticas de Polly no se aplican"

**Síntoma:** Los reintentos no funcionan.

**Solución:**
```csharp
// Asegurarse de agregar el PolicyHandler AL cliente Refit
builder.Services
    .AddRefitClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.ejemplo.com"))
    .AddPolicyHandler(PoliticasHttp.ObtenerPoliticaReintentos()); // ← IMPORTANTE
```

### Problema 5: "Binding no funciona en XAML"

**Síntoma:** Los datos no se actualizan en la UI.

**Solución:**
1. Verificar que `BindingContext` esté asignado:
```csharp
public MainPage(MainViewModel viewModel)
{
    InitializeComponent();
    BindingContext = viewModel; // ← Verificar esto
}
```

2. En XAML, usar `x:DataType` para IntelliSense:
```xml

```

---

## 📚 Referencias

### Documentación Oficial

| Recurso | URL |
|---------|-----|
| .NET MAUI Docs | https://learn.microsoft.com/dotnet/maui/ |
| CommunityToolkit.Mvvm | https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/ |
| Refit | https://github.com/reactiveui/refit |
| Polly | https://github.com/App-vNext/Polly |
| SecureStorage | https://learn.microsoft.com/dotnet/maui/platform-integration/storage/secure-storage |

### Tutoriales Recomendados

1. **MVVM en .NET MAUI:** [Microsoft Learn](https://learn.microsoft.com/training/modules/use-mvvm-pattern-xamarin-forms/)
2. **Consumo de APIs con Refit:** [YouTube - James Montemagno](https://www.youtube.com/watch?v=SZnUrjQsD1A)
3. **Inyección de Dependencias:** [Microsoft Docs](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection)

### Libros Sugeridos

- **"Enterprise Application Patterns using .NET MAUI"** - Michael Stonis
- **"Mobile Development with .NET"** - Can Bilgin

---

## 📝 Notas Finales

### Checklist de Entrega

- [ ] Código compila sin errores ni warnings
- [ ] Todas las carpetas siguen la estructura propuesta
- [ ] ViewModels usan `[ObservableProperty]` y `[RelayCommand]`
- [ ] Servicios registrados en `MauiProgram.cs`
- [ ] Comentarios XML en métodos públicos
- [ ] Variables y métodos en **español**
- [ ] README.md actualizado
- [ ] Capturas de pantalla de la aplicación funcionando

### Comandos Útiles
```bash
# Limpiar solución
dotnet clean

# Restaurar paquetes
dotnet restore

# Compilar proyecto
dotnet build

# Ejecutar en Android
dotnet build -t:Run -f net9.0-android

# Ejecutar en iOS
dotnet build -t:Run -f net9.0-ios
```

### Convenciones de Código
```csharp
// ✅ CORRECTO: Nombres en español, PascalCase para públicos
public class ServicioUsuarios
{
    private readonly IApiService _apiService; // camelCase con _ para privados
    
    public async Task<List> ObtenerUsuariosAsync()
    {
        // PascalCase para métodos públicos
        var listaUsuarios = new List(); // camelCase para locales
        return listaUsuarios;
    }
}

// ❌ INCORRECTO: Mezcla de idiomas
public class UserService
{
    private IApiService apiService; // falta _
    
    public async Task<List> GetUsers() // inglés
    {
        // ...
    }
}
```

---

## 🎓 Evaluación del Proyecto

### Criterios de Calificación

| Criterio | Puntos | Descripción |
|----------|--------|-------------|
| **Arquitectura** | 25% | Correcta separación MVVM, uso de interfaces |
| **Funcionalidad** | 30% | Aplicación funciona en al menos 2 plataformas |
| **Código Limpio** | 20% | Comentarios, nombres descriptivos, SOLID |
| **Manejo de Errores** | 15% | Try-catch, validaciones, mensajes al usuario |
| **Presentación** | 10% | README completo, capturas, demo funcionando |

### Rúbrica Detallada

#### Arquitectura (25 puntos)
- [ ] 10 pts: Uso correcto de MVVM
- [ ] 8 pts: Inyección de dependencias implementada
- [ ] 7 pts: Separación clara de responsabilidades (Views, ViewModels, Services)

#### Funcionalidad (30 puntos)
- [ ] 15 pts: Consumo exitoso de API con Refit
- [ ] 10 pts: Almacenamiento seguro funcionando
- [ ] 5 pts: Navegación entre páginas

#### Código Limpio (20 puntos)
- [ ] 8 pts: Comentarios XML en métodos públicos
- [ ] 7 pts: Nombres descriptivos en español
- [ ] 5 pts: Sin código duplicado, aplicación de DRY

#### Manejo de Errores (15 puntos)
- [ ] 8 pts: Try-catch en operaciones críticas
- [ ] 7 pts: Mensajes claros al usuario

#### Presentación (10 puntos)
- [ ] 5 pts: README.md completo
- [ ] 5 pts: Capturas de pantalla y demo

---


.
 
 Sonnet 4.5
 
mejorame y ordename este md y mejoralo para poder usar en un proyecto
