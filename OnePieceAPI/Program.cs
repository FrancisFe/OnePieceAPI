using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Exceptions;
using OnePieceAPI.Profiles;
using OnePieceAPI.Repositories;
using OnePieceAPI.Repositories.Interfaces;
using OnePieceAPI.Services;
using OnePieceAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
/*
Configuraci�n de servicios
 */

//Controladores
builder.Services.AddControllers();

//Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Automapper
builder.Services.AddAutoMapper(typeof(PirataProfile));

//Repositorios
builder.Services.AddScoped<IPirataRepository, PirataRepository>();
builder.Services.AddScoped<IFrutaDelDiabloRepository, FrutaDelDiabloRepository>();
builder.Services.AddScoped<ITripulacionRepository, TripulacionRepository>();

//Services
builder.Services.AddScoped<ITripulacionService, TripulacionService>();
builder.Services.AddScoped<IPirataService, PirataService>();


//DbContext (Conexion a la base de datos)
builder.Services.AddDbContext<OnePieceContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

//Middleware y entorno
app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

/*
 Seed Data
 */
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OnePieceContext>();
    SeedData.Initialize(context);
}



app.Run();
