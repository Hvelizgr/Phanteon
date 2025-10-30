# 📱 Phanteon - Sistema de Monitoreo de Dispositivos IoT

> 🎓 **Proyecto Universitario**: Aplicación multiplataforma para gestión y monitoreo de dispositivos IoT desarrollada con .NET 9 y MAUI
>
> 📚 **Conceptos clave**: Arquitectura MVVM | Inyección de Dependencias | APIs REST | Geolocalización | Monitoreo en Tiempo Real

---

## 👥 Integrantes del Equipo

| Nombre Completo | Código | Rol |
|----------------|---------|-----|
| **Héctor Eduardo Véliz Girón** | 000108304 | Desarrollador Principal - Backend & API ✅ |
| _Nombre Completo_ | _Código_ | Desarrollador Frontend - ViewModels |
| _Nombre Completo_ | _Código_ | Desarrollador Frontend - Páginas XAML |
| _Nombre Completo_ | _Código_ | Desarrollador Frontend - Navegación y Validaciones |

**Fecha de Entrega:** _____/_____/_____
**Docente:** _________________________________
**Curso:** _________________________________

---

## 🚀 Estado del Proyecto

### ✅ COMPLETADO (Héctor - 60%)
- ✅ Inicialización del proyecto .NET MAUI
- ✅ Configuración del repositorio Git
- ✅ Instalación y configuración de paquetes NuGet (Refit, Polly, CommunityToolkit)
- ✅ 4 Modelos de datos (Usuario, Dispositivo, Alerta, HistorialDispositivo)
- ✅ 4 Servicios de API configurados (IDispositivosService, IAlertasService, IUsuariosService, IHistorialDispositivosService)
- ✅ Configuración completa de inyección de dependencias
- ✅ Manejo de certificados SSL para desarrollo
- ✅ Helpers y Converters (ApiConfiguration, InvertedBoolConverter, StringNotEmptyConverter)
- ✅ 2 ViewModels iniciales (DispositivosViewModel, DiagnosticoViewModel)
- ✅ 2 Páginas iniciales (DispositivosPage, DiagnosticoPage)

### ⏳ PENDIENTE (Equipo - 40%)
- ⏳ 3 ViewModels faltantes (LoginViewModel, AlertasViewModel, DetalleDispositivoViewModel)
- ⏳ 3 Páginas XAML faltantes (LoginPage, AlertasPage, DetalleDispositivoPage)
- ⏳ Configuración de navegación (AppShell)
- ⏳ Validaciones y manejo de errores completo

---

## 📚 DOCUMENTACIÓN COMPLETA

### 🎯 Para Empezar (Lee estos primero):

| Documento | Descripción | Tiempo |
|-----------|-------------|--------|
| **[📢 00_LEEME_PRIMERO](doc/00_LEEME_PRIMERO.md)** | Resumen ejecutivo del proyecto | 3 min |
| **[⚡ 01_QUICK_START](doc/01_QUICK_START.md)** | Configurar y empezar en 5 minutos | 5 min |
| **[🔧 02_CONFIGURACION_BACKEND](doc/02_CONFIGURACION_BACKEND.md)** | Configurar la API backend | 10 min |

### 📋 Para Trabajar:

| Documento | Descripción |
|-----------|-------------|
| **[📋 03_DIVISION_TAREAS](doc/03_DIVISION_TAREAS.md)** | Tareas detalladas para cada miembro del equipo |
| **[📡 04_ENDPOINTS_DISPONIBLES](doc/04_ENDPOINTS_DISPONIBLES.md)** | Todos los endpoints de la API con ejemplos |
| **[🎨 05_PAGINAS_MOCKUPS](doc/05_PAGINAS_MOCKUPS.md)** | Mockups y ejemplos de código XAML |
| **[🐛 06_ERRORES_COMUNES](doc/06_ERRORES_COMUNES.md)** | Solución a problemas frecuentes |
| **[🧪 07_TESTING_POSTMAN](doc/07_TESTING_POSTMAN.md)** | Probar la API con Postman y scripts |
| **[📝 08_GUIA_COMMITS](doc/08_GUIA_COMMITS.md)** | Qué commits hacer y cómo escribirlos |

---

## ⚡ INICIO RÁPIDO (3 pasos)

### 1️⃣ Configurar el Backend API

```bash
# Clonar backend (FUERA del proyecto Phanteon)
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI

# Configurar base de datos y ejecutar
dotnet ef database update
dotnet run
```

### 2️⃣ Verificar que funciona

Abre: `https://localhost:7026/api/dispositivos/getall`

Si ves `[]`, ¡funciona! ✅

### 3️⃣ Abrir Phanteon y empezar a trabajar

```bash
cd Phanteon
start Phanteon.sln
```

📖 **Instrucciones completas:** [01_QUICK_START.md](doc/01_QUICK_START.md)

---

## 📑 Índice del README (Información Adicional)

1. [Descripción General](#-descripción-general)
2. [Requisitos del Sistema](#-requisitos-del-sistema)
3. [Arquitectura del Proyecto](#-arquitectura-del-proyecto)
4. [Tecnologías Utilizadas](#-tecnologías-utilizadas)
5. [Estructura del Proyecto](#-estructura-del-proyecto)
6. [Comandos Útiles](#-comandos-útiles)

---

## 🎯 Descripción General

Phanteon es una aplicación multiplataforma desarrollada con .NET MAUI que permite:
- Monitorear dispositivos IoT en tiempo real
- Gestionar alertas críticas del sistema
- Visualizar historial de eventos
- Dashboard con estadísticas y métricas
- Geolocalización de dispositivos

El proyecto consume una API REST desarrollada en ASP.NET Core con Entity Framework y SQL Server.

---

## 💻 Requisitos del Sistema

### Software Necesario:
- Visual Studio 2022 (v17.8+) con workload .NET MAUI
- .NET 9 SDK
- SQL Server (cualquier edición)
- Git

### Verificar instalación:
```bash
dotnet --version
dotnet workload list
```

---

## 🏗️ Arquitectura del Proyecto

### Patrón MVVM (Model-View-ViewModel)

```
┌─────────────────────────────────────────┐
│  Views/          (XAML - Interfaz UI)   │
│  ├── LoginPage.xaml                     │
│  ├── DispositivosPage.xaml              │
│  └── ...                                │
└─────────────────┬───────────────────────┘
                  │ Data Binding
┌─────────────────┴───────────────────────┐
│  ViewModels/     (Lógica Presentación)  │
│  ├── LoginViewModel.cs                  │
│  ├── DispositivosViewModel.cs           │
│  └── ...                                │
└─────────────────┬───────────────────────┘
                  │ Inyección de Dependencias
┌─────────────────┴───────────────────────┐
│  Services/       (Lógica de Negocio)    │
│  ├── IDispositivosService (Refit)       │
│  ├── IAlertasService (Refit)            │
│  └── ...                                │
└─────────────────┬───────────────────────┘
                  │ HTTP/REST
┌─────────────────┴───────────────────────┐
│  DevicesAPI      (Backend ASP.NET Core) │
│  └── SQL Server Database                │
└─────────────────────────────────────────┘
```

---

## 🛠️ Tecnologías Utilizadas

### Frontend (Phanteon - .NET MAUI)
- **.NET 9** - Framework multiplataforma
- **MAUI** - UI Framework (Android, iOS, Windows, macOS)
- **CommunityToolkit.Mvvm** - Simplificación de MVVM
- **CommunityToolkit.Maui** - Componentes UI adicionales
- **Refit** - Cliente HTTP declarativo para APIs REST
- **Polly** - Resiliencia y reintentos automáticos
- **Newtonsoft.Json** - Serialización JSON

### Backend (DevicesAPI)
- **ASP.NET Core** - Framework web
- **Entity Framework Core** - ORM
- **SQL Server** - Base de datos

---

## 📂 Estructura del Proyecto

```
Phanteon/
├── doc/                          ← 📚 DOCUMENTACIÓN DEL PROYECTO
│   ├── 00_LEEME_PRIMERO.md      ← ⭐ EMPIEZA AQUÍ
│   ├── 01_QUICK_START.md
│   ├── 02_CONFIGURACION_BACKEND.md
│   ├── 03_DIVISION_TAREAS.md
│   ├── 04_ENDPOINTS_DISPONIBLES.md
│   ├── 05_PAGINAS_MOCKUPS.md
│   └── 06_ERRORES_COMUNES.md
│
├── Models/                       ← Modelos de datos
│   ├── Usuario.cs
│   ├── Dispositivo.cs
│   ├── Alerta.cs
│   └── HistorialDispositivo.cs
│
├── Services/                     ← Servicios de API (Refit)
│   └── Interfaces/
│       ├── IDispositivosService.cs
│       ├── IAlertasService.cs
│       ├── IUsuariosService.cs
│       └── IHistorialDispositivosService.cs
│
├── ViewModels/                   ← Lógica de presentación
│   ├── DispositivosViewModel.cs  ✅
│   ├── DiagnosticoViewModel.cs   ✅
│   ├── LoginViewModel.cs         ⏳
│   ├── AlertasViewModel.cs       ⏳
│   └── DetalleDispositivoViewModel.cs ⏳
│
├── Views/                        ← Interfaces de usuario (XAML)
│   ├── DispositivosPage.xaml     ✅
│   ├── DiagnosticoPage.xaml      ✅
│   ├── LoginPage.xaml            ⏳
│   ├── AlertasPage.xaml          ⏳
│   └── DetalleDispositivoPage.xaml ⏳
│
├── Helpers/                      ← Utilidades
│   ├── ApiConfiguration.cs       ← Configuración de API
│   ├── InvertedBoolConverter.cs
│   └── StringNotEmptyConverter.cs
│
├── MauiProgram.cs                ← Configuración e inyección de dependencias
├── App.xaml                      ← Aplicación principal
├── AppShell.xaml                 ← Navegación
└── README.md                     ← Este archivo
```

---

## 🔧 Comandos Útiles

### Desarrollo:
```bash
# Restaurar paquetes
dotnet restore

# Compilar
dotnet build

# Ejecutar (Android)
dotnet build -t:Run -f net9.0-android

# Ejecutar (Windows)
dotnet build -t:Run -f net9.0-windows10.0.19041.0

# Limpiar
dotnet clean
```

### Backend API:
```bash
# Ejecutar backend
cd DevicesAPI
dotnet run

# Ver migraciones
dotnet ef migrations list

# Aplicar migraciones
dotnet ef database update

# Crear nueva migración
dotnet ef migrations add NombreMigracion
```

---

## 📞 Soporte y Contacto

**Líder del Proyecto:**
- Nombre: Héctor Eduardo Véliz Girón
- Código: 000108304
- Responsable: Backend, API, Modelos, Servicios, Configuración

**Para consultas:**
1. Revisa la documentación en `/doc`
2. Consulta [06_ERRORES_COMUNES.md](doc/06_ERRORES_COMUNES.md)
3. Contacta al líder del proyecto

---

## 📝 Licencia

Este proyecto es de uso educativo para fines académicos.

---
