using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;

public class NombreAliasUnicoHandler : ValidadorNacionalidadBase
{
    private readonly INacionalidadRepository _nacionalidadRepository;

    public NombreAliasUnicoHandler(INacionalidadRepository nacionalidadRepository)
    {
        _nacionalidadRepository = nacionalidadRepository;
    }

    public override async Task<ResultadoValidacion> ValidarAsync(ContextoNacionalidad contexto)
    {
        // En una modificación se excluye el propio registro
        if (await _nacionalidadRepository.ExisteNombreOAliasAsync(contexto.Nombre, contexto.Alias, contexto.Id))
            return ResultadoValidacion.Conflicto("Ya existe una nacionalidad con ese nombre o alias.");

        return await base.ValidarAsync(contexto);
    }
}
