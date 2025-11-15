# 📖 Referencia de API - Proyecto Phanteon

## 🎯 Cómo Usar Esta Guía

1. **Busca** la entidad que necesitas (Dispositivos, Alertas, etc.)
2. **Copia** el nombre del comando
3. **Pega** en tu XAML: `Command="{Binding NOMBRE_COMANDO}"`

**Nota:** Los comandos marcados con 🔜 están preparados pero la API no los soporta todavía.

---

## 📱 DISPOSITIVOS

### Propiedades para Binding

| Propiedad | Descripción | Uso en XAML |
|-----------|-------------|-------------|
| `Dispositivos` | Lista de dispositivos | `ItemsSource="{Binding Dispositivos}"` |
| `DispositivoSeleccionado` | Dispositivo seleccionado | `SelectedItem="{Binding DispositivoSeleccionado}"` |
| `EstaCargando` | Indicador de carga | `IsRunning="{Binding EstaCargando}"` |
| `MensajeError` | Mensaje de error | `Text="{Binding MensajeError}"` |

### Comandos Disponibles

| Estado | Comando | API | Uso |
|--------|---------|-----|-----|
| ✅ | `CargarDispositivosCommand` | GET /api/dispositivos/getall | Cargar todos los dispositivos |
| ✅ | `ObtenerDispositivoPorIdCommand` | GET /api/dispositivos/getbyid/{id} | Obtener dispositivo por ID |
| ✅ | `CrearDispositivoCommand` | POST /api/dispositivos/post | Crear nuevo dispositivo |
| 🔜 | `ActualizarDispositivoCommand` | PUT /api/dispositivos/put/{id} | Actualizar dispositivo |
| 🔜 | `EliminarDispositivoCommand` | DELETE /api/dispositivos/delete/{id} | Eliminar dispositivo |

### Ejemplo XAML

```xaml
<Grid>
    <!-- Botón cargar -->
    <Button Text="Cargar Dispositivos"
            Command="{Binding CargarDispositivosCommand}" />

    <!-- Lista de dispositivos -->
    <CollectionView ItemsSource="{Binding Dispositivos}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Label Text="{Binding SerialDispositivo}" />
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>

    <!-- Indicador de carga -->
    <ActivityIndicator IsRunning="{Binding EstaCargando}" />

    <!-- Mensaje de error -->
    <Label Text="{Binding MensajeError}" TextColor="Red" />
</Grid>
```

---

## 🚨 ALERTAS

### Propiedades para Binding

| Propiedad | Descripción | Uso en XAML |
|-----------|-------------|-------------|
| `Alertas` | Lista de alertas | `ItemsSource="{Binding Alertas}"` |
| `AlertaSeleccionada` | Alerta seleccionada | `SelectedItem="{Binding AlertaSeleccionada}"` |
| `EstaCargando` | Indicador de carga | `IsRunning="{Binding EstaCargando}"` |
| `MensajeError` | Mensaje de error | `Text="{Binding MensajeError}"` |

### Comandos Disponibles

| Estado | Comando | API | Uso |
|--------|---------|-----|-----|
| ✅ | `CargarAlertasCommand` | GET /api/alertas/getall | Cargar todas las alertas |
| ✅ | `ObtenerAlertaPorIdCommand` | GET /api/alertas/getbyid/{id} | Obtener alerta por ID |
| ✅ | `CrearAlertaCommand` | POST /api/alertas/post | Crear nueva alerta |
| 🔜 | `ActualizarAlertaCommand` | PUT /api/alertas/put/{id} | Actualizar alerta |
| 🔜 | `EliminarAlertaCommand` | DELETE /api/alertas/delete/{id} | Eliminar alerta |
| 🔜 | `MarcarAlertaProcesadaCommand` | PATCH /api/alertas/patch/{id} | Marcar como procesada |

### Ejemplo XAML

```xaml
<RefreshView IsRefreshing="{Binding EstaCargando}"
             Command="{Binding CargarAlertasCommand}">
    <CollectionView ItemsSource="{Binding Alertas}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <StackLayout>
                    <Label Text="{Binding TipoEvento}" FontAttributes="Bold" />
                    <Label Text="{Binding Severidad}" TextColor="Red" />
                    <Label Text="{Binding MarcaTiempo}" />
                </StackLayout>
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</RefreshView>
```

---

## 📊 HISTORIAL DISPOSITIVOS

### Propiedades para Binding

| Propiedad | Descripción | Uso en XAML |
|-----------|-------------|-------------|
| `Historiales` | Lista de historiales | `ItemsSource="{Binding Historiales}"` |
| `HistorialSeleccionado` | Historial seleccionado | `SelectedItem="{Binding HistorialSeleccionado}"` |
| `EstaCargando` | Indicador de carga | `IsRunning="{Binding EstaCargando}"` |
| `MensajeError` | Mensaje de error | `Text="{Binding MensajeError}"` |

### Comandos Disponibles

| Estado | Comando | API | Uso |
|--------|---------|-----|-----|
| ✅ | `CargarHistorialesCommand` | GET /api/historialdispositivos/getall | Cargar todo el historial |
| ✅ | `ObtenerHistorialPorIdCommand` | GET /api/historialdispositivos/getbyid/{id} | Obtener historial por ID |
| ✅ | `CrearHistorialCommand` | POST /api/historialdispositivos/post | Crear registro de historial |
| 🔜 | `ActualizarHistorialCommand` | PUT /api/historialdispositivos/put/{id} | Actualizar historial |
| 🔜 | `EliminarHistorialCommand` | DELETE /api/historialdispositivos/delete/{id} | Eliminar historial |
| 🔜 | `ObtenerHistorialPorDispositivoCommand` | GET /api/historialdispositivos/getbydispositivo/{id} | Obtener por dispositivo |

### Ejemplo XAML

```xaml
<Grid RowDefinitions="Auto,*">
    <Button Grid.Row="0"
            Text="Cargar Historial"
            Command="{Binding CargarHistorialesCommand}" />

    <CollectionView Grid.Row="1"
                    ItemsSource="{Binding Historiales}">
        <CollectionView.ItemTemplate>
            <DataTemplate>
                <Label Text="{Binding MarcaTiempo, StringFormat='{0:dd/MM/yyyy HH:mm}'}" />
            </DataTemplate>
        </CollectionView.ItemTemplate>
    </CollectionView>
</Grid>
```

---

## 👤 USUARIOS

### Propiedades para Binding

| Propiedad | Descripción | Uso en XAML |
|-----------|-------------|-------------|
| `Usuarios` | Lista de usuarios | `ItemsSource="{Binding Usuarios}"` |
| `UsuarioSeleccionado` | Usuario seleccionado | `SelectedItem="{Binding UsuarioSeleccionado}"` |
| `UsuarioActual` | Usuario autenticado | `Text="{Binding UsuarioActual.NombreUsuario}"` |
| `EstaCargando` | Indicador de carga | `IsRunning="{Binding EstaCargando}"` |
| `MensajeError` | Mensaje de error | `Text="{Binding MensajeError}"` |

### Comandos Disponibles

| Estado | Comando | API | Uso |
|--------|---------|-----|-----|
| ✅ | `CargarUsuariosCommand` | GET /api/usuarios/getall | Cargar todos los usuarios |
| ✅ | `ObtenerUsuarioPorIdCommand` | GET /api/usuarios/getbyid/{id} | Obtener usuario por ID |
| ✅ | `CrearUsuarioCommand` | POST /api/usuarios/post | Crear nuevo usuario (registro) |
| 🔜 | `ActualizarUsuarioCommand` | PUT /api/usuarios/put/{id} | Actualizar usuario |
| 🔜 | `EliminarUsuarioCommand` | DELETE /api/usuarios/delete/{id} | Eliminar usuario |
| ✅ | `LogoutCommand` | - | Cerrar sesión (local) |

### Ejemplo XAML - Formulario de Registro

```xaml
<VerticalStackLayout Spacing="10" Padding="20">
    <Entry Placeholder="Nombre de Usuario"
           Text="{Binding NuevoUsuario.NombreUsuario}" />

    <Entry Placeholder="Correo"
           Text="{Binding NuevoUsuario.Correo}" />

    <Entry Placeholder="Contraseña"
           IsPassword="True"
           Text="{Binding NuevoUsuario.PasswordHash}" />

    <Button Text="Registrar"
            Command="{Binding CrearUsuarioCommand}"
            CommandParameter="{Binding NuevoUsuario}" />

    <ActivityIndicator IsRunning="{Binding EstaCargando}" />

    <Label Text="{Binding MensajeError}"
           TextColor="Red" />
</VerticalStackLayout>
```

---

## 🔧 Converters Disponibles

### InvertedBoolConverter
Invierte un booleano (útil para deshabilitar botones durante la carga).

```xaml
<Button IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}" />
```

### StringNotEmptyConverter
Retorna `true` si el string NO está vacío (útil para mostrar mensajes de error).

```xaml
<Label IsVisible="{Binding MensajeError, Converter={StaticResource StringNotEmptyConverter}}" />
```

---

## 💡 Patrones Comunes

### 1. Lista con Pull-to-Refresh

```xaml
<RefreshView IsRefreshing="{Binding EstaCargando}"
             Command="{Binding CargarDispositivosCommand}">
    <CollectionView ItemsSource="{Binding Dispositivos}">
        <!-- Items -->
    </CollectionView>
</RefreshView>
```

### 2. Botón con Parámetro

```xaml
<Button Text="Ver Detalles"
        Command="{Binding ObtenerDispositivoPorIdCommand}"
        CommandParameter="{Binding IdDispositivo}" />
```

### 3. Botón Deshabilitado Durante Carga

```xaml
<Button Text="Guardar"
        Command="{Binding CrearDispositivoCommand}"
        IsEnabled="{Binding EstaCargando, Converter={StaticResource InvertedBoolConverter}}" />
```

### 4. Indicador de Carga + Lista

```xaml
<Grid RowDefinitions="Auto,*">
    <ActivityIndicator Grid.Row="0"
                       IsRunning="{Binding EstaCargando}"
                       IsVisible="{Binding EstaCargando}" />

    <CollectionView Grid.Row="1"
                    ItemsSource="{Binding Dispositivos}" />
</Grid>
```

---

## 📝 Uso desde Code-Behind

Si necesitas ejecutar comandos desde C#:

```csharp
public partial class MiPagina : ContentPage
{
    private readonly DispositivosViewModel _viewModel;

    public MiPagina(DispositivosViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Ejecutar comando al aparecer la página
        if (_viewModel.CargarDispositivosCommand.CanExecute(null))
        {
            await _viewModel.CargarDispositivosCommand.ExecuteAsync(null);
        }
    }
}
```

---

## ⚠️ Estado de la API

| Entidad | GET All | GET by ID | POST | PUT | DELETE |
|---------|---------|-----------|------|-----|--------|
| Alertas | ✅ | ✅ | ✅ | ❌ | ❌ |
| Dispositivos | ✅ | ✅ | ✅ | ❌ | ❌ |
| Historial | ✅ | ✅ | ✅ | ❌ | ❌ |
| Usuarios | ✅ | ✅ | ✅ | ❌ | ❌ |

**Leyenda:**
- ✅ = Implementado y funcionando
- ❌ = No implementado en la API (pero preparado en la app)
- 🔜 = Comando listo para usar cuando la API lo implemente

---

## 📚 Archivos Relacionados

- **`Helpers/ReferenciasAPI.cs`** - Constantes con nombres de comandos
- **`GUIA_PARA_DESARROLLADORES.md`** - Guía completa del proyecto
- **`ViewModels/`** - Carpeta con todos los ViewModels implementados

---

## 🚀 Resumen Rápido

**Para agregar un botón que cargue datos:**
```xaml
<Button Text="Cargar" Command="{Binding CargarDispositivosCommand}" />
```

**Para mostrar una lista:**
```xaml
<CollectionView ItemsSource="{Binding Dispositivos}" />
```

**Para mostrar estado de carga:**
```xaml
<ActivityIndicator IsRunning="{Binding EstaCargando}" />
```

**Para mostrar errores:**
```xaml
<Label Text="{Binding MensajeError}" TextColor="Red" />
```

---
