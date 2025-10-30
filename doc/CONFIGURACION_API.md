# Configuración de la API para Phanteon

## ⚠️ IMPORTANTE: Clonar y configurar el Backend primero

### Paso 1: Clonar el repositorio del Backend API

```bash
# En una carpeta FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
```

### Paso 2: Configurar SQL Server

Edita `appsettings.Development.json` con tu configuración:

```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

**Opciones de configuración:**

1. **SQL Server local con autenticación de Windows:**
   ```
   Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;
   ```

2. **SQL Server local con usuario y contraseña:**
   ```
   Server=localhost;Database=DevicesDB;User Id=sa;Password=TuPassword;TrustServerCertificate=True;
   ```

3. **SQL Server Express:**
   ```
   Server=localhost\\SQLEXPRESS;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;
   ```

### Paso 3: Aplicar migraciones y crear la base de datos

```bash
# Instalar EF Core CLI (si no lo tienes)
dotnet tool install --global dotnet-ef

# Aplicar migraciones para crear la base de datos
dotnet ef database update
```

### Paso 4: Ejecutar la API

```bash
dotnet run
```

**Salida esperada:**
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### Paso 5: Verificar que la API funciona

**Opción 1 - Navegador:**
- Abre: `http://localhost:5000/api/dispositivos/getall`
- Deberías ver: `[]` (lista vacía) o datos si hay dispositivos

**Opción 2 - PowerShell/CMD:**
```bash
curl http://localhost:5000/api/dispositivos/getall
```

**Opción 3 - Swagger (si está habilitado):**
- Abre: `http://localhost:5000/swagger`

---

## Estructura Implementada

Se ha implementado la conexión a la base de datos a través de la API REST. La estructura incluye:

### 1. Modelos (carpeta Models/)
- `Usuario.cs` - Modelo de usuarios
- `Dispositivo.cs` - Modelo de dispositivos
- `Alerta.cs` - Modelo de alertas
- `HistorialDispositivo.cs` - Modelo de historial de dispositivos

### 2. Servicios (carpeta Services/Interfaces/)
- `IDispositivosService.cs` - Servicio para gestionar dispositivos
- `IAlertasService.cs` - Servicio para gestionar alertas
- `IUsuariosService.cs` - Servicio para gestionar usuarios
- `IHistorialDispositivosService.cs` - Servicio para gestionar historial

### 3. Configuración
- `ApiConfiguration.cs` - Configuración de la URL base de la API
- `MauiProgram.cs` - Registro de servicios con inyección de dependencias

## Configuración de la URL de la API en Phanteon

Una vez que el backend esté corriendo, configura la URL en tu app MAUI.

Edita el archivo `Helpers/ApiConfiguration.cs` según tu entorno:

```csharp
public static string BaseUrl { get; set; } = "http://10.0.2.2:5000";
```

### Opciones de URL según dónde ejecutes la app:

1. **Emulador Android (desarrollo local)**:
   ```csharp
   public static string BaseUrl { get; set; } = "http://10.0.2.2:5000";
   ```
   ℹ️ La IP `10.0.2.2` es una IP especial que en el emulador Android apunta al `localhost` de tu PC

2. **Dispositivo Android físico en la misma red WiFi**:
   ```csharp
   public static string BaseUrl { get; set; } = "http://192.168.1.XXX:5000";
   ```
   ⚠️ Reemplaza `192.168.1.XXX` con tu IP local. Para encontrarla:
   - Windows: `ipconfig` en CMD (busca "IPv4 Address")
   - Linux/Mac: `ifconfig` o `ip addr`

3. **Windows (app MAUI Windows)**:
   ```csharp
   public static string BaseUrl { get; set; } = "http://localhost:5000";
   ```

4. **iOS Simulator**:
   ```csharp
   public static string BaseUrl { get; set; } = "http://localhost:5000";
   ```

5. **Producción (servidor en la nube)**:
   ```csharp
   public static string BaseUrl { get; set; } = "https://tu-dominio.com";
   ```

### 🔍 Cómo saber qué URL usar:

| Ejecutas la app en... | URL a configurar |
|----------------------|------------------|
| Emulador Android | `http://10.0.2.2:5000` |
| Dispositivo Android real | `http://[TU_IP]:5000` |
| Windows Desktop | `http://localhost:5000` |
| iOS Simulator | `http://localhost:5000` |

## Cómo Usar los Servicios

### Ejemplo en un ViewModel o Página:

```csharp
using Phanteon.Services.Interfaces;
using Phanteon.Models;

namespace Phanteon.ViewModels
{
    public class DispositivosViewModel
    {
        private readonly IDispositivosService _dispositivosService;

        // Inyección de dependencias en el constructor
        public DispositivosViewModel(IDispositivosService dispositivosService)
        {
            _dispositivosService = dispositivosService;
        }

        // Obtener todos los dispositivos
        public async Task<List<Dispositivo>> ObtenerDispositivosAsync()
        {
            try
            {
                var dispositivos = await _dispositivosService.GetAllDispositivosAsync();
                return dispositivos;
            }
            catch (Exception ex)
            {
                // Manejar error
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Dispositivo>();
            }
        }

        // Obtener un dispositivo por ID
        public async Task<Dispositivo?> ObtenerDispositivoPorIdAsync(int id)
        {
            try
            {
                return await _dispositivosService.GetDispositivoByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        // Crear un nuevo dispositivo
        public async Task<Dispositivo?> CrearDispositivoAsync(Dispositivo dispositivo)
        {
            try
            {
                return await _dispositivosService.CreateDispositivoAsync(dispositivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
```

### Registrar ViewModels en MauiProgram.cs:

Agrega tus ViewModels al contenedor de DI:

```csharp
// En MauiProgram.cs, antes de return builder.Build();
builder.Services.AddTransient<DispositivosViewModel>();
builder.Services.AddTransient<AlertasViewModel>();
// etc...
```

## Configurar la API Backend

### 1. Configurar la conexión a SQL Server

Edita `Example_BD/DevicesAPI/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=[TU_SERVIDOR];Database=[TU_BASE_DATOS];User Id=[USUARIO];Password=[CONTRASEÑA];TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### 2. Ejecutar la API

```bash
cd Example_BD/DevicesAPI
dotnet run
```

La API se ejecutará en `http://localhost:5000` (o el puerto configurado).

### 3. Crear la base de datos con Entity Framework

```bash
cd Example_BD/DevicesAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Endpoints Disponibles

### Dispositivos
- `GET /api/dispositivos/getall` - Obtener todos los dispositivos
- `GET /api/dispositivos/getbyid/{id}` - Obtener dispositivo por ID
- `POST /api/dispositivos/post` - Crear nuevo dispositivo

### Alertas
- `GET /api/alertas/getall` - Obtener todas las alertas
- `GET /api/alertas/getbyid/{id}` - Obtener alerta por ID
- `POST /api/alertas/post` - Crear nueva alerta

### Usuarios
- `GET /api/usuarios/getall` - Obtener todos los usuarios
- `GET /api/usuarios/getbyid/{id}` - Obtener usuario por ID
- `POST /api/usuarios/post` - Crear nuevo usuario

### Historial de Dispositivos
- `GET /api/historialdispositivos/getall` - Obtener todo el historial
- `GET /api/historialdispositivos/getbyid/{id}` - Obtener historial por ID
- `POST /api/historialdispositivos/post` - Crear nuevo registro de historial

## Características Implementadas

- ✅ Refit para consumir APIs REST
- ✅ Polly para políticas de reintento automático (3 intentos con backoff exponencial)
- ✅ Inyección de dependencias configurada
- ✅ Modelos sincronizados con la API
- ✅ Timeout configurable (30 segundos por defecto)

## 🚀 Resumen: Pasos para Empezar a Trabajar

### Backend API (Solo una vez)
1. ✅ Clonar: `git clone https://github.com/epinto17/DevicesAPI.git`
2. ✅ Configurar SQL Server en `appsettings.Development.json`
3. ✅ Ejecutar: `dotnet ef database update`
4. ✅ Iniciar: `dotnet run`
5. ✅ Verificar: Abrir `http://localhost:5000/api/dispositivos/getall`

### App MAUI Phanteon
1. ✅ Configurar URL en `Helpers/ApiConfiguration.cs`
2. ⏳ Crear ViewModels que inyecten los servicios
3. ⏳ Crear páginas XAML
4. ⏳ Configurar navegación
5. ⏳ Implementar validaciones y manejo de errores

---

## ⚠️ Solución de Problemas Comunes

### Error: "Unable to connect to database"
- Verifica que SQL Server esté corriendo
- Verifica la cadena de conexión en `appsettings.Development.json`
- Intenta con `Trusted_Connection=True` para autenticación de Windows

### Error: "Connection refused" desde la app MAUI
- Verifica que la API esté corriendo (`dotnet run`)
- Verifica la URL en `ApiConfiguration.cs`
- Si usas emulador Android, usa `10.0.2.2` en lugar de `localhost`
- Si usas dispositivo físico, asegúrate de estar en la misma red WiFi

### Error: "No se pueden aplicar las migraciones"
- Instala EF Core CLI: `dotnet tool install --global dotnet-ef`
- Navega a la carpeta del proyecto API
- Ejecuta: `dotnet ef database update`

### Error: "Port 5000 already in use"
- Cambia el puerto en `Properties/launchSettings.json` del backend
- Actualiza la URL en `ApiConfiguration.cs` de Phanteon

---

## 📞 Contacto para Soporte

Si tienes problemas con:
- **Backend API**: Contactar al creador del repo DevicesAPI
- **Configuración de Phanteon**: Contactar a Héctor (000108304)
