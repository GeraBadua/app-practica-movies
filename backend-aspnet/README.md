# Backend - ASP.NET Core 10 Web API

API REST de la app de películas hecha con .NET 10.

## Comandos

```bash
# Restaurar dependencias
dotnet restore

# Correr en modo desarrollo
dotnet run --launch-profile https
# La API queda en https://localhost:7208 (con https) o http://localhost:5163

# Build
dotnet build
```

## Endpoints

Todavía no tengo los endpoints de películas, solo está el que viene por defecto:

```
GET  /weatherforecast     → ejemplo de json de clima
```

Próximamente:
- `GET /api/movies` → listar películas
- `GET /api/movies/{id}` → película por id
- `POST /api/movies` → crear película
- `PUT /api/movies/{id}` → actualizar
- `DELETE /api/movies/{id}` → eliminar
- `GET /api/directors` → listar directores
- etc.

## Estructura

```
backend-aspnet/
├── Program.cs                        # entry point (ahi esta todo por ahora)
├── backend-aspnet.csproj             # depende de Microsoft.AspNetCore.OpenApi
├── appsettings.json                  # config general
├── appsettings.Development.json      # config de desarrollo (no se sube)
└── Properties/
    └── launchSettings.json           # perfiles de ejecución (puertos)
```

