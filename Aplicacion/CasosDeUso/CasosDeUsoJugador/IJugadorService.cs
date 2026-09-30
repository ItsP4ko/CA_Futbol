using Aplicacion.DTOs;

namespace Aplicacion.CasosDeUso.CasosDeUsoJugador;

public interface IJugadorService
{
    Task<List<JugadorDto>> ObtenerTodosLosJugadores();
    Task<JugadorDto?> ObtenerJugadorPorIdAsync(int id);
    Task<bool> CrearJugadorAsync(CrearJugadorDto? dto);
    Task<bool> ActualizarJugadorAsync(ActualizarJugadorDto dto);
    Task<bool> EliminarJugadorAsync(int id);
    Task<bool> CambiarEquipoiJugadorAsync(int idEquipo, int idJugado);
}