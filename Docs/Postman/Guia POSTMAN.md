# Guía de Pruebas Automatizadas con Postman - DevicesAPI

## Descripción

Esta colección de Postman contiene **pruebas automatizadas completas** para todos los endpoints de DevicesAPI, incluyendo validaciones de estructura, tipos de datos, códigos de respuesta y tiempos de ejecución.

## Archivos Incluidos

1. **DevicesAPI.postman_collection.json** - Colección completa con todos los endpoints y tests
2. **DevicesAPI.postman_environment.json** - Variables de entorno para facilitar las pruebas
3. **POSTMAN_TESTING_GUIDE.md** - Esta guía (documentación completa)

## Instalación y Configuración

### Paso 1: Importar la Colección en Postman

1. Abre **Postman**
2. Click en **Import** (botón en la esquina superior izquierda)
3. Arrastra y suelta o selecciona el archivo `API collection.json`
4. Click en **Import**

### Paso 2: Importar el Ambiente (Environment)

1. En Postman, ve a **Environments** en la barra lateral izquierda
2. Click en **Import**
3. Selecciona el archivo `API environment.json`
4. Activa el ambiente haciendo click en el dropdown superior derecho y seleccionando "DevicesAPI - Local Environment"

### Paso 3: Iniciar tu API

Antes de ejecutar las pruebas, asegúrate de que tu API esté corriendo:

```bash
cd C:\Users\herivort\Documents\GitHub\DevicesAPI
dotnet run
```

La API debe estar disponible en: `http://localhost:5094`

## Estructura de la Colección

La colección está organizada en **5 carpetas principales**:

### 1. Dispositivos (3 endpoints)
- `GET - Obtener Todos los Dispositivos`
- `GET - Obtener Dispositivo por ID`
- `POST - Crear Nuevo Dispositivo`

### 2. Usuarios (3 endpoints)
- `GET - Obtener Todos los Usuarios`
- `GET - Obtener Usuario por ID`
- `POST - Crear Nuevo Usuario`

### 3. Alertas (3 endpoints)
- `GET - Obtener Todas las Alertas`
- `GET - Obtener Alerta por ID`
- `POST - Crear Nueva Alerta`

### 4. Historial Dispositivos (3 endpoints)
- `GET - Obtener Todo el Historial`
- `GET - Obtener Historial por ID`
- `POST - Crear Registro de Historial`

### 5. Health Check (1 endpoint)
- `Verificar API Disponible`

**Total: 13 requests con pruebas automatizadas**

## Ejecutar las Pruebas

### Opción 1: Ejecutar Request Individual

1. Selecciona cualquier request en la colección
2. Click en **Send**
3. Ve a la pestaña **Test Results** para ver los resultados

### Opción 2: Ejecutar Toda la Carpeta

1. Click derecho en cualquier carpeta (ej. "Dispositivos")
2. Selecciona **Run folder**
3. Click en **Run DevicesAPI** para ejecutar todas las pruebas de esa carpeta

### Opción 3: Ejecutar Toda la Colección

1. Click derecho en "DevicesAPI - Complete Test Suite"
2. Selecciona **Run collection**
3. Configura las opciones:
   - **Iterations**: 1 (o más para stress testing)
   - **Delay**: 100ms entre requests (recomendado)
4. Click en **Run DevicesAPI**

### Opción 4: Ejecutar desde la Línea de Comandos (Newman)

Instala Newman (CLI de Postman):

```bash
npm install -g newman
```

Ejecuta la colección:

```bash
newman run DevicesAPI.postman_collection.json -e DevicesAPI.postman_environment.json
```

Para un reporte HTML detallado:

```bash
npm install -g newman-reporter-html
newman run DevicesAPI.postman_collection.json -e DevicesAPI.postman_environment.json -r html
```

## Pruebas Automatizadas Incluidas

Cada endpoint incluye validaciones específicas:

### Pruebas Generales (Todos los Endpoints)

- ✅ **Status Code**: Verifica códigos de respuesta correctos (200, 201, 404)
- ✅ **Response Time**: Valida tiempos de respuesta aceptables (< 2000ms)
- ✅ **Content Type**: Verifica que sea JSON
- ✅ **Response Structure**: Valida que tenga la estructura esperada

### Pruebas Específicas por Tipo

#### GET ALL (Obtener Todos)
- Verifica que la respuesta sea un array
- Guarda IDs automáticamente para pruebas posteriores
- Valida estructura de objetos si hay datos

#### GET BY ID (Obtener por ID)
- Verifica que el ID coincida con el solicitado
- Valida todos los campos obligatorios
- Verifica tipos de datos correctos

#### POST (Crear)
- Verifica que devuelva el objeto creado
- Guarda el nuevo ID para pruebas subsecuentes
- Valida que los datos enviados se guardaron correctamente
- Usa datos dinámicos (random) en cada ejecución

### Ejemplos de Validaciones Específicas

**Dispositivos:**
```javascript
- Valida serialDispositivo, MAC, firmware
- Verifica coordenadas (latitud, longitud)
- Valida estado activo
```

**Usuarios:**
```javascript
- Valida formato de email con regex
- Verifica estructura de rol
- Valida que el hash de contraseña existe
```

**Alertas:**
```javascript
- Verifica que 'procesado' sea booleano
- Valida severidad
- Verifica asociación con dispositivo
```

**Historial:**
```javascript
- Valida que valorProximidad sea numérico
- Verifica que movimientoDetectado sea booleano
- Valida estados permitidos
```

## Variables de Entorno

Las siguientes variables se actualizan automáticamente durante la ejecución:

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `base_url` | URL base de la API | `http://localhost:5094` |
| `dispositivo_id` | ID del último dispositivo obtenido | `1` |
| `usuario_id` | ID del último usuario obtenido | `1` |
| `alerta_id` | ID de la última alerta obtenida | `1` |
| `historial_id` | ID del último historial obtenido | `1` |
| `nuevo_dispositivo_id` | ID del dispositivo recién creado | Auto |
| `nuevo_usuario_id` | ID del usuario recién creado | Auto |
| `nueva_alerta_id` | ID de la alerta recién creada | Auto |
| `nuevo_historial_id` | ID del historial recién creado | Auto |

## Datos de Prueba Dinámicos

La colección usa variables dinámicas de Postman para generar datos únicos en cada ejecución:

- `{{$randomInt}}` - Número entero aleatorio
- `{{$randomMACAddress}}` - Dirección MAC aleatoria
- `{{$randomStreetAddress}}` - Dirección de calle aleatoria
- `{{$randomPassword}}` - Contraseña aleatoria

Esto permite ejecutar las pruebas múltiples veces sin conflictos de datos duplicados.

## Interpretando los Resultados

### En Postman (Interfaz Gráfica)

Después de ejecutar un request, ve a la pestaña **Test Results**:

- ✅ **Verde** = Prueba pasó exitosamente
- ❌ **Rojo** = Prueba falló
- 📊 **Resumen** = Muestra total de pruebas pasadas/falladas

### Consola de Postman

Abre la consola (View → Show Postman Console) para ver:
- Logs detallados de cada request
- Valores de variables
- Errores específicos

### Con Newman (CLI)

El reporte muestra:
```
┌─────────────────────────┬──────────┬──────────┐
│                         │ executed │   failed │
├─────────────────────────┼──────────┼──────────┤
│              iterations │        1 │        0 │
├─────────────────────────┼──────────┼──────────┤
│                requests │       13 │        0 │
├─────────────────────────┼──────────┼──────────┤
│            test-scripts │       26 │        0 │
├─────────────────────────┼──────────┼──────────┤
│      prerequest-scripts │       13 │        0 │
├─────────────────────────┼──────────┼──────────┤
│              assertions │       65 │        0 │
└─────────────────────────┴──────────┴──────────┘
```

## Troubleshooting (Solución de Problemas)

### Error: "Could not get any response"

**Causa**: La API no está corriendo o está en un puerto diferente.

**Solución**:
1. Verifica que la API esté corriendo: `dotnet run`
2. Verifica el puerto correcto en el navegador
3. Actualiza la variable `base_url` en el environment

### Tests Fallan: "Status code is 404"

**Causa**: No hay datos en la base de datos.

**Solución**:
1. Ejecuta primero los requests POST para crear datos
2. O ajusta los IDs en las variables de entorno

### Error: "Invalid SSL certificate"

**Causa**: Certificado HTTPS en desarrollo.

**Solución**:
1. En Postman: Settings → General → desactiva "SSL certificate verification"
2. O usa HTTP en lugar de HTTPS: `http://localhost:5094`

### Tests Fallan: "Response time too long"

**Causa**: La API responde lento.

**Solución**:
1. Revisa la conexión a la base de datos
2. Ajusta los límites de tiempo en los tests (edita los scripts)

## Personalizar las Pruebas

### Agregar Nuevas Validaciones

Edita la pestaña **Tests** de cualquier request:

```javascript
// Ejemplo: Validar que un dispositivo esté activo
pm.test("El dispositivo está activo", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.activo).to.eql("true");
});
```

### Cambiar Tiempos de Respuesta Esperados

```javascript
// Cambiar de 2000ms a 5000ms
pm.test("Tiempo de respuesta aceptable", function () {
    pm.expect(pm.response.responseTime).to.be.below(5000);
});
```

### Agregar Datos Específicos

En lugar de usar variables aleatorias, puedes usar datos fijos:

```json
{
  "serialDispositivo": "DEVICE-001",
  "mac": "AA:BB:CC:DD:EE:FF",
  "firmware": "v1.0.0"
}
```

## Integración con CI/CD

### GitHub Actions

Crea `.github/workflows/api-tests.yml`:

```yaml
name: API Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '8.0.x'
      - name: Start API
        run: dotnet run &
      - name: Wait for API
        run: sleep 10
      - name: Install Newman
        run: npm install -g newman
      - name: Run Tests
        run: newman run DevicesAPI.postman_collection.json -e DevicesAPI.postman_environment.json
```

## Mejores Prácticas

1. **Ejecuta Health Check primero** para verificar disponibilidad
2. **Ejecuta GETs antes de POSTs** para entender el estado actual
3. **Usa el ambiente correcto** (local, dev, prod)
4. **Revisa la consola** para debugging detallado
5. **Guarda los resultados** para comparación histórica
6. **Documenta cambios** cuando modifiques la colección

## Recursos Adicionales

- [Documentación oficial de Postman](https://learning.postman.com/)
- [Newman CLI Documentation](https://learning.postman.com/docs/running-collections/using-newman-cli/command-line-integration-with-newman/)
- [Postman Test Script Examples](https://learning.postman.com/docs/writing-scripts/test-scripts/)

## Soporte

Si encuentras problemas con las pruebas:

1. Verifica que la API esté corriendo
2. Revisa la consola de Postman para logs detallados
3. Valida las variables de entorno
4. Confirma que la base de datos esté accesible
