# 📢 LÉEME PRIMERO - Equipo Phanteon

> **De:** Héctor Eduardo Véliz Girón (Código: 000108304)
> **Para:** Equipo de desarrollo

---

## 👋 Resumen del Proyecto

Phanteon es un sistema de monitoreo de dispositivos IoT desarrollado con .NET MAUI que consume una API REST para gestionar dispositivos, alertas, usuarios e historial.

---

## ✅ (Infraestructura Coneccion API)

Ya implementé **toda la infraestructura backend y configuración del proyecto**:

### Backend/API y Modelos ✅
- ✅ Inicialización del proyecto .NET MAUI
- ✅ Configuración del repositorio Git
- ✅ Instalación de paquetes NuGet (Refit, Polly, CommunityToolkit)
- ✅ Creación de **4 modelos** (`Usuario`, `Dispositivo`, `Alerta`, `HistorialDispositivo`)
- ✅ Creación de **4 servicios** con Refit (`IUsuariosService`, `IDispositivosService`, `IAlertasService`, `IHistorialDispositivosService`)
- ✅ Configuración de API en `MauiProgram.cs` con:
  - Inyección de dependencias para los 4 servicios
  - Manejo de certificados SSL para desarrollo
  - Timeout de 30 segundos
- ✅ Helpers de configuración (`ApiConfiguration.cs`)
- ✅ Servicio de almacenamiento seguro (`SecureStorageService`)

### ViewModels de Referencia ✅
- ✅ `BaseViewModel.cs` - Clase base con propiedades comunes
- ✅ `DiagnosticoViewModel.cs` - Dashboard (estructura básica)
- ✅ `EjemploTesteoViewModel.cs` - Ejemplo completo de referencia
- ✅ `TestConexionApiViewModel.cs` - **SOLO para pruebas de conexión con la API**

### Páginas Básicas ✅
- ✅ `MainPage.xaml` - Página inicial básica
- ✅ `DiagnosticoPage.xaml` - Dashboard (estructura básica)

**⚠️ IMPORTANTE:** `TestConexionApiViewModel.cs` es SOLO una herramienta de debugging para verificar conexión con la API. NO debe usarse en producción.

**RESUMEN:** Infraestructura 100% lista, Frontend por completar

---

## 🎯 LO QUE FALTA (DIVISIÓN DE TRABAJO)

La infraestructura está 100% funcional y configurada.

### **📌 ESTRATEGIA: Cada persona crea su propia View + ViewModel completos**

Esto evita conflictos y permite trabajar independientemente.

---

### 👤 **Persona 1:**

**Archivos a crear:**
- `ViewModels/AlertasViewModel.cs`
- `Views/AlertasPage.xaml` + `AlertasPage.xaml.cs`

**Descripción:**
- Lista de alertas del sistema
- Filtros por tipo (Crítica, Advertencia, Info)
- Filtros por estado (Nueva, Leída, Resuelta)
- Ordenar por fecha
- Consumo de `IAlertasService`

**Referencia:** Ver `04_Ejemplos_Visuales.md` sección AlertasPage

---

### 👤 **Persona 2:**

**Archivos a crear:**
- `ViewModels/DetalleDispositivoViewModel.cs`
- `Views/DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`

**Descripción:**
- Detalle completo de un dispositivo específico
- Información general (Serial, MAC, Firmware, etc.)
- Historial de eventos del dispositivo
- Alertas asociadas al dispositivo
- Consumo de `IDispositivosService`, `IHistorialDispositivosService`, `IAlertasService`

**Referencia:** Ver `04_Ejemplos_Visuales.md` sección DetalleDispositivoPage

---

### 👤 **Persona 3:**

**Archivos a crear:**
- `ViewModels/DispositivosViewModel.cs` (CREAR desde cero)
- `Views/DispositivosPage.xaml` + `DispositivosPage.xaml.cs` (CREAR desde cero)
- `AppShell.xaml` + `AppShell.xaml.cs`
- Configurar navegación completa

**⚠️ IMPORTANTE:**
- **NO usar** `TestConexionApiViewModel.cs` como base (es solo para pruebas)
- Crear DispositivosViewModel **desde CERO** siguiendo el patrón de `EjemploTesteoViewModel.cs`

**Descripción:**
- Lista de dispositivos conectados
- SearchBar para filtrar por Serial o MAC
- Estadísticas (Activos/Inactivos/Total)
- Navegación a detalle de dispositivo
- Pull-to-refresh
- Configurar AppShell con menú lateral (Flyout)
- Implementar todas las rutas de navegación

**Referencia:** Ver `04_Ejemplos_Visuales.md` sección DispositivosPage (tiene mockup completo)

---

### 👤 **Héctor (Tarea Adicional):**

**Archivos a crear:**
- `ViewModels/LoginViewModel.cs`
- `Views/LoginPage.xaml` + `LoginPage.xaml.cs`

**Descripción:**
- Página de login con formulario
- Validaciones de correo y contraseña
- Navegación al dashboard después de login exitoso
- Consumo de `IUsuariosService`

**Referencia:** Ver `04_Ejemplos_Visuales.md` sección LoginPage

---

## 📚 DOCUMENTACIÓN DISPONIBLE

| Archivo | Qué Contiene |
|---------|-------------|
| **[02_Empezar_Aqui.md](02_Empezar_Aqui.md)** | Guía de 5 minutos para configurar todo |
| **[03_Tu_Tarea.md](03_Tu_Tarea.md)** | Tu asignación específica con checklist completo |
| **[04_Ejemplos_Visuales.md](04_Ejemplos_Visuales.md)** | Mockups y código de ejemplo para copiar |
| **[05_Guia_Rapida_API.md](05_Guia_Rapida_API.md)** | Comandos y bindings XAML para usar |
| **[06_Solucion_Problemas.md](06_Solucion_Problemas.md)** | Errores comunes y cómo resolverlos |
| **[07_Como_Hacer_Commits.md](07_Como_Hacer_Commits.md)** | Cómo escribir buenos commits |
| **[Postman/](Postman/)** | Colección para probar la API con Postman |

---

## ⚡ INICIO RÁPIDO

### Paso 1: Obtener Acceso al Backend API

**⚠️ IMPORTANTE:** La API **NO es parte de este proyecto**. Es un repositorio externo.

**Pasos para obtener acceso:**

1. **Solicitar autorización:**
   - Contactar a **Erick Pinto** (GitHub: @epinto17)
   - Solicitar acceso al repositorio: https://github.com/epinto17/DevicesAPI
   - **Esperar confirmación** antes de continuar

2. **Una vez autorizado, clonar el backend:**

```bash
# Clonar FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI

# Ejecutar (ya viene configurado)
dotnet run
```

### Paso 2: Verificar que funciona

Abre en el navegador: `https://localhost:7026/api/dispositivos/getall`

Si ves `[]`, ¡funciona! ✅

**Nota:** En `Docs/Postman/` hay una colección completa para probar todos los endpoints.

### Paso 3: Configurar Phanteon

La URL ya está configurada en `Helpers/ApiConfiguration.cs`:
```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```

**Importante:** Esta URL es para emulador Android. Si usas otra plataforma:
- Windows Desktop: `"https://localhost:7026"`
- Dispositivo Android físico: `"https://[TU_IP]:7026"`

### Paso 4: Probar Conexión (Opcional)

Si quieres verificar que la conexión funciona, puedes usar `TestConexionApiViewModel.cs` como referencia, pero recuerda que es SOLO para debugging.

---

## 🚦 SERVICIOS YA CONFIGURADOS Y LISTOS PARA USAR

Todos estos servicios están registrados en `MauiProgram.cs` y listos para inyectar:

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

**Nota:** La API actualmente solo soporta GET y POST. PUT y DELETE están preparados en el cliente pero no disponibles en la API aún.

---

## 📋 CHECKLIST DEL PROYECTO

### Infraestructura (Héctor) ✅
- [x] Proyecto inicializado
- [x] NuGet packages instalados
- [x] Modelos creados (4)
- [x] Servicios configurados (4)
- [x] Helpers y configuración API
- [x] Estructura base del proyecto
- [x] ViewModel de prueba de conexión

### Persona 1 ⏳
- [ ] AlertasViewModel completo
- [ ] AlertasPage completa
- [ ] Filtros por tipo y estado
- [ ] Registro en MauiProgram.cs

### Persona 2 ⏳
- [ ] DetalleDispositivoViewModel completo
- [ ] DetalleDispositivoPage completa
- [ ] Integración de 3 servicios
- [ ] Registro en MauiProgram.cs

### Persona 3 ⏳
- [ ] DispositivosViewModel desde cero
- [ ] DispositivosPage desde cero
- [ ] SearchBar y estadísticas
- [ ] AppShell con navegación
- [ ] Rutas registradas
- [ ] Validaciones en todos los ViewModels

### Héctor - Tarea Adicional ⏳
- [ ] LoginViewModel completo con validaciones
- [ ] LoginPage completa con formulario
- [ ] Registro en MauiProgram.cs

---

## 🆘 ERRORES COMUNES Y SOLUCIONES

**Para errores detallados, consulta [06_Solucion_Problemas.md](06_Solucion_Problemas.md)**

### ❌ Error: "Connection refused" o "Unable to connect"

**Causa:** El backend no está corriendo o la URL es incorrecta

**Solución:**
1. Verifica que el backend esté corriendo (`dotnet run` en DevicesAPI)
2. Verifica la URL en `ApiConfiguration.cs`
3. Si usas emulador Android: `https://10.0.2.2:7026`
4. Si usas Windows: `https://localhost:7026`

---

### ❌ Error: "No se puede resolver el servicio IXXXService"

**Solución:** Los servicios ya están registrados en `MauiProgram.cs`. Pero debes registrar tus ViewModels y Pages:
```csharp
// En MauiProgram.cs, después de los servicios Refit
builder.Services.AddTransient<LoginViewModel>();
builder.Services.AddTransient<LoginPage>();
builder.Services.AddTransient<AlertasViewModel>();
builder.Services.AddTransient<AlertasPage>();
builder.Services.AddTransient<DispositivosViewModel>();
builder.Services.AddTransient<DispositivosPage>();
builder.Services.AddTransient<DetalleDispositivoViewModel>();
builder.Services.AddTransient<DetalleDispositivoPage>();
```

---

## 🎯 OBJETIVO FINAL

Al completar el proyecto, la app debe tener:
- ✅ Login funcional con validación
- ✅ Lista de dispositivos desde la API con búsqueda
- ✅ Dashboard con estadísticas en tiempo real
- ✅ Lista de alertas con filtros
- ✅ Detalle completo de cada dispositivo
- ✅ Navegación fluida entre todas las páginas
- ✅ Manejo correcto de errores

---

## 🚀 SIGUIENTE PASO

1. Lee el **[02_Empezar_Aqui.md](02_Empezar_Aqui.md)** para comenzar en 5 minutos
2. Revisa **[03_Tu_Tarea.md](03_Tu_Tarea.md)** para tu asignación específica
3. Consulta **[04_Ejemplos_Visuales.md](04_Ejemplos_Visuales.md)** para ver ejemplos visuales y código

Si tienes dudas, consulta los demás documentos en esta carpeta `/Docs/`.

---

**¡Éxito con el desarrollo!** 🚀
