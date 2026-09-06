using UsuarioApi.DTOs;
using UsuarioApi.Entities;
using UsuarioApi.Interfaces;

namespace UsuarioApi.Services;

public class UsuarioService : IUsuarioService
{
    private const int TamanoPaginaPorDefecto = 10;
    private const int TamanoPaginaMaximo = 50;

    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultadoPaginadoDto<UsuarioResponseDto>> ObtenerTodosAsync(string? buscar, int pagina, int tamanoPagina)
    {
        // Reglas de paginacion: nunca pedir una pagina menor a 1 ni traer mas de 50 registros.
        pagina = pagina < 1 ? 1 : pagina;
        tamanoPagina = tamanoPagina < 1 ? TamanoPaginaPorDefecto : tamanoPagina;
        tamanoPagina = tamanoPagina > TamanoPaginaMaximo ? TamanoPaginaMaximo : tamanoPagina;

        var (usuarios, totalRegistros) = await _repository.BuscarAsync(buscar, pagina, tamanoPagina);
        var totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanoPagina);

        return new ResultadoPaginadoDto<UsuarioResponseDto>(
            usuarios.Select(Mapear).ToList(),
            pagina,
            tamanoPagina,
            totalRegistros,
            totalPaginas);
    }

    public async Task<UsuarioResponseDto?> ObtenerPorIdAsync(int id)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);
        return usuario is null ? null : Mapear(usuario);
    }

    public async Task<UsuarioResponseDto> CrearAsync(CrearUsuarioDto dto)
    {
        var existente = await _repository.ObtenerPorCorreoAsync(dto.Correo);
        if (existente is not null)
            throw new InvalidOperationException("El correo ya está registrado.");

        var usuario = new Usuario
        {
            Nombre = dto.Nombre.Trim(),
            Correo = dto.Correo.Trim().ToLower(),
            Telefono = dto.Telefono.Trim()
        };

        await _repository.CrearAsync(usuario);
        return Mapear(usuario);
    }

    public async Task<bool> ActualizarAsync(int id, ActualizarUsuarioDto dto)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);
        if (usuario is null) return false;

        usuario.Nombre = dto.Nombre.Trim();
        usuario.Correo = dto.Correo.Trim().ToLower();
        usuario.Telefono = dto.Telefono.Trim();
        usuario.Activo = dto.Activo;

        // Auditoria: se sella la fecha en cada actualizacion efectiva.
        usuario.FechaActualizacion = DateTime.UtcNow;

        await _repository.ActualizarAsync(usuario);
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var usuario = await _repository.ObtenerPorIdAsync(id);
        if (usuario is null) return false;

        await _repository.EliminarAsync(usuario);
        return true;
    }

    private static UsuarioResponseDto Mapear(Usuario u) =>
        new(u.Id, u.Nombre, u.Correo, u.Telefono, u.Activo, u.FechaCreacion, u.FechaActualizacion);
}
