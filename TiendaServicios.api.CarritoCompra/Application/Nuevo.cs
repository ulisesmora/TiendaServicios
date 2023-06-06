using MediatR;
using TiendaServicios.api.CarritoCompra.Model;
using TiendaServicios.api.CarritoCompra.Persistence;

namespace TiendaServicios.api.CarritoCompra.Application
{
    public class Nuevo
    {
        public class Ejecuta:IRequest
        {
            public DateTime FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CarritoContexto _contexto;
            public Manejador(CarritoContexto contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };
                _contexto.CarritoSesion.Add(carritoSesion);
               var value = await _contexto.SaveChangesAsync();
                if(value==0)
                {
                    throw new Exception("Can't add new items in car");
                }

                int id = carritoSesion.CarritoSesionId;
                foreach(var obj in request.ProductoLista)
                {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = obj
                    };
                    _contexto.CarritoSesionDetalle.Add(detalleSesion);

                }
              
                value = await _contexto.SaveChangesAsync();

                if(value>0)
                {
                    return Unit.Value;
                }
                throw new Exception("Can't insert items");

            }
        }
    }
}
