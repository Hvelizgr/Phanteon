# 📚 Índice de Documentación - Phanteon

Bienvenido a la documentación del proyecto Phanteon. Lee los documentos en el orden recomendado.

## 🚀 Por Dónde Empezar

Si eres nuevo en el proyecto, comienza aquí:

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

### Necesitas saber cómo...

| Qué necesitas | Dónde encontrarlo |
|--------------|-------------------|
| Configurar el proyecto desde cero | 02_Empezar_Aqui.md |
| Crear un nuevo módulo/feature | 08_Arquitectura.md + 10_Guia_Inicio_Rapido.md |
| Usar servicios API con Refit | 09_Configuracion_Servicios.md |
| Solucionar error de compilación | 06_Solucion_Problemas.md |
| Hacer commits correctamente | 07_Como_Hacer_Commits.md |
| Ver ejemplos de código XAML | 04_Ejemplos_Visuales.md + 05_Guia_Rapida_API.md |
| Entender la arquitectura | 08_Arquitectura.md |
| Ver qué falta por hacer | 11_Lista_Tareas.md + 03_Tu_Tarea.md |
| Probar la API | Postman/ |

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
