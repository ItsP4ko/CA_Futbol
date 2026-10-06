namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

// Chain of Responsibility: cada validador decide si corta la cadena o le pasa la solicitud al siguiente
public abstract class ValidadorNacionalidadBase
{
    private ValidadorNacionalidadBase? _siguiente;

    // Devuelve el siguiente para poder encadenar: a.SetSiguiente(b).SetSiguiente(c)
    public ValidadorNacionalidadBase SetSiguiente(ValidadorNacionalidadBase siguiente)
    {
        _siguiente = siguiente;
        return siguiente;
    }

    public virtual Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        return _siguiente is null
            ? Task.FromResult(ResultadoValidacion.Ok())
            : _siguiente.ValidarAsync(contexto);
    }
}
