# 📢 LÉEME PRIMERO - Equipo Phanteon

> **Mensaje de Héctor para el equipo**

---

## 👋 Hola Equipo!

Ya implementé **toda la infraestructura del backend**, así que ustedes solo deben enfocarse en crear las interfaces (páginas XAML) y la lógica de presentación (ViewModels).

---

## ✅ ¿QUÉ YA ESTÁ HECHO?

- ✅ **Modelos de datos** → Clases listas en `Models/`
- ✅ **Servicios de API** → Ya pueden llamar a la API desde `Services/`
- ✅ **Configuración completa** → `MauiProgram.cs` ya tiene todo registrado
- ✅ **Backend API funcionando** → Endpoints listos para usar

**NO necesitan tocar nada de esto** ✋

---

## 🎯 ¿QUÉ DEBEN HACER USTEDES?

### 📖 OPCIÓN 1: Leer el Quick Start (RECOMENDADO)

👉 **[QUICK_START.md](QUICK_START.md)** ← ¡Empieza aquí en 5 minutos!

Este archivo te dice:
- Cómo configurar el backend en 5 pasos
- Cómo crear un ViewModel
- Cómo crear una Página XAML
- Problemas comunes y soluciones

---

### 📋 OPCIÓN 2: Ver las tareas asignadas

👉 **[INSTRUCCIONES_EQUIPO.md](INSTRUCCIONES_EQUIPO.md)**

Este archivo tiene:
- Los 4 pasos que deben completar
- Checklist de tareas
- Ejemplos de código

---

### 🎨 OPCIÓN 3: Ver mockups de las páginas

👉 **[PAGINAS_Y_ENDPOINTS.md](PAGINAS_Y_ENDPOINTS.md)**

Este archivo muestra:
- Cómo debe verse cada página (mockups ASCII)
- Qué endpoints usar en cada ViewModel
- Propiedades necesarias

---

## ⚡ INICIO RÁPIDO (3 comandos)

### 1️⃣ Clonar y configurar el backend:

```bash
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
dotnet ef database update
dotnet run
```

### 2️⃣ Verificar que funciona:

Abre en el navegador: `http://localhost:5000/api/dispositivos/getall`

### 3️⃣ Trabajar en Phanteon:

Abre `Phanteon.sln` en Visual Studio y empieza a crear ViewModels y Páginas.

---

## 🗂️ ARCHIVOS IMPORTANTES

| Archivo | Para qué sirve |
|---------|---------------|
| **[QUICK_START.md](QUICK_START.md)** | ⚡ Empezar rápido |
| **[INSTRUCCIONES_EQUIPO.md](INSTRUCCIONES_EQUIPO.md)** | 📋 Tareas del equipo |
| **[CONFIGURACION_API.md](CONFIGURACION_API.md)** | 🔧 Configurar backend |
| **[PAGINAS_Y_ENDPOINTS.md](PAGINAS_Y_ENDPOINTS.md)** | 🎨 Mockups y ejemplos |
| **[README.md](README.md)** | 📚 Documentación completa |

---

## 📊 DIVISIÓN DE TRABAJO (4 PERSONAS)

### 👤 Persona 1: ViewModels
Crear 5 ViewModels en `ViewModels/`:
- LoginViewModel.cs
- DispositivosViewModel.cs
- DetalleDispositivoViewModel.cs
- AlertasViewModel.cs
- DiagnosticoViewModel.cs

### 👤 Persona 2: Páginas XAML
Crear 5 páginas en `Views/`:
- LoginPage.xaml + .cs
- DispositivosPage.xaml + .cs
- DetalleDispositivoPage.xaml + .cs
- AlertasPage.xaml + .cs
- DiagnosticoPage.xaml + .cs

### 👤 Persona 3: Navegación
- Configurar AppShell.xaml
- Registrar rutas
- Probar navegación entre páginas

### 👤 Persona 4: Validaciones y Errores
- Agregar validaciones en ViewModels
- Verificar conectividad
- Manejo de errores con try-catch
- Mensajes al usuario

---

## 🚦 ENDPOINTS DISPONIBLES

Ya están implementados y funcionando:

### Dispositivos
```csharp
await _dispositivosService.GetAllDispositivosAsync();
await _dispositivosService.GetDispositivoByIdAsync(id);
await _dispositivosService.CreateDispositivoAsync(dispositivo);
```

### Usuarios (para Login)
```csharp
await _usuariosService.GetAllUsuariosAsync();
await _usuariosService.GetUsuarioByIdAsync(id);
await _usuariosService.CreateUsuarioAsync(usuario);
```

### Alertas
```csharp
await _alertasService.GetAllAlertasAsync();
await _alertasService.GetAlertaByIdAsync(id);
await _alertasService.CreateAlertaAsync(alerta);
```

### Historial
```csharp
await _historialService.GetAllHistorialAsync();
await _historialService.GetHistorialByIdAsync(id);
await _historialService.CreateHistorialAsync(historial);
```

---

## ⚠️ IMPORTANTE - NO MODIFICAR

🚫 **NO modifiquen estos archivos:**
- Carpeta `Models/`
- Carpeta `Services/`
- `ApiConfiguration.cs` (excepto la URL si es necesario)
- `MauiProgram.cs` (excepto para registrar sus ViewModels/Pages)

✅ **Solo creen archivos nuevos:**
- ViewModels en `ViewModels/`
- Páginas en `Views/`
- Modificar `AppShell.xaml` para navegación

---

## 🆘 ¿NECESITAS AYUDA?

### Si tienes dudas sobre:
- **Cómo usar los endpoints** → Lee [CONFIGURACION_API.md](CONFIGURACION_API.md)
- **Cómo crear un ViewModel** → Lee [QUICK_START.md](QUICK_START.md)
- **Cómo debe verse una página** → Lee [PAGINAS_Y_ENDPOINTS.md](PAGINAS_Y_ENDPOINTS.md)
- **Problemas con el backend** → Contactar a Héctor

### Problemas comunes:
1. **"Connection refused"** → El backend no está corriendo (`dotnet run`)
2. **"Unable to connect to database"** → Verifica SQL Server
3. **ViewModel no se inyecta** → Falta registrarlo en `MauiProgram.cs`

---

## 📅 CRONOGRAMA SUGERIDO

### Semana 1:
- ✅ Configurar backend (todos)
- ✅ Crear LoginViewModel + LoginPage
- ✅ Probar que funciona el login

### Semana 2:
- ✅ Crear DispositivosViewModel + DispositivosPage
- ✅ Crear DiagnosticoViewModel + DiagnosticoPage
- ✅ Configurar navegación básica

### Semana 3:
- ✅ Crear AlertasViewModel + AlertasPage
- ✅ Crear DetalleDispositivoViewModel + DetalleDispositivoPage
- ✅ Agregar validaciones

### Semana 4:
- ✅ Pruebas completas
- ✅ Corrección de errores
- ✅ Preparar presentación

---

## 🎓 ORDEN RECOMENDADO DE IMPLEMENTACIÓN

1. **LoginPage** (más simple)
2. **DispositivosPage** (lista básica)
3. **DiagnosticoPage** (dashboard)
4. **AlertasPage** (filtros)
5. **DetalleDispositivoPage** (más complejo)

---

## 📞 CONTACTO

**Héctor Eduardo Véliz Girón**
- Código: 000108304
- Rol: Desarrollador Backend & API
- Responsable de: Modelos, Servicios, API

---

## 🎯 OBJETIVO FINAL

Al terminar, la app debe:
- ✅ Mostrar login funcional
- ✅ Listar dispositivos desde la API
- ✅ Mostrar detalle de cada dispositivo
- ✅ Listar alertas
- ✅ Mostrar dashboard con estadísticas
- ✅ Navegar entre todas las páginas
- ✅ Manejar errores correctamente

---

## 🚀 ¡EMPECEMOS!

**Paso siguiente:** Lee el **[QUICK_START.md](QUICK_START.md)**

Si tienes dudas, escríbeme. ¡Éxito! 💪

---

_Última actualización: 29/10/2024_
_Creado por: Héctor Eduardo Véliz Girón_
