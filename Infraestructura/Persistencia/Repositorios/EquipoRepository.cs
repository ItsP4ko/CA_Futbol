using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;

namespace Infraestructura.Persistencia.Repositorios;

public class EquipoRepository : IEquipoRepository
{
    private readonly LigaDbContext _context;

    public EquipoRepository(LigaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Jugador>> ObtenerJugadoresEquipoAsync(int equipoId)
    {
        return await _context.Jugadores
            .AsNoTracking()
            .Include(j => j.Nacionalidad)
            .Where(j => j.EquipoId == equipoId)
            .ToListAsync();
    }

    // Devuelve el agregado completo (con sus jugadores) para que Equipo pueda validar sus reglas
    public async Task<Equipo?> ObtenerEquipoPorIdAsync(int idEquipo)
    {
        return await _context.Equipos
            .Include(e => e.Jugadores)
            .FirstOrDefaultAsync(e => e.Id == idEquipo);
    }

    public async Task<List<Equipo>> ObtenerEquiposAsync()
    {
        return await _context.Equipos
            .AsNoTracking()
            .ToListAsync();
    }
    

    public async Task CrearEquipoAsync(Equipo equipo)
    {
        _context.Equipos.Add(equipo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EliminarEquipoAsync(int equipoId)
    {
        // Con los jugadores cargados, EF los deja sin equipo en vez de fallar por la FK
        Equipo? equipo = await ObtenerEquipoPorIdAsync(equipoId);
        if  (equipo == null)
            return false;
        _context.Equipos.Remove(equipo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task GuardarCambiosAsync()
    {
        await _context.SaveChangesAsync();
    }
    
}

