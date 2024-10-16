using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using LojaSeuManoel.Application.Interfaces;
using LojaSeuManoel.Domain.Services;
using LojaSeuManoel.Application.Services;

var builder = WebApplication.CreateBuilder(args);


// Configuración de la autenticación JWT
// var key = Encoding.ASCII.GetBytes("f8D&3j$kB!z@7Q^nP$e*1Yw%8WqM#2bT");
// var key = Encoding.UTF8.GetBytes("f8D&3j$kB!z@7Q^nP$e*1Yw%8WqM#2bT");

var key = Encoding.UTF8.GetBytes("f8D&3j$kB!z@7Q^nP$e*1Yw%8WqM#2bT");
var symmetricKey = new SymmetricSecurityKey(key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = symmetricKey,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.RequireHttpsMetadata = true;
//     options.SaveToken = true;
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuerSigningKey = true,
//         IssuerSigningKey = new SymmetricSecurityKey(key),
//         ValidateIssuer = false,
//         ValidateAudience = false
//     };
// });


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmpacotamentoService, EmpacotamentoService>();
builder.Services.AddScoped<IAuthService, AuthService>();

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

// Añadir el middleware de autenticación y autorización
app.UseAuthentication(); // Asegúrate de que esté antes de UseAuthorization
app.UseAuthorization();

app.MapControllers(); // Isto permitirá o uso de controladores como PedidosController

app.Run();