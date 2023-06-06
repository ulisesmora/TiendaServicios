using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Persistence
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options): base(options) { }

        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradoAcademico { get; set; }
    }
}
