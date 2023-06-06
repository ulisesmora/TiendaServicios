using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.api.Libro.Application.Dtos;
using TiendaServicios.api.Libro.Model;

namespace TiendaServicios.api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest() { 
            CreateMap<LibreriaMaterial,LibroMaterialDto>();
        }
    }
}
