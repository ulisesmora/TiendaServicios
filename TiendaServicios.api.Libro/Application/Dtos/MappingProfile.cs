using AutoMapper;
using TiendaServicios.api.Libro.Controllers;
using TiendaServicios.api.Libro.Model;

namespace TiendaServicios.api.Libro.Application.Dtos
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
