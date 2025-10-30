# ⚡ QUICK START - Comenzar en 5 Minutos

---

## 🎯 Prerrequisitos

- Visual Studio 2022 con workload .NET MAUI
- .NET 9 SDK
- SQL Server (cualquier edición)
- Git

---

## 🚀 PASO 1: Configurar el Backend API (5 minutos)

### 1.1 Clonar el repositorio del backend

```bash
# En una carpeta FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI
```

### 1.2 Configurar SQL Server

Abre `appsettings.Development.json` y configura tu conexión:


```

### 1.3 Crear la base de datos

```bash
# Instalar EF Core Tools (solo primera vez)
dotnet tool install --global dotnet-ef

# Crear la base de datos
dotnet ef database update


**Salida esperada:**
```
Build started...
Build succeeded.
Applying migration '20241001_InitialCreate'.
Done.
```

### 1.4 Ejecutar el backend

```bash
dotnet run
```

**Salida esperada:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:7026
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

✅ **Deja esta terminal abierta. El backend debe estar corriendo siempre que trabajes.**

### 1.5 Verificar que funciona

**Opción 1 - Navegador:**
Abre: `https://localhost:7026/api/dispositivos/getall`

**Opción 2 - CMD/PowerShell:**
```bash
curl https://localhost:7026/api/dispositivos/getall
```

Debes ver: `[]` (array vacío) o datos si hay dispositivos.

---

## 💻 PASO 2: Abrir el Proyecto Phanteon

### 2.1 Clonar el repositorio (si aún no lo tienes)

```bash
cd C:\Users\[TU_USUARIO]\Documents\GitHub
git clone [URL_DEL_REPO_PHANTEON]
cd Phanteon
```

### 2.2 Abrir en Visual Studio

```bash
start Phanteon.sln
```

O desde Visual Studio:
- File → Open → Project/Solution
- Navega a `Phanteon.sln`

### 2.3 Restaurar paquetes NuGet

Visual Studio lo hará automáticamente, pero si no:

```bash
dotnet restore
```

---

## ⚙️ PASO 3: Configurar la URL de la API

Abre `Helpers/ApiConfiguration.cs` y verifica la configuración:

```csharp
public static string BaseUrl { get; set; } = "https://10.0.2.2:7026";
```

### Configuración según plataforma:

| Plataforma | URL a usar | Cuándo |
|-----------|-----------|---------|
| **Emulador Android** | `https://10.0.2.2:7026` | Por defecto (ya configurado) |
| **Windows Desktop** | `https://localhost:7026` | Si ejecutas en Windows |
| **Dispositivo Android Físico** | `https://192.168.X.X:7026` | Reemplaza con tu IP local |
| **iOS Simulator** | `https://localhost:7026` | Si ejecutas en Mac |

**Encontrar tu IP local (para dispositivo físico):**
```bash
# Windows
ipconfig

# Linux/Mac
ifconfig
```

---

## 🏃 PASO 4: Ejecutar la Aplicación

### 4.1 Seleccionar plataforma

En Visual Studio, en la barra de herramientas:
- **Windows Machine** - Para ejecutar en Windows
- **Android Emulator** - Para emulador Android
- O selecciona un dispositivo físico conectado

### 4.2 Ejecutar

Presiona **F5** o click en el botón ▶️ **Start**

### 4.3 Verificar funcionamiento

- La app debe compilar sin errores
- Debe abrir la interfaz de DiagnosticoPage
- Si el backend está corriendo, verás los datos cargados

---

## 📋 WORKFLOW DIARIO

Cada vez que trabajes en el proyecto:

### Terminal 1 - Backend API
```bash
cd DevicesAPI
dotnet run
# Dejar corriendo
```

### Terminal 2 - Proyecto Phanteon
```bash
cd Phanteon
start Phanteon.sln
# Presionar F5 en Visual Studio
```

---

## 🛠️ EMPEZAR A DESARROLLAR

## (ViewModels):

**Crear un nuevo ViewModel:**

```csharp
// ViewModels/AlertasViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Phanteon.Models;
using Phanteon.Services.Interfaces;
using System.Collections.ObjectModel;

namespace Phanteon.ViewModels
{
    public partial class AlertasViewModel : ObservableObject
    {
        private readonly IAlertasService _alertasService;

        public AlertasViewModel(IAlertasService alertasService)
        {
            _alertasService = alertasService;
        }

        [ObservableProperty]
        private ObservableCollection<Alerta> alertas = new();

        [ObservableProperty]
        private bool estaCargando = false;

        [RelayCommand]
        private async Task CargarAlertas()
        {
            EstaCargando = true;
            try
            {
                var lista = await _alertasService.GetAllAlertasAsync();
                Alertas.Clear();
                foreach (var alerta in lista)
                {
                    Alertas.Add(alerta);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error",
                    $"No se pudieron cargar las alertas: {ex.Message}", "OK");
            }
            finally
            {
                EstaCargando = false;
            }
        }
    }
}
```

**Registrar en MauiProgram.cs:**
```csharp
builder.Services.AddTransient<AlertasViewModel>();
```

---

### (Páginas XAML):

**Crear una nueva página:**

```xml
<!-- Views/AlertasPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodel="clr-namespace:Phanteon.ViewModels"
             xmlns:models="clr-namespace:Phanteon.Models"
             x:Class="Phanteon.Views.AlertasPage"
             x:DataType="viewmodel:AlertasViewModel"
             Title="Alertas">

    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Actualizar" Command="{Binding CargarAlertasCommand}"/>
    </ContentPage.ToolbarItems>

    <Grid>
        <CollectionView ItemsSource="{Binding Alertas}">
            <CollectionView.ItemTemplate>
                <DataTemplate x:DataType="models:Alerta">
                    <Frame Padding="10" Margin="10">
                        <VerticalStackLayout>
                            <Label Text="{Binding TipoAlerta}" FontAttributes="Bold"/>
                            <Label Text="{Binding Mensaje}"/>
                            <Label Text="{Binding FechaHora, StringFormat='{0:dd/MM/yyyy HH:mm}'}"
                                   FontSize="12" TextColor="Gray"/>
                        </VerticalStackLayout>
                    </Frame>
                </DataTemplate>
            </CollectionView.ItemTemplate>
        </CollectionView>

        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"/>
    </Grid>
</ContentPage>
```

```csharp
// Views/AlertasPage.xaml.cs
namespace Phanteon.Views;

public partial class AlertasPage : ContentPage
{
    private readonly AlertasViewModel _viewModel;

    public AlertasPage(AlertasViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.CargarAlertasCommand.ExecuteAsync(null);
    }
}
```

**Registrar en MauiProgram.cs:**
```csharp
builder.Services.AddTransient<AlertasPage>();
```

---

### (Navegación):

**Configurar AppShell.xaml:**

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:views="clr-namespace:Phanteon.Views"
       x:Class="Phanteon.AppShell"
       FlyoutBehavior="Flyout">

    <FlyoutItem Title="Dashboard" Icon="home.png">
        <ShellContent Route="diagnostico"
                     ContentTemplate="{DataTemplate views:DiagnosticoPage}"/>
    </FlyoutItem>

    <FlyoutItem Title="Dispositivos" Icon="devices.png">
        <ShellContent Route="dispositivos"
                     ContentTemplate="{DataTemplate views:DispositivosPage}"/>
    </FlyoutItem>

    <FlyoutItem Title="Alertas" Icon="alert.png">
        <ShellContent Route="alertas"
                     ContentTemplate="{DataTemplate views:AlertasPage}"/>
    </FlyoutItem>
</Shell>
```

**Registrar rutas en AppShell.xaml.cs:**
```csharp
namespace Phanteon;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("detalleDispositivo", typeof(Views.DetalleDispositivoPage));
        Routing.RegisterRoute("login", typeof(Views.LoginPage));
    }
}
```

---

## 🐛 Problemas Comunes

### ❌ "Connection refused"
**Solución:** Verifica que el backend esté corriendo (`dotnet run`)

### ❌ "SSL Certificate error"
**Solución:** Ya está resuelto en el código con `ServerCertificateCustomValidationCallback` en modo DEBUG

### ❌ "Cannot resolve service"
**Solución:** Registra el ViewModel/Page en `MauiProgram.cs`

### ❌ "Port 7026 already in use"
**Solución:** Cambia el puerto en `Properties/launchSettings.json` del backend

---

## 📚 Documentos Relacionados

- **[03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md)** - Tareas detalladas
- **[04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)** - Lista de endpoints
- **[05_PAGINAS_MOCKUPS.md](05_PAGINAS_MOCKUPS.md)** - Ejemplos visuales
- **[06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)** - Más soluciones

---

**¡Listo para empezar! 🚀**

_Actualizado: 29/10/2024_
