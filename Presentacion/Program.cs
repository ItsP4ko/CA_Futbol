using Aplicacion.CasosDeUso.CasosDeUsoEquipo;
using Aplicacion.CasosDeUso.CasosDeUsoJugador;
using Aplicacion.CasosDeUso.CasosDeUsoNacionalidad;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Infraestructura.Persistencia.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LigaDbContext>(o => o.UseSqlite(builder.Configuration.GetConnectionString("Liga")));
builder.Services.AddScoped<IJugadorRepository, JugadorRepository>();
builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();
builder.Services.AddScoped<INacionalidadRepository, NacionalidadRepository>();
builder.Services.AddScoped<IJugadorService, JugadorService>();
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<INacionalidadService, NacionalidadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();