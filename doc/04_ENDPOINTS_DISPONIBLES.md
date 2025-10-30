# 📡 Endpoints Disponibles de la API

> **Nota:** Todos estos servicios ya están configurados y listos para usar en el proyecto

---

## 🔌 Servicios Registrados

Los siguientes servicios están registrados en `MauiProgram.cs` y pueden ser inyectados en cualquier ViewModel:

```csharp
private readonly IDispositivosService _dispositivosService;
private readonly IUsuariosService _usuariosService;
private readonly IAlertasService _alertasService;
private readonly IHistorialDispositivosService _historialService;
```

---

## 🔷 IDispositivosService

**Ubicación:** `Services/Interfaces/IDispositivosService.cs`

### Endpoints:

| Método HTTP | Endpoint | Método C# | Descripción |
|------------|----------|-----------|-------------|
| GET | `/api/dispositivos/getall` | `GetAllDispositivosAsync()` | Obtiene todos los dispositivos |
| GET | `/api/dispositivos/getbyid/{id}` | `GetDispositivoByIdAsync(int id)` | Obtiene un dispositivo por ID |
| POST | `/api/dispositivos/post` | `CreateDispositivoAsync(Dispositivo)` | Crea un nuevo dispositivo |

### Modelo Dispositivo:

```csharp
public class Dispositivo
{
    public int IdDispositivo { get; set; }
    public string SerialDispositivo { get; set; }     // Ej: "DEV-001"
    public string MAC { get; set; }                   // Ej: "00:1A:2B:3C:4D:5E"
    public string Firmware { get; set; }              // Ej: "v1.2.3"
    public string Direccion { get; set; }             // Ej: "Av. Principal 123"
    public double Latitud { get; set; }               // Ej: 13.6929
    public double Longitud { get; set; }              // Ej: -89.2182
    public DateTime Registro { get; set; }            // Fecha de registro
    public string Activo { get; set; }                // "Activo" o "Inactivo"
    public DateTime UltimaVista { get; set; }         // Última conexión
}
```

### Ejemplo de Uso:

```csharp
// En DispositivosViewModel.cs

private readonly IDispositivosService _dispositivosService;

public DispositivosViewModel(IDispositivosService dispositivosService)
{
    _dispositivosService = dispositivosService;
}

// Obtener todos los dispositivos
[RelayCommand]
private async Task CargarDispositivos()
{
    try
    {
        var dispositivos = await _dispositivosService.GetAllDispositivosAsync();

        Dispositivos.Clear();
        foreach (var disp in dispositivos)
        {
            Dispositivos.Add(disp);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// Obtener un dispositivo específico
[RelayCommand]
private async Task CargarDispositivo(int id)
{
    try
    {
        var dispositivo = await _dispositivosService.GetDispositivoByIdAsync(id);
        Dispositivo = dispositivo;
    }
    catch (Exception ex)
    {
        await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
    }
}

// Crear un nuevo dispositivo
[RelayCommand]
private async Task CrearDispositivo()
{
    var nuevoDispositivo = new Dispositivo
    {
        SerialDispositivo = "DEV-002",
        MAC = "00:1A:2B:3C:4D:5F",
        Firmware = "v1.2.3",
        Direccion = "Calle Secundaria 456",
        Latitud = 13.6929,
        Longitud = -89.2182,
        Registro = DateTime.Now,
        Activo = "Activo",
        UltimaVista = DateTime.Now
    };

    try
    {
        var created = await _dispositivosService.CreateDispositivoAsync(nuevoDispositivo);
        Dispositivos.Add(created);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error al crear: {ex.Message}");
    }
}
```

---

## 🔷 IUsuariosService

**Ubicación:** `Services/Interfaces/IUsuariosService.cs`

### Endpoints:

| Método HTTP | Endpoint | Método C# | Descripción |
|------------|----------|-----------|-------------|
| GET | `/api/usuarios/getall` | `GetAllUsuariosAsync()` | Obtiene todos los usuarios |
| GET | `/api/usuarios/getbyid/{id}` | `GetUsuarioByIdAsync(int id)` | Obtiene un usuario por ID |
| POST | `/api/usuarios/post` | `CreateUsuarioAsync(Usuario)` | Crea un nuevo usuario |

### Modelo Usuario:

```csharp
public class Usuario
{
    public int IdUsuario { get; set; }
    public string NombreUsuario { get; set; }         // Ej: "admin"
    public string Correo { get; set; }                // Ej: "admin@phanteon.com"
    public string PasswordHash { get; set; }          // Hash de la contraseña
    public string Rol { get; set; }                   // "Admin" o "Usuario"
}
```

### Ejemplo de Uso (Login):

```csharp
// En LoginViewModel.cs

private readonly IUsuariosService _usuariosService;

[ObservableProperty]
private string correo = string.Empty;

[ObservableProperty]
private string password = string.Empty;

public LoginViewModel(IUsuariosService usuariosService)
{
    _usuariosService = usuariosService;
}

[RelayCommand]
private async Task IniciarSesion()
{
    try
    {
        // Obtener todos los usuarios
        var usuarios = await _usuariosService.GetAllUsuariosAsync();

        // Buscar usuario por correo
        var usuario = usuarios.FirstOrDefault(u =>
            u.Correo.Equals(Correo, StringComparison.OrdinalIgnoreCase));

        if (usuario != null)
        {
            // En producción, verificar password hash aquí
            await Shell.Current.GoToAsync("///diagnostico");
        }
        else
        {
            MensajeError = "Correo o contraseña incorrectos";
        }
    }
    catch (Exception ex)
    {
        MensajeError = "Error al iniciar sesión";
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```

### Ejemplo de Uso (Registro):

```csharp
[RelayCommand]
private async Task RegistrarUsuario()
{
    var nuevoUsuario = new Usuario
    {
        NombreUsuario = NombreUsuario,
        Correo = Correo,
        PasswordHash = HashPassword(Password), // Implementar función de hash
        Rol = "Usuario"
    };

    try
    {
        var created = await _usuariosService.CreateUsuarioAsync(nuevoUsuario);
        await Toast.Make("Usuario registrado correctamente").Show();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```

---

## 🔷 IAlertasService

**Ubicación:** `Services/Interfaces/IAlertasService.cs`

### Endpoints:

| Método HTTP | Endpoint | Método C# | Descripción |
|------------|----------|-----------|-------------|
| GET | `/api/alertas/getall` | `GetAllAlertasAsync()` | Obtiene todas las alertas |
| GET | `/api/alertas/getbyid/{id}` | `GetAlertaByIdAsync(int id)` | Obtiene una alerta por ID |
| POST | `/api/alertas/post` | `CreateAlertaAsync(Alerta)` | Crea una nueva alerta |

### Modelo Alerta:

```csharp
public class Alerta
{
    public int IdAlerta { get; set; }
    public int IdDispositivo { get; set; }           // FK a Dispositivo
    public string TipoAlerta { get; set; }           // "Crítica", "Advertencia", "Info"
    public string Mensaje { get; set; }              // Descripción de la alerta
    public DateTime FechaHora { get; set; }          // Cuándo ocurrió
    public string Estado { get; set; }               // "Nueva", "Leída", "Resuelta"
}
```

### Ejemplo de Uso:

```csharp
// En AlertasViewModel.cs

private readonly IAlertasService _alertasService;

[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

public AlertasViewModel(IAlertasService alertasService)
{
    _alertasService = alertasService;
}

// Cargar todas las alertas
[RelayCommand]
private async Task CargarAlertas()
{
    try
    {
        var lista = await _alertasService.GetAllAlertasAsync();

        Alertas.Clear();
        foreach (var alerta in lista)
        {
            Alertas.Add(alerta);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// Filtrar alertas por tipo
[RelayCommand]
private async Task FiltrarPorTipo(string tipo)
{
    try
    {
        var todas = await _alertasService.GetAllAlertasAsync();

        if (tipo == "Todas")
        {
            AlertasFiltradas = new ObservableCollection<Alerta>(todas);
        }
        else
        {
            var filtradas = todas.Where(a => a.TipoAlerta == tipo).ToList();
            AlertasFiltradas = new ObservableCollection<Alerta>(filtradas);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// Obtener alertas de un dispositivo específico
public async Task<List<Alerta>> ObtenerAlertasDeDispositivo(int dispositivoId)
{
    try
    {
        var todas = await _alertasService.GetAllAlertasAsync();
        return todas.Where(a => a.IdDispositivo == dispositivoId).ToList();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        return new List<Alerta>();
    }
}
```

---

## 🔷 IHistorialDispositivosService

**Ubicación:** `Services/Interfaces/IHistorialDispositivosService.cs`

### Endpoints:

| Método HTTP | Endpoint | Método C# | Descripción |
|------------|----------|-----------|-------------|
| GET | `/api/historialdispositivos/getall` | `GetAllHistorialAsync()` | Obtiene todo el historial |
| GET | `/api/historialdispositivos/getbyid/{id}` | `GetHistorialByIdAsync(int id)` | Obtiene historial por ID |
| POST | `/api/historialdispositivos/post` | `CreateHistorialAsync(HistorialDispositivo)` | Crea registro de historial |

### Modelo HistorialDispositivo:

```csharp
public class HistorialDispositivo
{
    public int IdHistorial { get; set; }
    public int IdDispositivo { get; set; }           // FK a Dispositivo
    public string Evento { get; set; }               // Ej: "Conexión establecida"
    public DateTime FechaHora { get; set; }          // Cuándo ocurrió
    public string Detalles { get; set; }             // JSON con información adicional
}
```

### Ejemplo de Uso:

```csharp
// En DetalleDispositivoViewModel.cs

private readonly IHistorialDispositivosService _historialService;

[ObservableProperty]
private ObservableCollection<HistorialDispositivo> historial = new();

// Cargar historial de un dispositivo específico
public async Task CargarHistorialDispositivo(int dispositivoId)
{
    try
    {
        var todoHistorial = await _historialService.GetAllHistorialAsync();

        // Filtrar por dispositivo y ordenar por fecha
        var historialDispositivo = todoHistorial
            .Where(h => h.IdDispositivo == dispositivoId)
            .OrderByDescending(h => h.FechaHora)
            .Take(20) // Últimos 20 eventos
            .ToList();

        Historial.Clear();
        foreach (var evento in historialDispositivo)
        {
            Historial.Add(evento);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

// Crear un nuevo evento de historial
[RelayCommand]
private async Task RegistrarEvento()
{
    var nuevoEvento = new HistorialDispositivo
    {
        IdDispositivo = DispositivoId,
        Evento = "Firmware actualizado",
        FechaHora = DateTime.Now,
        Detalles = "{\"version_anterior\": \"v1.2.2\", \"version_nueva\": \"v1.2.3\"}"
    };

    try
    {
        await _historialService.CreateHistorialAsync(nuevoEvento);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}
```

-

Configurada en `Helpers/ApiConfiguration.cs`:

```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```

### Timeout:

```csharp
public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
```
---
### Manejo de SSL (DEBUG):

Configurado en `MauiProgram.cs` líneas 27-31:

```csharp
#if DEBUG
var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};
#endif
```

---

## 🚨 Manejo de Errores

### Errores Comunes:

#### HttpRequestException
```csharp
catch (HttpRequestException ex)
{
    // Error de red o servidor no disponible
    await Toast.Make("No se pudo conectar con el servidor").Show();
}
```

#### TaskCanceledException
```csharp
catch (TaskCanceledException ex)
{
    // Timeout (más de 30 segundos)
    await Toast.Make("La solicitud tardó demasiado").Show();
}
```

#### Exception General
```csharp
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
}
```

---

## 📚 Documentos Relacionados

- **[01_QUICK_START.md](01_QUICK_START.md)** - Inicio rápido
- **[02_CONFIGURACION_BACKEND.md](02_CONFIGURACION_BACKEND.md)** - Configurar backend
- **[03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md)** - División de trabajo
- **[06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)** - Solución de problemasS