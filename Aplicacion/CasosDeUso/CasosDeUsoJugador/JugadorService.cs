using Aplicacion.DTOs;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.CasosDeUso.CasosDeUsoJugador;

public class JugadorService : IJugadorService
{
    private readonly IJugadorRepository _jugadorRepository;
    private readonly IEquipoRepository _equipoRepository;

    public JugadorService(IJugadorRepository jugadorRepository,  IEquipoRepository equipoRepository)
    {
        _jugadorRepository = jugadorRepository;
        _equipoRepository = equipoRepository;
    }

    public async Task<List<JugadorDto>> ObtenerTodosLosJugadores()
    {
        var jugadores = await _jugadorRepository.ObtenerTodosAsync();
        return jugadores.Select(j => new JugadorDto
        {
            id = j.Id,
            nombre = j.Nombre,
            apellido = j.Apellido,
            posicion = j.Posicion,
            nacionalidadAlias = j.Nacionalidad?.Alias ?? "s/n"
        }).ToList();
    }

    public async Task<JugadorDto?> ObtenerJugadorPorIdAsync(int id)
    {
        // 1. Await obligatorio
        var jugador = await _jugadorRepository.ObtenerJugadorPorIdAsync(id);

        // 2. Control de nulos
        if (jugador is null)
        {
            return null;
        }

        // 3. Mapeo a DTO (extrayendo el texto de Nacionalidad)
        return new JugadorDto
        {
            id = jugador.Id,
            nombre = jugador.Nombre,
            apellido = jugador.Apellido,
            posicion = jugador.Posicion,
            nacionalidadAlias = jugador.Nacionalidad?.Alias ?? "s/n"
        };
    }

    public async Task<bool> CrearJugadorAsync(CrearJugadorDto? dto)
    {

        if (dto is null || await _jugadorRepository.ObtenerJugadorPorNombreApellidoAsync(dto.nombre, dto.apellido) is not null)
        {
            return false;
        }

        var jugador = new Jugador(dto.nombre, dto.apellido, dto.posicion, dto.nacionalidadId);

        // El alta en un equipo pasa por el agregado para respetar el cupo
        if (dto.equipoId is not null)
        {
            var equipo = await _equipoRepository.ObtenerEquipoPorIdAsync(dto.equipoId.Value);
            if (equipo is null)
            {
                return false;
            }
            equipo.AgregarJugador(jugador);
        }

        return await _jugadorRepository.CrearJugadorAsync(jugador) > 0;
    }

    public async Task<bool> ActualizarJugadorAsync(ActualizarJugadorDto dto)
    {
        if (dto is null)
        {
            return false;
        }
        var existente = await _jugadorRepository.ObtenerJugadorPorNombreApellidoAsync(dto.nombre, dto.apellido);
        if (existente is not null && existente.Id != dto.idJugador)
        {
            return false;
        }

        var jugador = await _jugadorRepository.ObtenerJugadorPorIdAsync(dto.idJugador);
        if (jugador is null)
        {
            return false;
        }

        jugador.ActualizarDatos(dto.nombre, dto.apellido, dto.posicion);
        await _jugadorRepository.GuardarCambiosAsync();
        return true;
    }

    public async Task<bool> EliminarJugadorAsync(int id)
    {
        if (id <= 0)
        {
            return false;
        }
        if (!await _jugadorRepository.EliminarJugadorAsync(id))
        {
            return false;
        }
        return true;
    }

    public async Task<bool> CambiarEquipoiJugadorAsync(int idEquipo, int idJugado)
    {
        if (idEquipo <= 0 || idJugado <= 0)
        {
            return false;
        }
        
        var jugador = await _jugadorRepository.ObtenerJugadorPorIdAsync(idJugado);
        var equipoNuevo = await _equipoRepository.ObtenerEquipoPorIdAsync(idEquipo);
        
        if (jugador == null || equipoNuevo == null)
        {
            return false;
        }
        if (jugador.EquipoId == idEquipo)
        {
            return true;
        }

        // Pase: baja del equipo actual y alta en el nuevo, ambos por el agregado (valida cupo)
        if (jugador.EquipoId is not null)
        {
            var equipoActual = await _equipoRepository.ObtenerEquipoPorIdAsync(jugador.EquipoId.Value);
            equipoActual?.EliminarJugador(jugador.Id);
        }
        equipoNuevo.AgregarJugador(jugador);

        await _equipoRepository.GuardarCambiosAsync();
        return true;
    }
}