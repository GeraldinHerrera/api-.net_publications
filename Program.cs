using Microsoft.EntityFrameworkCore;
using PresentadorDotNet.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar puerto 5000
builder.WebHost.UseUrls("http://0.0.0.0:5000");

// Cadena de conexión PostgreSQL
var connectionString = Environment.GetEnvironmentVariable("POSTGRESQL_CONNECTION_STRING") 
                       ?? builder.Configuration.GetConnectionString("PostgreSQL");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.MapGet("/", () => Results.Ok(new
{
    microservicio = "presentador_dotnet",
    tecnologia = ".NET 8 Web API (Clean Controllers)",
    estado = "operativo"
}));

app.Run();
