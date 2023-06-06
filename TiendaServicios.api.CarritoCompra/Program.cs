using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaServicios.api.CarritoCompra.Application;
using TiendaServicios.api.CarritoCompra.Persistence;
using TiendaServicios.api.CarritoCompra.RemoteInterface;
using TiendaServicios.api.CarritoCompra.RemoteService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILibrosService,LibrosService>();
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("ConexionDatabase");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 19));
builder.Services.AddDbContext<CarritoContexto>(options =>
{
    options.UseMySql(connectionString,serverVersion);
});

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
