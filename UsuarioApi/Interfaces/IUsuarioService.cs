using UsuarioApi.DTOs;

namespace UsuarioApi.Interfaces;

public interface IUsuarioService
{
    Task<List<UsuarioResponseDto>> ObtenerTodosAsync();
    Task<UsuarioResponseDto?> ObtenerPorIdAsync(int id);
    Task<UsuarioResponseDto> CrearAsync(CrearUsuarioDto dto);
    Task<bool> ActualizarAsync(int id, ActualizarUsuarioDto dto);
    Task<bool> EliminarAsync(int id);
}
