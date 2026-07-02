# Sistema de Gestión de Películas

Aplicación Full Stack para la gestión de directores y películas (CRUD completo).  
Frontend en Angular 22 con Tailwind CSS v4, backend en ASP.NET Core 9 con Entity Framework Core y MySQL/MariaDB.

---

## Tecnologías

| Capa       | Tecnologías |
|------------|-------------|
| **BD**     | MySQL / MariaDB |
| **Backend**| ASP.NET Core 9, Entity Framework Core 9, Pomelo.EntityFrameworkCore.MySql, Scalar API Reference |
| **Frontend**| Angular 22, Tailwind CSS 4, Vite, Vitest |

---

## Estructura

```
app-practica-movies/
├── database/            # Script SQL de inicialización
│   └── script.sql       # CREATE DATABASE + tablas (directors, movies)
├── backend-aspnet/      # API RESTful .NET 9
│   ├── Controllers/     # DirectorsController, MoviesController
│   ├── Models/          # Director.cs, Movie.cs
│   ├── Data/            # ApplicationDbContext.cs
│   ├── Program.cs       # Entry point (CORS, Scalar, OpenAPI)
│   └── appsettings.json # Conexión MySQL
└── front-angular/       # SPA Angular 22
    └── src/             # Componentes, rutas, estilos
```

---

## Backend (.NET 9)

### Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- MySQL o MariaDB corriendo en `localhost:3306`

### Configuración

La conexión a BD está en `backend-aspnet/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=pcd_movies_db;User=root;Password=;"
}
```

> **Nota:** La API usa `MariaDbServerVersion(10, 4, 28)`. Si tu servidor tiene otra versión, ajusta el versionado en `Program.cs`.

### Inicializar BD

Ejecuta el script `database/script.sql` en tu gestor MySQL.

### Comandos

```bash
cd backend-aspnet

dotnet restore
dotnet run --launch-profile https
# API disponible en https://localhost:7208 (https) o http://localhost:5163
```

### Documentación interactiva (Scalar)

Con el servidor en desarrollo, abre:

```
https://localhost:7208/scalar/v1
```

### Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/directors` | Listar directores |
| `GET` | `/api/directors/{id}` | Obtener director por ID |
| `POST` | `/api/directors` | Crear director |
| `PUT` | `/api/directors/{id}` | Actualizar director |
| `DELETE` | `/api/directors/{id}` | Eliminar director |
| `GET` | `/api/movies` | Listar películas (con datos del director) |
| `GET` | `/api/movies/{id}` | Obtener película por ID |
| `POST` | `/api/movies` | Crear película (requiere `directorId` válido) |
| `PUT` | `/api/movies/{id}` | Actualizar película |
| `DELETE` | `/api/movies/{id}` | Eliminar película |

### Modelo de datos

```json
// Director
{
  "id": 1,
  "name": "Christopher Nolan",
  "nationality": "Británico",
  "age": 53,
  "active": true
}

// Movie (con Director embebido)
{
  "id": 1,
  "name": "Inception",
  "releaseYear": 2010,
  "genre": "Ciencia Ficción",
  "duration": 148,
  "directorId": 1,
  "director": { ... }
}
```

---

## Frontend (Angular 22)

### Requisitos

- Node.js >= 22
- npm >= 11

### Comandos

```bash
cd front-angular

npm install
ng serve
# Abre http://localhost:4200/
```

### Tests

```bash
ng test   # Vitest
```

---

## Base de datos

### Esquema

```sql
-- Tabla: directors
id          INT PK AUTO_INCREMENT
name        VARCHAR(100)
nationality VARCHAR(50)
age         INT
active      BOOLEAN DEFAULT TRUE

-- Tabla: movies
id           INT PK AUTO_INCREMENT
name         VARCHAR(150)
release_year INT
genre        VARCHAR(50)
duration     INT (minutos)
director_id  INT FK → directors(id)
```

---

## Puesta en marcha rápida

```bash
# 1. Clonar e inicializar BD (ejecutar database/script.sql)

# 2. Backend
cd backend-aspnet
dotnet restore && dotnet run --launch-profile https

# 3. Frontend (otra terminal)
cd front-angular
npm install && ng serve

# 4. Abrir http://localhost:4200/
```

---

## Stack de pruebas

- **Backend:** Pruebas manuales via Scalar (`/scalar/v1`)
- **Frontend:** Vitest (vía `ng test`)