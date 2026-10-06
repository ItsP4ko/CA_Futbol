namespace Dominio.Entidades;

public class Nacionalidad
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = null!;
    public string Alias { get; private set; } = null!;

    private Nacionalidad() { }

    public Nacionalidad(string nombre, string alias)
    {
        Validar(nombre, alias);

        Nombre = nombre.Trim();
        Alias = alias.Trim().ToUpperInvariant();
    }

    public void ActualizarNacionalidad(string nombre, string alias)
    {
        Validar(nombre, alias);

        Nombre = nombre.Trim();
        Alias = alias.Trim().ToUpperInvariant();
    }

    private static void Validar(string nombre, string alias)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es requerido.");
        if (string.IsNullOrWhiteSpace(alias))
            throw new ArgumentException("El alias es requerido.");
    }
}
