using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTOs;

public record CrearEquipoDto
{
    [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del equipo debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;
}

public record ActualizarEquipoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0.")]
    public int idEquipo { get; set; }

    [Required(ErrorMessage = "El nombre del equipo es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del equipo debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;
}

public record AgregarRemoverJugadorEquipoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID del jugador debe ser mayor a 0.")]
    public int idJugador { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El ID del equipo debe ser mayor a 0.")]
    public int idEquipo { get; set; }

    [Required(ErrorMessage = "El estado/acción es obligatorio.")]
    [RegularExpression("^(AGREGAR|ELIMINAR|ALTA|BAJA)$", ErrorMessage = "El estado debe ser 'AGREGAR' o 'ELIMINAR'.")]
    public string estado { get; set; } = string.Empty;
}

// DTO de Salida/Lectura: no requiere validaciones de entrada
public record EquipoDto
{
    public int idEquipo { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string fundacion { get; set; } = string.Empty;
}