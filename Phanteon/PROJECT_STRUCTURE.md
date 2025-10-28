# Estructura del proyecto, tecnologías y buenas prácticas

Resumen
- Proyecto: .NET 9 / .NET MAUI
- Objetivo: mantener una arquitectura limpia, modular y preparada para MVVM, inyección de dependencias y consumo seguro de APIs.
- Paquetes NuGet instalados: `CommunityToolkit.Mvvm` (8.4.0), `Newtonsoft.Json` (13.0.4), `Refit` (8.0.0), `Polly` (8.6.4).
- API nativa de MAUI: `SecureStorage` (incluido en Microsoft.Maui.Essentials, no requiere paquete adicional).

Tecnologías y propósito
- CommunityToolkit.Mvvm — Patrón MVVM (source generators: `[ObservableProperty]`, `[RelayCommand]`), reduce boilerplate en ViewModels.
- SecureStorage (MAUI) — API nativa de .NET MAUI para almacenar tokens/credenciales de forma segura. Incluido en `Microsoft.Maui.Essentials` (no requiere paquete adicional). Uso: `await SecureStorage.SetAsync("key", "value")`.
- Newtonsoft.Json — Serialización/Deserialización flexible y compatible con Refit si necesitas control fino.
- Refit — Declarar endpoints HTTP como interfaces, simplifica llamadas REST.
- Polly — Resiliencia (retry, circuit-breaker, fallback) para políticas de HTTP.

Estructura de carpetas propuesta
- `Phanteon/Views/` — Páginas XAML y code-behind (ej. `MainPage.xaml`)
- `Phanteon/ViewModels/` — ViewModels (usar CommunityToolkit)
- `Phanteon/Models/` — DTOs y modelos de dominio
- `Phanteon/Services/Interfaces/` — Contratos (ej. `IApiService.cs`)
- `Phanteon/Services/Implementations/` — Implementaciones concretas (ej. `ApiService.cs`, wrappers)
- `Phanteon/Helpers/` — Utilidades (converters, extension methods, policies)
- `Phanteon/Resources/Styles/` — XAML de estilos y temas
- `Phanteon/Resources/Images/` — Imágenes/Assets
- `Phanteon/Resources/Fonts/` — Tipografías
- `Phanteon/Tests/` — Pruebas unitarias (si aplicas)
- `Phanteon/scripts/` — Scripts útiles (creación de carpetas, generación, etc.)

Qué va en cada carpeta (detallado)
- Views: solo XAML + code-behind mínimo. No lógica de negocio.
- ViewModels: lógica de presentación, comandos, propiedades observables. Inyectar servicios por constructor.
- Models: tipos simples, DTOs, mapping con AutoMapper (opcional).
- Services.Interfaces: todas las interfaces de servicios para facilitar testables y cumplir DIP.
- Services.Implementations: implementar las interfaces; aquí se orquesta Refit, SecureStorage y manejo de errores con Polly.
- Helpers: políticas de reintento, validadores, adaptadores de SecureStorage, factories.
- Resources: estilos, recursos estáticos. Mantén los nombres consistentes.

Registro e inyección de dependencias (ejemplo)
- Registrar `IApiService` (Refit) en `MauiProgram.cs` con `AddRefitClient<T>` y `AddPolicyHandler` (Polly).
- Registrar ViewModels y páginas si quieres resolver vía DI: `builder.Services.AddSingleton<MainViewModel>();`
- Evita `new` en ViewModels cuando dependan de servicios: usa inyección.

Buenas prácticas (MVVM y SOLID)
- Single Responsibility (S): Cada clase tiene una sola responsabilidad — ViewModels: presentación; Services: comunicación; Models: datos.
- Open/Closed (O): Extiende comportamiento mediante herencia o composición, no modificando clases existentes. Ex.: añadir un handler de caching sin cambiar `ApiService`.
- Liskov (L): Interfaces que pueden ser sustituidas sin romper la lógica (ej. `IApiService` implementada por/mockeada en tests).
- Interface Segregation (I): Interfaces pequeñas y específicas. Evita `IGeneralService` con muchas responsabilidades.
- Dependency Inversion (D): Depende de abstracciones (`IApiService`) no de implementaciones (`ApiService`).

Patrones y recomendaciones
- Refit + Newtonsoft.Json: usa `new RefitSettings(new NewtonsoftJsonContentSerializer())` si necesitas settings específicos.
- Polly: políticas de reintento y circuit-breaker en el `HttpClient` del Refit client.
- SecureStorage: envolver en un adapter (ej. `ISecureStorageService`) para facilitar test y manejo de errores.
- Manejo de errores: centralizar traducción de excepciones HTTP a mensajes amigables.
- Logging: utilizar `ILogger<T>` inyectado en servicios y ViewModels.

Instalación de paquetes NuGet
Los siguientes comandos instalan los paquetes requeridos (ya instalados en este proyecto):
```bash
dotnet add Phanteon/Phanteon.csproj package CommunityToolkit.Mvvm
dotnet add Phanteon/Phanteon.csproj package Newtonsoft.Json
dotnet add Phanteon/Phanteon.csproj package Refit
dotnet add Phanteon/Phanteon.csproj package Polly
```

Nota: `SecureStorage` viene incluido en .NET MAUI, no requiere instalación adicional.

Notas de uso y Visual Studio
- Para añadir carpetas usa __Solution Explorer__ > botón derecho en el proyecto > __Add > New Folder__ o ejecuta el script `scripts/create_folders.ps1`.
- Instalar paquetes: __Tools > NuGet Package Manager > Manage NuGet Packages for Solution__ o usa los comandos `dotnet add package` mostrados arriba.
- Compilar: __Build__ > __Build Solution__.
- Para usar `MainViewModel` por DI, registra la página y resuélvela con el contenedor o asigna `BindingContext` desde DI.

Ejemplo rápido de responsabilidades
- `MainPage.xaml` → solo binding a `MainViewModel`.
- `MainViewModel` → propiedades y comando `[RelayCommand]` que llama a `IApiService`.
- `IApiService` → interfaz Refit con endpoints.
- `ApiService` → si necesitas lógica adicional alrededor de Refit (cache, mapping).

Cómo desplegar cambios de carpetas
1. Añade las carpetas y archivos `.gitkeep` (incluidos aquí).
2. Mueve archivos existentes dentro de la nueva estructura.
3. Actualiza `namespace` si cambias la ubicación física de los archivos.
4. Corregir referencias en `MauiProgram.cs` y registrar servicios/ViewModels.

Referencias rápidas
- CommunityToolkit.Mvvm: usar source generators (compilar para generar miembros).
- Refit + Polly: registrar con `AddRefitClient` y `AddPolicyHandler`.
- SecureStorage: siempre manejar excepciones (permiso/availability).

Ejemplo de uso de SecureStorage
```csharp
using Microsoft.Maui.Storage;

// Guardar credenciales de forma segura
await SecureStorage.SetAsync("auth_token", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...");

// Leer credenciales
string token = await SecureStorage.GetAsync("auth_token");

// Eliminar credenciales
SecureStorage.Remove("auth_token");

// Limpiar todo el almacenamiento seguro
SecureStorage.RemoveAll();
```

Si quieres, adapto `MauiProgram.cs` y un par de ejemplos de `IApiService`/`MainViewModel` con la nueva estructura.