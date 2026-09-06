namespace UsuarioApi.DTOs;

public record UsuarioResponseDto(
    int Id,
    string Nombre,
    string Correo,
    string Telefono,
    bool Activo,
    DateTime FechaCreacion,
    DateTime? FechaActualizacion
);
