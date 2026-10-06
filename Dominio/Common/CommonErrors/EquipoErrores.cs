namespace Dominio.Common.CommonErrors;

public static class EquipoErrores                                                                                                                                                  
{                                                                                                                                                                                  
    public static Error NoEncontrado(int id) => Error.NotFound("Equipo.NoEncontrado", $"No existe el equipo con id {id}.");                                                        
    public static readonly Error NombreDuplicado = Error.Conflict("Equipo.NombreDuplicado", "Ya existe un equipo con ese nombre.");                                                
    public static readonly Error CupoCompleto = Error.Conflict("Equipo.CupoCompleto", "El equipo ya alcanzó el límite máximo de 22 jugadores.");                                   
    public static readonly Error JugadorYaEnEquipo = Error.Conflict("Equipo.JugadorYaEnEquipo", "El jugador ya forma parte de un equipo.");                                        
    public static readonly Error JugadorNoPertenece = Error.NotFound("Equipo.JugadorNoPertenece", "El jugador no pertenece a este equipo.");                                       
    public static readonly Error EstadoInvalido = Error.Validation("Equipo.EstadoInvalido", "Valores permitidos: AGREGAR, ELIMINAR.");                                             
} 