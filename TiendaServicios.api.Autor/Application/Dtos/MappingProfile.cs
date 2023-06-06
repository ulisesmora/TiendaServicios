using AutoMapper;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Application.Dtos
{
    public class MappingProfile : Profile
    {
        public MappingProfile() { 
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
