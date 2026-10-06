using Aplicacion.CasosDeUso.CasosDeUsoNacionalidad;
using Aplicacion.CasosDeUso.CasosDeUsoNacionalidad.Validaciones;
using Aplicacion.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CA_Futbol.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NacionalidadController : ControllerBase
{
    private readonly INacionalidadService _nacionalidadService;

    public NacionalidadController(INacionalidadService nacionalidadService)
    {
        _nacionalidadService = nacionalidadService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NacionalidadDto>>> ObtenerTodas()
    {
        return Ok(await _nacionalidadService.ObtenerTodasLasNacionalidadesAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<NacionalidadDto>> ObtenerPorId(int id)
    {
        var nacionalidad = await _nacionalidadService.ObtenerNacionalidadPorIdAsync(id);
        if (nacionalidad is null)
        {
            return NotFound($"No existe la nacionalidad con ID {id}.");
        }
        return Ok(nacionalidad);
    }

    [HttpPost]
    public async Task<ActionResult<NacionalidadDto>> Crear([FromBody] CrearNacionalidadDto dto)
    {
        var (resultado, creada) = await _nacionalidadService.CrearNacionalidadAsync(dto);
        if (!resultado.EsValido)
        {
            return MapearError(resultado);
        }
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada!.idNacionalidad }, creada);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarNacionalidadDto dto)
    {
        if (id != dto.idNacionalidad)
        {
            return BadRequest("El ID de la URL no coincide con el del cuerpo.");
        }
        var resultado = await _nacionalidadService.ActualizarNacionalidadAsync(dto);
        return resultado.EsValido ? NoContent() : MapearError(resultado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var resultado = await _nacionalidadService.EliminarNacionalidadAsync(id);
        return resultado.EsValido ? NoContent() : MapearError(resultado);
    }

    private ObjectResult MapearError(ResultadoValidacion resultado) => resultado.Error switch
    {
        TipoError.NoEncontrado => NotFound(resultado.Mensaje),
        TipoError.Conflicto => Conflict(resultado.Mensaje),
        _ => BadRequest(resultado.Mensaje)
    };
}
