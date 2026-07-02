# Backend - ASP.NET Core 9 API

API REST para el sistema de gestion de peliculas con .NET 9, Entity Framework Core y MySQL.

## Requisitos

- .NET 9 SDK
- MySQL o MariaDB corriendo en puerto 3306
- Base de datos creada (ver README raiz)

## Configuracion

La conexion se define en `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=pcd_movies_db;User=root;Password=;"
}
```

Si usas otra version de MySQL/MariaDB, ajusta el versionado en `Program.cs`:

```cs
options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 28)))
```

## Ejecucion

```bash
dotnet restore
dotnet run
```

La API corre en http://localhost:5163.

Documentacion interactiva (solo en desarrollo): http://localhost:5163/scalar/v1

## Endpoints

| Metodo | Ruta | Descripcion |
|--------|------|-------------|
| GET | /api/directors | Lista de directores |
| GET | /api/directors/{id} | Director por ID |
| POST | /api/directors | Crear director |
| PUT | /api/directors/{id} | Actualizar director |
| DELETE | /api/directors/{id} | Eliminar director |
| GET | /api/movies | Lista de peliculas (incluye director) |
| GET | /api/movies/{id} | Pelicula por ID |
| POST | /api/movies | Crear pelicula |
| PUT | /api/movies/{id} | Actualizar pelicula |
| DELETE | /api/movies/{id} | Eliminar pelicula |

## CORS

El backend acepta peticiones desde `http://localhost:4200`. Si cambias el puerto del frontend, ajustalo en `Program.cs`.
