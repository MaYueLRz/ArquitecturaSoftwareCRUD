using Microsoft.EntityFrameworkCore;
using UsuarioApi.Data;
using UsuarioApi.Entities;
using UsuarioApi.Interfaces;

namespace UsuarioApi.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Usuario>> ObtenerTodosAsync() =>
        _context.Usuarios.AsNoTracking().ToListAsync();

    public Task<Usuario?> ObtenerPorIdAsync(int id) =>
        _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

    public Task<Usuario?> ObtenerPorCorreoAsync(string correo) =>
        _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == correo);

    public async Task<Usuario> CrearAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task ActualizarAsync(Usuario usuario)
    {
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(Usuario usuario)
    {
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
    }
}