# 📚 Índice de Documentación - Phanteon

> Documentación completa del proyecto Phanteon organizadamente

---

## 📖 Guía de Lectura Recomendada

### Para Nuevos Miembros del Equipo:

1. **[00_LEEME_PRIMERO.md](00_LEEME_PRIMERO.md)** ⭐ (3 min)
2. **[01_QUICK_START.md](01_QUICK_START.md)** (5 min)
3. **[02_CONFIGURACION_BACKEND.md](02_CONFIGURACION_BACKEND.md)** (10 min)
4. **[03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md)** (15 min)

### Luego, según tu rol:

**Si trabajas con ViewModels:**
- [04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)
- [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Si trabajas con Páginas XAML:**
- [05_PAGINAS_MOCKUPS.md](05_PAGINAS_MOCKUPS.md)
- [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Si trabajas probando la API:**
- [07_TESTING_POSTMAN.md](07_TESTING_POSTMAN.md)

---

## 📄 Documentos Disponibles

### 🎯 00_LEEME_PRIMERO.md
**Tiempo de lectura:** 3 minutos

**Contenido:**
- Resumen ejecutivo del proyecto
- Lo que ya está hecho vs lo que falta
- División de trabajo (3 personas)
- Checklist de tareas
- Contacto e información importante

**Cuándo leer:** PRIMERO, antes de cualquier otra cosa

---

### ⚡ 01_QUICK_START.md
**Tiempo de lectura:** 5 minutos

**Contenido:**
- Configuración rápida del backend (5 pasos)
- Cómo abrir y ejecutar Phanteon
- Configuración de URL según plataforma
- Templates de código para ViewModels y Páginas
- Workflow diario de desarrollo

**Cuándo leer:** Segundo, para empezar a trabajar inmediatamente

---

### 🔧 02_CONFIGURACION_BACKEND.md
**Tiempo de lectura:** 10 minutos

**Contenido:**
- Instalación detallada del backend API
- Configuración de SQL Server (3 opciones)
- Entity Framework y migraciones
- Configuración de URL en Phanteon
- Manejo de certificados SSL
- Estructura de la base de datos
- Insertar datos de prueba
- Solución de problemas del backend

**Cuándo leer:** Si tienes problemas con el backend o quieres entenderlo a fondo

---

### 📋 03_DIVISION_TAREAS.md
**Tiempo de lectura:** 15 minutos

**Contenido:**
- **Tareas completadas por Héctor** (detallado)
- **Persona 1:** ViewModels faltantes (LoginViewModel, AlertasViewModel, DetalleDispositivoViewModel)
- **Persona 2:** Páginas XAML faltantes (LoginPage, AlertasPage, DetalleDispositivoPage)
- **Persona 3:** Navegación y validaciones (AppShell, manejo de errores)
- Cronograma sugerido (4 semanas)
- Checklist detallado de cada tarea
- Criterios de aceptación

**Cuándo leer:** Para saber exactamente qué te toca hacer

---

### 📡 04_ENDPOINTS_DISPONIBLES.md
**Tiempo de lectura:** 20 minutos

**Contenido:**
- **IDispositivosService** (GET all, GET by ID, POST create)
- **IUsuariosService** (GET all, GET by ID, POST create)
- **IAlertasService** (GET all, GET by ID, POST create)
- **IHistorialDispositivosService** (GET all, GET by ID, POST create)
- Modelos de datos completos
- Ejemplos de código para cada endpoint
- Ejemplo completo de DetalleDispositivoViewModel (usa 3 servicios)
- Configuración de API
- Manejo de errores

**Cuándo leer:** Cuando estés implementando ViewModels que consumen la API

---

### 🎨 05_PAGINAS_MOCKUPS.md
**Tiempo de lectura:** Variable (documento de referencia)

**Contenido:**
- Mockups ASCII de las 5 páginas
- Componentes de UI específicos para cada página
- Layouts recomendados
- Endpoints a usar en cada página
- Propiedades del ViewModel
- Colores y estilos
- Ejemplos de código XAML completos
- Code-behind examples
- Flujo de navegación

**Cuándo leer:** Cuando estés creando las páginas XAML

---

### 🐛 06_ERRORES_COMUNES.md
**Tiempo de lectura:** Variable (documento de referencia)

**Contenido:**
- **Errores de conexión con la API** (Connection refused, SSL errors, Timeout)
- **Errores de base de datos** (Unable to connect, No such table)
- **Errores de configuración** (Cannot resolve service, Port in use)
- **Errores de compilación** (ObservableProperty, RelayCommand)
- **Errores en runtime** (Binding not found, Command not found)
- **Errores de navegación** (Route not found, QueryProperty)
- **Errores de red** (No internet)
- Checklist de debugging
- Herramientas de debugging

**Cuándo leer:** Cuando tengas un error y no sepas cómo resolverlo

---

### 🧪 07_TESTING_POSTMAN.md
**Tiempo de lectura:** 15 minutos

**Contenido:**
- Instalación y configuración de Postman
- Crear Collection y Environment
- Todos los endpoints con ejemplos de request/response
- Scripts de Postman (Pre-request y Tests)
- Collection completa para importar
- Escenarios de testing comunes
- Variables dinámicas de Postman
- Troubleshooting de Postman

**Cuándo leer:** Cuando quieras probar la API directamente sin la app

---

### 📝 08_GUIA_COMMITS.md
**Tiempo de lectura:** 10 minutos

**Contenido:**
- Filosofía y principios de commits
- Formato estándar de mensajes (tipo, alcance, descripción)
- Tipos de commit (feat, fix, docs, etc.)
- **Commits sugeridos para cada persona del equipo**
- Workflow de commits recomendado
- Buenas prácticas vs malas prácticas
- Ejemplos de buenos commits
- Commits por milestone
- Checklist antes de hacer commit

**Cuándo leer:** Antes de hacer tu primer commit y como referencia constante

---

## 🔍 Búsqueda Rápida

### ¿Cómo hago...?

| Pregunta | Documento | Sección |
|----------|-----------|---------|
| ¿Cómo configuro el backend? | 01_QUICK_START.md | Paso 1 |
| ¿Cómo creo un ViewModel? | 01_QUICK_START.md | Para Persona 1 |
| ¿Cómo creo una página XAML? | 01_QUICK_START.md | Para Persona 2 |
| ¿Qué endpoints hay disponibles? | 04_ENDPOINTS_DISPONIBLES.md | Todo el documento |
| ¿Cómo debe verse LoginPage? | 05_PAGINAS_MOCKUPS.md | Página 1 |
| ¿Cómo pruebo la API? | 07_TESTING_POSTMAN.md | Todo el documento |
| ¿Por qué no se conecta a la API? | 06_ERRORES_COMUNES.md | Errores de Conexión |
| ¿Cómo configuro AppShell? | 03_DIVISION_TAREAS.md | Persona 3 - Tarea 3.1 |
| ¿Qué commits debo hacer? | 08_GUIA_COMMITS.md | Commits para tu Persona |
| ¿Cómo escribir un buen commit? | 08_GUIA_COMMITS.md | Buenas Prácticas |

---

## 🏷️ Por Rol

### 👤 Desarrollador de ViewModels (Persona 1)

**Documentos principales:**
1. [00_LEEME_PRIMERO.md](00_LEEME_PRIMERO.md)
2. [01_QUICK_START.md](01_QUICK_START.md)
3. [03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md) - Persona 1
4. [04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)
5. [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Tareas:**
- LoginViewModel.cs
- AlertasViewModel.cs
- DetalleDispositivoViewModel.cs

---

### 👤 Desarrollador de Páginas XAML (Persona 2)

**Documentos principales:**
1. [00_LEEME_PRIMERO.md](00_LEEME_PRIMERO.md)
2. [01_QUICK_START.md](01_QUICK_START.md)
3. [03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md) - Persona 2
4. [05_PAGINAS_MOCKUPS.md](05_PAGINAS_MOCKUPS.md)
5. [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Tareas:**
- LoginPage.xaml + .cs
- AlertasPage.xaml + .cs
- DetalleDispositivoPage.xaml + .cs

---

### 👤 Desarrollador de Navegación (Persona 3)

**Documentos principales:**
1. [00_LEEME_PRIMERO.md](00_LEEME_PRIMERO.md)
2. [01_QUICK_START.md](01_QUICK_START.md)
3. [03_DIVISION_TAREAS.md](03_DIVISION_TAREAS.md) - Persona 3
4. [04_ENDPOINTS_DISPONIBLES.md](04_ENDPOINTS_DISPONIBLES.md)
5. [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)

**Tareas:**
- Configurar AppShell.xaml
- Registrar rutas
- Agregar validaciones
- Implementar manejo de errores

---

### 👤 Líder del Proyecto (Héctor)

**Documentos de referencia:**
- [02_CONFIGURACION_BACKEND.md](02_CONFIGURACION_BACKEND.md)
- [07_TESTING_POSTMAN.md](07_TESTING_POSTMAN.md)
- Todos los demás para dar soporte al equipo

---

## 📊 Estadísticas de la Documentación

- **Total de documentos:** 8 (incluyendo este)
- **Páginas aproximadas:** ~100 páginas
- **Ejemplos de código:** ~50 snippets
- **Tiempo total de lectura:** ~2 horas (leyendo todo)
- **Tiempo mínimo para empezar:** 20 minutos (3 primeros documentos)

---

## 🔄 Última Actualización

**Fecha:** 29/10/2024
**Versión:** 1.0
**Autor:** Héctor Eduardo Véliz Girón (000108304)

---

## 📞 Soporte

Si no encuentras lo que buscas en esta documentación:
1. Usa Ctrl+F para buscar palabras clave
2. Consulta [06_ERRORES_COMUNES.md](06_ERRORES_COMUNES.md)
3. Contacta a Héctor (líder del proyecto)

---

**Volver al README principal:** [../README.md](../README.md)
