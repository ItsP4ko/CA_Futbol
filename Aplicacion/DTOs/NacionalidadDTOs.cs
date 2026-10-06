using System.ComponentModel.DataAnnotations;

namespace Aplicacion.DTOs;

public record CrearNacionalidadDto
{
    [Required(ErrorMessage = "El nombre del país es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del país debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El alias es obligatorio.")]
    [StringLength(5, MinimumLength = 2, ErrorMessage = "El alias debe tener entre 2 y 5 caracteres.")]
    public string alias { get; set; } = string.Empty;
}

public record ActualizarNacionalidadDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El ID de la nacionalidad debe ser mayor a 0.")]
    public int idNacionalidad { get; set; }

    [Required(ErrorMessage = "El nombre del país es obligatorio.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre del país debe tener entre 2 y 100 caracteres.")]
    public string nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El alias es obligatorio.")]
    [StringLength(5, MinimumLength = 2, ErrorMessage = "El alias debe tener entre 2 y 5 caracteres.")]
    public string alias { get; set; } = string.Empty;
}

// DTO de Salida/Lectura: no requiere validaciones de entrada
public record NacionalidadDto
{
    public int idNacionalidad { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string alias { get; set; } = string.Empty;
}
