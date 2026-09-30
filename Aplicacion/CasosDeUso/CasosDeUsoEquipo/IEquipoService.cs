using Aplicacion.DTOs;

namespace Aplicacion.CasosDeUso.CasosDeUsoEquipo;

public interface IEquipoService
{
    Task<List<EquipoDto>> ObtenerEquiposAsync();
    Task<List<JugadorDto>> ObtenerJugadoresEquipoAsync(int equipoId);
    Task<EquipoDto?> ObtenerEquipoAsync(int id);
    Task<EquipoDto?> CrearEquipoAsync(EquipoDto dto);
    Task<bool> ActualizarEquipoAsync(int id, string nombre);
    Task<bool> EliminarEquipoAsync(int id);
    Task<bool> ActualizarJugadorEquipoAsync(int idEquipo, int idJugador, string estado);
}