# Phanteon

Aplicación móvil multiplataforma desarrollada en .NET MAUI para la gestión y monitoreo de dispositivos IoT.

## 📋 Descripción

Phanteon es una aplicación cliente que consume una API externa ([DevicesAPI](https://github.com/epinto17/DevicesAPI)) para proporcionar funcionalidades de:

- Gestión de dispositivos IoT
- Monitoreo de alertas en tiempo real
- Visualización de historial de eventos
- Dashboard con estadísticas
- Sistema de autenticación de usuarios

## 🚀 Tecnologías Utilizadas

- **.NET MAUI** - Framework multiplataforma
- **CommunityToolkit.Mvvm** - Patrón MVVM
- **Refit** - Cliente HTTP para APIs REST
- **Polly** - Políticas de resiliencia y reintentos
- **C#** - Lenguaje de programación

## 📱 Plataformas Soportadas

- Android
- iOS
- Windows
- macOS

## 🏗️ Arquitectura del Proyecto (Actualizada - Feature-based)

```
Phanteon/
├── Features/            # Módulos por funcionalidad (Views + ViewModels juntos)
│   ├── Main/           # Página principal
│   │   ├── MainPage.xaml/.cs
│   │   └── MainViewModel.cs
│   ├── Auth/           # Autenticación (pendiente)
│   ├── Dispositivos/   # Gestión de dispositivos (pendiente)
│   └── Alertas/        # Sistema de alertas (pendiente)
│
├── Core/                # Componentes reutilizables
│   ├── ViewModels/     # BaseViewModel con EstaCargando, MensajeError, etc.
│   ├── Converters/     # BoolToColor, InvertedBool, StringNotEmpty
│   ├── Behaviors/      # EventToCommand
│   └── Controls/       # Controles personalizados (futuro)
│
├── Services/            # Servicios organizados por categoría
│   ├── Api/            # Interfaces Refit para APIs REST
│   │   ├── IDispositivosApi.cs
│   │   ├── IUsuariosApi.cs
│   │   └── IAlertasApi.cs
│   ├── Http/           # ApiHttpClientFactory
│   ├── Storage/        # SecureStorageService
│   └── Navigation/     # NavigationService
│
├── Models/              # Modelos de datos
│   ├── Alerta.cs
│   ├── Dispositivo.cs
│   ├── HistorialDispositivo.cs
│   └── Usuario.cs
│
├── Constants/           # Constantes centralizadas
│   ├── ApiEndpoints.cs
│   ├── AppConstants.cs
│   └── ErrorMessages.cs
│
├── Helpers/             # Utilidades
│   └── ApiConfiguration.cs
│
├── Data/                # Capa de datos (repositorios, DB local)
│   ├── Repositories/
│   └── Local/
│
├── Docs/                # 📚 Documentación completa
│   ├── 01-07: Docs originales
│   ├── 08_Arquitectura.md              # Nueva arquitectura detallada
│   ├── 09_Configuracion_Servicios.md   # Setup APIs con Refit
│   ├── 10_Guia_Inicio_Rapido.md        # Guía rápida con ejemplos
│   ├── 11_Lista_Tareas.md              # Checklist de tareas
│   └── Postman/
│
├── Resources/           # Recursos de la aplicación
├── Platforms/           # Código específico de plataforma
└── MauiProgram.cs       # DI y configuración
```

## 📚 Documentación

### Documentación Original (Base del Proyecto)
1. **[01_Introduccion.md](Docs/01_Introduccion.md)** - Contexto del proyecto
2. **[02_Empezar_Aqui.md](Docs/02_Empezar_Aqui.md)** - ⚡ Guía de inicio rápido (EMPIEZA AQUÍ)
3. **[03_Tu_Tarea.md](Docs/03_Tu_Tarea.md)** - División de tareas del equipo
4. **[04_Ejemplos_Visuales.md](Docs/04_Ejemplos_Visuales.md)** - Mockups y código de ejemplo
5. **[05_Guia_Rapida_API.md](Docs/05_Guia_Rapida_API.md)** - Comandos y bindings XAML
6. **[06_Solucion_Problemas.md](Docs/06_Solucion_Problemas.md)** - Errores comunes
7. **[07_Como_Hacer_Commits.md](Docs/07_Como_Hacer_Commits.md)** - Guía de Git

### Nueva Documentación (Estructura Actualizada) ⭐
8. **[08_Arquitectura.md](Docs/08_Arquitectura.md)** - 📐 Arquitectura completa del proyecto
9. **[09_Configuracion_Servicios.md](Docs/09_Configuracion_Servicios.md)** - ⚙️ Setup de APIs con Refit
10. **[10_Guia_Inicio_Rapido.md](Docs/10_Guia_Inicio_Rapido.md)** - 🚀 Guía con ejemplos de código
11. **[11_Lista_Tareas.md](Docs/11_Lista_Tareas.md)** - ✅ Checklist de tareas pendientes

### Testing
- **[Postman/](Docs/Postman/)** - Colecciones Postman para testing de la API

## 🔧 Requisitos Previos

- Visual Studio 2022 (versión 17.8 o superior)
- .NET 8.0 SDK
- Cargas de trabajo de MAUI instaladas
- Emulador Android o dispositivo físico
- Acceso a la API externa DevicesAPI

## ⚙️ Instalación y Configuración

### 1. Clonar el Repositorio y Crear tu Rama

```bash
git clone https://github.com/Hvelizgr/Phanteon.git
cd Phanteon

# Cambiar a la rama de desarrollo actual
git checkout ControllerBD

# Crear tu rama personal desde ControllerBD
git checkout -b feature/tu-nombre-tarea
# Ejemplo: feature/hector-login
# Ejemplo: feature/persona1-alertas
# Ejemplo: feature/persona2-detalle
# Ejemplo: feature/persona3-dispositivos
```

**⚠️ IMPORTANTE:**
- **NO trabajes directamente en `master` o `ControllerBD`**
- Cada persona debe crear su propia rama
- Usa el formato: `feature/nombre-tarea`

### 2. Restaurar Paquetes NuGet

```bash
dotnet restore
```

### 3. Configurar la URL de la API

Editar `Helpers/ApiConfiguration.cs` según tu entorno:

```csharp
// Para emulador Android
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";

// Para Windows Desktop
// public static string BaseUrl { get; set; } = "https://localhost:7026";

// Para dispositivo físico (reemplazar con tu IP)
// public static string BaseUrl { get; set; } = "https://192.168.1.100:7026";
```

### 4. Ejecutar la Aplicación

**Desde Visual Studio:**
- Seleccionar la plataforma objetivo (Android, Windows, etc.)
- Presionar F5 para ejecutar

**Desde CLI:**
```bash
dotnet build
dotnet run
```

## 🔌 API Externa

Este proyecto consume la API DevicesAPI, que es un repositorio externo:

- **Repositorio:** [https://github.com/epinto17/DevicesAPI](https://github.com/epinto17/DevicesAPI)
- **Propietario:** Erick Pinto (@epinto17)
- **Tecnología:** .NET Web API + Entity Framework Core + SQL Server

**Nota:** Es necesario tener la API corriendo localmente antes de usar Phanteon.

## 🧪 Testing

### Pruebas de API con Postman

La carpeta `Docs/Postman/` contiene:
- Colección completa de requests
- Environment configurado
- Guía detallada de uso

Para importar:
1. Abrir Postman
2. Import → Seleccionar `API collection.json`
3. Import → Seleccionar `API environment.json`

### ViewModel de Prueba

El proyecto incluye `TestConexionApiViewModel.cs` que sirve ÚNICAMENTE para verificar la conexión con la API. Este archivo NO debe usarse en producción, es solo una herramienta de debugging.

## 👥 Equipo de Desarrollo

- **Héctor Eduardo Véliz Girón** (000108304) - Lead Developer & Infrastructure
- 3 desarrolladores adicionales (Ver [03_Tu_Tarea.md](Docs/03_Tu_Tarea.md))

### División de Trabajo (Actualizado - Feature-based)

Cada miembro trabaja en su propia carpeta Feature:

- **Héctor:** `Features/Auth/` - LoginPage + LoginViewModel
- **Persona 1:** `Features/Alertas/` - AlertasPage + AlertasViewModel
- **Persona 2:** `Features/Dispositivos/DispositivoDetail/` - Detalle de dispositivo
- **Persona 3:** `Features/Dispositivos/DispositivosList/` - Lista + Navegación

## 📦 Paquetes NuGet Principales

```xml
<PackageReference Include="CommunityToolkit.Maui" Version="12.2.0" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.4.0" />
<PackageReference Include="Refit" Version="8.0.0" />
<PackageReference Include="Refit.HttpClientFactory" Version="8.0.0" />
<PackageReference Include="Polly" Version="8.6.4" />
<PackageReference Include="Polly.Extensions.Http" Version="3.0.0" />
```

## 🔑 Características Implementadas

### ✅ Completadas
- Infraestructura del proyecto
- Configuración de inyección de dependencias
- Modelos de datos sincronizados con API
- Servicios Refit para consumo de API
- ViewModels base y de ejemplo
- Helpers y converters
- Sistema de almacenamiento seguro
- ViewModel de prueba de conexión (TestConexionApiViewModel)

### 🚧 En Desarrollo
- Páginas de Login, Alertas, Dispositivos y Detalle
- Sistema de navegación completo (AppShell)
- Validaciones y manejo de errores
- Implementación final de todas las vistas

## 🐛 Errores Comunes y Soluciones

Ver [06_Solucion_Problemas.md](Docs/06_Solucion_Problemas.md) para una lista completa de problemas frecuentes y sus soluciones.

## 📝 Convenciones de Código

- **MVVM Pattern:** Separación estricta entre vistas y lógica
- **Async/Await:** Operaciones asíncronas para todas las llamadas de red
- **Inyección de Dependencias:** Constructor injection
- **Converters:** Para transformaciones en XAML bindings
- **Nomenclatura:** PascalCase para clases, camelCase para variables

## 🤝 Flujo de Trabajo con Git

### 📌 Estructura de Ramas

- **`master`** - Rama principal de producción (NO tocar)
- **`ControllerBD`** - Rama de desarrollo actual (NO trabajar directamente aquí)
- **`feature/nombre-tarea`** - Tu rama personal de trabajo

### 🔄 Flujo de Trabajo Recomendado

#### 1. Crear tu Rama Personal

```bash
# Asegúrate de estar en ControllerBD
git checkout ControllerBD

# Actualizar desde remoto
git pull origin ControllerBD

# Crear tu rama
git checkout -b feature/tu-nombre-tarea
```

**Ejemplos de nombres de rama:**
- `feature/hector-login`
- `feature/maria-alertas`
- `feature/jose-detalle`
- `feature/ana-dispositivos-navegacion`

#### 2. Trabajar en tu Rama

```bash
# Ver archivos modificados
git status

# Agregar cambios
git add .

# Hacer commit (seguir convenciones en 07_Como_Hacer_Commits.md)
git commit -m "feat: Implementar LoginViewModel con validaciones"

# Subir cambios a tu rama
git push origin feature/tu-nombre-tarea
```

#### 3. Actualizar desde ControllerBD (Importante)

```bash
# Cambiar a ControllerBD
git checkout ControllerBD

# Actualizar
git pull origin ControllerBD

# Volver a tu rama
git checkout feature/tu-nombre-tarea

# Traer cambios de ControllerBD a tu rama
git merge ControllerBD

# Resolver conflictos si hay (pedir ayuda si es necesario)
# Después de resolver:
git add .
git commit -m "merge: Actualizar desde ControllerBD"
git push origin feature/tu-nombre-tarea
```

#### 4. Crear Pull Request

Cuando termines tu tarea:

1. Ve a GitHub: https://github.com/Hvelizgr/Phanteon
2. Click en "Pull Requests" → "New Pull Request"
3. **Base:** `ControllerBD` ← **Compare:** `feature/tu-nombre-tarea`
4. Título descriptivo: "feat: Implementar LoginPage y LoginViewModel"
5. Descripción detallada de lo que hiciste
6. Asignar reviewers (compañeros del equipo)
7. Click "Create Pull Request"

**⚠️ IMPORTANTE:** NO hacer merge tú mismo, esperar revisión del equipo.

### 🚫 Qué NO Hacer

- ❌ NO trabajar directamente en `master`
- ❌ NO trabajar directamente en `ControllerBD`
- ❌ NO hacer `git push --force` (puede borrar trabajo de otros)
- ❌ NO hacer merge de tu PR sin revisión
- ❌ NO subir archivos grandes (imágenes, videos, etc.)

### ✅ Buenas Prácticas

- ✅ Hacer commits pequeños y frecuentes
- ✅ Usar mensajes de commit descriptivos
- ✅ Actualizar tu rama desde ControllerBD frecuentemente
- ✅ Probar tu código antes de hacer commit
- ✅ Seguir las convenciones en [07_Como_Hacer_Commits.md](Docs/07_Como_Hacer_Commits.md)

