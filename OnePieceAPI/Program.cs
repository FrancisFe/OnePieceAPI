using Microsoft.EntityFrameworkCore;
using OnePieceAPI.Data;
using OnePieceAPI.Profiles;
using OnePieceAPI.Services;
using OnePieceAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PirataProfile));


builder.Services.AddScoped<IPirataService, PirataService>();
builder.Services.AddScoped<IFrutaDelDiabloService, FrutaDelDiabloService>();


builder.Services.AddDbContext<OnePieceContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OnePieceContext>();
    SeedData.Initialize(context);
}
app.Run();
