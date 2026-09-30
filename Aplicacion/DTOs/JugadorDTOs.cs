using System.ComponentModel.DataAnnotations;
using Dominio.Entidades;

namespace Aplicacion.DTOs;

public record CrearJugadorDto
{
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres.")]
    public string apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La posición es obligatoria.")]
    [MaxLength(50, ErrorMessage = "La posición no puede superar los 50 caracteres.")]
    public string posicion { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Debe proporcionar una nacionalidad válida (ID > 0).")]
    public int nacionalidadId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe proporcionar un equipo válido (ID > 0).")]
    public int? equipoId { get; set; }
}

public record ActualizarJugadorDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del jugador no es válido.")]
    public int idJugador { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 100 caracteres.")]
    public string apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La posición es obligatoria.")]
    [MaxLength(50, ErrorMessage = "La posición no puede superar los 50 caracteres.")]
    public string posicion { get; set; } = string.Empty;
}

public record ActualizarEquipoJugadorDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del jugador debe ser mayor a 0.")]
    public int idJugador { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0.")]
    public int idEquipo { get; set; }
}

// DTO de Salida/Lectura: no requiere validaciones de entrada
public record JugadorDto()
{
    public int id { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string posicion { get; set; } = string.Empty;
    public string nacionalidadAlias { get; set; } = string.Empty;
}