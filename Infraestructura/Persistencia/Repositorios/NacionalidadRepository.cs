using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;

namespace Infraestructura.Persistencia.Repositorios;

public class NacionalidadRepository : INacionalidadRepository
{
    private readonly LigaDbContext _context;

    public NacionalidadRepository(LigaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Nacionalidad>> ObtenerTodasAsync()
    {
        return await _context.Nacionalidades
            .AsNoTracking()
            .OrderBy(n => n.Nombre)
            .ToListAsync();
    }

    public async Task<Nacionalidad?> ObtenerNacionalidadPorIdAsync(int id)
    {
        return await _context.Nacionalidades.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<bool> ExisteNombreOAliasAsync(string nombre, string alias, int? excluirId = null)
    {
        var nombreNormalizado = nombre.Trim().ToLower();
        var aliasNormalizado = alias.Trim().ToLower();

        return await _context.Nacionalidades.AnyAsync(n =>
            n.Id != excluirId &&
            (n.Nombre.ToLower() == nombreNormalizado || n.Alias.ToLower() == aliasNormalizado));
    }

    public async Task<bool> TieneJugadoresAsync(int id)
    {
        return await _context.Jugadores.AnyAsync(j => j.NacionalidadId == id);
    }

    public async Task<int> CrearNacionalidadAsync(Nacionalidad nacionalidad)
    {
        _context.Nacionalidades.Add(nacionalidad);
        await _context.SaveChangesAsync();
        return nacionalidad.Id;
    }

    public async Task GuardarCambiosAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EliminarNacionalidadAsync(int id)
    {
        var nacionalidad = await _context.Nacionalidades.FindAsync(id);
        if (nacionalidad == null)
        {
            return false;
        }
        _context.Nacionalidades.Remove(nacionalidad);
        await _context.SaveChangesAsync();
        return true;
    }
}
