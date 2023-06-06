using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.Autor.Application.Dtos;
using TiendaServicios.api.Autor.Model;
using TiendaServicios.api.Autor.Persistence;

namespace TiendaServicios.api.Autor.Application.Queries
{
    public class ConsultaFiltro
    {
        public class AutorUnique : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnique, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto,IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnique request, CancellationToken cancellationToken)
            {
                var author = await _contexto.AutorLibros.Where(x => x.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (author == null)
                {
                    throw new Exception("Can´t Find Author");
                }

                var authorDto = _mapper.Map<AutorLibro, AutorDto>(author);
                return authorDto;
            }
        }
    }
}
