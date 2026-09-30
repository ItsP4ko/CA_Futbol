using Aplicacion.DTOs;
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

    public async Task<EquipoDto?> ObtenerEquipoAsync(int id)
    {
        var equipos = await _equipoRepository.ObtenerEquipoPorIdAsync(id);
        if (equipos is null)
        {
            return null;
        }
        return new EquipoDto
        {
            idEquipo = equipos.Id,
            nombre = equipos.Nombre,
            fundacion = equipos.Fundacion.ToString()
        };
    }

    public async Task<EquipoDto?> CrearEquipoAsync(EquipoDto dto)
    {
        var equipos = await _equipoRepository.ObtenerEquiposAsync();

        if (equipos.Any(e => e.Nombre.Equals(dto.nombre, StringComparison.OrdinalIgnoreCase)))
        {
            return null; 
        }
        var nuevoEquipo = new Equipo(dto.nombre);
        if (!await _equipoRepository.CrearEquipoAsync(nuevoEquipo))
        {
            return null;    
        }
        dto.idEquipo = nuevoEquipo.Id;
        return dto;
    }

    public async Task<bool> ActualizarEquipoAsync(int id, string nombre)
    {
        var equipo = await _equipoRepository.ObtenerEquipoPorIdAsync(id);
        if (equipo is null)
        {
            return false;
        }

        equipo.ActualizarEquipo(nombre);
        await _equipoRepository.GuardarCambiosAsync();
        return true;
    }

    public async Task<bool> EliminarEquipoAsync(int id)
    {
        if (id <= 0)
        {
            return false;
        }
        if (!await _equipoRepository.EliminarEquipoAsync(id))
        {
            return false;
        }
        return true;
    }

    public async Task<bool> ActualizarJugadorEquipoAsync(int idEquipo, int idJugador, string estado)
    {
        var equipo = await _equipoRepository.ObtenerEquipoPorIdAsync(idEquipo);
        if (equipo is null)
        {
            return false;
        }

        switch (estado?.Trim().ToUpperInvariant())
        {
            case "AGREGAR" or "ALTA":
                var jugador = await _jugadorRepository.ObtenerJugadorPorIdAsync(idJugador);
                if (jugador is null)
                {
                    return false;
                }
                // El agregado valida cupo de 22 y pertenencia
                equipo.AgregarJugador(jugador);
                break;

            case "ELIMINAR" or "BAJA":
                equipo.EliminarJugador(idJugador);
                break;

            default:
                throw new ArgumentException($"Estado no válido: '{estado}'. Valores permitidos: AGREGAR, ELIMINAR.");
        }

        await _equipoRepository.GuardarCambiosAsync();
        return true;
    }
    
    
}