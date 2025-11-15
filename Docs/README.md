# 📚 Documentación Phanteon

> Guía completa para desarrolladores del proyecto Phanteon - Sistema de monitoreo IoT

<div align="center">

![Status](https://img.shields.io/badge/Status-En%20Desarrollo-yellow)
![Docs](https://img.shields.io/badge/Docs-11%20archivos-blue)
![Last Update](https://img.shields.io/badge/Última%20actualización-14%2F11%2F2025-green)

</div>

---

## 🎯 Inicio Rápido

**¿Primera vez en el proyecto?** Sigue esta ruta de 20 minutos:

1. **[01_Empezar_Aqui.md](01_Empezar_Aqui.md)** (10 min) → Configuración inicial
2. **[06_Arquitectura.md](06_Arquitectura.md)** (10 min) → Estructura del proyecto

---

## 📖 Índice Completo

### 🟢 Fundamentos (Empieza aquí)

**[01_Empezar_Aqui.md](01_Empezar_Aqui.md)** ⚡ **EMPIEZA AQUÍ**
- Guía de configuración inicial
- Setup del entorno
- Configuración de la API

---

## 📖 Documentación de Desarrollo

### Ejemplos y Guías

2. **[02_Ejemplos_Visuales.md](02_Ejemplos_Visuales.md)**
   - Mockups de las pantallas
   - Código de ejemplo completo
   - Diseños visuales

3. **[03_Guia_Rapida_API.md](03_Guia_Rapida_API.md)**
   - Referencia rápida de comandos
   - Bindings XAML
   - Snippets útiles

### Solución de Problemas

4. **[04_Solucion_Problemas.md](04_Solucion_Problemas.md)**
   - Errores comunes
   - Soluciones paso a paso
   - Troubleshooting

5. **[05_Como_Hacer_Commits.md](05_Como_Hacer_Commits.md)**
   - Convenciones de Git
   - Mensajes de commit
   - Flujo de trabajo con ramas

---

## ⭐ Arquitectura y Patrones

6. **[06_Arquitectura.md](06_Arquitectura.md)** 📐
   - Feature-based Architecture completa
   - Patrones y principios (MVVM, DI, etc.)
   - Estructura de carpetas detallada
   - Convenciones de nomenclatura
   - Buenas prácticas

7. **[07_Configuracion_Servicios.md](07_Configuracion_Servicios.md)** ⚙️
   - Registro de servicios Refit en MauiProgram.cs
   - Uso en ViewModels con ejemplos
   - Headers personalizados (autenticación)
   - Manejo de respuestas con ApiResponse

8. **[08_Guia_Inicio_Rapido.md](08_Guia_Inicio_Rapido.md)** 🚀
   - Guía rápida con ejemplos
   - Patrones de código comunes
   - Recursos adicionales

### 🆕 Referencias Rápidas

**[CHEATSHEET.md](CHEATSHEET.md)** 📋
- Referencia rápida todo-en-uno
- Templates de código listo para copiar
- Comandos Git más usados
- Soluciones a errores comunes
- Snippets de ViewModel y XAML

---

## 🧪 Testing

### Postman
- **[Postman/](Postman/)** - Colecciones para testing de la API
  - Importar colección en Postman
  - Probar endpoints de Dispositivos, Usuarios, Alertas
  - Verificar conexión con el backend

---

## 📂 Estructura de la Documentación

```
Docs/
├── README.md                          ← Estás aquí (índice)
│
├── 📘 Documentación Original (Base del Proyecto)
│   ├── 01_Introduccion.md
│   ├── 02_Empezar_Aqui.md             ⚡ EMPIEZA AQUÍ
│   ├── 03_Tu_Tarea.md
│   ├── 04_Ejemplos_Visuales.md
│   ├── 05_Guia_Rapida_API.md
│   ├── 06_Solucion_Problemas.md
│   └── 07_Como_Hacer_Commits.md
│
├── ⭐ Nueva Documentación (Estructura Actualizada)
│   ├── 08_Arquitectura.md              📐 Arquitectura completa
│   ├── 09_Configuracion_Servicios.md   ⚙️ Setup de APIs
│   ├── 10_Guia_Inicio_Rapido.md        🚀 Guía con ejemplos
│   └── 11_Lista_Tareas.md              ✅ Checklist
│
└── 🧪 Testing
    └── Postman/                        Colecciones de API
```

---

## 🎯 Flujo de Lectura Recomendado

### Para Nuevos Desarrolladores

1. **01_Empezar_Aqui.md** - Configura tu entorno (10 min)
2. **06_Arquitectura.md** - Entiende la estructura (10 min)
3. **08_Guia_Inicio_Rapido.md** - Ve ejemplos de código (10 min)
4. Empieza a desarrollar

### Para Desarrollo Diario

- **Referencia rápida:** CHEATSHEET.md
- **Problemas:** 04_Solucion_Problemas.md
- **Commits:** 05_Como_Hacer_Commits.md
- **Setup APIs:** 07_Configuracion_Servicios.md

---

## 🔍 Búsqueda Rápida

<details>
<summary><b>📋 ¿Qué necesitas hacer?</b> (Click para expandir)</summary>

| Necesito... | Documento |
|-------------|-----------|
| **Configurar el proyecto** | [01_Empezar_Aqui.md](01_Empezar_Aqui.md) |
| **Crear un módulo/feature** | [08_Guia_Inicio_Rapido.md](08_Guia_Inicio_Rapido.md) |
| **Usar servicios API con Refit** | [07_Configuracion_Servicios.md](07_Configuracion_Servicios.md) |
| **Solucionar un error** | [04_Solucion_Problemas.md](04_Solucion_Problemas.md) |
| **Hacer commits correctos** | [05_Como_Hacer_Commits.md](05_Como_Hacer_Commits.md) |
| **Ver ejemplos de código** | [02_Ejemplos_Visuales.md](02_Ejemplos_Visuales.md) |
| **Entender la arquitectura** | [06_Arquitectura.md](06_Arquitectura.md) |
| **Probar endpoints de API** | [Postman/](Postman/) |

</details>

<details>
<summary><b>🐛 Errores Comunes</b></summary>

| Error | Solución Rápida | Documento |
|-------|----------------|-----------|
| `Connection refused` | Verificar que DevicesAPI esté corriendo | [06_Solucion_Problemas.md](06_Solucion_Problemas.md#-error-connection-refused) |
| `Cannot resolve service` | Registrar en MauiProgram.cs | [06_Solucion_Problemas.md](06_Solucion_Problemas.md#-error-cannot-resolve-service) |
| `ObservableProperty not found` | Agregar `using CommunityToolkit.Mvvm` | [06_Solucion_Problemas.md](06_Solucion_Problemas.md#-error-observableproperty) |
| `SSL Certificate failed` | Ya solucionado en MauiProgram.cs | [06_Solucion_Problemas.md](06_Solucion_Problemas.md#-error-ssl-certificate) |
| `Timeout` | Aumentar timeout o verificar backend | [06_Solucion_Problemas.md](06_Solucion_Problemas.md#-error-timeout) |

</details>

<details>
<summary><b>💡 Snippets Útiles</b></summary>

### Crear un nuevo ViewModel
```csharp
public partial class MiViewModel : BaseViewModel
{
    private readonly IMiApi _api;

    public MiViewModel(IMiApi api)
    {
        _api = api;
        Titulo = "Mi Título";
    }

    [ObservableProperty]
    private ObservableCollection<MiModelo> items = new();

    [RelayCommand]
    private async Task CargarAsync()
    {
        try
        {
            EstaCargando = true;
            var data = await _api.GetAsync();
            Items = new(data);
        }
        catch (Exception ex)
        {
            ManejarError(ex, "cargar datos");
        }
        finally
        {
            EstaCargando = false;
        }
    }
}
```

Ver más en: [10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md)

</details>

---

## 📌 Notas Importantes

### ⚠️ Arquitectura del Proyecto

El proyecto utiliza **Feature-based Architecture**:

- ✅ Views y ViewModels organizados en `Features/{NombreModulo}/`
- ✅ Servicios organizados por categoría en `Services/{Http|Api|Storage|Navigation}/`
- ✅ Core components en `Core/{ViewModels|Converters|Behaviors}/`
- ✅ Constants centralizados en `Constants/`

---

## 🆘 ¿Necesitas Ayuda?

1. Revisa **06_Solucion_Problemas.md** primero
2. Busca en la documentación usando Ctrl+F
3. Pregunta al equipo en el chat del proyecto
4. Revisa el código de ejemplo en **04_Ejemplos_Visuales.md**

---

**Última actualización:** Noviembre 2025

**[⬆ Volver al README principal](../README.md)**
