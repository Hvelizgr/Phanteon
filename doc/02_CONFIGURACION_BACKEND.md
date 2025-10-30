# 🔧 Configuración del Backend API

---

## 📋 Información del Backend

**Repositorio:** https://github.com/epinto17/DevicesAPI
**Puerto HTTPS:** 7026
**Puerto HTTP:** 5000

---

## 🚀 Instalación del Backend

### Paso 1: Clonar el repositorio

```bash
# Clonar en una carpeta FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
```

---

### Paso 2: Configurar SQL Server

El backend usa **Entity Framework Core** con SQL Server.

#### Editar `appsettings.Development.json`:

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

#### Opciones de configuración:

**SQL Server Local (Windows Authentication):**
```
Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;
```

**SQL Server Express:**
```
Server=localhost\\SQLEXPRESS;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;
```

**SQL Server con usuario/contraseña:**
```
Server=localhost;Database=DevicesDB;User Id=sa;Password=TuPassword123;TrustServerCertificate=True;
```

**Azure SQL Database:**
```
Server=tcp:tuservidor.database.windows.net,1433;Database=DevicesDB;User Id=admin@tuservidor;Password=TuPassword123;Encrypt=True;
```

---

### Paso 3: Instalar Entity Framework CLI

```bash
dotnet tool install --global dotnet-ef
```

Verificar instalación:
```bash
dotnet ef --version
```

Debe mostrar algo como: `Entity Framework Core .NET Command-line Tools 8.x.x`

---

### Paso 4: Crear la Base de Datos

```bash
# Asegúrate de estar en la carpeta DevicesAPI
cd DevicesAPI

# Aplicar migraciones
dotnet ef database update
```

**Salida exitosa:**
```
Build started...
Build succeeded.
Applying migration '20241001000000_InitialCreate'.
Done.
```

**Si hay errores:**
- Verifica que SQL Server esté corriendo
- Verifica la cadena de conexión
- Verifica que tengas permisos para crear bases de datos

---

### Paso 5: Ejecutar el Backend

```bash
dotnet run
```

**Salida esperada:**
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7026
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

✅ **El backend está corriendo correctamente**

---

### Paso 6: Verificar Endpoints

**Método 1 - Navegador:**

Abre en tu navegador:
- `https://localhost:7026/api/dispositivos/getall`
- `https://localhost:7026/api/alertas/getall`
- `https://localhost:7026/api/usuarios/getall`
- `https://localhost:7026/api/historialdispositivos/getall`

Debes ver: `[]` (array vacío, es correcto si no hay datos)

**Método 2 - PowerShell/CMD:**

```bash
curl https://localhost:7026/api/dispositivos/getall
curl https://localhost:7026/api/alertas/getall
```

**Método 3 - Swagger (si está habilitado):**

Abre: `https://localhost:7026/swagger`

---

## ⚙️ Configuración de Phanteon para Conectarse

### Ubicación de configuración:

Archivo: `Phanteon/Helpers/ApiConfiguration.cs`

```csharp
namespace Phanteon.Helpers
{
    public static class ApiConfiguration
    {
        // URL base de la API
        public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";

        // Timeout para las peticiones HTTP
        public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
    }
}
```

### Configuración según plataforma:

#### Emulador Android (Recomendado - Por defecto)
```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```
ℹ️ La IP `10.0.2.2` es una IP especial del emulador que apunta a `localhost` de tu PC

#### Windows Desktop
```csharp
public static string BaseUrl { get; set; } = "https://localhost:7026";
```

#### Dispositivo Android Físico (misma red WiFi)
```csharp
public static string BaseUrl { get; set; } = "https://192.168.1.XXX:7026";
```
⚠️ Reemplaza `192.168.1.XXX` con tu IP local

**Encontrar tu IP:**
```bash
# Windows
ipconfig
# Busca "Dirección IPv4" en tu adaptador de red WiFi

# Linux/Mac
ifconfig
```

**Permitir conexiones externas en el firewall (solo dispositivo físico):**
```bash
# Windows PowerShell (como Administrador)
netsh advfirewall firewall add rule name="DevicesAPI" dir=in action=allow protocol=TCP localport=7026
```

#### iOS Simulator (Mac)
```csharp
public static string BaseUrl { get; set; } = "https://localhost:7026";
```

---

## 🔒 Manejo de Certificados SSL

### Problema:
En desarrollo, el backend usa un certificado SSL autofirmado que no es confiable.

### Solución (Ya implementada):

En `MauiProgram.cs` líneas 27-31:

```csharp
#if DEBUG
var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};
#endif
```

Esto **desactiva la validación SSL solo en modo DEBUG**.

⚠️ **En producción (Release), los certificados se validan correctamente.**

---

## 🗄️ Estructura de la Base de Datos

El backend crea 4 tablas automáticamente:

### Tabla: Usuarios
```sql
IdUsuario INT PRIMARY KEY IDENTITY
NombreUsuario NVARCHAR(100)
Correo NVARCHAR(100)
PasswordHash NVARCHAR(500)
Rol NVARCHAR(50)
```

### Tabla: Dispositivos
```sql
IdDispositivo INT PRIMARY KEY IDENTITY
SerialDispositivo NVARCHAR(100)
MAC NVARCHAR(50)
Firmware NVARCHAR(50)
Direccion NVARCHAR(200)
Latitud FLOAT
Longitud FLOAT
Registro DATETIME
Activo NVARCHAR(20)
UltimaVista DATETIME
```

### Tabla: Alertas
```sql
IdAlerta INT PRIMARY KEY IDENTITY
IdDispositivo INT FOREIGN KEY
TipoAlerta NVARCHAR(50)
Mensaje NVARCHAR(500)
FechaHora DATETIME
Estado NVARCHAR(50)
```

### Tabla: HistorialDispositivos
```sql
IdHistorial INT PRIMARY KEY IDENTITY
IdDispositivo INT FOREIGN KEY
Evento NVARCHAR(200)
FechaHora DATETIME
Detalles NVARCHAR(MAX)
```

---

## 🧪 Probar el Backend (Insertar datos de prueba)

### Opción 1: SQL Server Management Studio (SSMS)

```sql
USE DevicesDB;

-- Insertar un usuario
INSERT INTO Usuarios (NombreUsuario, Correo, PasswordHash, Rol)
VALUES ('admin', 'admin@phanteon.com', 'hash123', 'Admin');

-- Insertar un dispositivo
INSERT INTO Dispositivos (SerialDispositivo, MAC, Firmware, Direccion, Latitud, Longitud, Registro, Activo, UltimaVista)
VALUES ('DEV-001', '00:1A:2B:3C:4D:5E', 'v1.2.3', 'Av. Principal 123', 13.6929, -89.2182, GETDATE(), 'Activo', GETDATE());

-- Insertar una alerta
INSERT INTO Alertas (IdDispositivo, TipoAlerta, Mensaje, FechaHora, Estado)
VALUES (1, 'Crítica', 'Batería baja', GETDATE(), 'Nueva');
```

### Opción 2: Postman/Insomnia (POST requests)

**Crear Dispositivo:**
```
POST https://localhost:7026/api/dispositivos/post
Content-Type: application/json

{
  "serialDispositivo": "DEV-002",
  "mac": "00:1A:2B:3C:4D:5F",
  "firmware": "v1.2.3",
  "direccion": "Calle Secundaria 456",
  "latitud": 13.6929,
  "longitud": -89.2182,
  "registro": "2024-10-29T10:00:00",
  "activo": "Activo",
  "ultimaVista": "2024-10-29T10:00:00"
}
```

---

## ⚠️ Solución de Problemas

### Error: "Unable to connect to database"

**Posibles causas:**
1. SQL Server no está corriendo
2. Cadena de conexión incorrecta
3. Permisos insuficientes

**Solución:**
```bash
# Verificar que SQL Server esté corriendo
# Windows: Services → SQL Server (MSSQLSERVER) → Running

# Probar conexión manualmente
sqlcmd -S localhost -E
# Si conecta, la cadena de conexión debe funcionar
```

---

### Error: "Port 7026 already in use"

**Solución:**

Cambiar el puerto en `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "https": {
      "applicationUrl": "https://localhost:7027;http://localhost:5001"
    }
  }
}
```

Actualizar en Phanteon (`ApiConfiguration.cs`):
```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7027";
```

---

### Error: "A connection was successfully established... but an error occurred"

**Causa:** Problema con certificado SSL

**Solución:** Ya está resuelto en `MauiProgram.cs` con el `HttpClientHandler`

---

### Error: "Timeout expired"

**Causa:** La API tarda demasiado en responder

**Solución:** Aumentar timeout en `ApiConfiguration.cs`:
```csharp
public static TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(60);
```

---

## 🔄 Workflow Diario

### Al iniciar el día:

```bash
# Terminal 1 - Backend
cd DevicesAPI
dotnet run
# Dejar corriendo

# Terminal 2 - Phanteon
cd Phanteon
start Phanteon.sln
```

### Al terminar:
- Ctrl+C en la terminal del backend para detenerlo
- Cerrar Visual Studio

---

## 📚 Documentos Relacionados

- **[01_QUICK_START.md](01_QUICK_START.md)** - Inicio rápido
- **[04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)** - Lista de endpoints
- **[06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)** - Más soluciones

---

_Actualizado: 29/10/2024_
