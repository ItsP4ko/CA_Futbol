using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface INacionalidadRepository
{
    Task<List<Nacionalidad>> ObtenerTodasAsync();
    Task<Nacionalidad?> ObtenerNacionalidadPorIdAsync(int id);
    Task<bool> ExisteNombreOAliasAsync(string nombre, string alias, int? excluirId = null);
    Task<bool> TieneJugadoresAsync(int id);
    Task<int> CrearNacionalidadAsync(Nacionalidad nacionalidad);
    Task GuardarCambiosAsync();
    Task<bool> EliminarNacionalidadAsync(int id);
}
