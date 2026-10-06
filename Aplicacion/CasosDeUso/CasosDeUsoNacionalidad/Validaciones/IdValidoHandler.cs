namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public class IdValidoHandler : ValidadorNacionalidadBase
{
    public override Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        if (contexto.Id is null or <= 0)
            return Task.FromResult(ResultadoValidacion.Invalido("El ID de la nacionalidad debe ser mayor a 0."));

        return base.ValidarAsync(contexto);
    }
}
