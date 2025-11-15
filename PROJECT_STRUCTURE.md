# Estructura del Proyecto Phanteon

## Resumen

Este documento describe la organización del proyecto después de la reorganización aplicando principios de Clean Architecture y SOLID.

---

## Estructura de Carpetas

```
Phanteon/
│
├── Features/                           # Organización por características (Feature-based)
│   ├── Auth/                          # Autenticación y registro
│   │   ├── Views/                     # Páginas XAML y code-behind
│   │   │   ├── LoginPage.xaml
│   │   │   ├── LoginPage.xaml.cs
│   │   │   ├── RegisterPage.xaml
│   │   │   └── RegisterPage.xaml.cs
│   │   └── ViewModels/                # ViewModels de autenticación
│   │       ├── LoginViewModel.cs
│   │       └── RegisterViewModel.cs
│   │
│   ├── Main/                          # Página principal
│   │   ├── Views/
│   │   │   ├── MainPage.xaml
│   │   │   └── MainPage.xaml.cs
│   │   └── ViewModels/
│   │       └── MainViewModel.cs
│   │
│   ├── Profile/                       # Perfil de usuario
│   │   ├── Views/
│   │   │   ├── ProfilePage.xaml
│   │   │   └── ProfilePage.xaml.cs
│   │   └── ViewModels/
│   │       └── ProfileViewModel.cs
│   │
│   ├── Dispositivos/                  # Gestión de dispositivos
│   │   ├── Views/
│   │   │   ├── DispositivosPage.xaml
│   │   │   ├── DispositivosPage.xaml.cs
│   │   │   ├── DispositivoDetailPage.xaml
│   │   │   └── DispositivoDetailPage.xaml.cs
│   │   └── ViewModels/
│   │       ├── DispositivosViewModel.cs
│   │       └── DispositivoDetailViewModel.cs
│   │
│   └── Alertas/                       # Sistema de alertas
│       ├── Views/
│       │   ├── AlertasPage.xaml
│       │   └── AlertasPage.xaml.cs
│       └── ViewModels/
│           └── AlertasViewModel.cs
│
├── Resources/                          # Recursos de la aplicación
│   ├── Converters/                    # Value Converters
│   │   ├── BoolToColorConverter.cs
│   │   ├── BoolToEyeIconConverter.cs
│   │   ├── DispositivoActivoToColorConverter.cs
│   │   ├── FiltroSelectedConverter.cs
│   │   ├── InvertedBoolConverter.cs
│   │   ├── IsNotNullConverter.cs
│   │   ├── SeveridadToColorConverter.cs
│   │   └── StringNotEmptyConverter.cs
│   │
│   ├── Behaviors/                     # Behaviors de MAUI
│   │   └── EventToCommandBehavior.cs
│   │
│   └── Styles/                        # Estilos y colores
│       ├── Colors.xaml
│       └── Styles.xaml
│
├── Services/                           # Servicios de la aplicación (aplicando SOLID)
│   ├── Api/                           # Interfaces Refit para APIs
│   │   ├── IAlertasApi.cs
│   │   ├── IDispositivosApi.cs
│   │   └── IUsuariosApi.cs
│   │
│   ├── Auth/                          # Servicios de autenticación
│   │   ├── IAuthService.cs
│   │   ├── AuthService.cs
│   │   ├── ISessionManager.cs         # ⭐ NUEVO (SRP)
│   │   └── SessionManager.cs          # ⭐ NUEVO (SRP)
│   │
│   ├── Http/                          # Servicios HTTP
│   │   ├── IApiHttpClientFactory.cs
│   │   └── ApiHttpClientFactory.cs
│   │
│   ├── Navigation/                    # Servicios de navegación
│   │   ├── INavigationService.cs
│   │   ├── NavigationService.cs
│   │   ├── IStartupNavigationService.cs  # ⭐ NUEVO (ISP)
│   │   └── StartupNavigationService.cs   # ⭐ NUEVO (ISP)
│   │
│   └── Storage/                       # Servicios de almacenamiento
│       ├── ISecureStorageService.cs
│       └── SecureStorageService.cs
│
├── Models/                            # Modelos de datos
│   ├── Alerta.cs
│   ├── Dispositivo.cs
│   ├── HistorialDispositivo.cs
│   └── Usuario.cs
│
├── Core/                              # Componentes base
│   └── ViewModels/
│       └── BaseViewModel.cs
│
├── Constants/                         # Constantes de la aplicación
│   ├── ApiEndpoints.cs
│   ├── AppConstants.cs
│   └── ErrorMessages.cs
│
├── Helpers/                           # Utilidades
│   └── ApiConfiguration.cs
│
├── Platforms/                         # Código específico de plataforma
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   ├── Tizen/
│   └── Windows/
│
├── App.xaml                           # Aplicación principal
├── App.xaml.cs                        # ⭐ REFACTORIZADO (DIP, SRP)
├── AppShell.xaml                      # Shell de navegación
├── AppShell.xaml.cs                   # ⭐ ACTUALIZADO (nuevos namespaces)
├── MauiProgram.cs                     # ⭐ ACTUALIZADO (nuevos servicios)
├── SOLID_REFACTORING.md               # ⭐ Documentación de principios SOLID
└── PROJECT_STRUCTURE.md               # ⭐ Este archivo
```

---

## Convenciones de Nombres y Namespaces

### Features (Características)

Cada feature se organiza en su propia carpeta con la siguiente estructura:

```
Features/
└── [FeatureName]/
    ├── Views/              # Namespace: Phanteon.Features.[FeatureName].Views
    │   ├── [Name]Page.xaml
    │   └── [Name]Page.xaml.cs
    └── ViewModels/         # Namespace: Phanteon.Features.[FeatureName].ViewModels
        └── [Name]ViewModel.cs
```

**Ejemplo:**
```csharp
// LoginPage.xaml.cs
namespace Phanteon.Features.Auth.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel) { ... }
    }
}

// LoginViewModel.cs
namespace Phanteon.Features.Auth.ViewModels
{
    public partial class LoginViewModel : ObservableObject { ... }
}
```

### Resources (Recursos)

Los recursos se organizan por tipo:

```
Resources/
├── Converters/         # Namespace: Phanteon.Resources.Converters
├── Behaviors/          # Namespace: Phanteon.Resources.Behaviors
└── Styles/            # Archivos XAML, sin namespace C#
```

### Services (Servicios)

Los servicios se organizan por responsabilidad:

```
Services/
├── Api/               # Namespace: Phanteon.Services.Api
├── Auth/              # Namespace: Phanteon.Services.Auth
├── Http/              # Namespace: Phanteon.Services.Http
├── Navigation/        # Namespace: Phanteon.Services.Navigation
└── Storage/           # Namespace: Phanteon.Services.Storage
```

---

## Ventajas de Esta Estructura

### 1. **Organización por Feature**
- ✅ Cada característica está contenida en su propia carpeta
- ✅ Views y ViewModels relacionados están juntos
- ✅ Fácil encontrar y modificar código relacionado
- ✅ Escalable: agregar nuevas features es simple

### 2. **Separación de Concerns**
- ✅ Resources separados por tipo (Converters, Behaviors, Styles)
- ✅ Services organizados por responsabilidad
- ✅ Models en su propia carpeta
- ✅ No hay mezcla entre UI y lógica de negocio

### 3. **Mantenibilidad**
- ✅ Estructura clara y predecible
- ✅ Fácil de navegar para nuevos desarrolladores
- ✅ Namespaces consistentes
- ✅ Reduce conflictos de merge en equipos

### 4. **Testabilidad**
- ✅ ViewModels separados de Views
- ✅ Servicios con interfaces claras
- ✅ Fácil crear mocks para testing
- ✅ Inyección de dependencias bien definida

---

## Guía de Uso

### Crear una Nueva Feature

1. **Crear carpetas:**
   ```bash
   mkdir Features/[FeatureName]/Views
   mkdir Features/[FeatureName]/ViewModels
   ```

2. **Crear View:**
   ```xaml
   <!-- Features/[FeatureName]/Views/[Name]Page.xaml -->
   <?xml version="1.0" encoding="utf-8" ?>
   <ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                xmlns:viewmodel="clr-namespace:Phanteon.Features.[FeatureName].ViewModels"
                x:Class="Phanteon.Features.[FeatureName].Views.[Name]Page"
                x:DataType="viewmodel:[Name]ViewModel">
       <!-- Contenido -->
   </ContentPage>
   ```

   ```csharp
   // Features/[FeatureName]/Views/[Name]Page.xaml.cs
   using Phanteon.Features.[FeatureName].ViewModels;

   namespace Phanteon.Features.[FeatureName].Views
   {
       public partial class [Name]Page : ContentPage
       {
           public [Name]Page([Name]ViewModel viewModel)
           {
               InitializeComponent();
               BindingContext = viewModel;
           }
       }
   }
   ```

3. **Crear ViewModel:**
   ```csharp
   // Features/[FeatureName]/ViewModels/[Name]ViewModel.cs
   using CommunityToolkit.Mvvm.ComponentModel;

   namespace Phanteon.Features.[FeatureName].ViewModels
   {
       public partial class [Name]ViewModel : ObservableObject
       {
           // Lógica del ViewModel
       }
   }
   ```

4. **Registrar en MauiProgram.cs:**
   ```csharp
   // ViewModels
   builder.Services.AddTransient<[Name]ViewModel>();

   // Pages
   builder.Services.AddTransient<[Name]Page>();
   ```

5. **Registrar ruta en AppShell.xaml.cs:**
   ```csharp
   Routing.RegisterRoute("[RouteName]", typeof([Name]Page));
   ```

### Agregar un Nuevo Converter

1. **Crear archivo:**
   ```csharp
   // Resources/Converters/[Name]Converter.cs
   using System.Globalization;

   namespace Phanteon.Resources.Converters
   {
       public class [Name]Converter : IValueConverter
       {
           public object Convert(object? value, Type targetType,
                               object? parameter, CultureInfo culture)
           {
               // Implementación
           }

           public object ConvertBack(object? value, Type targetType,
                                   object? parameter, CultureInfo culture)
           {
               throw new NotImplementedException();
           }
       }
   }
   ```

2. **Registrar en App.xaml:**
   ```xaml
   <Application.Resources>
       <ResourceDictionary>
           <converters:[Name]Converter x:Key="[Name]Converter" />
       </ResourceDictionary>
   </Application.Resources>
   ```

### Agregar un Nuevo Service

1. **Crear interfaz y implementación:**
   ```csharp
   // Services/[Category]/I[Name]Service.cs
   namespace Phanteon.Services.[Category]
   {
       public interface I[Name]Service
       {
           // Métodos
       }
   }

   // Services/[Category]/[Name]Service.cs
   namespace Phanteon.Services.[Category]
   {
       public class [Name]Service : I[Name]Service
       {
           // Implementación
       }
   }
   ```

2. **Registrar en MauiProgram.cs:**
   ```csharp
   builder.Services.AddSingleton<I[Name]Service, [Name]Service>();
   // o AddTransient/AddScoped según sea necesario
   ```

---

## Cambios Recientes

### ✅ Reorganización Completa (2025)

**Antes:**
```
Features/
└── Auth/
    ├── LoginPage.xaml          # Todo mezclado
    ├── LoginViewModel.cs
    ├── RegisterPage.xaml
    └── RegisterViewModel.cs

Core/
├── Converters/                 # Mezcla de concerns
└── Behaviors/
```

**Después:**
```
Features/
└── Auth/
    ├── Views/                  # ✅ Separado claramente
    │   ├── LoginPage.xaml
    │   ├── LoginPage.xaml.cs
    │   ├── RegisterPage.xaml
    │   └── RegisterPage.xaml.cs
    └── ViewModels/
        ├── LoginViewModel.cs
        └── RegisterViewModel.cs

Resources/                      # ✅ Recursos centralizados
├── Converters/
├── Behaviors/
└── Styles/
```

### ✅ Aplicación de Principios SOLID

Ver `SOLID_REFACTORING.md` para detalles completos.

---

## Referencias

- [.NET MAUI App Architecture](https://learn.microsoft.com/dotnet/maui/fundamentals/app-architecture)
- [MVVM Pattern in MAUI](https://learn.microsoft.com/dotnet/maui/xaml/fundamentals/mvvm)
- [Feature-based Organization](https://www.scottbrady91.com/aspnet-core/feature-based-organization)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
