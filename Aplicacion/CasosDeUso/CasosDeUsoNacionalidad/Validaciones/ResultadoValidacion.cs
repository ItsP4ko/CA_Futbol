namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public enum TipoError
{
    Ninguno,
    Invalido,
    NoEncontrado,
    Conflicto
}

public class ResultadoValidacion
{
    public TipoError Error { get; }
    public string Mensaje { get; }
    public bool EsValido => Error == TipoError.Ninguno;

    private ResultadoValidacion(TipoError error, string mensaje)
    {
        Error = error;
        Mensaje = mensaje;
    }

    public static ResultadoValidacion Ok() => new(TipoError.Ninguno, string.Empty);
    public static ResultadoValidacion Invalido(string mensaje) => new(TipoError.Invalido, mensaje);
    public static ResultadoValidacion NoEncontrado(string mensaje) => new(TipoError.NoEncontrado, mensaje);
    public static ResultadoValidacion Conflicto(string mensaje) => new(TipoError.Conflicto, mensaje);
}
