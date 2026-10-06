using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public class SinJugadoresHandler : ValidadorNacionalidadBase
{
    private readonly INacionalidadRepository _nacionalidadRepository;

    public SinJugadoresHandler(INacionalidadRepository nacionalidadRepository)
    {
        _nacionalidadRepository = nacionalidadRepository;
    }

    public override async Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        // Sin esta validación EF borraría en cascada a los jugadores de ese país
        if (await _nacionalidadRepository.TieneJugadoresAsync(contexto.Id!.Value))
            return ResultadoValidacion.Conflicto("No se puede eliminar la nacionalidad porque tiene jugadores asociados.");

        return await base.ValidarAsync(contexto);
    }
}
