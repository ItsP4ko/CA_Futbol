namespace Dominio.Entidades;

public class Nacionalidad
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Alias { get; private set; } 
    
    public Nacionalidad (string nombre, string alias)
    {
        Nombre = nombre;
        Alias = alias;
    }

    public void ActualizarNacionalidad(string nombre, string alias)
    {
        Nombre = nombre;
        Alias = alias;
    }
}