namespace Dominio.Entidades;

public class Jugador
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Apellido { get; private set; } = null!;
    public string Posicion { get; private set; } = null!;

    // Claves foráneas (IDs)
    public int NacionalidadId { get; private set; }
    public int? EquipoId { get; private set; }

    // Propiedades de navegación para EF Core
    public Nacionalidad? Nacionalidad { get; private set; }
    public Equipo? Equipo { get; private set; }

    private Jugador() { }

    // En el constructor de negocio pasás los IDs (o IDs + objetos si fuera necesario)
    public Jugador(string nombre, string apellido, string posicion, int nacionalidadId)
    {
        Validar(nombre, apellido, posicion);

        Nombre = nombre;
        Apellido = apellido;
        Posicion = posicion;
        NacionalidadId = nacionalidadId;
    }

    public void ActualizarDatos(string nombre, string apellido, string posicion)
    {
        Validar(nombre, apellido, posicion);

        Nombre = nombre;
        Apellido = apellido;
        Posicion = posicion;
    }

    // Solo Equipo (raíz del agregado) cambia el equipo del jugador, así siempre se valida el cupo
    internal void CambiarEquipo(int nuevoEquipoId)
    {
        if (nuevoEquipoId <= 0)
            throw new ArgumentException("El identificador del equipo no es válido.");

        EquipoId = nuevoEquipoId;
    }

    internal void QuitarEquipo()
    {
        EquipoId = null;
    }

    private static void Validar(string nombre, string apellido, string posicion)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ArgumentException("El apellido es requerido.");
        if (string.IsNullOrWhiteSpace(posicion))
            throw new ArgumentException("La posición es requerida.");
    }
}