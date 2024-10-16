using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); // Cadastramos os controladores
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cadastro de IEmpacotamentoService
builder.Services.AddScoped<IEmpacotamentoService, EmpacotamentoService>();

var app = builder.Build();


// Configure o pipeline de requisições HTTP
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = "swagger"; // Ruta donde se accede a Swagger UI
    });
}


if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers(); // Isto permitirá o uso de controladores como PedidosController

app.Run();