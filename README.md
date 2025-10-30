# 📋 División de Tareas del Equipo

---

## 👥 Equipo (4 personas)

| Miembro | Código | 
|---------|--------|
| **Héctor Eduardo Véliz Girón** | 000108304 | 
| **Persona 1** | _________ | 
| **Persona 2** | _________ |
| **Persona 3** | _________ | 

---

## ✅ TAREAS COMPLETADAS 

### 🏗️ Infraestructura del Proyecto

- [x] **Inicialización del proyecto .NET MAUI**
  - Creación del proyecto base
  - Configuración de plataformas (Android, iOS, Windows)

- [x] **Configuración del repositorio Git**
  - Inicialización de Git
  - Configuración de .gitignore
  - Commits iniciales

- [x] **Instalación de paquetes NuGet**
  - CommunityToolkit.Maui (v12.2.0)
  - CommunityToolkit.Mvvm (v8.4.0)
  - Refit (v8.0.0)
  - Refit.HttpClientFactory (v8.0.0)
  - Newtonsoft.Json (v13.0.4)
  - Polly (v8.6.4)
  - Polly.Extensions.Http (v3.0.0)

### 📦 Modelos de Datos

Ubicación: `Models/`

- [x] **Usuario.cs**
  ```csharp
  - IdUsuario: int
  - NombreUsuario: string
  - Correo: string
  - PasswordHash: string
  - Rol: string
  ```

- [x] **Dispositivo.cs**
  ```csharp
  - IdDispositivo: int
  - SerialDispositivo: string
  - MAC: string
  - Firmware: string
  - Direccion: string
  - Latitud: double
  - Longitud: double
  - Registro: DateTime
  - Activo: string
  - UltimaVista: DateTime
  ```

- [x] **Alerta.cs**
  ```csharp
  - IdAlerta: int
  - IdDispositivo: int
  - TipoAlerta: string
  - Mensaje: string
  - FechaHora: DateTime
  - Estado: string
  ```

- [x] **HistorialDispositivo.cs**
  ```csharp
  - IdHistorial: int
  - IdDispositivo: int
  - Evento: string
  - FechaHora: DateTime
  - Detalles: string
  ```

### 🔌 Servicios de API (Refit)

Ubicación: `Services/Interfaces/`

- [x] **IDispositivosService.cs**
  - GetAllDispositivosAsync()
  - GetDispositivoByIdAsync(int id)
  - CreateDispositivoAsync(Dispositivo)

- [x] **IUsuariosService.cs**
  - GetAllUsuariosAsync()
  - GetUsuarioByIdAsync(int id)
  - CreateUsuarioAsync(Usuario)

- [x] **IAlertasService.cs**
  - GetAllAlertasAsync()
  - GetAlertaByIdAsync(int id)
  - CreateAlertaAsync(Alerta)

- [x] **IHistorialDispositivosService.cs**
  - GetAllHistorialAsync()
  - GetHistorialByIdAsync(int id)
  - CreateHistorialAsync(HistorialDispositivo)

### ⚙️ Configuración

- [x] **MauiProgram.cs**
  - Configuración de inyección de dependencias
  - Registro de servicios Refit
  - Configuración de HttpClient con:
    - BaseAddress desde ApiConfiguration
    - Timeout de 30 segundos
    - Manejo de certificados SSL en DEBUG
  - Registro de ViewModels y Pages existentes

- [x] **ApiConfiguration.cs** (`Helpers/`)
  - BaseUrl configurada: `https://10.0.2.2:7026`
  - Timeout: 30 segundos

### 🛠️ Helpers y Converters

Ubicación: `Helpers/`

- [x] **InvertedBoolConverter.cs**
  - Convierte true ↔ false
  - Uso: Deshabilitar botones mientras carga

- [x] **StringNotEmptyConverter.cs**
  - Retorna true si string NO está vacío
  - Uso: Mostrar mensajes de error condicionales

### 🎨 ViewModels Iniciales

- [x] **DispositivosViewModel.cs**
  - Propiedades: Dispositivos (ObservableCollection), EstaCargando
  - Comandos: CargarDispositivosCommand
  - Consume: IDispositivosService

- [x] **DiagnosticoViewModel.cs**
  - Propiedades: TotalDispositivos, DispositivosActivos, AlertasActivas, etc.
  - Comandos: ActualizarDashboardCommand
  - Consume: IDispositivosService, IAlertasService

### 📱 Páginas Iniciales

- [x] **DispositivosPage.xaml + .cs**
  - Lista de dispositivos con CollectionView
  - ActivityIndicator
  - ToolbarItem para actualizar

- [x] **DiagnosticoPage.xaml + .cs**
  - Dashboard con estadísticas
  - Tarjetas de resumen
  - Botón actualizar

---

### Checklist

- [x] Crear LoginViewModel.cs
  - [ ] Propiedades con [ObservableProperty]
  - [ ] Comando IniciarSesionCommand
  - [ ] Validaciones de correo y password
  - [ ] Consumo de IUsuariosService
  - [ ] Navegación a dashboard

- [ ] Crear AlertasViewModel.cs
  - [ ] Propiedades para alertas y filtros
  - [ ] Comando CargarAlertasCommand
  - [ ] Comando FiltrarPorTipoCommand
  - [ ] Consumo de IAlertasService
  - [ ] Lógica de filtrado

- [ ] Crear DetalleDispositivoViewModel.cs
  - [ ] Propiedades para dispositivo, historial y alertas
  - [ ] Comando CargarDetalleCommand
  - [ ] QueryProperty para recibir ID
  - [ ] Consumo de 3 servicios
  - [ ] Manejo de errores

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<LoginViewModel>();`
  - [ ] `builder.Services.AddTransient<AlertasViewModel>();`
  - [ ] `builder.Services.AddTransient<DetalleDispositivoViewModel>();`


- [ ] Crear LoginPage.xaml
  - [ ] Entry de correo
  - [ ] Entry de password
  - [ ] Button iniciar sesión
  - [ ] Label de error
  - [ ] ActivityIndicator
  - [ ] Bindings correctos

- [ ] Crear LoginPage.xaml.cs
  - [ ] Constructor con inyección de ViewModel
  - [ ] Asignar BindingContext

- [ ] Crear AlertasPage.xaml
  - [ ] ToolbarItem actualizar
  - [ ] Filtros por tipo
  - [ ] CollectionView con alertas
  - [ ] ItemTemplate con colores
  - [ ] ActivityIndicator

- [ ] Crear AlertasPage.xaml.cs
  - [ ] Constructor con inyección
  - [ ] Override OnAppearing

- [ ] Crear DetalleDispositivoPage.xaml
  - [ ] Sección de información
  - [ ] Sección de historial
  - [ ] Sección de alertas
  - [ ] ScrollView
  - [ ] ActivityIndicator

- [ ] Crear DetalleDispositivoPage.xaml.cs
  - [ ] Constructor con inyección
  - [ ] Override OnAppearing

- [ ] Registrar en MauiProgram.cs
  - [ ] `builder.Services.AddTransient<LoginPage>();`
  - [ ] `builder.Services.AddTransient<AlertasPage>();`
  - [ ] `builder.Services.AddTransient<DetalleDispositivoPage>();`

- [ ] Configurar AppShell.xaml
  - [ ] FlyoutItem Dashboard
  - [ ] FlyoutItem Dispositivos
  - [ ] FlyoutItem Alertas
  - [ ] ShellContent Login (IsVisible=False)
  - [ ] ShellContent DetalleDispositivo (IsVisible=False)

- [ ] Configurar AppShell.xaml.cs
  - [ ] Registrar ruta "detalleDispositivo"
  - [ ] Registrar ruta "login"

- [ ] Configurar App.xaml.cs
  - [ ] Decidir página inicial (Login o Dashboard)

- [ ] Agregar validaciones en LoginViewModel
  - [ ] Validar correo no vacío
  - [ ] Validar formato de correo
  - [ ] Validar password no vacío
  - [ ] Validar longitud de password
  - [ ] Verificar conectividad

- [ ] Agregar manejo de errores en ViewModels
  - [ ] Try-catch en DispositivosViewModel
  - [ ] Try-catch en DiagnosticoViewModel
  - [ ] Try-catch en AlertasViewModel
  - [ ] Try-catch en LoginViewModel
  - [ ] Try-catch en DetalleDispositivoViewModel

- [ ] Verificación de conectividad
  - [ ] Usar Connectivity.NetworkAccess
  - [ ] Mostrar mensaje con Toast

- [ ] Probar navegación
  - [ ] Login → Dashboard
  - [ ] Dashboard → Dispositivos
  - [ ] Dispositivos → Detalle
  - [ ] Detalle → Volver atrás
  - [ ] Menú lateral funciona

---


## ✅ Criterios de Aceptación

Cada tarea se considera completada cuando:

1. ✅ El código compila sin errores ni warnings
2. ✅ Está registrado correctamente en MauiProgram.cs
3. ✅ Funciona correctamente (probado)
4. ✅ Sigue el mismo estilo de código del proyecto
5. ✅ Está documentado con comentarios básicos

---


