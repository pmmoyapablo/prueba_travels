using Microsoft.AspNetCore.Mvc;
using Infraestructura;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Aplicacion;
using Dominio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorioEF>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorioEF>();
builder.Services.AddScoped<IMedioPagoRepositorio, MedioPagoRepositorioEF>();
builder.Services.AddScoped<IVehiculoRepositorio, VehiculoRepositorioEF>();
builder.Services.AddScoped<AplicacionMain, AplicacionMain>();
//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "V-1.0.0",
          Title = "API TRAVEL - DEV",
          Description = "Un ejemplo simple ASP.NET Core Web API",
          TermsOfService = new Uri("https://deti.com/licence"),
          Contact = new OpenApiContact
          {
            Name = "Pablo Moya",
            Email = "pmmoyapablo@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/pablo-moya-30902278/")
          },
          License = new OpenApiLicense
          {
            Name = "Uso Open Source LICX",
            Url = new Uri("https://deti.com/licence")
          }
         }
        );
       });

var stringConection = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("MilerRentaCars"));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API Ejemplo V1");
      });
}

app.MapGet("/vehiculos", (AplicacionMain aplicacion)=>{

  var response = aplicacion.ListarVehiculos();
   
  if(!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.MapGet("/reservas/{estatus}", (AplicacionMain aplicacion, [FromRoute] int estatus) => {

  var response = aplicacion.ListarAlgunasReservas(estatus);

  if (!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.MapGet("/itinerarios/{clienteId}/{desde}/{hasta}", (AplicacionMain aplicacion, [FromRoute] string clienteId, [FromRoute] string desde, [FromRoute] string hasta) =>{

  var response = aplicacion.ConsultarItinerarios(clienteId, Convert.ToDateTime(desde), Convert.ToDateTime(hasta));
   
  if(!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});


app.MapPost("/cliente", (AplicacionMain aplicacion, [FromBody] ClienteDto dto )=>{
 
  var response = aplicacion.ConsignarDatosCliente(dto);
   
  if(!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.MapPost("/preferencias", (AplicacionMain aplicacion, [FromBody] PreferenciasDto dto) => {

  var response = aplicacion.DefinirPreferencias(dto);

  if (!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.MapPost("/reserva", (AplicacionMain aplicacion, [FromBody] ReservaDto dto) => {

  var response = aplicacion.ReservarVehiculo(dto);

  if (!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.MapPut("/reserva/{id}", (AplicacionMain aplicacion, [FromRoute] string id,  [FromBody] int  newEstatus) => {

  var response = aplicacion.ActualizarReserva(id, newEstatus);

  if (!response.IsValida)
    return Results.NotFound(response);

  return Results.Ok(response);
});

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
