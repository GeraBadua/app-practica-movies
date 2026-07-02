using Microsoft.EntityFrameworkCore;
using backend_aspnet.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 4, 28))));

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => {
        options.WithTitle("PCD Systems - Movies API")
               .WithTheme(ScalarTheme.Purple);
    });
}

app.UseCors("AllowAngular");
app.MapControllers();
app.Run();
