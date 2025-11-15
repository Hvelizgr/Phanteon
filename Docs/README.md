# 📚 Documentación Phanteon

> Guía completa para desarrolladores del proyecto Phanteon - Sistema de monitoreo IoT

<div align="center">

![Status](https://img.shields.io/badge/Status-En%20Desarrollo-yellow)
![Docs](https://img.shields.io/badge/Docs-11%20archivos-blue)
![Last Update](https://img.shields.io/badge/Última%20actualización-14%2F11%2F2025-green)

</div>

---

## 🎯 Inicio Rápido

**¿Primera vez en el proyecto?** Sigue esta ruta de 30 minutos:

1. **[01_Introduccion.md](01_Introduccion.md)** (5 min) → Contexto general
2. **[02_Empezar_Aqui.md](02_Empezar_Aqui.md)** (10 min) → ⚡ Setup completo
3. **[08_Arquitectura.md](08_Arquitectura.md)** (10 min) → Estructura del proyecto
4. **[03_Tu_Tarea.md](03_Tu_Tarea.md)** (5 min) → Tu asignación específica

---

## 📖 Índice Completo

### 🟢 Fundamentos (Empieza aquí)

1. **[01_Introduccion.md](01_Introduccion.md)** (11 KB)
   - Contexto del proyecto
   - Qué es Phanteon
   - Tecnologías utilizadas

2. **[02_Empezar_Aqui.md](02_Empezar_Aqui.md)** ⚡ **EMPIEZA AQUÍ** (11 KB)
   - Guía de inicio rápido
   - Configuración en 5 minutos
   - Ejemplos de código con la nueva estructura

3. **[03_Tu_Tarea.md](03_Tu_Tarea.md)** (23 KB)
   - División de tareas del equipo
   - Checklist completo
   - Ubicaciones actualizadas (Feature-based)

---

## 📖 Documentación de Desarrollo

### Ejemplos y Guías

4. **[04_Ejemplos_Visuales.md](04_Ejemplos_Visuales.md)** (22 KB)
   - Mockups de las pantallas
   - Código de ejemplo completo
   - Diseños visuales

5. **[05_Guia_Rapida_API.md](05_Guia_Rapida_API.md)** (11 KB)
   - Referencia rápida de comandos
   - Bindings XAML
   - Snippets útiles

### Solución de Problemas

6. **[06_Solucion_Problemas.md](06_Solucion_Problemas.md)** (12 KB)
   - Errores comunes
   - Soluciones paso a paso
   - Troubleshooting

7. **[07_Como_Hacer_Commits.md](07_Como_Hacer_Commits.md)** (17 KB)
   - Convenciones de Git
   - Mensajes de commit
   - Flujo de trabajo con ramas

---

## ⭐ Nueva Documentación (Estructura Actualizada)

### Arquitectura y Patrones

8. **[08_Arquitectura.md](08_Arquitectura.md)** 📐 (8 KB)
   - **Feature-based Architecture** completa
   - Patrones y principios (MVVM, DI, etc.)
   - Estructura de carpetas detallada
   - Cómo agregar nuevas features
   - Convenciones de nomenclatura
   - Buenas prácticas

9. **[09_Configuracion_Servicios.md](09_Configuracion_Servicios.md)** ⚙️ (11 KB)
   - Registro de servicios Refit en MauiProgram.cs
   - Opciones: Sin Polly, Con Polly, Con Factory personalizado
   - Uso en ViewModels con ejemplos completos
   - Headers personalizados (autenticación)
   - Manejo de respuestas con ApiResponse
   - Testing de servicios

10. **[10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md)** 🚀 (11 KB)
    - Guía rápida con la nueva estructura
    - Ejemplo completo: crear módulo de Dispositivos
    - Patrones de código comunes
    - Próximos pasos recomendados
    - Recursos adicionales

11. **[11_Lista_Tareas.md](11_Lista_Tareas.md)** ✅ (4 KB)
    - Checklist de tareas completadas
    - Configuración básica pendiente
    - Features a desarrollar
    - Mejoras adicionales (SQLite, Logging, Testing, etc.)
    - Próximo paso inmediato

### 🆕 Referencias Rápidas

12. **[CHEATSHEET.md](CHEATSHEET.md)** 📋 **NUEVO**
    - Referencia rápida todo-en-uno
    - Templates de código listo para copiar
    - Comandos Git más usados
    - Soluciones a errores comunes
    - Snippets de ViewModel y XAML
    - Tips y trucos útiles

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

### Para Nuevos Miembros del Equipo

1. Lee **01_Introduccion.md** - Entiende el contexto (5 min)
2. Lee **02_Empezar_Aqui.md** - Configura tu entorno (10 min)
3. Lee **08_Arquitectura.md** - Entiende la nueva estructura (15 min)
4. Lee **03_Tu_Tarea.md** - Identifica tu tarea (5 min)
5. Lee **10_Guia_Inicio_Rapido.md** - Ve ejemplos de código (10 min)
6. Empieza a desarrollar tu feature

### Para Desarrollo Diario

- **Referencia rápida:** 05_Guia_Rapida_API.md
- **Problemas:** 06_Solucion_Problemas.md
- **Commits:** 07_Como_Hacer_Commits.md
- **Setup APIs:** 09_Configuracion_Servicios.md

### Para Arquitectura y Patrones

- **Estructura del proyecto:** 08_Arquitectura.md
- **Agregar features:** 10_Guia_Inicio_Rapido.md (Sección "Agregar una Nueva Característica")
- **Configurar servicios:** 09_Configuracion_Servicios.md

---

## 🔍 Búsqueda Rápida

<details>
<summary><b>📋 ¿Qué necesitas hacer?</b> (Click para expandir)</summary>

| Necesito... | Documento | Tiempo |
|-------------|-----------|---------|
| **Configurar el proyecto desde cero** | [02_Empezar_Aqui.md](02_Empezar_Aqui.md) | 10 min |
| **Crear un nuevo módulo/feature** | [08_Arquitectura.md](08_Arquitectura.md) + [10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md) | 15 min |
| **Usar servicios API con Refit** | [09_Configuracion_Servicios.md](09_Configuracion_Servicios.md) | 10 min |
| **Solucionar un error** | [06_Solucion_Problemas.md](06_Solucion_Problemas.md) | 5 min |
| **Hacer commits correctos** | [07_Como_Hacer_Commits.md](07_Como_Hacer_Commits.md) | 5 min |
| **Ver ejemplos de código XAML** | [04_Ejemplos_Visuales.md](04_Ejemplos_Visuales.md) | 10 min |
| **Entender la arquitectura** | [08_Arquitectura.md](08_Arquitectura.md) | 10 min |
| **Ver tareas pendientes** | [11_Lista_Tareas.md](11_Lista_Tareas.md) | 3 min |
| **Probar endpoints de API** | [Postman/](Postman/) | 5 min |
| **Crear un ViewModel** | [10_Guia_Inicio_Rapido.md](10_Guia_Inicio_Rapido.md) | 15 min |

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

### ⚠️ Cambios Recientes (11/11/2025)

El proyecto fue reorganizado con **Feature-based Architecture**:

- ✅ Las Views y ViewModels ahora van juntos en `Features/{NombreModulo}/`
- ✅ Servicios organizados por categoría en `Services/{Http|Api|Storage|Navigation}/`
- ✅ Core components en `Core/{ViewModels|Converters|Behaviors}/`
- ✅ Constants centralizados en `Constants/`

**Documentos actualizados:**
- 02_Empezar_Aqui.md - Ejemplos con nueva estructura
- 03_Tu_Tarea.md - Ubicaciones actualizadas
- README.md (raíz) - Estructura actualizada

**Nuevos documentos:**
- 08_Arquitectura.md
- 09_Configuracion_Servicios.md
- 10_Guia_Inicio_Rapido.md
- 11_Lista_Tareas.md

---

## 🆘 ¿Necesitas Ayuda?

1. Revisa **06_Solucion_Problemas.md** primero
2. Busca en la documentación usando Ctrl+F
3. Pregunta al equipo en el chat del proyecto
4. Revisa el código de ejemplo en **04_Ejemplos_Visuales.md**

---

**Última actualización:** 11/11/2025 - Documentación completa reorganizada

**Volver a:** [README principal](../README.md)
