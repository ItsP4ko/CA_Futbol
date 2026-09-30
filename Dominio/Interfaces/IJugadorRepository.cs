
using System.Runtime.CompilerServices;
using Dominio.Entidades;

namespace Dominio.Interfaces;

public interface IJugadorRepository
{
    Task<List<Jugador>> ObtenerTodosAsync();
    Task<Jugador?> ObtenerJugadorPorIdAsync(int id);
    Task<Jugador?> ObtenerJugadorPorNombreApellidoAsync(string nombre, string apellido);
    Task<int> CrearJugadorAsync(Jugador jugador);
    Task GuardarCambiosAsync();
    Task<bool> EliminarJugadorAsync(int id);
}