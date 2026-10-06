using Aplicacion.DTOs;
using Dominio.Common;
using Dominio.Common.CommonErrors;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoEquipo;

public class EquipoService : IEquipoService
{
    private readonly IEquipoRepository _equipoRepository;
    private readonly IJugadorRepository _jugadorRepository;
    
    public EquipoService (IEquipoRepository equipoRepository, IJugadorRepository jugadorRepository)
        {
            _equipoRepository = equipoRepository;
            _jugadorRepository = jugadorRepository;
        }

    public async Task<List<EquipoDto>> ObtenerEquiposAsync()
    {
        var equipos = await _equipoRepository.ObtenerEquiposAsync();

        return equipos.Select(e => new EquipoDto
        {
            idEquipo = e.Id,
            nombre = e.Nombre,
            fundacion = e.Fundacion.ToString()
        }).ToList();
    }

    public async Task<List<JugadorDto>> ObtenerJugadoresEquipoAsync(int equipoId)
    {
        var jugadores = await _equipoRepository.ObtenerJugadoresEquipoAsync(equipoId);

        return jugadores.Select(j => new JugadorDto
        {
            id = j.Id,
            nombre = j.Nombre,
            apellido = j.Apellido,
            nacionalidadAlias = j.Nacionalidad?.Alias ?? "s/n",
            posicion = j.Posicion
        }).ToList();

    }

    public async Task<Result<EquipoDto>> ObtenerEquipoAsync(int id)
    {
        var equipos = await _equipoRepository.ObtenerEquipoPorIdAsync(id);
        if (equipos is null)
        {
            return EquipoErrores.NoEncontrado(id);
        }
        return new EquipoDto
        {
            idEquipo = equipos.Id,
            nombre = equipos.Nombre,
            fundacion = equipos.Fundacion.ToString()
        };
    }

    public async Task<Result<EquipoDto>> CrearEquipoAsync(EquipoDto dto)
    {
        var equipos = await _equipoRepository.ObtenerEquiposAsync();

        if (equipos.Any(e => e.Nombre.Equals(dto.nombre, StringComparison.OrdinalIgnoreCase)))
        {
            return EquipoErrores.NombreDuplicado;
        }
        var nuevoEquipo = new Equipo(dto.nombre);
        // Si la base falla, la excepción sube y termina en un 500
        await _equipoRepository.CrearEquipoAsync(nuevoEquipo);
        dto.idEquipo = nuevoEquipo.Id;
        return dto;
    }

    public async Task<Result> ActualizarEquipoAsync(int id, string nombre)
    {
        var equipo = await _equipoRepository.ObtenerEquipoPorIdAsync(id);
        if (equipo is null)
        {
            return EquipoErrores.NoEncontrado(id);
        }

        equipo.ActualizarEquipo(nombre);
        await _equipoRepository.GuardarCambiosAsync();
        return Result.Success();
    }

    public async Task<Result> EliminarEquipoAsync(int id)
    {
        if (id <= 0)
        {
            return EquipoErrores.NoEncontrado(id);
        }
        if (!await _equipoRepository.EliminarEquipoAsync(id))
        {
            return EquipoErrores.NoEncontrado(id);
        }
        return Result.Success();
    }

    public async Task<Result> ActualizarJugadorEquipoAsync(int idEquipo, int idJugador, string estado)
    {
        var equipo = await _equipoRepository.ObtenerEquipoPorIdAsync(idEquipo);
        if (equipo is null)
        {
            return EquipoErrores.NoEncontrado(idEquipo);
        }

        Result resultado; 
        switch (estado?.Trim().ToUpperInvariant())
        {
            case "AGREGAR" or "ALTA":
                var jugador = await _jugadorRepository.ObtenerJugadorPorIdAsync(idJugador);
                if (jugador is null)
                {
                    return JugadorErrores.NoEncontrado(idJugador);
                }
                // El agregado valida cupo de 22 y pertenencia
                resultado = equipo.AgregarJugador(jugador);
                break;

            case "ELIMINAR" or "BAJA":
                resultado = equipo.EliminarJugador(idJugador);
                break;

            default:
                return EquipoErrores.EstadoInvalido;
            
        }

        if (resultado.IsFailure)
            return resultado;

        await _equipoRepository.GuardarCambiosAsync();
        return Result.Success();
    }
    
    
}