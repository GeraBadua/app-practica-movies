# Cinema Admin

CRUD de directores y peliculas con Angular 22, ASP.NET Core 9 y MySQL.

## Stack

- **Frontend:** Angular 22 + Tailwind CSS 4
- **Backend:** ASP.NET Core 9 + Entity Framework Core + Pomelo MySQL
- **Base de datos:** MySQL / MariaDB

## Base de datos

Ejecutar en MySQL / MariaDB:

```sql
CREATE DATABASE IF NOT EXISTS pcd_movies_db;
USE pcd_movies_db;

CREATE TABLE directors (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    nationality VARCHAR(50) NOT NULL,
    age INT NOT NULL,
    active BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE movies (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(150) NOT NULL,
    release_year INT NOT NULL,
    genre VARCHAR(50) NOT NULL,
    duration INT NOT NULL,
    director_id INT NOT NULL,
    CONSTRAINT fk_movies_directors FOREIGN KEY (director_id) REFERENCES directors(id)
);
```

## Requisitos

- XAMPP con MariaDB (puerto 3306)
- .NET 9 SDK
- Node.js >= 22

## Arranque

```bash
# Backend (puerto 5163)
cd backend-aspnet
dotnet restore
dotnet run

# Frontend (puerto 4200)
cd front-angular
npm install
ng serve
```

## Puertos

| Servicio | URL |
|----------|-----|
| Frontend | http://localhost:4200 |
| Backend API | http://localhost:5163 |
| Documentacion API | http://localhost:5163/scalar/v1 |

## Estructura

```
database/           Script SQL
backend-aspnet/     API REST
front-angular/      SPA Angular
```
