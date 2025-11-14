# Lista de Tareas - Phanteon

## ✅ Completado

- [x] Reorganizar proyecto con estructura Features
- [x] Crear Core con BaseViewModel, Converters, Behaviors
- [x] Reorganizar Services por categorías (Http, Api, Storage, Navigation)
- [x] Crear Constants para endpoints y mensajes
- [x] Crear interfaces Refit para las 3 APIs principales
- [x] Actualizar MauiProgram.cs con DI
- [x] Actualizar AppShell.xaml con nuevas rutas
- [x] Convertir MainPage a MVVM completo
- [x] Eliminar archivos duplicados
- [x] Crear documentación completa
- [x] Verificar que el proyecto compile sin errores

## 🔲 Pendiente - Configuración Básica

- [ ] **Actualizar URL de API**
  - Archivo: `Helpers/ApiConfiguration.cs`
  - Cambiar `BaseUrl` por tu URL real

- [ ] **Actualizar endpoints de API**
  - Archivo: `Constants/ApiEndpoints.cs`
  - Descomentar y ajustar las rutas

- [ ] **Registrar servicios Refit** (opcional, solo si usas las APIs)
  - Archivo: `MauiProgram.cs`
  - Descomentar líneas 38-42 o usar ejemplos de `SERVICES_SETUP.md`

## 🔲 Desarrollo de Features

### Feature: Autenticación (Ejemplo)
- [ ] Crear `Features/Auth/LoginPage.xaml`
- [ ] Crear `Features/Auth/LoginViewModel.cs`
- [ ] Crear `Services/Api/IAuthApi.cs`
- [ ] Registrar en MauiProgram.cs
- [ ] Agregar ruta en AppShell.xaml

### Feature: Dispositivos
- [ ] Crear `Features/Dispositivos/DispositivosListPage.xaml`
- [ ] Crear `Features/Dispositivos/DispositivosListViewModel.cs`
- [ ] Crear `Features/Dispositivos/DispositivoDetailPage.xaml`
- [ ] Crear `Features/Dispositivos/DispositivoDetailViewModel.cs`
- [ ] Registrar en MauiProgram.cs
- [ ] Agregar rutas en AppShell.xaml

### Feature: Usuarios
- [ ] Crear `Features/Usuarios/UsuariosListPage.xaml`
- [ ] Crear `Features/Usuarios/UsuariosListViewModel.cs`
- [ ] Registrar en MauiProgram.cs
- [ ] Agregar rutas en AppShell.xaml

### Feature: Alertas
- [ ] Crear `Features/Alertas/AlertasPage.xaml`
- [ ] Crear `Features/Alertas/AlertasViewModel.cs`
- [ ] Registrar en MauiProgram.cs
- [ ] Agregar rutas en AppShell.xaml

## 🔲 Mejoras Adicionales

### Base de Datos Local (SQLite)
- [ ] Instalar paquete `sqlite-net-pcl`
- [ ] Crear `Data/Local/AppDatabase.cs`
- [ ] Crear repositorios en `Data/Repositories/`
- [ ] Registrar en MauiProgram.cs

### Logging
- [ ] Instalar `Serilog.Extensions.Logging`
- [ ] Crear `Services/Logging/ILoggingService.cs`
- [ ] Configurar en MauiProgram.cs

### Testing
- [ ] Crear proyecto de pruebas unitarias
- [ ] Agregar mocks para servicios
- [ ] Crear tests para ViewModels

### Seguridad
- [ ] Implementar autenticación JWT
- [ ] Crear `Services/Http/AuthHeaderHandler.cs`
- [ ] Configurar refresh tokens

### UI/UX
- [ ] Crear controles personalizados en `Core/Controls/`
- [ ] Agregar animaciones
- [ ] Mejorar estilos en `Resources/Styles/`
- [ ] Agregar temas (Light/Dark mode)

### Optimización
- [ ] Implementar caché local
- [ ] Optimizar carga de imágenes
- [ ] Implementar virtualización para listas largas
- [ ] Revisar uso de memoria

## 📚 Documentación a Leer

1. Lee `QUICK_START.md` - Guía rápida con ejemplos
2. Lee `ARCHITECTURE.md` - Arquitectura completa del proyecto
3. Lee `SERVICES_SETUP.md` - Configuración de APIs con Refit

## 🎯 Próximo Paso Inmediato

**Recomendación:** Empieza por actualizar `Helpers/ApiConfiguration.cs` con tu URL de API, y luego crea tu primera feature completa siguiendo el ejemplo de `Features/Main/`.

---

**Nota:** Este archivo es para tu referencia. Puedes ir marcando tareas completadas con `[x]` a medida que avanzas.
