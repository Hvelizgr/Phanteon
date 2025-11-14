# ⚡ QUICK START - Comenzar en 5 Minutos

---

## 🎯 Prerrequisitos

- Visual Studio 2022 con workload .NET MAUI
- .NET 9 SDK
- SQL Server (cualquier edición)
- Git

---

## 🚀 PASO 1: Configurar el Backend API (Repositorio Externo)

**⚠️ IMPORTANTE:** La API es un repositorio separado, **NO es parte de Phanteon**.

### Prerrequisitos:
- Acceso autorizado al repositorio de @epinto17
- .NET SDK instalado
- SQL Server instalado y corriendo

### Pasos:

**1. Solicitar acceso al repositorio:**
   - Contactar a **Pinto** (GitHub: @epinto17)
   - Repositorio: https://github.com/epinto17/DevicesAPI
   - **Esperar confirmación** antes de continuar

**2. Clonar y ejecutar (una vez autorizado):**

```bash
# Clonar FUERA del proyecto Phanteon
git clone https://github.com/epinto17/DevicesAPI.git
cd DevicesAPI

# Ejecutar (la configuración ya viene lista)
dotnet run
```

**3. Verificar que funciona:**

Abre en el navegador: `https://localhost:7026/api/dispositivos/getall`

✅ Si ves `[]` está funcionando correctamente.

**Nota:** La configuración del backend (SQL Server, migraciones, Entity Framework) ya viene lista en el repositorio. Solo necesitas ejecutar `dotnet run`. Ver `Postman/Guia POSTMAN.md` para probar los endpoints.

___

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

**📖 NUEVA ESTRUCTURA:** El proyecto ahora usa **Feature-based Architecture**.

Ver documentación completa:
- **[08_Arquitectura.md](08_Arquitectura.md)** - Arquitectura del proyecto
- **[10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md)** - Guía rápida con ejemplos

### Nueva Organización:

```
Features/               ← Views + ViewModels por módulo
├── Main/
│   ├── MainPage.xaml
│   ├── MainPage.xaml.cs
│   └── MainViewModel.cs
├── Alertas/           ← Crear tu módulo aquí
├── Dispositivos/
└── Auth/

Core/                  ← Componentes reutilizables
├── ViewModels/
│   └── BaseViewModel.cs  ← Heredar de aquí
├── Converters/
└── Behaviors/

Services/              ← Servicios organizados
├── Api/              ← Interfaces Refit
├── Http/
├── Storage/
└── Navigation/
```

## (ViewModels):

**Crear un nuevo ViewModel (en su Feature):**

```csharp
// Features/Alertas/AlertasViewModel.cs
using Phanteon.Core.ViewModels;
using Phanteon.Services.Api;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Phanteon.Features.Alertas
{
    public partial class AlertasViewModel : BaseViewModel  // ← Heredar de BaseViewModel
    {
        private readonly IAlertasApi _alertasApi;

        public AlertasViewModel(IAlertasApi alertasApi)
        {
            _alertasApi = alertasApi;
            Titulo = "Alertas";  // Viene de BaseViewModel
        }

        [ObservableProperty]
        private ObservableCollection<Alerta> alertas = new();

        [RelayCommand]
        private async Task CargarAlertasAsync()
        {
            try
            {
                EstaCargando = true;  // Viene de BaseViewModel
                LimpiarError();       // Viene de BaseViewModel

                var lista = await _alertasApi.GetAlertasAsync();
                Alertas.Clear();
                foreach (var alerta in lista)
                {
                    Alertas.Add(alerta);
                }
            }
            catch (Exception ex)
            {
                ManejarError(ex, "cargar alertas");  // Viene de BaseViewModel
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
// ViewModels
builder.Services.AddTransient<AlertasViewModel>();

// Pages
builder.Services.AddTransient<AlertasPage>();
```

---

### (Páginas XAML):

**Crear una nueva página (en su Feature):**

```xml
<!-- Features/Alertas/AlertasPage.xaml -->
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:vm="clr-namespace:Phanteon.Features.Alertas"
             xmlns:models="clr-namespace:Phanteon.Models"
             x:Class="Phanteon.Features.Alertas.AlertasPage"
             x:DataType="vm:AlertasViewModel"
             Title="{Binding Titulo}">

    <Grid RowDefinitions="Auto,*">
        <!-- Indicador de carga (usando BaseViewModel) -->
        <ActivityIndicator IsRunning="{Binding EstaCargando}"
                          IsVisible="{Binding EstaCargando}"
                          Grid.Row="0"/>

        <!-- Mensaje de error (usando BaseViewModel) -->
        <Label Text="{Binding MensajeError}"
               IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}"
               TextColor="Red"
               Grid.Row="0"/>

        <!-- Lista de alertas -->
        <CollectionView ItemsSource="{Binding Alertas}"
                       Grid.Row="1">
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
    </Grid>
</ContentPage>
```

```csharp
// Features/Alertas/AlertasPage.xaml.cs
namespace Phanteon.Features.Alertas;

public partial class AlertasPage : ContentPage
{
    public AlertasPage(AlertasViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AlertasViewModel vm)
        {
            await vm.CargarAlertasCommand.ExecuteAsync(null);
        }
    }
}
```

---

### (Navegación):

**Configurar AppShell.xaml (con la nueva estructura):**

```xml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
       xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
       xmlns:main="clr-namespace:Phanteon.Features.Main"
       xmlns:alertas="clr-namespace:Phanteon.Features.Alertas"
       x:Class="Phanteon.AppShell"
       FlyoutBehavior="Flyout">

    <FlyoutItem Title="Home" Icon="home.png">
        <ShellContent Route="main"
                     ContentTemplate="{DataTemplate main:MainPage}"/>
    </FlyoutItem>

    <FlyoutItem Title="Alertas" Icon="alert.png">
        <ShellContent Route="alertas"
                     ContentTemplate="{DataTemplate alertas:AlertasPage}"/>
    </FlyoutItem>
</Shell>
```

**Registrar rutas adicionales en AppShell.xaml.cs:**
```csharp
namespace Phanteon;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Rutas para navegación programática
        Routing.RegisterRoute("dispositivodetail", typeof(Features.Dispositivos.DispositivoDetailPage));
    }
}
```

---

## 🐛 Problemas Comunes

**Para una lista completa de errores y soluciones, ver [06_Solucion_Problemas.md](06_Solucion_Problemas.md)**

### ❌ "Connection refused"
**Solución:** Verifica que el backend esté corriendo (`dotnet run` en DevicesAPI)

### ❌ "Cannot resolve service"
**Solución:** Registra el ViewModel/Page en `MauiProgram.cs`

---

## 📚 Documentos Relacionados

### Documentación Original
- **[03_Tu_Tarea.md](03_Tu_Tarea.md)** - Tu asignación específica con checklist
- **[04_Ejemplos_Visuales.md](04_Ejemplos_Visuales.md)** - Mockups y código de ejemplo
- **[05_Guia_Rapida_API.md](05_Guia_Rapida_API.md)** - Comandos y bindings XAML
- **[06_Solucion_Problemas.md](06_Solucion_Problemas.md)** - Errores comunes resueltos
- **[07_Como_Hacer_Commits.md](07_Como_Hacer_Commits.md)** - Guía de Git
- **[Postman/](Postman/)** - Testing de la API

### Nueva Documentación (Estructura Actualizada)
- **[08_Arquitectura.md](08_Arquitectura.md)** - 📐 Arquitectura completa del proyecto
- **[09_Configuracion_Servicios.md](09_Configuracion_Servicios.md)** - ⚙️ Setup de APIs con Refit
- **[10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md)** - 🚀 Guía rápida con ejemplos
- **[11_Lista_Tareas.md](11_Lista_Tareas.md)** - ✅ Checklist de tareas pendientes

---

**¡Listo para empezar! 🚀**

_Última actualización: 11/11/2025 - Estructura reorganizada a Feature-based Architecture_
