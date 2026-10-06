using System.ComponentModel.DataAnnotations;
using Dominio.Common;
using Dominio.Common.CommonErrors;

namespace Dominio.Entidades;

public class Equipo
{
    [Required]
    public int Id { get; private set; }
    [Required]
    public string Nombre { get; private set; }
    [Required]
    public DateOnly Fundacion { get; private set; }
    
    private readonly List<Jugador> _jugadores = new();
    public IReadOnlyCollection<Jugador> Jugadores => _jugadores.AsReadOnly();

    public Equipo(string nombre)
    {
        Nombre = nombre;
        Fundacion = DateOnly.FromDateTime(DateTime.Now);
    }

    public void ActualizarEquipo(string nombre)
    {
        Nombre = nombre;
    }

    public Result AgregarJugador(Jugador jugador)
    {
        ArgumentNullException.ThrowIfNull(jugador);

        // Reglas de negocio del dominio
        if (_jugadores.Count >= 22)
            return EquipoErrores.CupoCompleto;

        // Cubre también el caso de que ya esté en este equipo
        if (jugador.EquipoId is not null)
            return EquipoErrores.JugadorYaEnEquipo;

        _jugadores.Add(jugador);
        jugador.CambiarEquipo(this.Id);
        return Result.Success();
    }

    public Result EliminarJugador(int id)
    {
        Jugador? jugador = _jugadores.FirstOrDefault(j => j.Id == id);
        if (jugador == null)
            return EquipoErrores.JugadorNoPertenece;
        
        _jugadores.Remove(jugador);
        jugador.QuitarEquipo();
        return Result.Success();
    }
}