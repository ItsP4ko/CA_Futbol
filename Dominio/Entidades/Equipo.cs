using System.ComponentModel.DataAnnotations;

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

    public void AgregarJugador(Jugador jugador)
    {
        ArgumentNullException.ThrowIfNull(jugador);

        // Reglas de negocio del dominio
        if (_jugadores.Count >= 22)
            throw new InvalidOperationException("El equipo ya alcanzó el límite máximo de 22 jugadores.");

        if (_jugadores.Any(j => j.Id == jugador.Id && j.Id != 0))
            throw new InvalidOperationException("El jugador ya forma parte de este equipo.");

        if (jugador.EquipoId is not null)
        {
            throw new InvalidOperationException("El jugador ya forma parte de un equipo.");
        }

        _jugadores.Add(jugador);
        jugador.CambiarEquipo(this.Id);
    }

    public void EliminarJugador(int id)
    {
        Jugador? jugador = _jugadores.FirstOrDefault(j => j.Id == id);
        if (jugador == null)
            throw new InvalidOperationException("El jugador no existe.");
        _jugadores.Remove(jugador);
        jugador.QuitarEquipo();
    }
}