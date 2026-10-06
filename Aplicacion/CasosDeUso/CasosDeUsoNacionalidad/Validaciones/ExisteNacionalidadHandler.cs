using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public class ExisteNacionalidadHandler : ValidadorNacionalidadBase
{
    private readonly INacionalidadRepository _nacionalidadRepository;

    public ExisteNacionalidadHandler(INacionalidadRepository nacionalidadRepository)
    {
        _nacionalidadRepository = nacionalidadRepository;
    }

    public override async Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        if (await _nacionalidadRepository.ObtenerNacionalidadPorIdAsync(contexto.Id!.Value) is null)
            return ResultadoValidacion.NoEncontrado($"No existe la nacionalidad con ID {contexto.Id}.");

        return await base.ValidarAsync(contexto);
    }
}
