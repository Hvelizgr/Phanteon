# 📱 Phanteon - Aplicación .NET MAUI

> Aplicación multiplataforma desarrollada con .NET 9 y MAUI
> Arquitectura MVVM | Inyección de Dependencias | APIs Seguras

---

## 👥 Integrantes del Equipo

| Nombre Completo | Código | Rol |
|----------------|---------|-----|
| Héctor Eduardo Véliz Girón | 000108304 | Desarrollador Principal |

**Fecha de Entrega:** _____/_____/_____
**Docente:** _________________________________
**Curso:** _________________________________

---

## 📑 Índice

1. [Descripción General](#-descripción-general)
2. [Requisitos del Sistema](#-requisitos-del-sistema)
3. [Tecnologías Utilizadas](#-tecnologías-utilizadas)
4. [Arquitectura](#-arquitectura)
5. [Estructura del Proyecto](#-estructura-del-proyecto)
6. [Instalación y Configuración](#-instalación-y-configuración)
7. [Comandos Útiles](#-comandos-útiles)

---

## 🎯 Descripción General

### Objetivo
Desarrollar una aplicación multiplataforma usando .NET MAUI que implemente:
- Arquitectura MVVM limpia
- Inyección de dependencias
- Consumo seguro de APIs REST
- Almacenamiento seguro de credenciales

### Características
- ✅ Arquitectura MVVM con separación de responsabilidades
- ✅ Inyección de dependencias nativa de .NET
- ✅ Consumo de APIs REST con Refit
- ✅ Almacenamiento seguro con SecureStorage
- ✅ Manejo de errores resiliente con Polly

### Plataformas Soportadas
- 📱 **Android** (API 21+)
- 🍎 **iOS** (14.0+)
- 💻 **Windows** (10.0.17763+)
- 🍏 **macOS** (10.15+)

---

## 💻 Requisitos del Sistema

### Software Necesario

| Herramienta | Versión | Propósito |
|-------------|---------|-----------|
| Visual Studio 2022 | 17.8+ | IDE principal |
| .NET SDK | 9.0+ | Framework |
| Android SDK | API 34 | Desarrollo Android |
| Xcode | 15.0+ (macOS) | Desarrollo iOS |

### Verificar Instalación
```bash
# Verificar .NET MAUI
dotnet workload list

# Instalar si no está presente
dotnet workload install maui
```

---

## 🛠️ Tecnologías Utilizadas

| Paquete | Versión | Descripción |
|---------|---------|-------------|
| **CommunityToolkit.Maui** | 12.2.0 | Componentes y helpers para MAUI |
| **CommunityToolkit.Mvvm** | 8.4.0 | MVVM simplificado con source generators |
| **Refit** | 8.0.0 | Cliente HTTP declarativo para APIs |
| **Refit.HttpClientFactory** | 8.0.0 | Integración con HttpClientFactory |
| **Newtonsoft.Json** | 13.0.4 | Serialización JSON |
| **Polly** | 8.6.4 | Resiliencia y manejo de fallos |
| **Polly.Extensions.Http** | 3.0.0 | Extensiones HTTP para Polly |
| **Microsoft.Maui.Essentials** | Incluido | APIs nativas (SecureStorage, etc.) |

### SecureStorage - Implementación Nativa
**Estado:** ✅ **Actualizado y funcionando correctamente**

SecureStorage es parte de MAUI Essentials y **NO requiere paquetes adicionales**. Utiliza APIs nativas:
- **Android:** EncryptedSharedPreferences (API 23+) o KeyStore
- **iOS:** Keychain
- **Windows:** Data Protection API (DPAPI)
- **macOS:** Keychain

---

## 🏗️ Arquitectura

### Patrón MVVM

```
┌─────────────────────────────────────────┐
│        VISTA (Views/)                    │
│  - MainPage.xaml                        │
│  - Definición UI en XAML                │
└─────────────┬───────────────────────────┘
              │ Data Binding
              ▼
┌─────────────────────────────────────────┐
│      VIEW MODEL (ViewModels/)           │
│  - BaseViewModel.cs                     │
│  - Lógica de presentación               │
│  - Commands y propiedades observables   │
└─────────────┬───────────────────────────┘
              │ Inyección de Dependencias
              ▼
┌─────────────────────────────────────────┐
│       SERVICIOS (Services/)             │
│  Interfaces/                            │
│  - IApiService.cs                       │
│  - ISecureStorageService.cs             │
│  Implementations/                       │
│  - SecureStorageService.cs              │
└─────────────┬───────────────────────────┘
              │
              ▼
┌─────────────────────────────────────────┐
│        MODELOS (Models/)                │
│  - DTOs y entidades de dominio          │
└─────────────────────────────────────────┘
```

### Principios SOLID

| Principio | Aplicación |
|-----------|------------|
| **Single Responsibility** | Cada clase tiene una única responsabilidad |
| **Open/Closed** | Extensible mediante interfaces sin modificar código |
| **Liskov Substitution** | Interfaces sustituibles por mocks para testing |
| **Interface Segregation** | Interfaces específicas y pequeñas |
| **Dependency Inversion** | Dependencias de abstracciones, no implementaciones |

---

## 📂 Estructura del Proyecto

```
Phanteon/
├── Views/                          # Páginas XAML
│   ├── MainPage.xaml
│   └── MainPage.xaml.cs
│
├── ViewModels/                     # Lógica de presentación
│   └── BaseViewModel.cs
│
├── Models/                         # DTOs y modelos
│
├── Services/
│   ├── Interfaces/                 # Contratos
│   │   ├── IApiService.cs
│   │   └── ISecureStorageService.cs
│   │
│   └── Implementations/            # Implementaciones
│       └── SecureStorageService.cs
│
├── Helpers/                        # Utilidades
│
├── Resources/                      # Recursos
│   ├── Styles/
│   │   ├── Colors.xaml
│   │   └── Styles.xaml
│   ├── Images/
│   └── Fonts/
│
├── Platforms/                      # Código específico de plataforma
│   ├── Android/
│   ├── iOS/
│   ├── Windows/
│   ├── MacCatalyst/
│   └── Tizen/
│
├── Tests/                          # Pruebas unitarias
│
├── App.xaml                        # Configuración de la app
├── AppShell.xaml                   # Shell de navegación
├── MauiProgram.cs                  # Punto de entrada y DI
└── Phanteon.csproj                 # Configuración del proyecto
```

### Responsabilidades por Carpeta

- **Views/**: Archivos XAML y code-behind mínimo. Solo bindings, sin lógica de negocio.
- **ViewModels/**: Lógica de presentación, comandos, propiedades observables con `[ObservableProperty]`
- **Models/**: DTOs simples para transferencia de datos
- **Services/**: Lógica de negocio, consumo de APIs, acceso a datos
- **Helpers/**: Converters, extensions, utilidades compartidas

---

## ⚙️ Instalación y Configuración

### 1. Clonar el Repositorio
```bash
git clone https://github.com/tu-usuario/Phanteon.git
cd Phanteon
```

### 2. Restaurar Paquetes
```bash
dotnet restore
```

### 3. Compilar el Proyecto
```bash
# Compilación general
dotnet build

# Compilación para Android
dotnet build -f net9.0-android
```

### 4. Ejecutar la Aplicación
```bash
# Android
dotnet build -t:Run -f net9.0-android

# iOS (solo macOS)
dotnet build -t:Run -f net9.0-ios

# Windows
dotnet build -t:Run -f net9.0-windows10.0.19041.0
```

---

## 🛠️ Comandos Útiles

### Gestión del Proyecto
```bash
# Limpiar solución
dotnet clean

# Restaurar paquetes
dotnet restore

# Compilar sin ejecutar
dotnet build

# Reconstruir completamente
dotnet clean && dotnet build
```

### Gestión de Paquetes
```bash
# Ver paquetes instalados
dotnet list package

# Agregar nuevo paquete
dotnet add package NombrePaquete --version X.X.X

# Actualizar paquete
dotnet add package NombrePaquete
```

### Debugging
```bash
# Ver logs detallados
dotnet build -v detailed

# Ver diagnósticos
dotnet build /bl
```

---
---

## 📚 Referencias

### Documentación Oficial
- [.NET MAUI](https://learn.microsoft.com/dotnet/maui/)
- [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- [Refit](https://github.com/reactiveui/refit)
- [Polly](https://github.com/App-vNext/Polly)
- [SecureStorage](https://learn.microsoft.com/dotnet/maui/platform-integration/storage/secure-storage)

### Tutoriales
- [MVVM en .NET MAUI - Microsoft Learn](https://learn.microsoft.com/training/modules/use-mvvm-pattern-xamarin-forms/)
- [Inyección de Dependencias - Microsoft Docs](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection)

---

## 📄 Licencia

Este proyecto es de uso educativo.

---

**Última actualización:** Octubre 2025
**Versión:** 1.0.0
