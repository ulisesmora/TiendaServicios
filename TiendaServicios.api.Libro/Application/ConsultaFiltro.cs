using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.Libro.Application.Dtos;
using TiendaServicios.api.Libro.Model;
using TiendaServicios.api.Libro.Persistence;

namespace TiendaServicios.api.Libro.Application
{
    public class ConsultaFiltro
    {
        public class LibroUnico : MediatR.IRequest<LibroMaterialDto>
        {
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibroMaterialDto>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<LibroMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _contexto.LibreriaMaterial.Where(x => x.LibreriaMaterialId == request.LibroId).FirstOrDefaultAsync();
                if (libro == null)
                {
                    throw new Exception("Can´t find book");
                }
                var libroDto = _mapper.Map<LibreriaMaterial,LibroMaterialDto>(libro);
                return libroDto;
            }
        }
    }
}
