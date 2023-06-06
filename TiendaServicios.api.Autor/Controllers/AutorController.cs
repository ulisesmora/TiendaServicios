using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TiendaServicios.api.Autor.Application.Commands;
using TiendaServicios.api.Autor.Application.Dtos;
using TiendaServicios.api.Autor.Application.Queries;
using TiendaServicios.api.Autor.Model;

namespace TiendaServicios.api.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Nuevo.Ejecuta data ) {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAuthors()
        {
            return await _mediator.Send(new Consulta.ListaAutor());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAuthorById(string id)
        {
            return await _mediator.Send(new ConsultaFiltro.AutorUnique { AutorGuid = id});
        }
    }
}
