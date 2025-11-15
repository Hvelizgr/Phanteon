# Arquitectura del Proyecto Phanteon

## Estructura de Carpetas

```
Phanteon/
├── Features/                          # Características de la aplicación (por módulo)
│   ├── Main/                         # Módulo principal
│   │   ├── MainPage.xaml            # Vista principal
│   │   ├── MainPage.xaml.cs         # Code-behind
│   │   └── MainViewModel.cs         # ViewModel MVVM
│   └── Shared/                       # Componentes compartidos entre features
│
├── Core/                             # Componentes centrales reutilizables
│   ├── ViewModels/                  # ViewModels base
│   │   └── BaseViewModel.cs        # ViewModel base con propiedades comunes
│   ├── Behaviors/                   # Behaviors XAML personalizados
│   ├── Converters/                  # Value Converters para XAML
│   └── Controls/                    # Controles personalizados
│
├── Services/                         # Servicios de la aplicación
│   ├── Http/                        # Servicios HTTP
│   │   ├── IApiHttpClientFactory.cs
│   │   └── ApiHttpClientFactory.cs
│   ├── Api/                         # Interfaces Refit y servicios API
│   ├── Storage/                     # Servicios de almacenamiento
│   │   ├── ISecureStorageService.cs
│   │   └── SecureStorageService.cs
│   └── Navigation/                  # Servicios de navegación
│       ├── INavigationService.cs
│       └── NavigationService.cs
│
├── Models/                           # Modelos de datos
│   ├── Dispositivo.cs
│   ├── Usuario.cs
│   ├── Alerta.cs
│   └── HistorialDispositivo.cs
│
├── Data/                            # Capa de acceso a datos
│   ├── Repositories/               # Repositorios (patrón Repository)
│   └── Local/                      # Base de datos local (SQLite, etc.)
│
├── Constants/                       # Constantes de la aplicación
│   ├── ApiEndpoints.cs             # Endpoints de la API
│   ├── AppConstants.cs             # Constantes generales
│   └── ErrorMessages.cs            # Mensajes de error estandarizados
│
├── Helpers/                         # Clases auxiliares
│   └── ApiConfiguration.cs         # Configuración de API
│
├── Resources/                       # Recursos de la aplicación
│   ├── Images/                     # Imágenes
│   ├── Fonts/                      # Fuentes
│   ├── Styles/                     # Estilos XAML
│   └── Raw/                        # Archivos raw
│
├── Platforms/                       # Código específico de plataforma
│   ├── Android/
│   ├── iOS/
│   ├── Windows/
│   └── MacCatalyst/
│
├── App.xaml                         # Aplicación principal
├── AppShell.xaml                    # Shell de navegación
└── MauiProgram.cs                   # Punto de entrada y DI
```

## Patrones y Principios

### 1. MVVM (Model-View-ViewModel)
- **Views**: En `Features/{ModuleName}/`
- **ViewModels**: En `Features/{ModuleName}/`
- **Models**: En `Models/`

### 2. Dependency Injection
Todos los servicios y ViewModels se registran en `MauiProgram.cs`:

```csharp
// Servicios (Singleton para servicios sin estado)
builder.Services.AddSingleton<IApiHttpClientFactory, ApiHttpClientFactory>();
builder.Services.AddSingleton<ISecureStorageService, SecureStorageService>();

// ViewModels (Transient para nueva instancia cada vez)
builder.Services.AddTransient<MainViewModel>();

// Pages (Transient)
builder.Services.AddTransient<MainPage>();
```

### 3. Feature-Based Structure
Cada feature agrupa su Vista, ViewModel y lógica relacionada en una carpeta:
- `Features/Main/` - Funcionalidad principal
- `Features/Dispositivos/` - Gestión de dispositivos (ejemplo)
- `Features/Auth/` - Autenticación (ejemplo)

### 4. Separación de Responsabilidades

#### Services/Http
Servicios relacionados con comunicación HTTP y configuración de clientes

#### Services/Api
Interfaces Refit para consumir APIs REST

#### Services/Storage
Almacenamiento de datos (SecureStorage, Preferences, SQLite)

#### Services/Navigation
Navegación entre páginas

#### Constants
Valores constantes centralizados (endpoints, mensajes, rutas)

## Ejemplo: Agregar Módulo de Dispositivos

### Estructura de carpetas
```
Features/Dispositivos/
├── DispositivosListPage.xaml
├── DispositivosListPage.xaml.cs
├── DispositivosListViewModel.cs
├── DispositivoDetailPage.xaml
├── DispositivoDetailPage.xaml.cs
└── DispositivoDetailViewModel.cs
```

### Registro en MauiProgram.cs
```csharp
// ViewModels
builder.Services.AddTransient<DispositivosListViewModel>();
builder.Services.AddTransient<DispositivoDetailViewModel>();

// Pages
builder.Services.AddTransient<DispositivosListPage>();
builder.Services.AddTransient<DispositivoDetailPage>();
```

### Rutas en AppShell.xaml
```xml
<ShellContent
    Title="Dispositivos"
    ContentTemplate="{DataTemplate dispositivos:DispositivosListPage}"
    Route="Dispositivos" />
```

### Servicio API (opcional)
```csharp
// Services/Api/IDispositivosApi.cs
public interface IDispositivosApi
{
    [Get("/api/dispositivos")]
    Task<List<Dispositivo>> GetDispositivosAsync();
}

// Registrar en MauiProgram.cs
builder.Services.AddRefitClient<IDispositivosApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(ApiConfiguration.BaseUrl));
```

## Convenciones de Nomenclatura

- **Clases**: PascalCase
- **Interfaces**: IPascalCase
- **Métodos**: PascalCase
- **Propiedades**: PascalCase
- **Variables privadas**: camelCase o _camelCase
- **Constantes**: PascalCase
- **Archivos**: Mismo nombre que la clase principal

## Tecnologías Utilizadas

- **.NET MAUI 9.0**
- **CommunityToolkit.Maui** - Componentes adicionales
- **CommunityToolkit.Mvvm** - MVVM helpers (ObservableProperty, RelayCommand)
- **Refit** - Cliente HTTP tipado
- **Polly** - Políticas de resiliencia (retry, timeout)
- **Newtonsoft.Json** - Serialización JSON

## Buenas Prácticas

1. **Usar CommunityToolkit.Mvvm** para reducir boilerplate:
   ```csharp
   [ObservableProperty]
   private string nombre;  // Genera propiedad Nombre automáticamente

   [RelayCommand]
   private async Task GuardarAsync() { }  // Genera GuardarCommand
   ```

2. **Inyectar dependencias en constructores**:
   ```csharp
   public DispositivosViewModel(IDispositivosApi api, INavigationService navigation)
   {
       _api = api;
       _navigation = navigation;
   }
   ```

3. **Usar Constants para valores fijos**:
   ```csharp
   await _storage.SetAsync(AppConstants.StorageKeys.AuthToken, token);
   ```

4. **Heredar de BaseViewModel** para funcionalidades comunes:
   ```csharp
   public class DispositivosViewModel : BaseViewModel
   {
       // Ya tienes EstaCargando, MensajeError, Titulo, etc.
   }
   ```

5. **Manejar errores de manera consistente**:
   ```csharp
   try
   {
       EstaCargando = true;
       // operación
   }
   catch (Exception ex)
   {
       ManejarError(ex, "cargar dispositivos");
   }
   finally
   {
       EstaCargando = false;
   }
   ```

---

**Última actualización:** Noviembre 2025
