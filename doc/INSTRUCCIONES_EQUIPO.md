# 🚀 INSTRUCCIONES RÁPIDAS PARA EL EQUIPO

> **Para:** Compañeros de Héctor
> **De:** Héctor Eduardo Véliz Girón
> **Proyecto:** Phanteon - Sistema de Monitoreo IoT

---

## ⚠️ ANTES DE EMPEZAR: Configurar el Backend API

### Paso 1: Clonar el Backend API

```bash
# En una carpeta FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
```

### Paso 2: Configurar la Base de Datos

1. Abre `appsettings.Development.json`
2. Configura tu cadena de conexión de SQL Server:

```json
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Paso 3: Crear la Base de Datos

```bash
# Instalar Entity Framework CLI (si no lo tienes)
dotnet tool install --global dotnet-ef

# Crear la base de datos
dotnet ef database update
```

### Paso 4: Ejecutar el Backend API

```bash
dotnet run
```

La API correrá en: `http://localhost:5000` (o el puerto que indique)

### Paso 5: Verificar que funciona

Abre en el navegador: `http://localhost:5000/api/dispositivos/getall`

Si ves `[]` o una lista de dispositivos, ¡la API funciona! ✅

---

## ✅ LO QUE YA ESTÁ HECHO (No toquen esto)

Héctor ya implementó:
- ✅ Modelos de datos (`Models/`)
- ✅ Servicios de API (`Services/Interfaces/`)
- ✅ Configuración de API y DI (`MauiProgram.cs`)
- ✅ Backend API con endpoints funcionales
- ✅ Conexión configurada a la API

**TODO EL BACKEND ESTÁ LISTO Y FUNCIONA ✅**

---

## 🎯 LO QUE DEBEN HACER (4 PASOS)

### PASO 1: ViewModels (Lógica de Presentación)
**Responsable:** _[Nombre]_

**Archivos a crear en `ViewModels/`:**
1. `LoginViewModel.cs`
2. `DispositivosViewModel.cs`
3. `DetalleDispositivoViewModel.cs`
4. `AlertasViewModel.cs`
5. `DiagnosticoViewModel.cs`

**Ejemplo base:** Ver README.md sección "PASO 1"

**Registrar en MauiProgram.cs:**
```csharp
builder.Services.AddTransient<LoginViewModel>();
builder.Services.AddTransient<DispositivosViewModel>();
builder.Services.AddTransient<DetalleDispositivoViewModel>();
builder.Services.AddTransient<AlertasViewModel>();
builder.Services.AddTransient<DiagnosticoViewModel>();
```

---

### PASO 2: Páginas XAML (Interfaz)
**Responsable:** _[Nombre]_

**Archivos a crear en `Views/`:**
1. `LoginPage.xaml` + `LoginPage.xaml.cs`
2. `DispositivosPage.xaml` + `DispositivosPage.xaml.cs`
3. `DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`
4. `AlertasPage.xaml` + `AlertasPage.xaml.cs`
5. `DiagnosticoPage.xaml` + `DiagnosticoPage.xaml.cs`

**Ejemplo base:** Ver README.md sección "PASO 2"

**Registrar en MauiProgram.cs:**
```csharp
builder.Services.AddTransient<LoginPage>();
builder.Services.AddTransient<DispositivosPage>();
builder.Services.AddTransient<DetalleDispositivoPage>();
builder.Services.AddTransient<AlertasPage>();
builder.Services.AddTransient<DiagnosticoPage>();
```

---

### PASO 3: Navegación
**Responsable:** _[Nombre]_

**Archivo a modificar:** `AppShell.xaml`

Agregar:
- Menú lateral con 3 opciones (Dashboard, Dispositivos, Alertas)
- Ruta para DetalleDispositivoPage

**Ejemplo completo:** Ver README.md sección "PASO 3"

---

### PASO 4: Validaciones y Errores
**Responsable:** _[Nombre]_

**Tareas:**
1. Agregar validaciones en LoginViewModel (correo, password)
2. Verificar conectividad antes de llamar APIs
3. Manejo de errores con try-catch
4. Mensajes amigables al usuario

**Ejemplo completo:** Ver README.md sección "PASO 4"

---

## 📡 ENDPOINTS DISPONIBLES (YA FUNCIONAN)

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

## 📋 CHECKLIST DE TAREAS

### ViewModels (PASO 1)
- [ ] LoginViewModel.cs creado
- [ ] DispositivosViewModel.cs creado
- [ ] DetalleDispositivoViewModel.cs creado
- [ ] AlertasViewModel.cs creado
- [ ] DiagnosticoViewModel.cs creado
- [ ] Todos registrados en MauiProgram.cs

### Páginas XAML (PASO 2)
- [ ] LoginPage.xaml + .cs creados
- [ ] DispositivosPage.xaml + .cs creados
- [ ] DetalleDispositivoPage.xaml + .cs creados
- [ ] AlertasPage.xaml + .cs creados
- [ ] DiagnosticoPage.xaml + .cs creados
- [ ] Todas registradas en MauiProgram.cs

### Navegación (PASO 3)
- [ ] AppShell.xaml configurado con menú lateral
- [ ] Rutas registradas en AppShell.xaml.cs
- [ ] Navegación entre páginas funciona

### Validaciones (PASO 4)
- [ ] Validaciones en LoginViewModel
- [ ] Verificación de conectividad
- [ ] Try-catch en todos los métodos async
- [ ] Mensajes de error implementados

---

## 🆘 ¿NECESITAS AYUDA?

1. **Lee el README.md completo** - Tiene ejemplos de código
2. **Revisa CONFIGURACION_API.md** - Info sobre los endpoints
3. **Consulta con Héctor** - Para dudas sobre la API
4. **Revisa el código existente** - MainPage.xaml y DiagnosticoPage.xaml ya están hechos como ejemplo

---

## 📞 CONTACTO

**Héctor Eduardo Véliz Girón**
- Código: 000108304
- Rol: Backend & API Developer

---

## ⚠️ IMPORTANTE

- NO modifiquen la carpeta `Models/` - Ya está completa
- NO modifiquen la carpeta `Services/` - Ya está completa
- NO modifiquen `MauiProgram.cs` (excepto para registrar sus ViewModels/Pages)
- NO modifiquen `ApiConfiguration.cs` - Ya está configurado

**Solo creen ViewModels, Pages y configuren navegación ✅**

---

## 🎯 FECHA DE ENTREGA

**Fecha límite:** _____/_____/_____

**Reunión de revisión:** _____/_____/_____ a las _____

---

**¡MUCHA SUERTE! 🚀**
