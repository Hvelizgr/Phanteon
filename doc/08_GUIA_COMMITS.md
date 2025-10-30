# 📝 Guía de Commits - Proyecto Phanteon

> Estándares y buenas prácticas para mensajes de commit en Git

---

## 🎯 Filosofía de Commits

### Principios básicos:
1. **Commits pequeños y frecuentes** - Mejor muchos commits pequeños que uno grande
2. **Mensajes descriptivos** - Explica QUÉ y POR QUÉ, no CÓMO
3. **Un commit = Una funcionalidad** - No mezclar cambios no relacionados
4. **Commits que compilan** - Nunca hacer commit de código que no compila

---

## 📐 Formato de Mensajes de Commit

### Estructura:
```
<tipo>(<alcance>): <descripción corta>

<descripción larga opcional>

<footer opcional>
```

### Ejemplo:
```
feat(ViewModels): Agregar LoginViewModel con validaciones

- Implementar propiedades observables (Correo, Password)
- Agregar comando IniciarSesionCommand
- Validar formato de correo y longitud de password
- Integrar con IUsuariosService

Closes #12
```

---

## 🏷️ Tipos de Commit

### Tipos principales:

| Tipo | Descripción | Ejemplo |
|------|-------------|---------|
| `feat` | Nueva funcionalidad | `feat(ViewModels): Agregar AlertasViewModel` |
| `fix` | Corrección de bug | `fix(API): Corregir timeout en IDispositivosService` |
| `docs` | Cambios en documentación | `docs: Actualizar README con instrucciones de instalación` |
| `style` | Formato de código (no afecta lógica) | `style(ViewModels): Aplicar formato consistente` |
| `refactor` | Refactorización de código | `refactor(Services): Extraer lógica común a clase base` |
| `test` | Agregar o modificar tests | `test(ViewModels): Agregar tests para LoginViewModel` |
| `chore` | Tareas de mantenimiento | `chore: Actualizar paquetes NuGet` |
| `build` | Cambios en build o dependencias | `build: Agregar paquete Refit 8.0.0` |
| `ci` | Cambios en CI/CD | `ci: Configurar GitHub Actions` |
| `perf` | Mejoras de performance | `perf(API): Optimizar consulta de dispositivos` |

---

## 📦 Alcance (Scope)

El alcance indica QUÉ parte del proyecto se modificó:

### Alcances del proyecto Phanteon:

| Alcance | Uso |
|---------|-----|
| `Models` | Cambios en modelos de datos |
| `ViewModels` | Cambios en ViewModels |
| `Views` | Cambios en páginas XAML |
| `Services` | Cambios en servicios de API |
| `Helpers` | Cambios en utilidades |
| `Config` | Cambios en configuración |
| `Navigation` | Cambios en navegación |
| `UI` | Cambios generales de interfaz |
| `API` | Cambios en configuración de API |
| `Docs` | Cambios en documentación |

---

## ✅ COMMITS SUGERIDOS PARA EL EQUIPO

### 🔷 Commits Iniciales (Ya hechos por Héctor)

```bash
# 1. Inicialización del proyecto
git commit -m "chore: Inicializar proyecto .NET MAUI Phanteon"

# 2. Configuración de NuGet
git commit -m "build: Agregar paquetes NuGet (Refit, Polly, CommunityToolkit)"

# 3. Modelos
git commit -m "feat(Models): Agregar modelos Usuario, Dispositivo, Alerta, HistorialDispositivo"

# 4. Servicios
git commit -m "feat(Services): Configurar servicios de API con Refit"

# 5. Configuración
git commit -m "feat(Config): Configurar inyección de dependencias y manejo de SSL"

# 6. Helpers
git commit -m "feat(Helpers): Agregar ApiConfiguration y Converters"

# 7. ViewModels iniciales
git commit -m "feat(ViewModels): Agregar DispositivosViewModel y DiagnosticoViewModel"

# 8. Páginas iniciales
git commit -m "feat(Views): Agregar DispositivosPage y DiagnosticoPage"

# 9. Documentación
git commit -m "docs: Agregar documentación completa del proyecto en /doc"
```

---

### 🔷 Commits para Persona 1 (ViewModels)

#### LoginViewModel

```bash
# 1. Crear archivo básico
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Crear LoginViewModel base con propiedades"

# 2. Agregar validaciones
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Agregar validaciones de correo y password en LoginViewModel"

# 3. Integrar con servicio
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Integrar LoginViewModel con IUsuariosService"

# 4. Agregar navegación
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Implementar navegación post-login en LoginViewModel"

# 5. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar LoginViewModel en inyección de dependencias"
```

#### AlertasViewModel

```bash
# 1. Crear archivo básico
git add ViewModels/AlertasViewModel.cs
git commit -m "feat(ViewModels): Crear AlertasViewModel con carga de alertas"

# 2. Agregar filtros
git add ViewModels/AlertasViewModel.cs
git commit -m "feat(ViewModels): Implementar filtrado por tipo en AlertasViewModel"

# 3. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar AlertasViewModel en DI"
```

#### DetalleDispositivoViewModel

```bash
# 1. Crear archivo básico
git add ViewModels/DetalleDispositivoViewModel.cs
git commit -m "feat(ViewModels): Crear DetalleDispositivoViewModel con carga de datos"

# 2. Integrar múltiples servicios
git add ViewModels/DetalleDispositivoViewModel.cs
git commit -m "feat(ViewModels): Integrar DetalleDispositivoViewModel con 3 servicios (Dispositivos, Historial, Alertas)"

# 3. Agregar manejo de parámetros
git add ViewModels/DetalleDispositivoViewModel.cs
git commit -m "feat(ViewModels): Implementar QueryProperty para recibir ID de dispositivo"

# 4. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar DetalleDispositivoViewModel en DI"
```

---

### 🔷 Commits para Persona 2 (Páginas XAML)

#### LoginPage

```bash
# 1. Crear archivo XAML
git add Views/LoginPage.xaml Views/LoginPage.xaml.cs
git commit -m "feat(Views): Crear LoginPage con formulario de login"

# 2. Mejorar UI
git add Views/LoginPage.xaml
git commit -m "style(Views): Mejorar diseño de LoginPage con logos y estilos"

# 3. Agregar validaciones visuales
git add Views/LoginPage.xaml
git commit -m "feat(Views): Agregar indicadores de error y carga en LoginPage"

# 4. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar LoginPage en DI"
```

#### AlertasPage

```bash
# 1. Crear archivo XAML
git add Views/AlertasPage.xaml Views/AlertasPage.xaml.cs
git commit -m "feat(Views): Crear AlertasPage con lista de alertas"

# 2. Agregar colores por tipo
git add Views/AlertasPage.xaml
git commit -m "style(Views): Implementar colores según tipo de alerta"

# 3. Agregar filtros
git add Views/AlertasPage.xaml
git commit -m "feat(Views): Agregar controles de filtro en AlertasPage"

# 4. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar AlertasPage en DI"
```

#### DetalleDispositivoPage

```bash
# 1. Crear archivo XAML
git add Views/DetalleDispositivoPage.xaml Views/DetalleDispositivoPage.xaml.cs
git commit -m "feat(Views): Crear DetalleDispositivoPage con información completa"

# 2. Agregar sección de historial
git add Views/DetalleDispositivoPage.xaml
git commit -m "feat(Views): Agregar sección de historial en DetalleDispositivoPage"

# 3. Agregar sección de alertas
git add Views/DetalleDispositivoPage.xaml
git commit -m "feat(Views): Agregar sección de alertas activas en DetalleDispositivoPage"

# 4. Registrar en DI
git add MauiProgram.cs
git commit -m "chore(Config): Registrar DetalleDispositivoPage en DI"
```

---

### 🔷 Commits para Persona 3 (Navegación y Validaciones)

#### Navegación

```bash
# 1. Configurar AppShell
git add AppShell.xaml
git commit -m "feat(Navigation): Configurar AppShell con menú lateral y rutas principales"

# 2. Registrar rutas de detalle
git add AppShell.xaml.cs
git commit -m "feat(Navigation): Registrar rutas de navegación para páginas de detalle"

# 3. Configurar página inicial
git add App.xaml.cs
git commit -m "feat(Navigation): Configurar LoginPage como página inicial"
```

#### Validaciones

```bash
# 1. Validaciones en LoginViewModel
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Agregar validaciones completas en LoginViewModel

- Validar correo no vacío y formato
- Validar password longitud mínima
- Verificar conectividad antes de llamar API"

# 2. Manejo de errores en DispositivosViewModel
git add ViewModels/DispositivosViewModel.cs
git commit -m "fix(ViewModels): Agregar manejo de errores con try-catch en DispositivosViewModel"

# 3. Manejo de errores en DiagnosticoViewModel
git add ViewModels/DiagnosticoViewModel.cs
git commit -m "fix(ViewModels): Implementar manejo de errores en DiagnosticoViewModel"

# 4. Manejo de errores en AlertasViewModel
git add ViewModels/AlertasViewModel.cs
git commit -m "fix(ViewModels): Agregar try-catch y mensajes de error en AlertasViewModel"

# 5. Verificación de conectividad
git add ViewModels/*.cs
git commit -m "feat(ViewModels): Agregar verificación de conectividad en todos los ViewModels"
```

---

## 🔄 Workflow de Commits Recomendado

### Flujo de trabajo diario:

```bash
# 1. Actualizar tu rama
git pull origin ControllerBD

# 2. Hacer cambios pequeños

# 3. Revisar cambios
git status
git diff

# 4. Agregar archivos específicos (NO uses git add .)
git add ViewModels/LoginViewModel.cs

# 5. Hacer commit con mensaje descriptivo
git commit -m "feat(ViewModels): Agregar LoginViewModel con validaciones"

# 6. Repetir pasos 2-5 para cada funcionalidad

# 7. Al final del día, subir cambios
git push origin ControllerBD
```

---

## ✅ Buenas Prácticas

### ✔️ DO (Hacer):

```bash
# ✅ Commits específicos y descriptivos
git commit -m "feat(ViewModels): Agregar validación de formato de correo en LoginViewModel"

# ✅ Commits que compilan
# Siempre verificar que el proyecto compile antes de hacer commit

# ✅ Commits pequeños
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Crear LoginViewModel base"

git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Agregar validaciones en LoginViewModel"

# ✅ Usar el alcance apropiado
git commit -m "feat(Views): Crear LoginPage"
git commit -m "fix(Services): Corregir timeout en API"

# ✅ Mensajes en español (según estándar del proyecto)
git commit -m "feat(ViewModels): Agregar LoginViewModel"
```

### ❌ DON'T (No hacer):

```bash
# ❌ Commits genéricos
git commit -m "cambios"
git commit -m "fix"
git commit -m "update"

# ❌ Commits gigantes
git add .
git commit -m "Agregar todo el código del proyecto"

# ❌ Commits de código que no compila
git commit -m "WIP: trabajo en progreso (no compila)"

# ❌ Mezclar cambios no relacionados
git add ViewModels/LoginViewModel.cs Views/AlertasPage.xaml
git commit -m "varios cambios"

# ❌ Mensajes sin contexto
git commit -m "fix bug"
```

---

## 🎨 Ejemplos de Buenos Commits

### Ejemplo 1: Nueva funcionalidad
```bash
git add ViewModels/LoginViewModel.cs
git commit -m "feat(ViewModels): Implementar LoginViewModel con autenticación

- Agregar propiedades Correo, Password, EstaCargando
- Implementar IniciarSesionCommand
- Integrar con IUsuariosService para autenticación
- Agregar navegación a dashboard después de login exitoso

Closes #15"
```

### Ejemplo 2: Corrección de bug
```bash
git add ViewModels/DispositivosViewModel.cs
git commit -m "fix(ViewModels): Corregir NullReferenceException en CargarDispositivos

La colección Dispositivos no se inicializaba correctamente,
causando una excepción al intentar agregar elementos.

Fixes #23"
```

### Ejemplo 3: Refactorización
```bash
git add ViewModels/AlertasViewModel.cs ViewModels/DispositivosViewModel.cs
git commit -m "refactor(ViewModels): Extraer lógica común de manejo de errores

Crear método base ManejarError() para evitar duplicación
de código en múltiples ViewModels."
```

### Ejemplo 4: Documentación
```bash
git add doc/03_DIVISION_TAREAS.md
git commit -m "docs: Actualizar checklist de tareas completadas

Marcar LoginViewModel como completado y agregar
notas sobre problemas encontrados."
```

### Ejemplo 5: Configuración
```bash
git add MauiProgram.cs
git commit -m "chore(Config): Registrar nuevos ViewModels en DI

- LoginViewModel
- AlertasViewModel
- DetalleDispositivoViewModel"
```

---

## 🔍 Commits para Situaciones Específicas

### Al completar una funcionalidad completa:
```bash
git add ViewModels/LoginViewModel.cs Views/LoginPage.xaml Views/LoginPage.xaml.cs MauiProgram.cs
git commit -m "feat(Auth): Implementar módulo completo de autenticación

- LoginViewModel con validaciones
- LoginPage con formulario y estilos
- Integración con IUsuariosService
- Navegación post-login
- Registro en DI

Closes #10"
```

### Al corregir un error crítico:
```bash
git add Helpers/ApiConfiguration.cs
git commit -m "fix(API): Corregir URL del backend para emulador Android

Cambiar de localhost a 10.0.2.2 para permitir conexión
desde emulador Android al backend local.

BREAKING CHANGE: Los dispositivos físicos necesitarán
configurar la IP manualmente."
```

### Al actualizar dependencias:
```bash
git add Phanteon.csproj
git commit -m "build: Actualizar Refit de 8.0.0 a 8.0.1

Incluye corrección de bug de serialización JSON."
```

---

## 📊 Commits por Milestone

### Milestone 1: ViewModels 
```bash
1. feat(ViewModels): Crear LoginViewModel base
2. feat(ViewModels): Agregar validaciones en LoginViewModel
3. feat(ViewModels): Integrar LoginViewModel con IUsuariosService
4. chore(Config): Registrar LoginViewModel en DI
5. feat(ViewModels): Crear AlertasViewModel
6. feat(ViewModels): Implementar filtrado en AlertasViewModel
7. chore(Config): Registrar AlertasViewModel en DI
8. feat(ViewModels): Crear DetalleDispositivoViewModel
9. feat(ViewModels): Integrar múltiples servicios en DetalleDispositivoViewModel
10. chore(Config): Registrar DetalleDispositivoViewModel en DI
```

### Milestone 2: Páginas XAML 
1. feat(Views): Crear LoginPage con formulario
2. style(Views): Mejorar diseño de LoginPage
3. chore(Config): Registrar LoginPage en DI
4. feat(Views): Crear AlertasPage con lista
5. style(Views): Implementar colores por tipo en AlertasPage
6. chore(Config): Registrar AlertasPage en DI
7. feat(Views): Crear DetalleDispositivoPage
8. feat(Views): Agregar secciones de historial y alertas
9. chore(Config): Registrar DetalleDispositivoPage en DI
```

### Milestone 3: Navegación (Persona 3)
```bash
1. feat(Navigation): Configurar AppShell con menú lateral
2. feat(Navigation): Registrar rutas de navegación
3. feat(Navigation): Configurar página inicial
4. feat(ViewModels): Agregar validaciones completas
5. fix(ViewModels): Implementar manejo de errores en todos los ViewModels
6. feat(ViewModels): Agregar verificación de conectividad
```

---

## 🚀 Commits al Finalizar el Proyecto

```bash
# 1. Testing final
git commit -m "test: Verificar funcionamiento completo de la aplicación

Probados todos los flujos:
- Login y autenticación
- Lista de dispositivos
- Detalle de dispositivo
- Alertas con filtros
- Dashboard con estadísticas
- Navegación entre páginas"

# 2. Documentación final
git commit -m "docs: Actualizar README con información del proyecto completo"

# 3. Limpieza
git commit -m "chore: Limpiar código comentado y archivos temporales"

# 4. Release
git commit -m "chore: Preparar versión 1.0.0 para entrega

Proyecto completo con:
- 5 ViewModels funcionales
- 5 Páginas XAML
- Navegación completa
- Validaciones implementadas
- Manejo de errores
- Documentación completa"
```

---

## 📋 Checklist antes de hacer Commit

Antes de cada commit, verifica:

- [ ] El código compila sin errores
- [ ] No hay warnings importantes
- [ ] Solo incluyes archivos relacionados con el cambio
- [ ] El mensaje describe claramente QUÉ cambió
- [ ] Usaste el tipo de commit correcto (feat/fix/docs/etc)
- [ ] Incluiste el alcance apropiado
- [ ] El código sigue el estilo del proyecto
- [ ] Eliminaste código comentado innecesario
- [ ] No incluyes archivos temporales o de configuración personal

---

## 🔗 Referencias

- **Conventional Commits:** https://www.conventionalcommits.org/
- **Git Best Practices:** https://git-scm.com/book/en/v2
- **Semantic Versioning:** https://semver.org/

---

## 📞 Consultas

Si tienes dudas sobre qué tipo de commit usar, consulta:
1. Este documento
2. El historial de commits del proyecto: `git log --oneline`
3. Héctor (líder del proyecto)

---

_Actualizado: 29/10/2024_
_Autor: Héctor Eduardo Véliz Girón_
