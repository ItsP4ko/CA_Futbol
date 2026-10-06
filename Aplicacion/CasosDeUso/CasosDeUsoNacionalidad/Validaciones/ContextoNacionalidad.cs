namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

// Datos de la solicitud que viajan por la cadena de validadores
public class ContextoNacionalidad
{
    public int? Id { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string Alias { get; init; } = string.Empty;
}
