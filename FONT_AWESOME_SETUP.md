# 🎨 Configuración de Font Awesome en Phanteon

## ✅ Estado Actual

**NuGet Package:** FontAwesome.Sharp v6.6.0 ✅ **INSTALADO**

Ahora solo necesitas **descargar las fuentes TTF** y configurarlas.

---

## 📥 Pasos de Instalación

### 1️⃣ Descargar Font Awesome (Gratis)

**Opción A: Desde la web oficial**
1. Ve a: https://fontawesome.com/download
2. Descarga la versión **"Free for Web"**
3. Descomprime el archivo ZIP

**Opción B: Descarga directa**
- Link directo: https://use.fontawesome.com/releases/v6.5.1/fontawesome-free-6.5.1-web.zip

---

### 2️⃣ Copiar Archivos de Fuente

Una vez descargado, necesitas estos 3 archivos del folder `webfonts/`:

```
📁 fontawesome-free-6.5.1-web/
  📁 webfonts/
    📄 fa-solid-900.ttf         ← Copiar este
    📄 fa-regular-400.ttf       ← Copiar este
    📄 fa-brands-400.ttf        ← Copiar este
```

**Copiarlos a:**
```
📁 Phanteon/
  📁 Resources/
    📁 Fonts/
      📄 fa-solid-900.ttf       ← Pegar aquí
      📄 fa-regular-400.ttf     ← Pegar aquí
      📄 fa-brands-400.ttf      ← Pegar aquí
```

---

### 3️⃣ Configurar MauiProgram.cs

Abre `MauiProgram.cs` y actualiza la sección `ConfigureFonts`:

```csharp
.ConfigureFonts(fonts =>
{
    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

    // ✨ Agregar Font Awesome
    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
    fonts.AddFont("fa-brands-400.ttf", "FontAwesomeBrands");
});
```

---

### 4️⃣ Verificar en el .csproj

Abre `Phanteon.csproj` y verifica que las fuentes estén incluidas:

```xml
<ItemGroup>
  <!-- Fuentes existentes -->
  <MauiFont Include="Resources\Fonts\OpenSans-Regular.ttf" />
  <MauiFont Include="Resources\Fonts\OpenSans-Semibold.ttf" />

  <!-- ✨ Font Awesome (agregar si no están) -->
  <MauiFont Include="Resources\Fonts\fa-solid-900.ttf" />
  <MauiFont Include="Resources\Fonts\fa-regular-400.ttf" />
  <MauiFont Include="Resources\Fonts\fa-brands-400.ttf" />
</ItemGroup>
```

Si no están, agrégalas manualmente.

---

## 🎯 Cómo Usar los Iconos

### Método 1: En un Label

```xml
<Label Text="&#xF015;"
       FontFamily="FontAwesomeSolid"
       FontSize="24"
       TextColor="{StaticResource Primary}" />
```

### Método 2: En un Button (ImageSource)

```xml
<Button Text="Guardar">
    <Button.ImageSource>
        <FontImageSource FontFamily="FontAwesomeSolid"
                        Glyph="&#xF0C7;"
                        Size="20"
                        Color="White"/>
    </Button.ImageSource>
</Button>
```

### Método 3: En ToolbarItem

```xml
<ToolbarItem Text="Agregar">
    <ToolbarItem.IconImageSource>
        <FontImageSource FontFamily="FontAwesomeSolid"
                        Glyph="&#xF067;"
                        Size="20"/>
    </ToolbarItem.IconImageSource>
</ToolbarItem>
```

---

## 📚 Diferencias entre Fuentes

| Fuente | Nombre en MAUI | Uso |
|--------|----------------|-----|
| fa-solid-900.ttf | `FontAwesomeSolid` | Iconos sólidos (más usados) |
| fa-regular-400.ttf | `FontAwesomeRegular` | Iconos con contorno |
| fa-brands-400.ttf | `FontAwesomeBrands` | Logos de marcas (Facebook, Twitter, etc.) |

### Ejemplos de Uso por Fuente

```xml
<!-- Solid (Sólido) - MÁS USADO -->
<Label Text="&#xF015;" FontFamily="FontAwesomeSolid" /> <!-- Home sólido -->

<!-- Regular (Contorno) -->
<Label Text="&#xF015;" FontFamily="FontAwesomeRegular" /> <!-- Home con contorno -->

<!-- Brands (Marcas) -->
<Label Text="&#xF09B;" FontFamily="FontAwesomeBrands" /> <!-- GitHub logo -->
<Label Text="&#xF099;" FontFamily="FontAwesomeBrands" /> <!-- Twitter logo -->
```

---

## ✅ Verificación

Después de instalar, compila y ejecuta:

```bash
dotnet clean
dotnet build
```

Si todo está bien configurado, deberías ver los iconos en tu app.

---

## 🐛 Solución de Problemas

### ❌ Los iconos no se ven (aparecen cuadros/caracteres raros)

**Causas comunes:**
1. Las fuentes TTF no están en `Resources/Fonts/`
2. No se agregaron en `MauiProgram.cs`
3. No se ejecutó `dotnet clean` después de agregar las fuentes
4. El nombre de la fuente está mal escrito

**Solución:**
```bash
# Limpiar y reconstruir
dotnet clean
dotnet build
```

### ❌ Error: "Could not find font family FontAwesomeSolid"

**Solución:**
Verifica que el nombre en `MauiProgram.cs` coincida con el usado en XAML:
- En XAML: `FontFamily="FontAwesomeSolid"`
- En MauiProgram: `fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");`

### ❌ Los archivos TTF no se copian al build

**Solución:**
Asegúrate de que estén marcados como `<MauiFont>` en el `.csproj`:
```xml
<MauiFont Include="Resources\Fonts\fa-solid-900.ttf" />
```

---

## 🔍 Buscar Códigos de Iconos

### Font Awesome Cheatsheet
https://fontawesome.com/search?o=r&m=free

### Cómo convertir:
1. Busca el icono en: https://fontawesome.com/icons
2. Copia el código Unicode (ejemplo: `f015`)
3. Conviértelo a: `&#xF015;`

**Fórmula:** `f015` → `&#xF015;`

---

## 📦 Iconos Más Usados (Quick Reference)

```xml
<!-- Navegación -->
<Label Text="&#xF015;" FontFamily="FontAwesomeSolid" /> <!-- Home -->
<Label Text="&#xF053;" FontFamily="FontAwesomeSolid" /> <!-- Chevron Left -->
<Label Text="&#xF054;" FontFamily="FontAwesomeSolid" /> <!-- Chevron Right -->
<Label Text="&#xF0C9;" FontFamily="FontAwesomeSolid" /> <!-- Bars/Menu -->

<!-- Acciones -->
<Label Text="&#xF067;" FontFamily="FontAwesomeSolid" /> <!-- Plus -->
<Label Text="&#xF304;" FontFamily="FontAwesomeSolid" /> <!-- Pen/Edit -->
<Label Text="&#xF2ED;" FontFamily="FontAwesomeSolid" /> <!-- Trash -->
<Label Text="&#xF002;" FontFamily="FontAwesomeSolid" /> <!-- Search -->
<Label Text="&#xF021;" FontFamily="FontAwesomeSolid" /> <!-- Sync -->

<!-- Estados -->
<Label Text="&#xF058;" FontFamily="FontAwesomeSolid" /> <!-- Check Circle -->
<Label Text="&#xF057;" FontFamily="FontAwesomeSolid" /> <!-- Times Circle -->
<Label Text="&#xF06A;" FontFamily="FontAwesomeSolid" /> <!-- Exclamation Circle -->
<Label Text="&#xF05A;" FontFamily="FontAwesomeSolid" /> <!-- Info Circle -->

<!-- Usuario -->
<Label Text="&#xF007;" FontFamily="FontAwesomeSolid" /> <!-- User -->
<Label Text="&#xF013;" FontFamily="FontAwesomeSolid" /> <!-- Cog -->
<Label Text="&#xF2F5;" FontFamily="FontAwesomeSolid" /> <!-- Sign Out -->
<Label Text="&#xF023;" FontFamily="FontAwesomeSolid" /> <!-- Lock -->

<!-- Dispositivos -->
<Label Text="&#xF2DB;" FontFamily="FontAwesomeSolid" /> <!-- Microchip -->
<Label Text="&#xF3CD;" FontFamily="FontAwesomeSolid" /> <!-- Mobile -->
<Label Text="&#xF1EB;" FontFamily="FontAwesomeSolid" /> <!-- WiFi -->
<Label Text="&#xF0E7;" FontFamily="FontAwesomeSolid" /> <!-- Bolt -->
```

---

## ✨ Resumen de Pasos

1. ✅ **Instalar NuGet** - FontAwesome.Sharp v6.6.0 (YA HECHO)
2. 📥 **Descargar** - Fuentes TTF de fontawesome.com
3. 📁 **Copiar** - Archivos TTF a `Resources/Fonts/`
4. ⚙️ **Configurar** - Agregar fuentes en `MauiProgram.cs`
5. 🔧 **Verificar** - Revisar que estén en `.csproj`
6. 🧹 **Limpiar y Compilar** - `dotnet clean && dotnet build`
7. 🎨 **Usar** - Implementar iconos con códigos Unicode

---

**¿Necesitas ayuda?** Consulta el archivo `ICONS_LIST.md` para ver todos los iconos disponibles con sus códigos.

**Última actualización:** 2025-11-14
