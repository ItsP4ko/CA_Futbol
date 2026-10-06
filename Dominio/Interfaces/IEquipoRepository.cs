using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IEquipoRepository
{
    Task<List<Jugador>> ObtenerJugadoresEquipoAsync(int equipoId);
    Task<List<Equipo>> ObtenerEquiposAsync();
    Task<Equipo?> ObtenerEquipoPorIdAsync(int idEquipo);
    Task CrearEquipoAsync(Equipo equipo);
    Task<bool> EliminarEquipoAsync(int equipoId);
    Task GuardarCambiosAsync();
}