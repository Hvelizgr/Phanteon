# Phanteon

> Aplicación móvil multiplataforma desarrollada en .NET MAUI para la gestión y monitoreo de dispositivos IoT

[![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-9.0-512BD4?logo=.net)](https://dotnet.microsoft.com/apps/maui)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://docs.microsoft.com/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## 📋 Descripción

**Phanteon** es una aplicación cliente móvil que consume una API REST externa ([DevicesAPI](https://github.com/epinto17/DevicesAPI)) diseñada específicamente para:

- **Gestión de dispositivos IoT** - Control centralizado de todos tus dispositivos
- **Monitoreo en tiempo real** - Alertas y notificaciones instantáneas
- **Historial de eventos** - Registro completo de actividades
- **Dashboard interactivo** - Estadísticas y métricas visuales
- **Autenticación segura** - Sistema de usuarios con almacenamiento seguro

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

> 📋 **Nuevo:** [Cheat Sheet](Docs/CHEATSHEET.md) - Referencia rápida con todo lo que necesitas

### 🟢 Esenciales (Empieza aquí)
| Documento | Descripción | Tiempo |
|-----------|-------------|---------|
| **[01_Empezar_Aqui.md](Docs/01_Empezar_Aqui.md)** ⚡ | Guía de configuración | 10 min |
| **[06_Arquitectura.md](Docs/06_Arquitectura.md)** 📐 | Estructura del proyecto | 10 min |
| **[CHEATSHEET.md](Docs/CHEATSHEET.md)** 📋 | Referencia rápida todo-en-uno | 5 min |

### 📖 Documentación Completa

<details>
<summary><b>Documentación de Desarrollo</b></summary>

- **[01_Empezar_Aqui.md](Docs/01_Empezar_Aqui.md)** - Guía de configuración inicial
- **[02_Ejemplos_Visuales.md](Docs/02_Ejemplos_Visuales.md)** - Mockups y código de ejemplo
- **[03_Guia_Rapida_API.md](Docs/03_Guia_Rapida_API.md)** - Comandos y bindings XAML
- **[04_Solucion_Problemas.md](Docs/04_Solucion_Problemas.md)** - Errores comunes y soluciones
- **[05_Como_Hacer_Commits.md](Docs/05_Como_Hacer_Commits.md)** - Guía de Git y workflow

</details>

<details>
<summary><b>Arquitectura y Servicios</b></summary>

- **[06_Arquitectura.md](Docs/06_Arquitectura.md)** - Arquitectura Feature-based completa
- **[07_Configuracion_Servicios.md](Docs/07_Configuracion_Servicios.md)** - Setup de APIs con Refit
- **[08_Guia_Inicio_Rapido.md](Docs/08_Guia_Inicio_Rapido.md)** - Guía práctica con ejemplos

</details>

<details>
<summary><b>Referencias y Testing</b></summary>

- **[CHEATSHEET.md](Docs/CHEATSHEET.md)** 📋 - Referencia rápida todo-en-uno
- **[Postman/](Docs/Postman/)** - Colecciones para testing de la API
- **[README de Docs](Docs/README.md)** - Índice completo de documentación

</details>

## 🔧 Requisitos Previos

Antes de comenzar, asegúrate de tener instalado:

| Requisito | Versión Mínima | Descripción |
|-----------|----------------|-------------|
| **Visual Studio 2022** | 17.8+ | Con workload .NET MAUI instalado |
| **.NET SDK** | 9.0+ | [Descargar](https://dotnet.microsoft.com/download) |
| **Android SDK** | API 21+ | Incluido con Visual Studio |
| **Emulador/Dispositivo** | - | Android, iOS, Windows o macOS |
| **DevicesAPI** | Latest | [Repositorio Backend](https://github.com/epinto17/DevicesAPI) |

### Verificar instalación:
```bash
dotnet --version  # Debe mostrar 9.0 o superior
dotnet workload list  # Debe incluir 'maui'
```

## ⚙️ Inicio Rápido

### Opción 1: Inicio Guiado (Recomendado para nuevos desarrolladores)

Lee la **[Guía de Inicio Rápido](Docs/02_Empezar_Aqui.md)** que te llevará paso a paso en 5 minutos.

### Opción 2: Instalación Rápida (Para desarrolladores experimentados)

```bash
# 1. Clonar el repositorio
git clone https://github.com/Hvelizgr/Phanteon.git
cd Phanteon

# 2. Cambiar a rama de desarrollo y crear tu rama personal
git checkout ControllerBD
git checkout -b feature/tu-nombre-tarea

# 3. Restaurar dependencias
dotnet restore

# 4. Configurar API (editar Helpers/ApiConfiguration.cs)
# - Emulador Android: https://10.0.2.2:7026
# - Windows Desktop: https://localhost:7026
# - Dispositivo físico: https://TU_IP:7026

# 5. Ejecutar
dotnet build && dotnet run
```

### Configuración de API por Plataforma

| Plataforma | URL Base | Notas |
|------------|----------|-------|
| **Emulador Android** | `https://10.0.2.2:7026` | Ya configurado por defecto |
| **Windows Desktop** | `https://localhost:7026` | Cambiar en ApiConfiguration.cs |
| **Dispositivo Android** | `https://[TU_IP]:7026` | Usar `ipconfig` para ver tu IP |
| **iOS Simulator** | `https://localhost:7026` | Cambiar en ApiConfiguration.cs |

**⚠️ Importante:** Debes tener el [DevicesAPI](https://github.com/epinto17/DevicesAPI) corriendo en tu máquina antes de ejecutar la app.

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

Proyecto desarrollado como parte de un curso académico de desarrollo de aplicaciones móviles.

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

### Patrones y Estándares

| Categoría | Convención | Ejemplo |
|-----------|------------|---------|
| **Clases** | PascalCase | `DispositivoViewModel` |
| **Interfaces** | IPascalCase | `IDispositivosApi` |
| **Métodos** | PascalCase | `CargarDispositivosAsync()` |
| **Propiedades** | PascalCase | `EstaCargando` |
| **Variables privadas** | camelCase o _camelCase | `_dispositivosApi` |
| **Constantes** | PascalCase | `MaxRetryAttempts` |
| **Archivos** | PascalCase | `LoginPage.xaml` |

### Principios de Diseño

- ✅ **MVVM Pattern** - Separación estricta Views/ViewModels/Models
- ✅ **Async/Await** - Todas las operaciones de red deben ser asíncronas
- ✅ **Dependency Injection** - Constructor injection para todas las dependencias
- ✅ **Single Responsibility** - Cada clase tiene una única responsabilidad
- ✅ **DRY (Don't Repeat Yourself)** - Reutilizar código en BaseViewModel y Helpers

## 🤝 Flujo de Trabajo con Git

### 📌 Estructura de Ramas

```mermaid
gitGraph
    commit id: "Initial"
    branch ControllerBD
    checkout ControllerBD
    commit id: "Setup"
    branch feature/hector-login
    branch feature/maria-alertas
    checkout feature/hector-login
    commit id: "Login UI"
    commit id: "Login Logic"
    checkout ControllerBD
    merge feature/hector-login
    checkout master
    merge ControllerBD tag: "v1.0"
```

| Rama | Propósito | Permisos |
|------|-----------|----------|
| `master` | Producción estable | Solo administrador |
| `ControllerBD` | Desarrollo activo | No trabajar directamente |
| `feature/*` | Tu trabajo personal | Tu rama de desarrollo |

### 🔄 Workflow Completo

<details>
<summary><b>1️⃣ Crear tu Rama Personal</b></summary>

```bash
# Asegúrate de estar actualizado
git checkout ControllerBD
git pull origin ControllerBD

# Crea tu rama
git checkout -b feature/tu-nombre-tarea

# Ejemplos de nombres válidos:
# - feature/hector-login
# - feature/maria-alertas-filtros
# - feature/jose-dispositivo-detalle
```
</details>

<details>
<summary><b>2️⃣ Trabajar y Hacer Commits</b></summary>

```bash
# Ver cambios
git status

# Agregar archivos específicos (recomendado)
git add Features/Auth/LoginPage.xaml
git add Features/Auth/LoginViewModel.cs

# O agregar todo
git add .

# Commit con mensaje descriptivo
git commit -m "feat: Implementar LoginViewModel con validaciones"

# Subir a tu rama remota
git push origin feature/tu-nombre-tarea
```

**Convenciones de commits:**
- `feat:` - Nueva funcionalidad
- `fix:` - Corrección de bug
- `docs:` - Cambios en documentación
- `refactor:` - Refactorización sin cambios funcionales
- `test:` - Agregar o modificar tests

Ver [Guía Completa de Commits](Docs/07_Como_Hacer_Commits.md)
</details>

<details>
<summary><b>3️⃣ Mantener tu Rama Actualizada</b></summary>

```bash
# Traer cambios de ControllerBD a tu rama
git checkout ControllerBD
git pull origin ControllerBD
git checkout feature/tu-nombre-tarea
git merge ControllerBD

# Si hay conflictos:
# 1. Abre los archivos marcados como conflicto
# 2. Busca <<<<<<, ====== y >>>>>>
# 3. Decide qué código mantener
# 4. Elimina los marcadores
# 5. Guarda y haz commit

git add .
git commit -m "merge: Actualizar desde ControllerBD"
git push origin feature/tu-nombre-tarea
```
</details>

<details>
<summary><b>4️⃣ Crear Pull Request</b></summary>

1. Ve a: https://github.com/Hvelizgr/Phanteon
2. Click **"Pull Requests"** → **"New Pull Request"**
3. Configurar:
   - **Base:** `ControllerBD`
   - **Compare:** `feature/tu-nombre-tarea`
4. Título: `feat: Implementar LoginPage y LoginViewModel`
5. Descripción:
   ```markdown
   ## Cambios realizados
   - Implementada LoginPage con formulario
   - Agregado LoginViewModel con validaciones
   - Conectado con IUsuariosApi

   ## Testing
   - ✅ Probado en emulador Android
   - ✅ Validaciones funcionando correctamente

   ## Screenshots
   [Opcional: agregar capturas]
   ```
6. Asignar **Reviewers**
7. **Create Pull Request**

⚠️ **NO hacer merge tú mismo** - Esperar aprobación del equipo
</details>

### 🚫 Evitar Errores Comunes

| ❌ NO Hacer | ✅ Hacer en su lugar |
|------------|---------------------|
| `git push --force` | `git push` (normal) |
| Trabajar en `master` | Trabajar en `feature/*` |
| Commits grandes | Commits pequeños y frecuentes |
| `git add .` sin revisar | `git status` primero, luego agregar |
| Subir archivos grandes | Usar `.gitignore` |

### 💡 Comandos Útiles

```bash
# Ver estado actual
git status

# Ver historial de commits
git log --oneline --graph

# Deshacer último commit (mantener cambios)
git reset --soft HEAD~1

# Descartar cambios locales en un archivo
git checkout -- archivo.cs

# Ver diferencias antes de commit
git diff
```

---

## ❓ Preguntas Frecuentes (FAQ)

<details>
<summary><b>¿Cómo empiezo si soy nuevo en el proyecto?</b></summary>

Sigue esta ruta:
1. Lee [Docs/01_Introduccion.md](Docs/01_Introduccion.md) para contexto
2. Lee [Docs/02_Empezar_Aqui.md](Docs/02_Empezar_Aqui.md) para setup
3. Lee [Docs/08_Arquitectura.md](Docs/08_Arquitectura.md) para entender la estructura
4. Revisa [Docs/03_Tu_Tarea.md](Docs/03_Tu_Tarea.md) para tu asignación
5. Comienza a codear siguiendo los ejemplos en [Docs/10_Guia_Inicio_Rapido.md](Docs/10_Guia_Inicio_Rapido.md)
</details>

<details>
<summary><b>¿Dónde está el backend (API)?</b></summary>

El backend es un repositorio **separado** y **externo**:
- **Repositorio:** https://github.com/epinto17/DevicesAPI
- **Propietario:** @epinto17 (Erick Pinto)
- **Tecnología:** .NET 9 + Entity Framework + SQL Server

Para obtener acceso, contacta a @epinto17. No está incluido en este proyecto.
</details>

<details>
<summary><b>¿Qué URL debo usar para la API?</b></summary>

Depende de tu plataforma:

| Plataforma | URL |
|------------|-----|
| Emulador Android | `https://10.0.2.2:7026` (ya configurado) |
| Windows Desktop | `https://localhost:7026` |
| Dispositivo Android | `https://TU_IP:7026` (usa `ipconfig`) |
| iOS Simulator | `https://localhost:7026` |

Edita `Helpers/ApiConfiguration.cs` para cambiar la URL.
</details>

<details>
<summary><b>¿Cómo creo un nuevo módulo/feature?</b></summary>

1. Crea carpeta en `Features/NombreModulo/`
2. Agrega tu `Page.xaml` y `ViewModel.cs` ahí
3. Registra ambos en `MauiProgram.cs`:
   ```csharp
   builder.Services.AddTransient<MiViewModel>();
   builder.Services.AddTransient<MiPage>();
   ```
4. Agrega ruta en `AppShell.xaml` o `AppShell.xaml.cs`

Ver guía completa: [Docs/08_Arquitectura.md](Docs/08_Arquitectura.md)
</details>

<details>
<summary><b>¿Cómo uso los servicios de API con Refit?</b></summary>

Los servicios ya están configurados. Solo inyéctalos:

```csharp
public class MiViewModel : BaseViewModel
{
    private readonly IDispositivosApi _api;

    public MiViewModel(IDispositivosApi api)
    {
        _api = api;
    }

    [RelayCommand]
    private async Task CargarAsync()
    {
        var datos = await _api.GetDispositivosAsync();
    }
}
```

Ver ejemplos completos: [Docs/09_Configuracion_Servicios.md](Docs/09_Configuracion_Servicios.md)
</details>

<details>
<summary><b>Error: "Connection refused" al ejecutar</b></summary>

**Causas:**
1. El backend no está corriendo
2. URL incorrecta en `ApiConfiguration.cs`

**Solución:**
```bash
# 1. En otra terminal, ejecuta el backend
cd DevicesAPI
dotnet run

# 2. Verifica la URL en Helpers/ApiConfiguration.cs
# 3. Vuelve a ejecutar la app
```

Ver más soluciones: [Docs/06_Solucion_Problemas.md](Docs/06_Solucion_Problemas.md)
</details>

<details>
<summary><b>Error: "Cannot resolve service for type IXXXApi"</b></summary>

**Causa:** El servicio no está registrado en `MauiProgram.cs`

**Solución:**
Agrega en `MauiProgram.cs`:
```csharp
// Para servicios API (ya deberían estar)
builder.Services.AddRefitClient<IDispositivosApi>()...

// Para tus ViewModels
builder.Services.AddTransient<TuViewModel>();

// Para tus Pages
builder.Services.AddTransient<TuPage>();
```
</details>

<details>
<summary><b>¿Cómo hago un commit correctamente?</b></summary>

Usa el formato de Conventional Commits:

```bash
git add .
git commit -m "feat: Descripción breve de tu cambio"
```

**Prefijos:**
- `feat:` - Nueva funcionalidad
- `fix:` - Corrección de bug
- `docs:` - Cambios en documentación
- `refactor:` - Refactorización
- `test:` - Agregar tests

Ver guía completa: [Docs/07_Como_Hacer_Commits.md](Docs/07_Como_Hacer_Commits.md)
</details>

<details>
<summary><b>¿Dónde pongo mi código?</b></summary>

Sigue la arquitectura Feature-based:

```
Features/
├── Auth/              # Tu módulo de autenticación
│   ├── LoginPage.xaml
│   ├── LoginViewModel.cs
│   └── RegisterPage.xaml
├── Alertas/           # Módulo de alertas
└── Dispositivos/      # Módulo de dispositivos
```

**NO pongas:**
- ViewModels en carpeta raíz `ViewModels/`
- Views en carpeta raíz `Views/`

Todo debe ir en `Features/{NombreModulo}/`
</details>

<details>
<summary><b>¿Cómo pruebo los endpoints de la API?</b></summary>

Usa Postman:
1. Importa la colección en `Docs/Postman/API collection.json`
2. Importa el environment `Docs/Postman/API environment.json`
3. Ejecuta las requests

O usa el navegador:
```
https://localhost:7026/api/dispositivos/getall
```

Ver guía: [Docs/Postman/Guia POSTMAN.md](Docs/Postman/Guia%20POSTMAN.md)
</details>

<details>
<summary><b>¿Qué es BaseViewModel y cómo lo uso?</b></summary>

`BaseViewModel` es una clase base que proporciona propiedades comunes:

```csharp
public partial class MiViewModel : BaseViewModel
{
    // Ya tienes disponible:
    // - EstaCargando (bool)
    // - MensajeError (string)
    // - Titulo (string)
    // - ManejarError(Exception, string)
    // - LimpiarError()
    // - EstablecerError(string)
}
```

Ver ejemplos: [Docs/10_Guia_Inicio_Rapido.md](Docs/10_Guia_Inicio_Rapido.md)
</details>

---

## 📞 Soporte y Contacto

### Para Problemas Técnicos
1. Revisa [Docs/06_Solucion_Problemas.md](Docs/06_Solucion_Problemas.md)
2. Busca en la documentación (Ctrl+F)
3. Pregunta en el chat del equipo

### Para Problemas con el Backend (DevicesAPI)
- Repositorio: https://github.com/epinto17/DevicesAPI

---

## 📄 Licencia

Este proyecto es académico y se usa únicamente con fines educativos.

---

<div align="center">

**Última actualización:** Noviembre 2025

**[⬆ Volver arriba](#phanteon)**

</div>

