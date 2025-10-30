# 📱 PÁGINAS A CREAR Y ENDPOINTS A USAR

---

## 🔵 PÁGINA 1: LoginPage (Inicio de Sesión)

### 📋 Información
- **ViewModel:** `LoginViewModel.cs`
- **View:** `LoginPage.xaml` + `LoginPage.xaml.cs`
- **Servicio:** `IUsuariosService`

### 🎨 Componentes de UI
```
┌──────────────────────────────────┐
│      PHANTEON - LOGIN            │
├──────────────────────────────────┤
│                                  │
│  ╔════════════════════════════╗  │
│  ║ Correo Electrónico         ║  │ ← Entry binding Correo
│  ╚════════════════════════════╝  │
│                                  │
│  ╔════════════════════════════╗  │
│  ║ ••••••••••                 ║  │ ← Entry binding Password (IsPassword="True")
│  ╚════════════════════════════╝  │
│                                  │
│  ⚠️ Error: correo inválido        │ ← Label binding MensajeError (IsVisible cuando hay error)
│                                  │
│  ┌──────────────────────────┐   │
│  │   INICIAR SESIÓN         │   │ ← Button command IniciarSesionCommand
│  └──────────────────────────┘   │
│                                  │
│         ⏳ Cargando...           │ ← ActivityIndicator binding EstaCargando
│                                  │
└──────────────────────────────────┘
```

### 🔌 Endpoints usados
```csharp
// 1. Obtener todos los usuarios
var usuarios = await _usuariosService.GetAllUsuariosAsync();

// 2. Buscar usuario por correo
var usuario = usuarios.FirstOrDefault(u => u.Correo.Equals(correoIngresado, StringComparison.OrdinalIgnoreCase));

// 3. Si existe, navegar a dashboard
if (usuario != null)
{
    await Shell.Current.GoToAsync("///dashboard");
}
```

### ✅ Validaciones
- [x] Correo no vacío
- [x] Correo contiene @
- [x] Password no vacío
- [x] Password mínimo 6 caracteres
- [x] Verificar conexión a internet

---

## 🔵 PÁGINA 2: DispositivosPage (Lista de Dispositivos)

### 📋 Información
- **ViewModel:** `DispositivosViewModel.cs`
- **View:** `DispositivosPage.xaml` + `DispositivosPage.xaml.cs`
- **Servicio:** `IDispositivosService`

### 🎨 Componentes de UI
```
┌──────────────────────────────────┐
│   DISPOSITIVOS      [🔄 Actualizar]│
├──────────────────────────────────┤
│                                  │
│  ┌────────────────────────────┐ │
│  │ 📟 DEV-001                 │ │ ← SerialDispositivo
│  │ 📍 Av. Principal 123       │ │ ← Direccion
│  │ 🔗 00:1A:2B:3C:4D:5E      │ │ ← MAC
│  │ ✅ Activo                  │ │ ← Activo
│  │           [Ver Detalle →]  │ │ ← Button VerDetalleCommand
│  └────────────────────────────┘ │
│                                  │
│  ┌────────────────────────────┐ │
│  │ 📟 DEV-002                 │ │
│  │ 📍 Calle Secundaria 456    │ │
│  │ 🔗 00:1A:2B:3C:4D:5F      │ │
│  │ ❌ Inactivo                │ │
│  │           [Ver Detalle →]  │ │
│  └────────────────────────────┘ │
│                                  │
│         ⏳ Cargando...           │
│                                  │
└──────────────────────────────────┘
```

### 🔌 Endpoints usados
```csharp
// 1. Cargar todos los dispositivos
[RelayCommand]
private async Task CargarDispositivos()
{
    var lista = await _dispositivosService.GetAllDispositivosAsync();

    Dispositivos.Clear();
    foreach (var dispositivo in lista)
    {
        Dispositivos.Add(dispositivo);
    }
}

// 2. Navegar a detalle
[RelayCommand]
private async Task VerDetalle(Dispositivo dispositivo)
{
    await Shell.Current.GoToAsync($"detalleDispositivo?id={dispositivo.IdDispositivo}");
}
```

### 📊 Propiedades del ViewModel
```csharp
[ObservableProperty]
private ObservableCollection<Dispositivo> dispositivos = new();

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private Dispositivo? dispositivoSeleccionado;
```

---

## 🔵 PÁGINA 3: DetalleDispositivoPage (Detalle de Dispositivo)

### 📋 Información
- **ViewModel:** `DetalleDispositivoViewModel.cs`
- **View:** `DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`
- **Servicios:** `IDispositivosService`, `IHistorialDispositivosService`, `IAlertasService`

### 🎨 Componentes de UI
```
┌──────────────────────────────────┐
│   DETALLE DISPOSITIVO [🔄 Actualizar]│
├──────────────────────────────────┤
│                                  │
│  📟 INFORMACIÓN GENERAL          │
│  ┌────────────────────────────┐ │
│  │ Serial: DEV-001            │ │
│  │ MAC: 00:1A:2B:3C:4D:5E     │ │
│  │ Firmware: v1.2.3           │ │
│  │ Dirección: Av. Principal   │ │
│  │ Estado: ✅ Activo           │ │
│  │ Registro: 01/01/2024       │ │
│  │ Última vista: Hace 5 min   │ │
│  └────────────────────────────┘ │
│                                  │
│  📍 UBICACIÓN                    │
│  ┌────────────────────────────┐ │
│  │ Lat: 13.6929               │ │
│  │ Lon: -89.2182              │ │
│  │ [🗺️ Ver en Mapa]           │ │
│  └────────────────────────────┘ │
│                                  │
│  📜 HISTORIAL RECIENTE           │
│  ┌────────────────────────────┐ │
│  │ • Conexión establecida     │ │
│  │   15/10/2024 10:30 AM      │ │
│  ├────────────────────────────┤ │
│  │ • Firmware actualizado     │ │
│  │   14/10/2024 09:15 AM      │ │
│  └────────────────────────────┘ │
│                                  │
│  🚨 ALERTAS ACTIVAS              │
│  ┌────────────────────────────┐ │
│  │ 🔴 Batería baja (15%)      │ │
│  │    Hace 2 horas            │ │
│  └────────────────────────────┘ │
│                                  │
└──────────────────────────────────┘
```

### 🔌 Endpoints usados
```csharp
// 1. Obtener dispositivo por ID (del parámetro de navegación)
var dispositivo = await _dispositivosService.GetDispositivoByIdAsync(dispositivoId);

// 2. Obtener historial del dispositivo
var todosHistoriales = await _historialService.GetAllHistorialAsync();
var historialDispositivo = todosHistoriales
    .Where(h => h.IdDispositivo == dispositivoId)
    .OrderByDescending(h => h.FechaHora)
    .Take(10)
    .ToList();

// 3. Obtener alertas del dispositivo
var todasAlertas = await _alertasService.GetAllAlertasAsync();
var alertasDispositivo = todasAlertas
    .Where(a => a.IdDispositivo == dispositivoId && a.Estado != "Resuelta")
    .ToList();
```

### 📊 Propiedades del ViewModel
```csharp
[ObservableProperty]
private Dispositivo? dispositivo;

[ObservableProperty]
private ObservableCollection<HistorialDispositivo> historial = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private bool estaCargando = false;
```

---

## 🔵 PÁGINA 4: AlertasPage (Lista de Alertas)

### 📋 Información
- **ViewModel:** `AlertasViewModel.cs`
- **View:** `AlertasPage.xaml` + `AlertasPage.xaml.cs`
- **Servicio:** `IAlertasService`

### 🎨 Componentes de UI
```
┌──────────────────────────────────┐
│   ALERTAS           [🔄 Actualizar]│
├──────────────────────────────────┤
│  Filtros: [Todas] [Crítica] [Info]│
│                                  │
│  ┌────────────────────────────┐ │
│  │ 🔴 CRÍTICA                 │ │ ← TipoAlerta
│  │ Batería baja en DEV-001    │ │ ← Mensaje
│  │ 15/10/2024 10:30 AM        │ │ ← FechaHora
│  │ Estado: Nueva              │ │ ← Estado
│  └────────────────────────────┘ │
│                                  │
│  ┌────────────────────────────┐ │
│  │ 🟡 ADVERTENCIA             │ │
│  │ Temperatura elevada        │ │
│  │ 15/10/2024 09:15 AM        │ │
│  │ Estado: Leída              │ │
│  └────────────────────────────┘ │
│                                  │
│  ┌────────────────────────────┐ │
│  │ 🔵 INFO                    │ │
│  │ Conexión restablecida      │ │
│  │ 15/10/2024 08:00 AM        │ │
│  │ Estado: Leída              │ │
│  └────────────────────────────┘ │
│                                  │
└──────────────────────────────────┘
```

### 🔌 Endpoints usados
```csharp
// 1. Cargar todas las alertas
var alertas = await _alertasService.GetAllAlertasAsync();

// 2. Filtrar por tipo (opcional)
var alertasCriticas = alertas.Where(a => a.TipoAlerta == "Crítica").ToList();

// 3. Filtrar por estado
var alertasNuevas = alertas.Where(a => a.Estado == "Nueva").ToList();
```

### 📊 Propiedades del ViewModel
```csharp
[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasFiltradas = new();

[ObservableProperty]
private bool estaCargando = false;

[ObservableProperty]
private string filtroSeleccionado = "Todas"; // "Todas", "Crítica", "Advertencia", "Info"
```

### 🎨 Colores según tipo
```csharp
// En XAML usar DataTrigger o Converter
TipoAlerta == "Crítica"     → Color Rojo    (#DC3545)
TipoAlerta == "Advertencia" → Color Amarillo (#FFC107)
TipoAlerta == "Info"        → Color Azul    (#0D6EFD)
```

---

## 🔵 PÁGINA 5: DiagnosticoPage (Dashboard/Panel de Control)

### 📋 Información
- **ViewModel:** `DiagnosticoViewModel.cs`
- **View:** `DiagnosticoPage.xaml` + `DiagnosticoPage.xaml.cs`
- **Servicios:** `IDispositivosService`, `IAlertasService`

### 🎨 Componentes de UI
```
┌──────────────────────────────────┐
│   DASHBOARD         [🔄 Actualizar]│
├──────────────────────────────────┤
│                                  │
│  ┌─────────┐  ┌─────────┐       │
│  │   📟    │  │   ✅    │       │
│  │   25    │  │   18    │       │
│  │ Total   │  │ Activos │       │
│  └─────────┘  └─────────┘       │
│                                  │
│  ┌─────────┐  ┌─────────┐       │
│  │   ❌    │  │   🚨    │       │
│  │    7    │  │    3    │       │
│  │Inactivos│  │ Alertas │       │
│  └─────────┘  └─────────┘       │
│                                  │
│  📊 ESTADÍSTICAS                 │
│  ┌────────────────────────────┐ │
│  │ Dispositivos activos: 72%  │ │
│  │ ████████░░░░░░░░           │ │
│  │                            │ │
│  │ Alertas críticas: 12%      │ │
│  │ ██░░░░░░░░░░░░░░           │ │
│  └────────────────────────────┘ │
│                                  │
│  🕐 Última actualización:        │
│     15/10/2024 10:45 AM          │
│                                  │
└──────────────────────────────────┘
```

### 🔌 Endpoints usados
```csharp
// 1. Obtener todos los dispositivos
var dispositivos = await _dispositivosService.GetAllDispositivosAsync();

// 2. Calcular estadísticas
TotalDispositivos = dispositivos.Count;
DispositivosActivos = dispositivos.Count(d => d.Activo == "Activo");
DispositivosInactivos = dispositivos.Count(d => d.Activo == "Inactivo");

// 3. Obtener alertas
var alertas = await _alertasService.GetAllAlertasAsync();
AlertasActivas = alertas.Count(a => a.Estado != "Resuelta");
AlertasCriticas = alertas.Count(a => a.TipoAlerta == "Crítica" && a.Estado != "Resuelta");

// 4. Calcular porcentajes
double porcentajeActivos = (DispositivosActivos * 100.0) / TotalDispositivos;
```

### 📊 Propiedades del ViewModel
```csharp
[ObservableProperty]
private int totalDispositivos = 0;

[ObservableProperty]
private int dispositivosActivos = 0;

[ObservableProperty]
private int dispositivosInactivos = 0;

[ObservableProperty]
private int alertasActivas = 0;

[ObservableProperty]
private int alertasCriticas = 0;

[ObservableProperty]
private DateTime ultimaActualizacion;

[ObservableProperty]
private bool estaCargando = false;
```

---

## 📚 RESUMEN DE SERVICIOS DISPONIBLES

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

## 🎯 FLUJO DE NAVEGACIÓN

```
LoginPage
    ↓ (login exitoso)
DiagnosticoPage (Dashboard)
    ↓
┌───┬───────────────┬────────────┐
│   │               │            │
↓   ↓               ↓            ↓
DispositivosPage  AlertasPage  (Otras)
    ↓
DetalleDispositivoPage
```

---

## ✅ CHECKLIST DE IMPLEMENTACIÓN

### ViewModels
- [ ] LoginViewModel - Autenticación de usuarios
- [ ] DispositivosViewModel - Listar dispositivos
- [ ] DetalleDispositivoViewModel - Detalles + historial + alertas
- [ ] AlertasViewModel - Listar y filtrar alertas
- [ ] DiagnosticoViewModel - Dashboard con estadísticas

### Views
- [ ] LoginPage - Formulario de login
- [ ] DispositivosPage - Lista con CollectionView
- [ ] DetalleDispositivoPage - Información completa
- [ ] AlertasPage - Lista de alertas con colores
- [ ] DiagnosticoPage - Tarjetas con estadísticas

### Navegación
- [ ] AppShell.xaml configurado
- [ ] Rutas registradas
- [ ] Navegación entre páginas funciona

### Extras
- [ ] ActivityIndicator en todas las páginas
- [ ] Manejo de errores con try-catch
- [ ] Validaciones en formularios
- [ ] Mensajes al usuario (Toast/Alert)

---

**Fecha de creación:** 29/10/2024
**Autor:** Héctor Eduardo Véliz Girón
