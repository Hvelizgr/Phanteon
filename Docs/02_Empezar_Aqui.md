# Guía de Configuración - Phanteon

## 🎯 Prerrequisitos

- Visual Studio 2022 con workload .NET MAUI
- .NET 9 SDK
- Git

---

## 🚀 Paso 1: Configurar el Backend API

**⚠️ Nota:** La API es un repositorio separado y externo.

### Obtener el Backend:

```bash
# Clonar fuera del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI

# Ejecutar
dotnet run
```

### Verificar funcionamiento:

Abrir en el navegador: `https://localhost:7026/api/dispositivos/getall`

✅ Si ves `[]` está funcionando correctamente.

---

## 💻 Paso 2: Configurar Phanteon

### 2.1 Clonar el repositorio

```bash
git clone [URL_DEL_REPO_PHANTEON]
cd Phanteon
```

### 2.2 Abrir en Visual Studio

```bash
start Phanteon.sln
```

### 2.3 Restaurar paquetes NuGet

```bash
dotnet restore
```

---

## ⚙️ Paso 3: Configurar la URL de la API

Edita `Helpers/ApiConfiguration.cs`:

```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```

### URLs según plataforma:

| Plataforma | URL |
|-----------|-----|
| **Emulador Android** | `https://10.0.2.2:7026` (por defecto) |
| **Windows Desktop** | `https://localhost:7026` |
| **Dispositivo Android Físico** | `https://[TU_IP]:7026` |
| **iOS Simulator** | `https://localhost:7026` |

---

## 🏃 Paso 4: Ejecutar la Aplicación

1. Seleccionar plataforma en Visual Studio
2. Presionar **F5** o click en ▶️ **Start**
3. Verificar que compila sin errores

---

## 📐 Arquitectura del Proyecto

```
Phanteon/
├── Features/           # Módulos por funcionalidad
│   ├── Main/          # Página principal
│   ├── Auth/          # Autenticación
│   ├── Dispositivos/  # Gestión de dispositivos
│   └── Alertas/       # Sistema de alertas
│
├── Core/              # Componentes reutilizables
│   ├── ViewModels/   # BaseViewModel
│   ├── Converters/   # Value Converters
│   └── Behaviors/    # Behaviors XAML
│
├── Services/          # Servicios de la aplicación
│   ├── Api/          # Interfaces Refit
│   ├── Http/         # HttpClient Factory
│   ├── Storage/      # SecureStorage
│   └── Navigation/   # Navegación
│
├── Models/            # Modelos de datos
├── Constants/         # Constantes
└── Helpers/           # Utilidades
```

Ver **[08_Arquitectura.md](08_Arquitectura.md)** para más detalles.

---

## 🐛 Problemas Comunes

### ❌ "Connection refused"
**Solución:** Verifica que el backend esté corriendo (`dotnet run` en DevicesAPI)

### ❌ "Cannot resolve service"
**Solución:** Verifica que el servicio esté registrado en `MauiProgram.cs`

Ver **[06_Solucion_Problemas.md](06_Solucion_Problemas.md)** para más errores comunes.

---

## 📚 Documentos Relacionados

- **[08_Arquitectura.md](08_Arquitectura.md)** - Arquitectura completa del proyecto
- **[09_Configuracion_Servicios.md](09_Configuracion_Servicios.md)** - Setup de APIs con Refit
- **[10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md)** - Guía rápida con ejemplos
- **[06_Solucion_Problemas.md](06_Solucion_Problemas.md)** - Errores comunes y soluciones

---

**Última actualización:** Noviembre 2025
