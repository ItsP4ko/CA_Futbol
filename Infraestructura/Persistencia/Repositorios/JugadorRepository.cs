using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;

namespace Infraestructura.Persistencia.Repositorios;

public class JugadorRepository : IJugadorRepository
{
    private readonly LigaDbContext _context;
    public JugadorRepository(LigaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Jugador>> ObtenerTodosAsync()
    {
        return await _context.Jugadores.AsNoTracking().Include(j => j.Nacionalidad).ToListAsync();
    }

    public async Task<Jugador?> ObtenerJugadorPorIdAsync(int id)
    {
        return await _context.Jugadores
            .Include(j => j.Nacionalidad)
            .FirstOrDefaultAsync(j => j.Id == id);
    }
    
    public async Task<Jugador?> ObtenerJugadorPorNombreApellidoAsync(string nombre, string apellido)
    {
        return await _context.Jugadores.FirstOrDefaultAsync(j =>
            j.Nombre.Trim().ToLower() == nombre.Trim().ToLower() &&
            j.Apellido.Trim().ToLower() == apellido.Trim().ToLower());
    }

    public async Task<int> CrearJugadorAsync(Jugador jugador)
    {
        // 1. Agregamos la entidad al DbContext
        _context.Jugadores.Add(jugador);

        // 2. Persistimos en la base de datos
        await _context.SaveChangesAsync();

        // 3. Retornamos el Id generado por la BD
        return jugador.Id;
    }
    public async Task GuardarCambiosAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> EliminarJugadorAsync(int id)
    {
        var jugadorEliminar = await _context.Jugadores.FindAsync(id);
        if (jugadorEliminar == null)
        {
            return false;
        }
        _context.Jugadores.Remove(jugadorEliminar);
        await _context.SaveChangesAsync();
        return true;

    }
    
}