namespace Dominio.Common.CommonErrors;

public static class JugadorErrores
{
    public static Error NoEncontrado(int id) => Error.NotFound("Jugador.NoEncontrado", $"No existe el jugador con id {id}.");
    public static readonly Error NombreDuplicado = Error.Conflict("Jugador.NombreDuplicado", "Ya existe un jugador con ese nombre y apellido.");
}
