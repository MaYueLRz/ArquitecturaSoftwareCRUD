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

    public async Task<(List<Usuario> Items, int TotalRegistros)> BuscarAsync(string? buscar, int pagina, int tamanoPagina)
    {
        var consulta = _context.Usuarios.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            var termino = buscar.Trim();
            consulta = consulta.Where(x =>
                x.Nombre.Contains(termino) ||
                x.Correo.Contains(termino));
        }

        // Se cuenta antes de paginar para saber cuantos registros hay en total.
        var totalRegistros = await consulta.CountAsync();

        var items = await consulta
            .OrderBy(x => x.Id)
            .Skip((pagina - 1) * tamanoPagina)
            .Take(tamanoPagina)
            .ToListAsync();

        return (items, totalRegistros);
    }

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
