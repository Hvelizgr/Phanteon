# 📢 LÉEME PRIMERO - Equipo Phanteon

> **De:** Héctor Eduardo Véliz Girón (Código: 000108304)
> **Para:** Equipo de desarrollo

---

## 👋 Resumen del Proyecto

Phanteon es un sistema de monitoreo de dispositivos IoT desarrollado con .NET MAUI que consume una API REST para gestionar dispositivos, alertas, usuarios e historial.

---

## ✅ LO QUE YA HICE (Héctor)

Ya implementé **toda la infraestructura base del proyecto**:

### Backend/API y Modelos
- ✅ Inicialización del proyecto .NET MAUI
- ✅ Configuración del repositorio Git
- ✅ Instalación de paquetes NuGet (Refit, Polly, CommunityToolkit)
- ✅ Creación de **4 modelos** (`Usuario`, `Dispositivo`, `Alerta`, `HistorialDispositivo`)
- ✅ Creación de **4 servicios** con Refit (`IUsuariosService`, `IDispositivosService`, `IAlertasService`, `IHistorialDispositivosService`)
- ✅ Configuración de API en `MauiProgram.cs` con:
  - Inyección de dependencias
  - Manejo de certificados SSL para desarrollo
  - Timeout de 30 segundos
- ✅ Helpers de configuración (`ApiConfiguration.cs`)
- ✅ Converters para XAML (`InvertedBoolConverter`, `StringNotEmptyConverter`)

### ViewModels y Páginas (Parcial)
- ✅ `DispositivosViewModel.cs` - Lista de dispositivos
- ✅ `DiagnosticoViewModel.cs` - Dashboard
- ✅ `DispositivosPage.xaml` - Interfaz de lista de dispositivos
- ✅ `DiagnosticoPage.xaml` - Interfaz de dashboard

**RESUMEN:** Backend 100% listo, Frontend 40% completo

---

## 🎯 LO QUE FALTA (Para el equipo - 3 personas)

### División de Trabajo Propuesta:

#### 👤 **Persona 1: ViewModels Faltantes**
**Archivos a crear:**
- `ViewModels/LoginViewModel.cs`
- `ViewModels/AlertasViewModel.cs`
- `ViewModels/DetalleDispositivoViewModel.cs`

**Tareas:**
- Crear propiedades observables para cada ViewModel
- Implementar comandos (RelayCommand)
- Consumir los servicios ya configurados
- Agregar validaciones

---

#### 👤 **Persona 2: Páginas XAML Faltantes**
**Archivos a crear:**
- `Views/LoginPage.xaml` + `LoginPage.xaml.cs`
- `Views/AlertasPage.xaml` + `AlertasPage.xaml.cs`
- `Views/DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`

**Tareas:**
- Crear interfaces de usuario con XAML
- Conectar con sus respectivos ViewModels
- Implementar bindings correctamente
- Agregar indicadores de carga

---

#### 👤 **Persona 3: Navegación y Validaciones**
**Archivos a modificar/crear:**
- `AppShell.xaml` - Configurar menú lateral y rutas
- `AppShell.xaml.cs` - Registrar rutas de navegación
- Agregar validaciones en todos los ViewModels
- Implementar manejo de errores con try-catch
- Verificar conectividad antes de llamadas API

**Tareas:**
- Configurar Shell con FlyoutMenu
- Implementar navegación entre páginas
- Agregar validaciones de formularios
- Manejo de errores y mensajes al usuario

---

## 📚 DOCUMENTACIÓN DISPONIBLE

| Archivo | Qué Contiene |
|---------|-------------|
| **[01_QUICK_START.md](01_QUICK_START.md)** | Guía rápida de 5 minutos para empezar |
| **[02_CONFIGURACION_BACKEND.md](02_CONFIGURACION_BACKEND.md)** | Cómo configurar el backend API |
| **[03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md)** | Tareas detalladas por persona |
| **[04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)** | Todos los endpoints y cómo usarlos |
| **[05_PAGINAS_MOCKUPS.md](05_PAGINAS_MOCKUPS.md)** | Mockups y ejemplos de código |
| **[06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)** | Solución a problemas frecuentes |
| **[07_TESTING_POSTMAN.md](07_TESTING_POSTMAN.md)** | Cómo probar la API con Postman |
| **[08_GUIA_COMMITS.md](08_GUIA_COMMITS.md)** | Qué commits hacer y cómo escribirlos |

---

## ⚡ INICIO RÁPIDO (5 minutos)

### Paso 1: Configurar el Backend API

```bash
# Clonar el backend (FUERA del proyecto Phanteon)
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI

# Configurar base de datos
# Editar appsettings.Development.json con tu SQL Server

# Crear la base de datos
dotnet ef database update

# Ejecutar API
dotnet run
```

### Paso 2: Verificar que funciona

Abre en el navegador: `https://localhost:7026/api/dispositivos/getall`

Si ves `[]`, ¡funciona! ✅

### Paso 3: Configurar Phanteon

La URL ya está configurada en `Helpers/ApiConfiguration.cs`:
```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```

**Importante:** Esta URL es para emulador Android. Si usas otra plataforma:
- Windows Desktop: `"https://localhost:7026"`
- Dispositivo Android físico: `"https://[TU_IP]:7026"`

---

## 🚦 ENDPOINTS YA CONFIGURADOS

Todos estos servicios ya están registrados y listos para usar:

### IDispositivosService
```csharp
Task<List<Dispositivo>> GetAllDispositivosAsync();
Task<Dispositivo> GetDispositivoByIdAsync(int id);
Task<Dispositivo> CreateDispositivoAsync([Body] Dispositivo dispositivo);
```

### IUsuariosService
```csharp
Task<List<Usuario>> GetAllUsuariosAsync();
Task<Usuario> GetUsuarioByIdAsync(int id);
Task<Usuario> CreateUsuarioAsync([Body] Usuario usuario);
```

### IAlertasService
```csharp
Task<List<Alerta>> GetAllAlertasAsync();
Task<Alerta> GetAlertaByIdAsync(int id);
Task<Alerta> CreateAlertaAsync([Body] Alerta alerta);
```

### IHistorialDispositivosService
```csharp
Task<List<HistorialDispositivo>> GetAllHistorialAsync();
Task<HistorialDispositivo> GetHistorialByIdAsync(int id);
Task<HistorialDispositivo> CreateHistorialAsync([Body] HistorialDispositivo historial);
```

---

## 📋 CHECKLIST DEL PROYECTO

### Infraestructura (Héctor) ✅
- [x] Proyecto inicializado
- [x] NuGet packages instalados
- [x] Modelos creados
- [x] Servicios configurados
- [x] Helpers y Converters
- [x] 2 ViewModels iniciales
- [x] 2 Páginas iniciales

### ViewModels Faltantes (Persona 1) ⏳
- [ ] LoginViewModel
- [ ] AlertasViewModel
- [ ] DetalleDispositivoViewModel

### Páginas Faltantes (Persona 2) ⏳
- [ ] LoginPage
- [ ] AlertasPage
- [ ] DetalleDispositivoPage

### Navegación y Validaciones (Persona 3) ⏳
- [ ] Configurar AppShell
- [ ] Registrar rutas
- [ ] Validaciones en ViewModels
- [ ] Manejo de errores
- [ ] Verificación de conectividad

---

## 🆘 ERRORES COMUNES Y SOLUCIONES

### ❌ Error: "Connection refused" o "Unable to connect"

**Causa:** El backend no está corriendo o la URL es incorrecta

**Solución:**
1. Verifica que el backend esté corriendo (`dotnet run` en DevicesAPI)
2. Verifica la URL en `ApiConfiguration.cs`
3. Si usas emulador Android: `https://10.0.2.2:7026`
4. Si usas Windows: `https://localhost:7026`

---

### ❌ Error: "SSL Certificate validation failed"

**Causa:** Certificado SSL de desarrollo no confiable

**Solución:** Ya está resuelto en `MauiProgram.cs` con:
```csharp
#if DEBUG
var httpClientHandler = new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
};
#endif
```

Esto **solo aplica en modo DEBUG**, en producción se validarán los certificados correctamente.

---

### ❌ Error: "Unable to connect to database" en el backend

**Solución:**
1. Verifica que SQL Server esté instalado y corriendo
2. Verifica la cadena de conexión en `appsettings.Development.json`
3. Ejecuta: `dotnet ef database update`

---

### ❌ Error: "No se puede resolver el servicio IXXXService"

**Solución:** Ya está registrado en `MauiProgram.cs`, pero si agregas nuevos ViewModels/Pages:
```csharp
builder.Services.AddTransient<TuViewModel>();
builder.Services.AddTransient<TuPage>();
```

---

## 📞 CONTACTO

**Héctor Eduardo Véliz Girón**
- Código: 000108304
- Rol: Inicialización del proyecto, backend, modelos, servicios, configuración
- Disponible para consultas sobre la API y configuración

---

## 🎯 OBJETIVO FINAL

Al completar el proyecto, la app debe:
- ✅ Login funcional con validación
- ✅ Lista de dispositivos desde la API
- ✅ Dashboard con estadísticas en tiempo real
- ✅ Lista de alertas con filtros
- ✅ Detalle completo de cada dispositivo
- ✅ Navegación fluida entre todas las páginas
- ✅ Manejo correcto de errores
- ✅ Indicadores de carga en operaciones

---

## 🚀 SIGUIENTE PASO

Lee el **[01_QUICK_START.md](01_QUICK_START.md)** para comenzar en 5 minutos.

Si tienes dudas, consulta los demás documentos en esta carpeta `/doc/`.

---

**¡Éxito equipo! 💪**

_Última actualización: 29/10/2024_
_Autor: Héctor Eduardo Véliz Girón_
