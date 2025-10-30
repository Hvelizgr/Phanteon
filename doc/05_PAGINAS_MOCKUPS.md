# 🎨 Mockups y Ejemplos de Páginas XAML

> Guía visual y código de ejemplo para las páginas XAML del proyecto Phanteon

**Tiempo de lectura:** 30-40 minutos
**Para:** Persona 2 (Desarrollador de Páginas XAML)

---

## 📑 Índice

1. [Página 1: LoginPage](#página-1-loginpage)
2. [Página 2: DispositivosPage](#página-2-dispositivospage-ya-implementada)
3. [Página 3: AlertasPage](#página-3-alertaspage)
4. [Página 4: DetalleDispositivoPage](#página-4-detalledispositivopage)
5. [Página 5: DiagnosticoPage](#página-5-diagnosticopage-ya-implementada)
6. [Estilos Comunes](#estilos-comunes)
7. [Navegación entre Páginas](#navegación-entre-páginas)

---

## Página 1: LoginPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│                                          │
│          [LOGO PHANTEON]                 │
│     Sistema de Monitoreo IoT             │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ Correo Electrónico                 │  │
│  │ usuario@ejemplo.com                │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ Contraseña                         │  │
│  │ ••••••••                           │  │
│  └────────────────────────────────────┘  │
│                                          │
│  [ ] Recordar mi sesión                  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │       INICIAR SESIÓN               │  │
│  └────────────────────────────────────┘  │
│                                          │
│       ¿Olvidaste tu contraseña?          │
│                                          │
│  [ActivityIndicator] Iniciando sesión... │
│  [Label] Error: Credenciales inválidas   │
│                                          │
└──────────────────────────────────────────┘
```

### Componentes UI

- **Entry** para correo (Keyboard: Email)
- **Entry** para contraseña (IsPassword: true)
- **CheckBox** para "Recordar sesión"
- **Button** para "Iniciar Sesión"
- **Label** para errores
- **ActivityIndicator** para loading

### Código XAML Completo

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodels="clr-namespace:Phanteon.ViewModels"
             x:Class="Phanteon.Views.LoginPage"
             x:DataType="viewmodels:LoginViewModel"
             Shell.NavBarIsVisible="False"
             BackgroundColor="{StaticResource Primary}">

    <Grid RowDefinitions="Auto,*,Auto" Padding="20">

        <!-- Header -->
        <VerticalStackLayout Grid.Row="0"
                           Spacing="10"
                           Margin="0,40,0,0">
            <Image Source="phanteon_logo.png"
                   HeightRequest="80"
                   WidthRequest="80"
                   HorizontalOptions="Center"/>
            <Label Text="PHANTEON"
                   FontSize="32"
                   FontAttributes="Bold"
                   HorizontalOptions="Center"
                   TextColor="White"/>
            <Label Text="Sistema de Monitoreo IoT"
                   FontSize="16"
                   HorizontalOptions="Center"
                   TextColor="White"
                   Opacity="0.8"/>
        </VerticalStackLayout>

        <!-- Login Form -->
        <Border Grid.Row="1"
                BackgroundColor="White"
                StrokeThickness="0"
                Margin="0,40,0,0"
                VerticalOptions="Center">
            <Border.StrokeShape>
                <RoundRectangle CornerRadius="20"/>
            </Border.StrokeShape>

            <VerticalStackLayout Padding="30" Spacing="20">

                <!-- Email Entry -->
                <VerticalStackLayout Spacing="5">
                    <Label Text="Correo Electrónico"
                           TextColor="{StaticResource Gray900}"
                           FontSize="14"/>
                    <Entry Text="{Binding Correo}"
                           Placeholder="usuario@ejemplo.com"
                           Keyboard="Email"
                           PlaceholderColor="{StaticResource Gray400}"
                           TextColor="{StaticResource Gray900}"/>
                    <Label Text="{Binding CorreoError}"
                           TextColor="{StaticResource Danger}"
                           FontSize="12"
                           IsVisible="{Binding HasCorreoError}"/>
                </VerticalStackLayout>

                <!-- Password Entry -->
                <VerticalStackLayout Spacing="5">
                    <Label Text="Contraseña"
                           TextColor="{StaticResource Gray900}"
                           FontSize="14"/>
                    <Entry Text="{Binding Password}"
                           Placeholder="••••••••"
                           IsPassword="True"
                           PlaceholderColor="{StaticResource Gray400}"
                           TextColor="{StaticResource Gray900}"/>
                    <Label Text="{Binding PasswordError}"
                           TextColor="{StaticResource Danger}"
                           FontSize="12"
                           IsVisible="{Binding HasPasswordError}"/>
                </VerticalStackLayout>

                <!-- Remember Me -->
                <HorizontalStackLayout Spacing="10">
                    <CheckBox IsChecked="{Binding RecordarSesion}"
                              Color="{StaticResource Primary}"/>
                    <Label Text="Recordar mi sesión"
                           TextColor="{StaticResource Gray900}"
                           VerticalOptions="Center"/>
                </HorizontalStackLayout>

                <!-- Login Button -->
                <Button Text="INICIAR SESIÓN"
                        Command="{Binding LoginCommand}"
                        BackgroundColor="{StaticResource Primary}"
                        TextColor="White"
                        FontAttributes="Bold"
                        CornerRadius="10"
                        Margin="0,10,0,0"
                        HeightRequest="50"/>

                <!-- Forgot Password -->
                <Label Text="¿Olvidaste tu contraseña?"
                       HorizontalOptions="Center"
                       TextColor="{StaticResource Primary}"
                       FontSize="14">
                    <Label.GestureRecognizers>
                        <TapGestureRecognizer Command="{Binding OlvidoPasswordCommand}"/>
                    </Label.GestureRecognizers>
                </Label>

                <!-- Loading Indicator -->
                <ActivityIndicator IsRunning="{Binding IsBusy}"
                                 Color="{StaticResource Primary}"
                                 IsVisible="{Binding IsBusy}"
                                 HeightRequest="30"/>

                <!-- Error Message -->
                <Label Text="{Binding ErrorMessage}"
                       TextColor="{StaticResource Danger}"
                       FontSize="14"
                       HorizontalOptions="Center"
                       IsVisible="{Binding HasError}"/>

            </VerticalStackLayout>
        </Border>

        <!-- Footer -->
        <Label Grid.Row="2"
               Text="Versión 1.0.0 - Phanteon © 2024"
               HorizontalOptions="Center"
               TextColor="White"
               Opacity="0.6"
               FontSize="12"
               Margin="0,0,0,20"/>
    </Grid>

</ContentPage>
```

### Code-Behind (LoginPage.xaml.cs)

```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private string correo = string.Empty;

[ObservableProperty]
private string password = string.Empty;

[ObservableProperty]
private bool recordarSesion;

[ObservableProperty]
private string correoError = string.Empty;

[ObservableProperty]
private string passwordError = string.Empty;

[ObservableProperty]
private bool hasCorreoError;

[ObservableProperty]
private bool hasPasswordError;

[ObservableProperty]
private string errorMessage = string.Empty;

[ObservableProperty]
private bool hasError;

[ObservableProperty]
private bool isBusy;

[RelayCommand]
private async Task Login() { }

[RelayCommand]
private async Task OlvidoPassword() { }
```

### Endpoints Usados

- `POST /api/usuarios/login` (debes agregarlo al backend)
- `GET /api/usuarios/getbyid/{id}`

---

## Página 2: DispositivosPage (Ya Implementada ✅)

Esta página ya fue implementada por Héctor. Revisa el archivo `Views/DispositivosPage.xaml` como referencia.

### Características Implementadas

- CollectionView con lista de dispositivos
- RefreshView para pull-to-refresh
- Estado vacío cuando no hay dispositivos
- Indicador de carga
- Navegación a DetalleDispositivoPage

---

## Página 3: AlertasPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│  Alertas                          [Filtro]│
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🔴 CRÍTICA                         │  │
│  │ Dispositivo DEV-001 Desconectado   │  │
│  │ Hace 5 minutos                     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🟡 ADVERTENCIA                     │  │
│  │ Temperatura alta en DEV-003        │  │
│  │ Hace 15 minutos                    │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ┌────────────────────────────────────┐  │
│  │ 🟢 INFORMACIÓN                     │  │
│  │ Actualización de firmware DEV-005  │  │
│  │ Hace 1 hora                        │  │
│  └────────────────────────────────────┘  │
│                                          │
│  [Pull to refresh]                       │
│                                          │
└──────────────────────────────────────────┘
```

### Código XAML Completo

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodels="clr-namespace:Phanteon.ViewModels"
             xmlns:models="clr-namespace:Phanteon.Models"
             x:Class="Phanteon.Views.AlertasPage"
             x:DataType="viewmodels:AlertasViewModel"
             Title="Alertas">

    <Grid RowDefinitions="Auto,*">

        <!-- Toolbar con filtros -->
        <HorizontalStackLayout Grid.Row="0"
                             Padding="15,10"
                             Spacing="10"
                             BackgroundColor="{StaticResource Primary}">

            <Label Text="Filtrar por:"
                   TextColor="White"
                   VerticalOptions="Center"/>

            <Picker ItemsSource="{Binding TiposAlerta}"
                    SelectedItem="{Binding TipoAlertaSeleccionado}"
                    TextColor="White"
                    TitleColor="White"
                    BackgroundColor="{StaticResource Primary}"
                    WidthRequest="150"/>

            <Button Text="🔄 Actualizar"
                    Command="{Binding RefreshCommand}"
                    BackgroundColor="Transparent"
                    TextColor="White"
                    HorizontalOptions="EndAndExpand"/>
        </HorizontalStackLayout>

        <!-- Lista de Alertas -->
        <RefreshView Grid.Row="1"
                     IsRefreshing="{Binding IsRefreshing}"
                     Command="{Binding RefreshCommand}">

            <CollectionView ItemsSource="{Binding AlertasFiltradas}"
                          SelectionMode="None">

                <!-- Empty State -->
                <CollectionView.EmptyView>
                    <VerticalStackLayout VerticalOptions="Center"
                                       HorizontalOptions="Center"
                                       Spacing="15"
                                       Padding="40">
                        <Label Text="✓"
                               FontSize="60"
                               HorizontalOptions="Center"
                               TextColor="{StaticResource Primary}"/>
                        <Label Text="No hay alertas"
                               FontSize="18"
                               FontAttributes="Bold"
                               HorizontalOptions="Center"/>
                        <Label Text="Todos los dispositivos están funcionando correctamente"
                               FontSize="14"
                               HorizontalOptions="Center"
                               TextColor="{StaticResource Gray600}"/>
                    </VerticalStackLayout>
                </CollectionView.EmptyView>

                <!-- Item Template -->
                <CollectionView.ItemTemplate>
                    <DataTemplate x:DataType="models:Alerta">
                        <Border Margin="15,10,15,10"
                                BackgroundColor="White"
                                StrokeThickness="0"
                                Padding="0">
                            <Border.StrokeShape>
                                <RoundRectangle CornerRadius="10"/>
                            </Border.StrokeShape>

                            <Border.Shadow>
                                <Shadow Brush="Black"
                                       Offset="0,2"
                                       Radius="8"
                                       Opacity="0.1"/>
                            </Border.Shadow>

                            <Grid ColumnDefinitions="10,*,Auto"
                                  Padding="0">

                                <!-- Color Bar según tipo -->
                                <BoxView Grid.Column="0"
                                        BackgroundColor="{Binding TipoAlerta, Converter={StaticResource AlertaColorConverter}}"
                                        CornerRadius="10,0,0,10"/>

                                <!-- Contenido -->
                                <VerticalStackLayout Grid.Column="1"
                                                   Padding="15"
                                                   Spacing="5">
                                    <Label Text="{Binding TipoAlerta}"
                                           FontSize="12"
                                           FontAttributes="Bold"
                                           TextColor="{Binding TipoAlerta, Converter={StaticResource AlertaColorConverter}}"/>

                                    <Label Text="{Binding Descripcion}"
                                           FontSize="16"
                                           FontAttributes="Bold"
                                           TextColor="{StaticResource Gray900}"/>

                                    <Label Text="{Binding FechaHora, StringFormat='Hace {0:dd/MM/yyyy HH:mm}'}"
                                           FontSize="12"
                                           TextColor="{StaticResource Gray600}"/>
                                </VerticalStackLayout>

                                <!-- Estado -->
                                <Label Grid.Column="2"
                                       Text="{Binding Estado}"
                                       FontSize="12"
                                       Padding="10,0,15,0"
                                       VerticalOptions="Center"
                                       TextColor="{StaticResource Gray600}"/>

                            </Grid>
                        </Border>
                    </DataTemplate>
                </CollectionView.ItemTemplate>
            </CollectionView>
        </RefreshView>

    </Grid>

</ContentPage>
```

### Code-Behind (AlertasPage.xaml.cs)

```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

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

        // Cargar alertas al mostrar la página
        if (BindingContext is AlertasViewModel vm)
        {
            await vm.CargarAlertasAsync();
        }
    }
}
```

### Converter Necesario (AlertaColorConverter.cs)

Crear en carpeta `Helpers/`:

```csharp
using System.Globalization;

namespace Phanteon.Helpers;

public class AlertaColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string tipoAlerta)
        {
            return tipoAlerta.ToUpper() switch
            {
                "CRÍTICA" or "CRITICAL" => Colors.Red,
                "ADVERTENCIA" or "WARNING" => Colors.Orange,
                "INFORMACIÓN" or "INFO" => Colors.Green,
                _ => Colors.Gray
            };
        }
        return Colors.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
```

### Registrar el Converter en App.xaml

```xml
<Application.Resources>
    <ResourceDictionary>
        <helpers:AlertaColorConverter x:Key="AlertaColorConverter"/>
    </ResourceDictionary>
</Application.Resources>
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private ObservableCollection<Alerta> alertas = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasFiltradas = new();

[ObservableProperty]
private List<string> tiposAlerta = new() { "Todas", "Crítica", "Advertencia", "Información" };

[ObservableProperty]
private string tipoAlertaSeleccionado = "Todas";

[ObservableProperty]
private bool isRefreshing;

[RelayCommand]
private async Task CargarAlertas() { }

[RelayCommand]
private async Task Refresh() { }
```

### Endpoints Usados

- `GET /api/alertas/getall`
- `GET /api/alertas/getbyid/{id}`

---

## Página 4: DetalleDispositivoPage

### Mockup ASCII

```
┌──────────────────────────────────────────┐
│  ← DEV-001                        [Editar]│
│                                          │
│  ┌────────────────────────────────────┐  │
│  │  Estado: ACTIVO                 🟢 │  │
│  │  Última vista: Hace 2 minutos      │  │
│  └────────────────────────────────────┘  │
│                                          │
│  INFORMACIÓN GENERAL                     │
│  ┌────────────────────────────────────┐  │
│  │  Serial: DEV-001                   │  │
│  │  MAC: 00:1A:2B:3C:4D:5E           │  │
│  │  Firmware: v2.1.5                  │  │
│  │  Dirección: Av. Principal #123     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  UBICACIÓN                               │
│  ┌────────────────────────────────────┐  │
│  │  [MAPA]                            │  │
│  │  Lat: -12.0464                     │  │
│  │  Lon: -77.0428                     │  │
│  └────────────────────────────────────┘  │
│                                          │
│  ALERTAS RECIENTES (3)                   │
│  • Temperatura alta - Hace 1h            │
│  • Actualización firmware - Hace 2d      │
│  • Conexión restaurada - Hace 1w         │
│                                          │
│  HISTORIAL                               │
│  [Ver historial completo →]             │
│                                          │
└──────────────────────────────────────────┘
```

### Código XAML Completo

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:viewmodels="clr-namespace:Phanteon.ViewModels"
             xmlns:models="clr-namespace:Phanteon.Models"
             x:Class="Phanteon.Views.DetalleDispositivoPage"
             x:DataType="viewmodels:DetalleDispositivoViewModel"
             Title="{Binding Dispositivo.SerialDispositivo}">

    <ScrollView>
        <VerticalStackLayout Padding="15" Spacing="20">

            <!-- Estado Header -->
            <Border BackgroundColor="{StaticResource Primary}"
                    StrokeThickness="0"
                    Padding="20">
                <Border.StrokeShape>
                    <RoundRectangle CornerRadius="15"/>
                </Border.StrokeShape>

                <Grid ColumnDefinitions="*,Auto">
                    <VerticalStackLayout Grid.Column="0" Spacing="5">
                        <Label Text="{Binding EstadoTexto}"
                               FontSize="20"
                               FontAttributes="Bold"
                               TextColor="White"/>
                        <Label Text="{Binding UltimaVistaTexto}"
                               FontSize="14"
                               TextColor="White"
                               Opacity="0.9"/>
                    </VerticalStackLayout>

                    <Label Grid.Column="1"
                           Text="{Binding EstadoEmoji}"
                           FontSize="40"
                           VerticalOptions="Center"/>
                </Grid>
            </Border>

            <!-- Información General -->
            <VerticalStackLayout Spacing="10">
                <Label Text="INFORMACIÓN GENERAL"
                       FontSize="14"
                       FontAttributes="Bold"
                       TextColor="{StaticResource Gray600}"/>

                <Border BackgroundColor="White"
                        StrokeThickness="1"
                        Stroke="{StaticResource Gray200}"
                        Padding="15">
                    <Border.StrokeShape>
                        <RoundRectangle CornerRadius="10"/>
                    </Border.StrokeShape>

                    <VerticalStackLayout Spacing="12">
                        <!-- Serial -->
                        <Grid ColumnDefinitions="120,*">
                            <Label Grid.Column="0"
                                   Text="Serial:"
                                   FontAttributes="Bold"
                                   TextColor="{StaticResource Gray900}"/>
                            <Label Grid.Column="1"
                                   Text="{Binding Dispositivo.SerialDispositivo}"
                                   TextColor="{StaticResource Gray700}"/>
                        </Grid>

                        <!-- MAC -->
                        <Grid ColumnDefinitions="120,*">
                            <Label Grid.Column="0"
                                   Text="MAC:"
                                   FontAttributes="Bold"
                                   TextColor="{StaticResource Gray900}"/>
                            <Label Grid.Column="1"
                                   Text="{Binding Dispositivo.MAC}"
                                   TextColor="{StaticResource Gray700}"/>
                        </Grid>

                        <!-- Firmware -->
                        <Grid ColumnDefinitions="120,*">
                            <Label Grid.Column="0"
                                   Text="Firmware:"
                                   FontAttributes="Bold"
                                   TextColor="{StaticResource Gray900}"/>
                            <Label Grid.Column="1"
                                   Text="{Binding Dispositivo.Firmware}"
                                   TextColor="{StaticResource Gray700}"/>
                        </Grid>

                        <!-- Dirección -->
                        <Grid ColumnDefinitions="120,*">
                            <Label Grid.Column="0"
                                   Text="Dirección:"
                                   FontAttributes="Bold"
                                   TextColor="{StaticResource Gray900}"/>
                            <Label Grid.Column="1"
                                   Text="{Binding Dispositivo.Direccion}"
                                   TextColor="{StaticResource Gray700}"
                                   LineBreakMode="WordWrap"/>
                        </Grid>

                        <!-- Registro -->
                        <Grid ColumnDefinitions="120,*">
                            <Label Grid.Column="0"
                                   Text="Registrado:"
                                   FontAttributes="Bold"
                                   TextColor="{StaticResource Gray900}"/>
                            <Label Grid.Column="1"
                                   Text="{Binding Dispositivo.Registro, StringFormat='{0:dd/MM/yyyy HH:mm}'}"
                                   TextColor="{StaticResource Gray700}"/>
                        </Grid>
                    </VerticalStackLayout>
                </Border>
            </VerticalStackLayout>

            <!-- Ubicación -->
            <VerticalStackLayout Spacing="10">
                <Label Text="UBICACIÓN"
                       FontSize="14"
                       FontAttributes="Bold"
                       TextColor="{StaticResource Gray600}"/>

                <Border BackgroundColor="White"
                        StrokeThickness="1"
                        Stroke="{StaticResource Gray200}"
                        Padding="15">
                    <Border.StrokeShape>
                        <RoundRectangle CornerRadius="10"/>
                    </Border.StrokeShape>

                    <VerticalStackLayout Spacing="10">
                        <!-- Placeholder para mapa -->
                        <Border HeightRequest="200"
                                BackgroundColor="{StaticResource Gray100}"
                                StrokeThickness="0">
                            <Border.StrokeShape>
                                <RoundRectangle CornerRadius="8"/>
                            </Border.StrokeShape>
                            <Label Text="🗺️ MAPA"
                                   FontSize="40"
                                   HorizontalOptions="Center"
                                   VerticalOptions="Center"
                                   TextColor="{StaticResource Gray400}"/>
                        </Border>

                        <Grid ColumnDefinitions="*,*" ColumnSpacing="10">
                            <VerticalStackLayout Grid.Column="0">
                                <Label Text="Latitud"
                                       FontSize="12"
                                       TextColor="{StaticResource Gray600}"/>
                                <Label Text="{Binding Dispositivo.Latitud, StringFormat='{0:F4}'}"
                                       FontSize="14"
                                       FontAttributes="Bold"
                                       TextColor="{StaticResource Gray900}"/>
                            </VerticalStackLayout>

                            <VerticalStackLayout Grid.Column="1">
                                <Label Text="Longitud"
                                       FontSize="12"
                                       TextColor="{StaticResource Gray600}"/>
                                <Label Text="{Binding Dispositivo.Longitud, StringFormat='{0:F4}'}"
                                       FontSize="14"
                                       FontAttributes="Bold"
                                       TextColor="{StaticResource Gray900}"/>
                            </VerticalStackLayout>
                        </Grid>

                        <Button Text="📍 Ver en Google Maps"
                                Command="{Binding AbrirMapaCommand}"
                                BackgroundColor="{StaticResource Primary}"
                                TextColor="White"/>
                    </VerticalStackLayout>
                </Border>
            </VerticalStackLayout>

            <!-- Alertas Recientes -->
            <VerticalStackLayout Spacing="10">
                <Grid ColumnDefinitions="*,Auto">
                    <Label Grid.Column="0"
                           Text="ALERTAS RECIENTES"
                           FontSize="14"
                           FontAttributes="Bold"
                           TextColor="{StaticResource Gray600}"/>
                    <Label Grid.Column="1"
                           Text="{Binding AlertasRecientes.Count, StringFormat='({0})'}"
                           FontSize="14"
                           FontAttributes="Bold"
                           TextColor="{StaticResource Primary}"/>
                </Grid>

                <Border BackgroundColor="White"
                        StrokeThickness="1"
                        Stroke="{StaticResource Gray200}"
                        Padding="15"
                        IsVisible="{Binding TieneAlertas}">
                    <Border.StrokeShape>
                        <RoundRectangle CornerRadius="10"/>
                    </Border.StrokeShape>

                    <CollectionView ItemsSource="{Binding AlertasRecientes}"
                                  SelectionMode="None">
                        <CollectionView.ItemTemplate>
                            <DataTemplate x:DataType="models:Alerta">
                                <Grid ColumnDefinitions="Auto,*"
                                      ColumnSpacing="10"
                                      Padding="0,5">
                                    <Label Grid.Column="0"
                                           Text="•"
                                           FontSize="20"
                                           TextColor="{Binding TipoAlerta, Converter={StaticResource AlertaColorConverter}}"
                                           VerticalOptions="Start"/>
                                    <VerticalStackLayout Grid.Column="1" Spacing="2">
                                        <Label Text="{Binding Descripcion}"
                                               FontSize="14"
                                               TextColor="{StaticResource Gray900}"/>
                                        <Label Text="{Binding FechaHora, StringFormat='Hace {0:dd/MM/yyyy}'}"
                                               FontSize="12"
                                               TextColor="{StaticResource Gray600}"/>
                                    </VerticalStackLayout>
                                </Grid>
                            </DataTemplate>
                        </CollectionView.ItemTemplate>
                    </CollectionView>
                </Border>

                <Label Text="No hay alertas recientes para este dispositivo"
                       FontSize="14"
                       TextColor="{StaticResource Gray600}"
                       HorizontalOptions="Center"
                       Padding="20"
                       IsVisible="{Binding TieneAlertas, Converter={StaticResource InvertedBoolConverter}}"/>
            </VerticalStackLayout>

            <!-- Historial -->
            <VerticalStackLayout Spacing="10">
                <Label Text="HISTORIAL"
                       FontSize="14"
                       FontAttributes="Bold"
                       TextColor="{StaticResource Gray600}"/>

                <Border BackgroundColor="White"
                        StrokeThickness="1"
                        Stroke="{StaticResource Gray200}"
                        Padding="15">
                    <Border.StrokeShape>
                        <RoundRectangle CornerRadius="10"/>
                    </Border.StrokeShape>

                    <VerticalStackLayout Spacing="15">
                        <Label Text="{Binding HistorialCount, StringFormat='Mostrando últimos {0} eventos'}"
                               FontSize="14"
                               TextColor="{StaticResource Gray700}"/>

                        <Button Text="📋 Ver historial completo →"
                                Command="{Binding VerHistorialCompletoCommand}"
                                BackgroundColor="Transparent"
                                TextColor="{StaticResource Primary}"
                                BorderColor="{StaticResource Primary}"
                                BorderWidth="1"/>
                    </VerticalStackLayout>
                </Border>
            </VerticalStackLayout>

            <!-- Acciones -->
            <Grid ColumnDefinitions="*,*" ColumnSpacing="10" Margin="0,10,0,20">
                <Button Grid.Column="0"
                        Text="✏️ Editar"
                        Command="{Binding EditarCommand}"
                        BackgroundColor="Transparent"
                        TextColor="{StaticResource Primary}"
                        BorderColor="{StaticResource Primary}"
                        BorderWidth="1"/>

                <Button Grid.Column="1"
                        Text="🗑️ Eliminar"
                        Command="{Binding EliminarCommand}"
                        BackgroundColor="{StaticResource Danger}"
                        TextColor="White"/>
            </Grid>

        </VerticalStackLayout>
    </ScrollView>

</ContentPage>
```

### Code-Behind (DetalleDispositivoPage.xaml.cs)

```csharp
using Phanteon.ViewModels;

namespace Phanteon.Views;

public partial class DetalleDispositivoPage : ContentPage
{
    public DetalleDispositivoPage(DetalleDispositivoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DetalleDispositivoViewModel vm)
        {
            await vm.CargarDetallesAsync();
        }
    }
}
```

### Propiedades del ViewModel Necesarias

```csharp
[ObservableProperty]
private Dispositivo dispositivo = new();

[ObservableProperty]
private ObservableCollection<Alerta> alertasRecientes = new();

[ObservableProperty]
private bool tieneAlertas;

[ObservableProperty]
private string estadoTexto = string.Empty;

[ObservableProperty]
private string estadoEmoji = string.Empty;

[ObservableProperty]
private string ultimaVistaTexto = string.Empty;

[ObservableProperty]
private int historialCount;

[RelayCommand]
private async Task CargarDetalles() { }

[RelayCommand]
private async Task AbrirMapa() { }

[RelayCommand]
private async Task VerHistorialCompleto() { }

[RelayCommand]
private async Task Editar() { }

[RelayCommand]
private async Task Eliminar() { }
```

### Endpoints Usados

- `GET /api/dispositivos/getbyid/{id}`
- `GET /api/alertas/getall` (filtrar por IdDispositivo)
- `GET /api/historial/getall` (filtrar por IdDispositivo)

---

## Página 5: DiagnosticoPage (Ya Implementada ✅)

Esta página ya fue implementada por Héctor. Revisa el archivo `Views/DiagnosticoPage.xaml` como referencia.

---

## Estilos Comunes

### Colores (App.xaml)

```xml
<Application.Resources>
    <ResourceDictionary>

        <!-- Colors -->
        <Color x:Key="Primary">#512BD4</Color>
        <Color x:Key="Secondary">#DFD8F7</Color>
        <Color x:Key="Tertiary">#2B0B98</Color>

        <Color x:Key="White">White</Color>
        <Color x:Key="Black">Black</Color>

        <Color x:Key="Gray100">#F5F5F5</Color>
        <Color x:Key="Gray200">#E8E8E8</Color>
        <Color x:Key="Gray300">#CCCCCC</Color>
        <Color x:Key="Gray400">#999999</Color>
        <Color x:Key="Gray500">#666666</Color>
        <Color x:Key="Gray600">#4D4D4D</Color>
        <Color x:Key="Gray700">#333333</Color>
        <Color x:Key="Gray800">#1A1A1A</Color>
        <Color x:Key="Gray900">#0D0D0D</Color>

        <Color x:Key="Success">#28A745</Color>
        <Color x:Key="Warning">#FFC107</Color>
        <Color x:Key="Danger">#DC3545</Color>
        <Color x:Key="Info">#17A2B8</Color>

    </ResourceDictionary>
</Application.Resources>
```

### Estilos de Texto

```xml
<!-- Title Styles -->
<Style x:Key="PageTitleStyle" TargetType="Label">
    <Setter Property="FontSize" Value="24"/>
    <Setter Property="FontAttributes" Value="Bold"/>
    <Setter Property="TextColor" Value="{StaticResource Gray900}"/>
</Style>

<!-- Subtitle Styles -->
<Style x:Key="SubtitleStyle" TargetType="Label">
    <Setter Property="FontSize" Value="18"/>
    <Setter Property="FontAttributes" Value="Bold"/>
    <Setter Property="TextColor" Value="{StaticResource Gray700}"/>
</Style>

<!-- Body Styles -->
<Style x:Key="BodyStyle" TargetType="Label">
    <Setter Property="FontSize" Value="14"/>
    <Setter Property="TextColor" Value="{StaticResource Gray700}"/>
</Style>

<!-- Caption Styles -->
<Style x:Key="CaptionStyle" TargetType="Label">
    <Setter Property="FontSize" Value="12"/>
    <Setter Property="TextColor" Value="{StaticResource Gray600}"/>
</Style>
```

---

## Navegación entre Páginas

### Registrar Rutas en AppShell.xaml.cs

```csharp
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas para navegación
        Routing.RegisterRoute(nameof(Views.LoginPage), typeof(Views.LoginPage));
        Routing.RegisterRoute(nameof(Views.DispositivosPage), typeof(Views.DispositivosPage));
        Routing.RegisterRoute(nameof(Views.AlertasPage), typeof(Views.AlertasPage));
        Routing.RegisterRoute(nameof(Views.DetalleDispositivoPage), typeof(Views.DetalleDispositivoPage));
        Routing.RegisterRoute(nameof(Views.DiagnosticoPage), typeof(Views.DiagnosticoPage));
    }
}
```

### Ejemplo de Navegación desde ViewModel

```csharp
// Navegar a DetalleDispositivoPage pasando el ID
[RelayCommand]
private async Task VerDetalle(int idDispositivo)
{
    await Shell.Current.GoToAsync($"{nameof(DetalleDispositivoPage)}?id={idDispositivo}");
}

// Navegar atrás
[RelayCommand]
private async Task Volver()
{
    await Shell.Current.GoToAsync("..");
}

// Navegar a página raíz
[RelayCommand]
private async Task IrAInicio()
{
    await Shell.Current.GoToAsync("//DispositivosPage");
}
```

### Recibir Parámetros en ViewModel

```csharp
[QueryProperty(nameof(IdDispositivo), "id")]
public partial class DetalleDispositivoViewModel : ObservableObject
{
    private int _idDispositivo;

    public int IdDispositivo
    {
        get => _idDispositivo;
        set
        {
            _idDispositivo = value;
            // Cargar detalles cuando se asigna el ID
            _ = CargarDetallesAsync();
        }
    }
}
```

---

## Checklist de Implementación

### Para cada página, asegúrate de:

- [ ] Crear archivo `.xaml` con la interfaz
- [ ] Crear archivo `.xaml.cs` con el code-behind
- [ ] Inyectar ViewModel en el constructor
- [ ] Registrar la página en `MauiProgram.cs`
- [ ] Registrar la ruta en `AppShell.xaml.cs`
- [ ] Crear el ViewModel correspondiente (Persona 1)
- [ ] Implementar binding de propiedades
- [ ] Implementar comandos
- [ ] Agregar manejo de errores
- [ ] Probar navegación

---

## Recursos Adicionales

- **[Microsoft MAUI Docs](https://learn.microsoft.com/en-us/dotnet/maui/)** - Documentación oficial
- **[XAML Controls](https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/)** - Lista de controles disponibles
- **[Data Binding](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/)** - Guía de binding
- **[Shell Navigation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation)** - Navegación con Shell

---

**Siguiente:** [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Volver al índice:** [README.md](README.md)
