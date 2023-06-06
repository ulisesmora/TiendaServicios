using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.CarritoCompra.Application.Dtos;
using TiendaServicios.api.CarritoCompra.Persistence;
using TiendaServicios.api.CarritoCompra.RemoteInterface;

namespace TiendaServicios.api.CarritoCompra.Application
{
    public class Consulta
    {
        public class Ejecuta : IRequest<CarritoDto> {
            public int CarritoSesionId { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly CarritoContexto _contexto;
            private readonly ILibrosService _librosService;

            public Manejador(CarritoContexto contexto, ILibrosService librosService)
            {
                _contexto = contexto;
                _librosService = librosService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _contexto.CarritoSesion.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _contexto.CarritoSesionDetalle.Where(x => x.CarritoSesionId == request.CarritoSesionId).ToListAsync();
                var listaCarrito = new List<CarritoDetalleDto>();
                foreach(var libro in carritoSesionDetalle)
                {
                    var response = await _librosService.GetLibro(new Guid(libro.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.libro; ;
                        var carritoDetalle = new CarritoDetalleDto{
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };
                        listaCarrito.Add(carritoDetalle);
                    }
                }
                var carritoSesionDto = new CarritoDto
                {
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    ListaProductos = listaCarrito
                };
                return carritoSesionDto;
            }
        }
    }
}
