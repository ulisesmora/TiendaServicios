using TiendaServicios.api.CarritoCompra.RemoteModel;

namespace TiendaServicios.api.CarritoCompra.RemoteInterface
{
    public interface ILibrosService
    {
        Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
