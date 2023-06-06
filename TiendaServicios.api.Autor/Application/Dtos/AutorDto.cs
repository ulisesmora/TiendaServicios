using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application.Dtos
{
    public class AutorDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
