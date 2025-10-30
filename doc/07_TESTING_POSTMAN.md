# 🧪 Testing de Endpoints con Postman

> Guía completa para probar los endpoints de la API DevicesAPI usando Postman

---

## 📋 Prerequisitos

### 1. Instalar Postman

**Descargar:** https://www.postman.com/downloads/

**Alternativas:**
- Insomnia: https://insomnia.rest/
- Thunder Client (extensión de VS Code)
- REST Client (extensión de VS Code)

### 2. Backend corriendo

Asegúrate de que el backend DevicesAPI esté corriendo:

```bash
cd DevicesAPI
dotnet run
```

Salida esperada:
```
Now listening on: https://localhost:7026
Now listening on: http://localhost:5000
```

---

## 🚀 Configuración Inicial de Postman

### Paso 1: Crear una Collection

1. Abre Postman
2. Click en "Collections" → "Create Collection"
3. Nombre: `Phanteon DevicesAPI`
4. Descripción: `Endpoints para el proyecto Phanteon`

### Paso 2: Configurar Variables de Entorno

1. Click en "Environments" → "Create Environment"
2. Nombre: `DevicesAPI - Local`
3. Agregar variables:

| Variable | Initial Value | Current Value |
|----------|--------------|---------------|
| `base_url` | `https://localhost:7026` | `https://localhost:7026` |
| `base_url_http` | `http://localhost:5000` | `http://localhost:5000` |

4. Click "Save"
5. Seleccionar el environment "DevicesAPI - Local" en el dropdown superior derecho

---

## 📡 Endpoints Disponibles

### 🔷 Dispositivos

#### GET - Obtener todos los dispositivos

**Request:**
```
GET {{base_url}}/api/dispositivos/getall
```

**Headers:**
```
Accept: application/json
```

**Respuesta exitosa (200 OK):**
```json
[
  {
    "idDispositivo": 1,
    "serialDispositivo": "DEV-001",
    "mac": "00:1A:2B:3C:4D:5E",
    "firmware": "v1.2.3",
    "direccion": "Av. Principal 123",
    "latitud": 13.6929,
    "longitud": -89.2182,
    "registro": "2024-10-29T10:00:00",
    "activo": "Activo",
    "ultimaVista": "2024-10-29T15:30:00"
  }
]
```

**Respuesta vacía (200 OK):**
```json
[]
```

---

#### GET - Obtener dispositivo por ID

**Request:**
```
GET {{base_url}}/api/dispositivos/getbyid/1
```

**Headers:**
```
Accept: application/json
```

**Respuesta exitosa (200 OK):**
```json
{
  "idDispositivo": 1,
  "serialDispositivo": "DEV-001",
  "mac": "00:1A:2B:3C:4D:5E",
  "firmware": "v1.2.3",
  "direccion": "Av. Principal 123",
  "latitud": 13.6929,
  "longitud": -89.2182,
  "registro": "2024-10-29T10:00:00",
  "activo": "Activo",
  "ultimaVista": "2024-10-29T15:30:00"
}
```

**Respuesta error (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

#### POST - Crear nuevo dispositivo

**Request:**
```
POST {{base_url}}/api/dispositivos/post
```

**Headers:**
```
Content-Type: application/json
Accept: application/json
```

**Body (JSON):**
```json
{
  "serialDispositivo": "DEV-002",
  "mac": "00:1A:2B:3C:4D:5F",
  "firmware": "v1.2.3",
  "direccion": "Calle Secundaria 456",
  "latitud": 13.6929,
  "longitud": -89.2182,
  "registro": "2024-10-29T10:00:00",
  "activo": "Activo",
  "ultimaVista": "2024-10-29T10:00:00"
}
```

**Respuesta exitosa (201 Created):**
```json
{
  "idDispositivo": 2,
  "serialDispositivo": "DEV-002",
  "mac": "00:1A:2B:3C:4D:5F",
  "firmware": "v1.2.3",
  "direccion": "Calle Secundaria 456",
  "latitud": 13.6929,
  "longitud": -89.2182,
  "registro": "2024-10-29T10:00:00",
  "activo": "Activo",
  "ultimaVista": "2024-10-29T10:00:00"
}
```

---

### 🔷 Usuarios

#### GET - Obtener todos los usuarios

**Request:**
```
GET {{base_url}}/api/usuarios/getall
```

**Respuesta exitosa (200 OK):**
```json
[
  {
    "idUsuario": 1,
    "nombreUsuario": "admin",
    "correo": "admin@phanteon.com",
    "passwordHash": "hashed_password_here",
    "rol": "Admin"
  }
]
```

---

#### GET - Obtener usuario por ID

**Request:**
```
GET {{base_url}}/api/usuarios/getbyid/1
```

**Respuesta exitosa (200 OK):**
```json
{
  "idUsuario": 1,
  "nombreUsuario": "admin",
  "correo": "admin@phanteon.com",
  "passwordHash": "hashed_password_here",
  "rol": "Admin"
}
```

---

#### POST - Crear nuevo usuario

**Request:**
```
POST {{base_url}}/api/usuarios/post
```

**Headers:**
```
Content-Type: application/json
Accept: application/json
```

**Body (JSON):**
```json
{
  "nombreUsuario": "johndoe",
  "correo": "john@example.com",
  "passwordHash": "hashed_password_123",
  "rol": "Usuario"
}
```

**Respuesta exitosa (201 Created):**
```json
{
  "idUsuario": 2,
  "nombreUsuario": "johndoe",
  "correo": "john@example.com",
  "passwordHash": "hashed_password_123",
  "rol": "Usuario"
}
```

---

### 🔷 Alertas

#### GET - Obtener todas las alertas

**Request:**
```
GET {{base_url}}/api/alertas/getall
```

**Respuesta exitosa (200 OK):**
```json
[
  {
    "idAlerta": 1,
    "idDispositivo": 1,
    "tipoAlerta": "Crítica",
    "mensaje": "Batería baja (15%)",
    "fechaHora": "2024-10-29T14:30:00",
    "estado": "Nueva"
  }
]
```

---

#### GET - Obtener alerta por ID

**Request:**
```
GET {{base_url}}/api/alertas/getbyid/1
```

**Respuesta exitosa (200 OK):**
```json
{
  "idAlerta": 1,
  "idDispositivo": 1,
  "tipoAlerta": "Crítica",
  "mensaje": "Batería baja (15%)",
  "fechaHora": "2024-10-29T14:30:00",
  "estado": "Nueva"
}
```

---

#### POST - Crear nueva alerta

**Request:**
```
POST {{base_url}}/api/alertas/post
```

**Headers:**
```
Content-Type: application/json
Accept: application/json
```

**Body (JSON):**
```json
{
  "idDispositivo": 1,
  "tipoAlerta": "Advertencia",
  "mensaje": "Temperatura elevada (35°C)",
  "fechaHora": "2024-10-29T15:00:00",
  "estado": "Nueva"
}
```

**Tipos de alerta válidos:**
- `"Crítica"`
- `"Advertencia"`
- `"Info"`

**Estados válidos:**
- `"Nueva"`
- `"Leída"`
- `"Resuelta"`

**Respuesta exitosa (201 Created):**
```json
{
  "idAlerta": 2,
  "idDispositivo": 1,
  "tipoAlerta": "Advertencia",
  "mensaje": "Temperatura elevada (35°C)",
  "fechaHora": "2024-10-29T15:00:00",
  "estado": "Nueva"
}
```

---

### 🔷 Historial de Dispositivos

#### GET - Obtener todo el historial

**Request:**
```
GET {{base_url}}/api/historialdispositivos/getall
```

**Respuesta exitosa (200 OK):**
```json
[
  {
    "idHistorial": 1,
    "idDispositivo": 1,
    "evento": "Conexión establecida",
    "fechaHora": "2024-10-29T10:00:00",
    "detalles": "{\"ip\": \"192.168.1.100\", \"puerto\": 8080}"
  }
]
```

---

#### GET - Obtener historial por ID

**Request:**
```
GET {{base_url}}/api/historialdispositivos/getbyid/1
```

**Respuesta exitosa (200 OK):**
```json
{
  "idHistorial": 1,
  "idDispositivo": 1,
  "evento": "Conexión establecida",
  "fechaHora": "2024-10-29T10:00:00",
  "detalles": "{\"ip\": \"192.168.1.100\", \"puerto\": 8080}"
}
```

---

#### POST - Crear registro de historial

**Request:**
```
POST {{base_url}}/api/historialdispositivos/post
```

**Headers:**
```
Content-Type: application/json
Accept: application/json
```

**Body (JSON):**
```json
{
  "idDispositivo": 1,
  "evento": "Firmware actualizado",
  "fechaHora": "2024-10-29T15:30:00",
  "detalles": "{\"version_anterior\": \"v1.2.2\", \"version_nueva\": \"v1.2.3\"}"
}
```

**Respuesta exitosa (201 Created):**
```json
{
  "idHistorial": 2,
  "idDispositivo": 1,
  "evento": "Firmware actualizado",
  "fechaHora": "2024-10-29T15:30:00",
  "detalles": "{\"version_anterior\": \"v1.2.2\", \"version_nueva\": \"v1.2.3\"}"
}
```

---

## 🔬 Scripts de Postman

### Pre-request Scripts

Scripts que se ejecutan ANTES de enviar la request.

#### Generar timestamp automático

```javascript
// Pre-request Script
pm.environment.set("timestamp", new Date().toISOString());
```

Usar en el body:
```json
{
  "fechaHora": "{{timestamp}}"
}
```

#### Generar ID aleatorio

```javascript
// Pre-request Script
pm.environment.set("random_id", Math.floor(Math.random() * 1000) + 1);
```

---

### Tests (Validation Scripts)

Scripts que se ejecutan DESPUÉS de recibir la respuesta.

#### Validar código de estado 200

```javascript
// Tests
pm.test("Status code is 200", function () {
    pm.response.to.have.status(200);
});
```

#### Validar que la respuesta sea JSON

```javascript
// Tests
pm.test("Response is JSON", function () {
    pm.response.to.be.json;
});
```

#### Validar que la respuesta sea un array

```javascript
// Tests
pm.test("Response is an array", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.be.an('array');
});
```

#### Validar estructura de dispositivo

```javascript
// Tests
pm.test("Dispositivo has correct structure", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.have.property('idDispositivo');
    pm.expect(jsonData).to.have.property('serialDispositivo');
    pm.expect(jsonData).to.have.property('mac');
    pm.expect(jsonData).to.have.property('firmware');
    pm.expect(jsonData).to.have.property('activo');
});
```

#### Guardar ID de la respuesta para usar en otros requests

```javascript
// Tests (en POST Create Dispositivo)
pm.test("Status code is 201", function () {
    pm.response.to.have.status(201);
});

var jsonData = pm.response.json();
pm.environment.set("dispositivoId", jsonData.idDispositivo);
```

Luego usar en otro request:
```
GET {{base_url}}/api/dispositivos/getbyid/{{dispositivoId}}
```

---

## 📝 Collection completa de ejemplo

### Importar esta collection a Postman:

```json
{
  "info": {
    "name": "Phanteon DevicesAPI",
    "schema": "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
  },
  "item": [
    {
      "name": "Dispositivos",
      "item": [
        {
          "name": "Get All Dispositivos",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{base_url}}/api/dispositivos/getall",
              "host": ["{{base_url}}"],
              "path": ["api", "dispositivos", "getall"]
            }
          },
          "event": [
            {
              "listen": "test",
              "script": {
                "exec": [
                  "pm.test(\"Status code is 200\", function () {",
                  "    pm.response.to.have.status(200);",
                  "});",
                  "",
                  "pm.test(\"Response is an array\", function () {",
                  "    var jsonData = pm.response.json();",
                  "    pm.expect(jsonData).to.be.an('array');",
                  "});"
                ]
              }
            }
          ]
        },
        {
          "name": "Get Dispositivo by ID",
          "request": {
            "method": "GET",
            "header": [],
            "url": {
              "raw": "{{base_url}}/api/dispositivos/getbyid/1",
              "host": ["{{base_url}}"],
              "path": ["api", "dispositivos", "getbyid", "1"]
            }
          }
        },
        {
          "name": "Create Dispositivo",
          "request": {
            "method": "POST",
            "header": [
              {
                "key": "Content-Type",
                "value": "application/json"
              }
            ],
            "body": {
              "mode": "raw",
              "raw": "{\n  \"serialDispositivo\": \"DEV-TEST-{{$randomInt}}\",\n  \"mac\": \"00:1A:2B:3C:4D:{{$randomHex}}\",\n  \"firmware\": \"v1.2.3\",\n  \"direccion\": \"Test Address\",\n  \"latitud\": 13.6929,\n  \"longitud\": -89.2182,\n  \"registro\": \"{{$isoTimestamp}}\",\n  \"activo\": \"Activo\",\n  \"ultimaVista\": \"{{$isoTimestamp}}\"\n}"
            },
            "url": {
              "raw": "{{base_url}}/api/dispositivos/post",
              "host": ["{{base_url}}"],
              "path": ["api", "dispositivos", "post"]
            }
          },
          "event": [
            {
              "listen": "test",
              "script": {
                "exec": [
                  "pm.test(\"Status code is 201\", function () {",
                  "    pm.response.to.have.status(201);",
                  "});",
                  "",
                  "var jsonData = pm.response.json();",
                  "pm.environment.set(\"lastDispositivoId\", jsonData.idDispositivo);"
                ]
              }
            }
          ]
        }
      ]
    }
  ],
  "variable": [
    {
      "key": "base_url",
      "value": "https://localhost:7026"
    }
  ]
}
```

### Cómo importar:

1. Copia el JSON de arriba
2. En Postman: Import → Raw text → Pegar → Continue → Import
3. La collection "Phanteon DevicesAPI" aparecerá en tu sidebar

---

## 🎯 Escenarios de Testing Comunes

### Escenario 1: Crear dispositivo y luego consultarlo

**Paso 1:** POST Create Dispositivo
```json
{
  "serialDispositivo": "DEV-NUEVO",
  "mac": "00:11:22:33:44:55",
  "firmware": "v1.0.0",
  "direccion": "Test",
  "latitud": 0,
  "longitud": 0,
  "registro": "2024-10-29T10:00:00",
  "activo": "Activo",
  "ultimaVista": "2024-10-29T10:00:00"
}
```

**Guardar ID en Tests:**
```javascript
var jsonData = pm.response.json();
pm.environment.set("newDispositivoId", jsonData.idDispositivo);
```

**Paso 2:** GET por ID usando el ID guardado
```
GET {{base_url}}/api/dispositivos/getbyid/{{newDispositivoId}}
```

---

### Escenario 2: Crear dispositivo y alerta asociada

**Paso 1:** Crear dispositivo (guardar ID)

**Paso 2:** Crear alerta usando el ID del dispositivo
```json
{
  "idDispositivo": {{newDispositivoId}},
  "tipoAlerta": "Info",
  "mensaje": "Dispositivo registrado correctamente",
  "fechaHora": "{{$isoTimestamp}}",
  "estado": "Nueva"
}
```

---

## 🔍 Variables Dinámicas de Postman

Postman tiene variables predefinidas útiles:

| Variable | Ejemplo | Descripción |
|----------|---------|-------------|
| `{{$guid}}` | `612c7e3a-...` | GUID aleatorio |
| `{{$timestamp}}` | `1635523456` | Unix timestamp |
| `{{$isoTimestamp}}` | `2024-10-29T15:30:45.123Z` | ISO 8601 |
| `{{$randomInt}}` | `456` | Entero aleatorio |
| `{{$randomUUID}}` | `5c1e-...` | UUID aleatorio |
| `{{$randomHex}}` | `5F` | Hex aleatorio |

**Ejemplo de uso:**
```json
{
  "serialDispositivo": "DEV-{{$randomInt}}",
  "mac": "00:1A:2B:3C:4D:{{$randomHex}}",
  "registro": "{{$isoTimestamp}}"
}
```

---

## 🐛 Troubleshooting

### Error: "Could not get any response"

**Causa:** Backend no está corriendo o URL incorrecta

**Solución:**
1. Verificar que el backend esté corriendo: `dotnet run`
2. Verificar URL en environment

---

### Error: "SSL Error: Self signed certificate"

**Causa:** Certificado SSL de desarrollo

**Solución:**
1. En Postman: Settings → SSL certificate verification → OFF (solo para desarrollo)

---

### Error: "404 Not Found"

**Causa:** Endpoint incorrecto

**Solución:**
1. Verificar que el endpoint esté bien escrito
2. Verificar que la API tenga ese endpoint implementado

---

## 📚 Recursos Adicionales

- **Postman Documentation:** https://learning.postman.com/docs/getting-started/introduction/
- **Postman Variables:** https://learning.postman.com/docs/sending-requests/variables/
- **Postman Scripts:** https://learning.postman.com/docs/writing-scripts/intro-to-scripts/

---

_Actualizado: 29/10/2024_
_Autor: Héctor Eduardo Véliz Girón_
