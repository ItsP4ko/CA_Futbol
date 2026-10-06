using Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;
using Aplicacion.DTOs;

namespace Aplicacion.CasosDeUso.CasosDeUsoNacionalidad;

public interface INacionalidadService
{
    Task<List<NacionalidadDto>> ObtenerTodasLasNacionalidadesAsync();
    Task<NacionalidadDto?> ObtenerNacionalidadPorIdAsync(int id);
    Task<(ResultadoValidacion resultado, NacionalidadDto? nacionalidad)> CrearNacionalidadAsync(CrearNacionalidadDto dto);
    Task<ResultadoValidacion> ActualizarNacionalidadAsync(ActualizarNacionalidadDto dto);
    Task<ResultadoValidacion> EliminarNacionalidadAsync(int id);
}
