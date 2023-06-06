using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Application;
using TiendaServicios.api.Libro.Application.Dtos;
using TiendaServicios.api.Libro.Controllers;
using TiendaServicios.api.Libro.Model;
using TiendaServicios.api.Libro.Persistence;

namespace TiendaServicios.api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }
        
        private Mock<ContextoLibreria> CrearContexto()
        {
            var dataPruebas = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();
            
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPruebas.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPruebas.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPruebas.ElementType); 
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPruebas.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>().Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsymcEmumerator<LibreriaMaterial>(dataPruebas.GetEnumerator()));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);
            return contexto;


        }

        [Fact]
        public async void GetLibros()
        {
            System.Diagnostics.Debugger.Launch();
            // que metodo dentro de microservice se esta encargando de realizar la consulta de libros de la base de datos ???
            // Emular a la instancia de entity framework core
            // 1.- emular las acciones y eventos de un objeto en un ambiente en unit test
            // utilizamos objetos tipos mock
            var mockContexto = CrearContexto();


            // 2.- Emular al mapping IMapper
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mapConfig.CreateMapper();

            //3.- Instanciar a la clase Manejador y pasarle como parametros los mocks que he creado
            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object,mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();
            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(lista.Any());


        }
    }
}
