using System.Text.Json;
using TiendaServicios.api.CarritoCompra.RemoteInterface;
using TiendaServicios.api.CarritoCompra.RemoteModel;

namespace TiendaServicios.api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibrosService
    {
        private readonly IHttpClientFactory _httpClient;
        private readonly ILogger<LibrosService> _logger;
        public LibrosService(IHttpClientFactory httpClientFactory, ILogger<LibrosService> logger)
        {
            _httpClient = httpClientFactory;
            _logger = logger;
        }
        public async Task<(bool resultado, LibroRemote libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try {
                var client = _httpClient.CreateClient("Libros");
               var response = await client.GetAsync($"api/LibroMaterial/{LibroId}");
                if(response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, result, null);
                 }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
