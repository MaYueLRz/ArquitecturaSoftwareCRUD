using UsuarioApi.Entities;

namespace UsuarioApi.Interfaces;

public interface IUsuarioRepository
{
    /// <summary>
    /// Devuelve una pagina de usuarios y el total de registros que cumplen el filtro.
    /// Si <paramref name="buscar"/> es null o vacio no se aplica filtro.
    /// </summary>
    Task<(List<Usuario> Items, int TotalRegistros)> BuscarAsync(string? buscar, int pagina, int tamanoPagina);

    Task<Usuario?> ObtenerPorIdAsync(int id);
    Task<Usuario?> ObtenerPorCorreoAsync(string correo);
    Task<Usuario> CrearAsync(Usuario usuario);
    Task ActualizarAsync(Usuario usuario);
    Task EliminarAsync(Usuario usuario);
}
