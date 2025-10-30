# ⚡ QUICK START - Phanteon

> Guía rápida para empezar a trabajar en 5 minutos

---

## 🚀 Configuración Inicial (Solo hacer UNA vez)

### PASO 1: Clonar el Backend API

```bash
# Abre una terminal FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
```

---

### PASO 2: Configurar Base de Datos

**Opción A - SQL Server Local (Recomendado):**
```bash
# Edita appsettings.Development.json:
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**Opción B - SQL Server Express:**
```bash
{
  "ConnectionStrings": {
    "AZURE_SQL_CONNECTIONSTRING": "Server=localhost\\SQLEXPRESS;Database=DevicesDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

### PASO 3: Crear Base de Datos

```bash
# Instalar herramienta EF (solo una vez)
dotnet tool install --global dotnet-ef

# Crear la base de datos
dotnet ef database update
```

**Salida esperada:**
```
Build started...
Build succeeded.
Done.
```

---

### PASO 4: Iniciar el Backend

```bash
dotnet run
```

**Salida esperada:**
```
Now listening on: http://localhost:5000
Application started.
```

✅ **¡DEJA ESTA TERMINAL ABIERTA!** El backend debe estar corriendo mientras trabajas.

---

### PASO 5: Verificar que Funciona

**Opción 1 - Navegador:**

Abre: `http://localhost:5000/api/dispositivos/getall`

Debes ver: `[]` o una lista de dispositivos

**Opción 2 - CMD/PowerShell:**
```bash
curl http://localhost:5000/api/dispositivos/getall
```

---

## 🎯 Trabajar en el Frontend (Phanteon)

### 1. Abrir el proyecto Phanteon en Visual Studio 2022

```bash
cd C:\Users\herivort\Documents\GitHub\Phanteon
start Phanteon.sln
```

---

### 2. Verificar la URL de la API

Abre `Helpers/ApiConfiguration.cs` y verifica:

```csharp
public static string BaseUrl { get; set; } = "http://10.0.2.2:5000";
```

| Si vas a ejecutar en... | Cambia a... |
|------------------------|-------------|
| Emulador Android | `"http://10.0.2.2:5000"` (por defecto) |
| Windows Desktop | `"http://localhost:5000"` |
| Dispositivo Android físico | `"http://TU_IP_LOCAL:5000"` |

---

### 3. Ejecutar el Proyecto

1. En Visual Studio, selecciona el target:
   - Windows Machine
   - Android Emulator
   - O dispositivo físico

2. Presiona F5 o click en ▶️ Start

---

## 📋 Flujo de Trabajo Diario

### Antes de empezar a programar:

```bash
# 1. Abrir terminal en la carpeta DevicesAPI
cd DevicesAPI

# 2. Iniciar el backend
dotnet run

# 3. (Nueva terminal) Abrir Phanteon en Visual Studio
cd C:\Users\herivort\Documents\GitHub\Phanteon
start Phanteon.sln

# 4. Ejecutar app (F5)
```

---

## 🛠️ Tareas del Equipo

### ✅ PASO 1: Crear ViewModels

**Archivos a crear en `ViewModels/`:**
- [ ] `LoginViewModel.cs`
- [ ] `DispositivosViewModel.cs`
- [ ] `DetalleDispositivoViewModel.cs`
- [ ] `AlertasViewModel.cs`
- [ ] `DiagnosticoViewModel.cs`

**Template básico:**
```csharp
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Services.Interfaces;

namespace Phanteon.ViewModels
{
    public partial class DispositivosViewModel : ObservableObject
    {
        private readonly IDispositivosService _service;

        public DispositivosViewModel(IDispositivosService service)
        {
            _service = service;
        }

        [ObservableProperty]
        private bool estaCargando = false;

        [RelayCommand]
        private async Task CargarDatos()
        {
            EstaCargando = true;
            try
            {
                var datos = await _service.GetAllDispositivosAsync();
                // Tu lógica aquí
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }
}
```

**Registrar en `MauiProgram.cs`:**
```csharp
builder.Services.AddTransient<DispositivosViewModel>();
```

---

### ✅ PASO 2: Crear Páginas XAML

**Archivos a crear en `Views/`:**
- [ ] `LoginPage.xaml` + `LoginPage.xaml.cs`
- [ ] `DispositivosPage.xaml` + `DispositivosPage.xaml.cs`
- [ ] `DetalleDispositivoPage.xaml` + `DetalleDispositivoPage.xaml.cs`
- [ ] `AlertasPage.xaml` + `AlertasPage.xaml.cs`
- [ ] `DiagnosticoPage.xaml` + `DiagnosticoPage.xaml.cs`

**Template XAML:**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:Phanteon.ViewModels"
             x:Class="Phanteon.Views.DispositivosPage"
             x:DataType="viewmodel:DispositivosViewModel"
             Title="Dispositivos">

    <Grid>
        <!-- Tu UI aquí -->
    </Grid>
</ContentPage>
```

**Template Code-Behind:**
```csharp
namespace Phanteon.Views;

public partial class DispositivosPage : ContentPage
{
    private readonly DispositivosViewModel _viewModel;

    public DispositivosPage(DispositivosViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarDatosCommand.ExecuteAsync(null);
    }
}
```

**Registrar en `MauiProgram.cs`:**
```csharp
builder.Services.AddTransient<DispositivosPage>();
```

---

### ✅ PASO 3: Configurar Navegación

**Editar `AppShell.xaml`:**
```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:views="clr-namespace:Phanteon.Views"
       x:Class="Phanteon.AppShell">

    <FlyoutItem Title="Dashboard">
        <ShellContent Route="dashboard"
                     ContentTemplate="{DataTemplate views:DiagnosticoPage}"/>
    </FlyoutItem>

    <FlyoutItem Title="Dispositivos">
        <ShellContent Route="dispositivos"
                     ContentTemplate="{DataTemplate views:DispositivosPage}"/>
    </FlyoutItem>
</Shell>
```

---

### ✅ PASO 4: Validaciones y Errores

**Agregar en ViewModels:**

```csharp
using Microsoft.Maui.Networking;
using CommunityToolkit.Maui.Alerts;

[RelayCommand]
private async Task GuardarDatos()
{
    // Validación
    if (string.IsNullOrWhiteSpace(Nombre))
    {
        await Toast.Make("El nombre es requerido").Show();
        return;
    }

    // Verificar internet
    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
    {
        await Toast.Make("No hay conexión a internet").Show();
        return;
    }

    // Llamar API con manejo de errores
    EstaCargando = true;
    try
    {
        await _service.CreateDispositivoAsync(dispositivo);
        await Toast.Make("Guardado correctamente").Show();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
        await Shell.Current.DisplayAlert("Error", "No se pudo guardar", "OK");
    }
    finally
    {
        EstaCargando = false;
    }
}
```

---

## 🆘 Problemas Comunes

### ❌ "Connection refused" en la app

**Solución:**
1. Verifica que el backend esté corriendo (`dotnet run`)
2. Verifica la URL en `ApiConfiguration.cs`
3. Si usas emulador Android, usa `http://10.0.2.2:5000`

---

### ❌ "Unable to connect to database"

**Solución:**
1. Verifica que SQL Server esté instalado y corriendo
2. Verifica la cadena de conexión en `appsettings.Development.json`
3. Intenta: `dotnet ef database update` de nuevo

---

### ❌ "Port 5000 already in use"

**Solución:**
1. Cambia el puerto en `Properties/launchSettings.json` del backend
2. Actualiza `ApiConfiguration.cs` en Phanteon con el nuevo puerto

---

### ❌ El ViewModel no se inyecta

**Solución:**
Verifica que esté registrado en `MauiProgram.cs`:
```csharp
builder.Services.AddTransient<TuViewModel>();
```

---

## 📚 Recursos Útiles

- **README completo:** [README.md](README.md)
- **Configuración API:** [CONFIGURACION_API.md](CONFIGURACION_API.md)
- **Instrucciones equipo:** [INSTRUCCIONES_EQUIPO.md](INSTRUCCIONES_EQUIPO.md)
- **Páginas y endpoints:** [PAGINAS_Y_ENDPOINTS.md](PAGINAS_Y_ENDPOINTS.md)

---

## 🎯 Orden Recomendado de Implementación

1. ✅ **LoginViewModel + LoginPage** (más simple para probar)
2. ✅ **DispositivosViewModel + DispositivosPage** (lista básica)
3. ✅ **DiagnosticoViewModel + DiagnosticoPage** (dashboard)
4. ✅ **AlertasViewModel + AlertasPage** (filtros)
5. ✅ **DetalleDispositivoViewModel + DetalleDispositivoPage** (más complejo)
6. ✅ **Configurar navegación en AppShell**
7. ✅ **Agregar validaciones y manejo de errores**

---

## 📞 Contacto

**Héctor Eduardo Véliz Girón**
- Código: 000108304
- Responsable: Backend & API

---

**¡Buena suerte! 🚀**

_Última actualización: 29/10/2024_
