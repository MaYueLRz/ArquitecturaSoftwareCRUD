using Micosoft.EntityFrameworkCore;
using UsuariosApi.Entites;

namespace UsuariosApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; } = > Set<Usuario>();
    }
}