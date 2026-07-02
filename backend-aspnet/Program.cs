using Microsoft.EntityFrameworkCore;
using backend_aspnet.Data;
using Scalar.AspNetCore; // Importamos Scalar para la interfaz gráfica

var builder = WebApplication.CreateBuilder(args);

// Configuración de la Base de Datos con MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 28))));

// REGISTRAR LOS CONTROLADORES (Vital para que funcionen tus archivos CRUD)
builder.Services.AddControllers();

// CONFIGURAR CORS: Permitir que Angular (puerto 4200) consuma esta API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL del front de Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configuración nativa de OpenAPI de Microsoft
builder.Services.AddOpenApi();

var app = builder.Build();

// Configurar el Pipeline de Peticiones HTTP (Middlewares)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    
    // Activamos Scalar
    app.MapScalarApiReference(options => {
        options.WithTitle("PCD Systems - Movies API")
               .WithTheme(ScalarTheme.Purple);
    });
}

// Habilitar la política de CORS que creamos arriba
app.UseCors("AllowAngular");

app.UseHttpsRedirection();

// MAPEAR RUTAS DE CONTROLADORES
app.MapControllers();

app.Run();