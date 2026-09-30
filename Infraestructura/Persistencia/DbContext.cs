using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Persistencia;

public class LigaDbContext : DbContext
{
    public LigaDbContext(DbContextOptions<LigaDbContext> options): base(options)
    {
    }
    
    public DbSet<Jugador> Jugadores { get; set; }
    public DbSet<Equipo> Equipos { get; set; }
    public DbSet<Nacionalidad> Nacionalidades { get; set; }
    
}