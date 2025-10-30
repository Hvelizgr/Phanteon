# Instrucciones Rápidas - Conexión a Base de Datos

## ¿Qué se ha implementado?

Se ha configurado completamente la conexión entre tu aplicación MAUI (Phanteon) y la base de datos SQL Server a través de la API REST proporcionada en `Example_BD`.

## Estructura Implementada

```
Phanteon/
├── Models/                          # Modelos de datos
│   ├── Usuario.cs
│   ├── Dispositivo.cs
│   ├── Alerta.cs
│   └── HistorialDispositivo.cs
│
├── Services/Interfaces/             # Servicios para consumir la API
│   ├── IDispositivosService.cs
│   ├── IAlertasService.cs
│   ├── IUsuariosService.cs
│   └── IHistorialDispositivosService.cs
│
├── ViewModels/                      # ViewModels de ejemplo
│   └── DispositivosViewModel.cs
│
├── Views/                           # Páginas de ejemplo
│   ├── DispositivosPage.xaml
│   └── DispositivosPage.xaml.cs
│
└── Helpers/
    └── ApiConfiguration.cs          # Configuración de URL de la API
```

## Pasos para Empezar

### 1. Configurar la Base de Datos (Primero)

#### Opción A: Usar SQL Server local

1. Abre `Example_BD/DevicesAPI/appsettings.Development.json`
2. Configura la cadena de conexión:

```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=PhanteonDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. Crea la base de datos con Entity Framework:
```bash
cd Example_BD/DevicesAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### Opción B: Usar SQL Server Express o SQL Server existente

1. Usa esta cadena de conexión en `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=[TU_SERVIDOR];Database=PhanteonDB;User Id=[USUARIO];Password=[CONTRASEÑA];TrustServerCertificate=True;"
  }
}
```

### 2. Ejecutar la API Backend

```bash
cd Example_BD/DevicesAPI
dotnet run
```

La API se ejecutará en `http://localhost:5000` (o el puerto que se muestre en la consola).

### 3. Configurar la URL en Phanteon

Abre `Helpers/ApiConfiguration.cs` y ajusta la URL según tu entorno:

```csharp
// Para emulador Android
public static string BaseUrl { get; set; } = "http://10.0.2.2:5000";

// Para dispositivo físico en la misma red (cambia por tu IP)
// public static string BaseUrl { get; set; } = "http://192.168.1.100:5000";

// Para producción
// public static string BaseUrl { get; set; } = "https://tu-api.com";
```

**Importante:** Para obtener tu IP local:
- Windows: `ipconfig` (busca "Dirección IPv4")
- Mac/Linux: `ifconfig` o `ip addr`

### 4. Ejecutar la Aplicación Phanteon

```bash
dotnet build
# Luego ejecuta desde Visual Studio o VS Code
```

## Cómo Usar los Servicios en tu Código

### Ejemplo 1: En un ViewModel

```csharp
using Phanteon.Services.Interfaces;
using Phanteon.Models;

public class MiViewModel
{
    private readonly IDispositivosService _dispositivosService;

    // El servicio se inyecta automáticamente
    public MiViewModel(IDispositivosService dispositivosService)
    {
        _dispositivosService = dispositivosService;
    }

    public async Task CargarDatosAsync()
    {
        try
        {
            var dispositivos = await _dispositivosService.GetAllDispositivosAsync();
            // Usar los dispositivos...
        }
        catch (Exception ex)
        {
            // Manejar error
        }
    }
}
```

### Ejemplo 2: En una Página Code-Behind

```csharp
using Phanteon.Services.Interfaces;

public partial class MiPagina : ContentPage
{
    private readonly IAlertasService _alertasService;

    public MiPagina(IAlertasService alertasService)
    {
        InitializeComponent();
        _alertasService = alertasService;
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        var alertas = await _alertasService.GetAllAlertasAsync();
        // Mostrar alertas...
    }
}
```

## Servicios Disponibles

### IDispositivosService
- `GetAllDispositivosAsync()` - Obtener todos los dispositivos
- `GetDispositivoByIdAsync(int id)` - Obtener dispositivo por ID
- `CreateDispositivoAsync(Dispositivo)` - Crear nuevo dispositivo

### IAlertasService
- `GetAllAlertasAsync()` - Obtener todas las alertas
- `GetAlertaByIdAsync(int id)` - Obtener alerta por ID
- `CreateAlertaAsync(Alerta)` - Crear nueva alerta

### IUsuariosService
- `GetAllUsuariosAsync()` - Obtener todos los usuarios
- `GetUsuarioByIdAsync(int id)` - Obtener usuario por ID
- `CreateUsuarioAsync(Usuario)` - Crear nuevo usuario

### IHistorialDispositivosService
- `GetAllHistorialDispositivosAsync()` - Obtener todo el historial
- `GetHistorialDispositivoByIdAsync(int id)` - Obtener historial por ID
- `CreateHistorialDispositivoAsync(HistorialDispositivo)` - Crear nuevo historial

## Registrar Nuevos ViewModels o Páginas

Abre `MauiProgram.cs` y agrega:

```csharp
// Registrar ViewModels
builder.Services.AddTransient<MiViewModel>();

// Registrar Páginas
builder.Services.AddTransient<Views.MiPagina>();
```

## Verificar que Todo Funciona

1. La API debe estar corriendo en `http://localhost:5000`
2. Puedes probar la API directamente en el navegador:
   - `http://localhost:5000/api/dispositivos/getall`
   - `http://localhost:5000/api/alertas/getall`
   - `http://localhost:5000/api/usuarios/getall`

3. Si usas Swagger (en desarrollo), accede a:
   - `http://localhost:5000/swagger`

## Solución de Problemas

### Error: "No se puede conectar a la API"
- Verifica que la API esté ejecutándose
- Verifica la URL en `ApiConfiguration.cs`
- Si usas emulador: usa `http://10.0.2.2:5000`
- Si usas dispositivo físico: usa tu IP local

### Error: "No se puede conectar a SQL Server"
- Verifica que SQL Server esté ejecutándose
- Verifica la cadena de conexión en `appsettings.Development.json`
- Asegúrate de que la base de datos existe (ejecuta `dotnet ef database update`)

### Error: "Cannot resolve service"
- Asegúrate de que el servicio esté registrado en `MauiProgram.cs`
- Asegúrate de que el ViewModel/Página también esté registrado

## Próximos Pasos

1. Personaliza los modelos según tus necesidades
2. Agrega métodos UPDATE y DELETE a los servicios
3. Implementa autenticación si es necesario
4. Agrega manejo de errores más robusto
5. Implementa caché local si lo necesitas

## Documentación Completa

Para más detalles, consulta `CONFIGURACION_API.md`.
