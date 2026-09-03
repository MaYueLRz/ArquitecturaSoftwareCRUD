namespace UsuarioApi.DTOs;

public record ActualizarUsuarioDto(
    string Nombre,
    string Correo,
    string Telefono,
    bool Activo
);
