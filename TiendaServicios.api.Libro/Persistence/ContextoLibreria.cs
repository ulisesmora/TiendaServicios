using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.Libro.Model;

namespace TiendaServicios.api.Libro.Persistence
{
    public class ContextoLibreria : DbContext
    {
        public ContextoLibreria() { }
        public ContextoLibreria(DbContextOptions<ContextoLibreria> options) : base(options) { }
        public virtual DbSet<LibreriaMaterial> LibreriaMaterial { get; set;}
    }
}
