using Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;
using Aplicacion.DTOs;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad;

public class NacionalidadService : INacionalidadService
{
    private readonly INacionalidadRepository _nacionalidadRepository;

    public NacionalidadService(INacionalidadRepository nacionalidadRepository)
    {
        _nacionalidadRepository = nacionalidadRepository;
    }

    public async Task<List<NacionalidadDto>> ObtenerTodasLasNacionalidadesAsync()
    {
        var nacionalidades = await _nacionalidadRepository.ObtenerTodasAsync();
        return nacionalidades.Select(MapearDto).ToList();
    }

    public async Task<NacionalidadDto?> ObtenerNacionalidadPorIdAsync(int id)
    {
        var nacionalidad = await _nacionalidadRepository.ObtenerNacionalidadPorIdAsync(id);
        return nacionalidad is null ? null : MapearDto(nacionalidad);
    }

    public async Task<(ResultadoValidacion resultado, NacionalidadDto? nacionalidad)> CrearNacionalidadAsync(CrearNacionalidadDto dto)
    {
        var contexto = new ContextoNacionalidad { Nombre = dto.nombre, Alias = dto.alias };
        var resultado = await CadenaCrear().ValidarAsync(contexto);
        if (!resultado.EsValido)
        {
            return (resultado, null);
        }

        var nacionalidad = new Nacionalidad(dto.nombre, dto.alias);
        await _nacionalidadRepository.CrearNacionalidadAsync(nacionalidad);
        return (resultado, MapearDto(nacionalidad));
    }

    public async Task<ResultadoValidacion> ActualizarNacionalidadAsync(ActualizarNacionalidadDto dto)
    {
        var contexto = new ContextoNacionalidad { Id = dto.idNacionalidad, Nombre = dto.nombre, Alias = dto.alias };
        var resultado = await CadenaActualizar().ValidarAsync(contexto);
        if (!resultado.EsValido)
        {
            return resultado;
        }

        var nacionalidad = await _nacionalidadRepository.ObtenerNacionalidadPorIdAsync(dto.idNacionalidad);
        nacionalidad!.ActualizarNacionalidad(dto.nombre, dto.alias);
        await _nacionalidadRepository.GuardarCambiosAsync();
        return resultado;
    }

    public async Task<ResultadoValidacion> EliminarNacionalidadAsync(int id)
    {
        var resultado = await CadenaEliminar().ValidarAsync(new ContextoNacionalidad { Id = id });
        if (!resultado.EsValido)
        {
            return resultado;
        }

        await _nacionalidadRepository.EliminarNacionalidadAsync(id);
        return resultado;
    }

    // Armado de las cadenas (rol "Client" del patrón Chain of Responsibility).
    // Para sumar una regla nueva alcanza con crear el handler y agregarlo acá.
    private ValidadorNacionalidadBase CadenaCrear()
    {
        var cadena = new DatosRequeridosHandler();
        cadena.SetSiguiente(new NombreAliasUnicoHandler(_nacionalidadRepository));
        return cadena;
    }

    private ValidadorNacionalidadBase CadenaActualizar()
    {
        var cadena = new IdValidoHandler();
        cadena.SetSiguiente(new DatosRequeridosHandler())
              .SetSiguiente(new ExisteNacionalidadHandler(_nacionalidadRepository))
              .SetSiguiente(new NombreAliasUnicoHandler(_nacionalidadRepository));
        return cadena;
    }

    private ValidadorNacionalidadBase CadenaEliminar()
    {
        var cadena = new IdValidoHandler();
        cadena.SetSiguiente(new ExisteNacionalidadHandler(_nacionalidadRepository))
              .SetSiguiente(new SinJugadoresHandler(_nacionalidadRepository));
        return cadena;
    }

    private static NacionalidadDto MapearDto(Nacionalidad n) => new()
    {
        idNacionalidad = n.Id,
        nombre = n.Nombre,
        alias = n.Alias
    };
}
