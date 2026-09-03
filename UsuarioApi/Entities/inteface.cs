using UsuarioApi.Entities; 
 
namespace UsuarioApi.Interfaces; 
 
public interface IUsuarioRepository 
{ 
    Task<List<Usuario>> ObtenerTodosAsync(); 
    Task<Usuario?> ObtenerPorIdAsync(int id); 
    Task<Usuario?> ObtenerPorCorreoAsync(string correo); 
    Task<Usuario> CrearAsync(Usuario usuario); 
    Task ActualizarAsync(Usuario usuario); 
    Task EliminarAsync(Usuario usuario); 
} 
