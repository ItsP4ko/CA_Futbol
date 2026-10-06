using Aplicacion.DTOs;
using Dominio.Common;

namespace Aplicacion.CasosDeUso.CasosDeUsoEquipo;

public interface IEquipoService
{
    Task<List<EquipoDto>> ObtenerEquiposAsync();
    Task<List<JugadorDto>> ObtenerJugadoresEquipoAsync(int equipoId);
    Task<Result<EquipoDto>> ObtenerEquipoAsync(int id);
    Task<Result<EquipoDto>> CrearEquipoAsync(EquipoDto dto);
    Task<Result> ActualizarEquipoAsync(int id, string nombre);
    Task<Result> EliminarEquipoAsync(int id);
    Task<Result> ActualizarJugadorEquipoAsync(int idEquipo, int idJugador, string estado);
}