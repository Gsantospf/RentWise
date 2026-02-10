using Microsoft.EntityFrameworkCore;
using RentWise.Core.Interfaces;
using RentWise.Infrastructure.Context;
using RentWise.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RentWiseDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; // Isso faz o Swagger abrir direto na raiz (localhost:PORTA/)
    });

    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization(); 
app.MapControllers();


app.Run();