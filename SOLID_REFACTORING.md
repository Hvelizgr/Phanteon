# Refactorización aplicando Principios SOLID

## Resumen de Cambios

Este documento describe cómo se aplicaron los principios SOLID a la aplicación Phanteon para mejorar la mantenibilidad, testabilidad y escalabilidad del código.

---

## Principios SOLID Aplicados

### 1. **S**ingle Responsibility Principle (SRP)
*"Una clase debe tener una sola razón para cambiar"*

#### Antes:
- `App.xaml.cs` contenía lógica de navegación y verificación de sesión
- `AuthService` manejaba tanto autenticación como almacenamiento de sesión
- `LoginViewModel` mezclaba lógica de UI, validación, almacenamiento y navegación

#### Después:
**Nuevos servicios creados:**

1. **`SessionManager`** (`Services/Auth/SessionManager.cs`)
   - **Responsabilidad única:** Gestionar el almacenamiento de la sesión del usuario
   - Encapsula todas las operaciones de lectura/escritura de datos de sesión
   - No conoce detalles de navegación ni autenticación

2. **`StartupNavigationService`** (`Services/Navigation/StartupNavigationService.cs`)
   - **Responsabilidad única:** Decidir y ejecutar la navegación inicial de la app
   - No se mezcla con la navegación general de la aplicación

3. **`App.xaml.cs`**
   - **Responsabilidad única:** Inicializar la aplicación
   - Delega la navegación inicial a `StartupNavigationService`

4. **`LoginViewModel`**
   - Método `SaveSessionDataAsync()` encapsula la lógica de guardar sesión
   - Separa claramente validación, llamada a API, almacenamiento y navegación

---

### 2. **O**pen/Closed Principle (OCP)
*"Abierto para extensión, cerrado para modificación"*

#### Implementación:
- **Interfaces bien definidas** permiten extender funcionalidad sin modificar código existente
- Si necesitas otro tipo de almacenamiento de sesión (ej. SQLite, Preferences), solo creas una nueva implementación de `ISessionManager`
- Si quieres cambiar la lógica de navegación inicial, solo modificas `StartupNavigationService`

**Ejemplo de extensión:**
```csharp
// Puedes crear una nueva implementación sin tocar SessionManager
public class CachedSessionManager : ISessionManager
{
    // Implementación con caché en memoria
}
```

---

### 3. **L**iskov Substitution Principle (LSP)
*"Los objetos deben poder ser reemplazados por sus subtipos sin alterar el funcionamiento"*

#### Implementación:
- Todas las interfaces (`ISessionManager`, `IAuthService`, `IStartupNavigationService`) pueden ser reemplazadas por implementaciones alternativas
- Los consumidores dependen de abstracciones, no de implementaciones concretas

**Ejemplo:**
```csharp
// Puedes sustituir SessionManager por cualquier implementación de ISessionManager
public class App : Application
{
    // Funciona con SessionManager, CachedSessionManager, o cualquier ISessionManager
    public App(IStartupNavigationService startupNav) { }
}
```

---

### 4. **I**nterface Segregation Principle (ISP)
*"Los clientes no deben depender de interfaces que no usan"*

#### Antes:
- No había separación clara de responsabilidades en interfaces

#### Después:
**Interfaces segregadas:**

1. **`ISessionManager`** - Solo operaciones de sesión
   ```csharp
   Task<bool> HasActiveSessionAsync();
   Task<string?> GetUserIdAsync();
   Task SaveSessionAsync(...);
   Task ClearSessionAsync();
   ```

2. **`IStartupNavigationService`** - Solo navegación inicial
   ```csharp
   Task NavigateToInitialPageAsync(Shell shell);
   ```

3. **`IAuthService`** - Solo operaciones de autenticación
   ```csharp
   Task<bool> IsUserLoggedInAsync();
   Task LogoutAsync();
   ```

**Ventaja:** Si un componente solo necesita verificar sesión, usa `ISessionManager`. Si solo necesita logout, usa `IAuthService`. No están forzados a depender de una interfaz grande.

---

### 5. **D**ependency Inversion Principle (DIP)
*"Depender de abstracciones, no de concreciones"*

#### Antes:
```csharp
public class App : Application
{
    private readonly ISecureStorageService _storage;

    // Lógica de navegación directamente en App
    var userId = await _storage.GetAsync("user_id");
    if (!string.IsNullOrEmpty(userId)) { ... }
}
```

#### Después:
```csharp
public class App : Application
{
    private readonly IStartupNavigationService _startupNav;

    // Depende de abstracción, no sabe cómo funciona internamente
    await _startupNav.NavigateToInitialPageAsync(shell);
}
```

**Todas las clases ahora dependen de abstracciones:**
- `App` → `IStartupNavigationService`
- `AuthService` → `ISessionManager` + `INavigationService`
- `LoginViewModel` → `ISessionManager` + `INavigationService` + `IUsuariosApi`
- `StartupNavigationService` → `ISessionManager`

---

## Archivos Creados/Modificados

### Archivos Nuevos:
1. `Services/Auth/ISessionManager.cs` - Interfaz para gestión de sesión
2. `Services/Auth/SessionManager.cs` - Implementación de gestión de sesión
3. `Services/Navigation/IStartupNavigationService.cs` - Interfaz para navegación inicial
4. `Services/Navigation/StartupNavigationService.cs` - Implementación de navegación inicial

### Archivos Modificados:
1. `App.xaml.cs` - Simplificado, usa DIP
2. `Services/Auth/AuthService.cs` - Ahora usa ISessionManager (SRP + DIP)
3. `Features/Auth/LoginViewModel.cs` - Mejor separación de responsabilidades
4. `MauiProgram.cs` - Registro de nuevas dependencias

---

## Beneficios de la Refactorización

### 1. **Testabilidad Mejorada**
- Cada servicio puede ser probado de forma aislada
- Fácil crear mocks de interfaces para tests unitarios

**Ejemplo de test:**
```csharp
[Test]
public async Task StartupNav_WhenUserLoggedIn_NavigatesToMain()
{
    // Arrange
    var mockSession = new Mock<ISessionManager>();
    mockSession.Setup(s => s.HasActiveSessionAsync()).ReturnsAsync(true);
    var service = new StartupNavigationService(mockSession.Object);

    // Act & Assert
    // ...
}
```

### 2. **Mantenibilidad**
- Cada clase tiene una responsabilidad clara
- Cambios en una parte no afectan otras partes

### 3. **Extensibilidad**
- Fácil agregar nuevas implementaciones (SQLite, caché, etc.)
- No requiere modificar código existente

### 4. **Legibilidad**
- Código auto-documentado con comentarios SOLID
- Nombres de clases/métodos describen exactamente qué hacen

---

## Cómo Usar

### Navegación Inicial
```csharp
// App.xaml.cs - Inyección de dependencias automática
public App(IStartupNavigationService startupNav)
{
    await startupNav.NavigateToInitialPageAsync(shell);
}
```

### Gestión de Sesión
```csharp
// En cualquier ViewModel
public MyViewModel(ISessionManager sessionManager)
{
    var userId = await sessionManager.GetUserIdAsync();
    await sessionManager.SaveSessionAsync(...);
}
```

### Autenticación
```csharp
// En ProfileViewModel
public ProfileViewModel(IAuthService authService)
{
    await authService.LogoutAsync(); // Limpia sesión y navega
}
```

---

## Próximos Pasos Recomendados

1. **Agregar validadores separados** (SRP)
   - Crear `ILoginValidator` para separar lógica de validación

2. **Implementar Repository Pattern**
   - Crear `IUserRepository` para abstraer acceso a datos

3. **Agregar tests unitarios**
   - Aprovechar la nueva estructura testeable

4. **Implementar Result Pattern**
   - Mejorar manejo de errores con `Result<T>`

---

## Referencias

- [SOLID Principles (Microsoft)](https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/architectural-principles#solid)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Dependency Injection in .NET MAUI](https://learn.microsoft.com/dotnet/maui/fundamentals/dependency-injection)
