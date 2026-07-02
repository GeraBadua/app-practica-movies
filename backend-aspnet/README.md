# Backend - ASP.NET Core 9 Web API

API REST de la app de películas con .NET 9, Entity Framework Core + MySQL y documentación interactiva via Scalar.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- MySQL o MariaDB corriendo

## Comandos

```bash
dotnet restore
dotnet run --launch-profile https
# API en https://localhost:7208 (https) o http://localhost:5163

# Build
dotnet build
```

## Endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/directors` | Listar directores |
| `GET` | `/api/directors/{id}` | Obtener director por ID |
| `POST` | `/api/directors` | Crear director |
| `PUT` | `/api/directors/{id}` | Actualizar director |
| `DELETE` | `/api/directors/{id}` | Eliminar director |
| `GET` | `/api/movies` | Listar películas (con director) |
| `GET` | `/api/movies/{id}` | Obtener película por ID |
| `POST` | `/api/movies` | Crear película |
| `PUT` | `/api/movies/{id}` | Actualizar película |
| `DELETE` | `/api/movies/{id}` | Eliminar película |

## Documentación interactiva

Con el servidor corriendo en modo desarrollo, abre:

```
https://localhost:7208/scalar/v1
```

## Estructura

```
backend-aspnet/
├── Controllers/
│   ├── DirectorsController.cs    # CRUD de directores
│   └── MoviesController.cs       # CRUD de películas
├── Models/
│   ├── Director.cs
│   └── Movie.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Program.cs                    # Entry point (CORS, Scalar, EF, OpenAPI)
├── backend-aspnet.csproj         # net9.0 + EF Core + Pomelo + Scalar
├── appsettings.json              # Conexión MySQL
├── appsettings.Development.json  # Config desarrollo
└── Properties/
    └── launchSettings.json       # Puertos y perfiles
```
