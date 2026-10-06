namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public class DatosRequeridosHandler : ValidadorNacionalidadBase
{
    public override Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        if (string.IsNullOrWhiteSpace(contexto.Nombre))
            return Task.FromResult(ResultadoValidacion.Invalido("El nombre es requerido."));
        if (string.IsNullOrWhiteSpace(contexto.Alias))
            return Task.FromResult(ResultadoValidacion.Invalido("El alias es requerido."));

        return base.ValidarAsync(contexto);
    }
}
