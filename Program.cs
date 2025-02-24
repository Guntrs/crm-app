
using Evolve;  //biblioteca de Migraciones (1)
using Npgsql;  //proveedor de PostgreSql

using Microsoft.EntityFrameworkCore; //  EF Core


var builder = WebApplication.CreateBuilder(args);
//---------------------------------------------------------------------------------------------
//cadena de conexion del appsetting.js  (2)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//EVOLVE para manejar las migraciones (3)
using var connection = new NpgsqlConnection(connectionString);
var evolve = new Evolve.Evolve(connection, msg => Console.WriteLine(msg))
{
    Locations = new[] { "Resources/sql" },// Ruta donde está el archivo SQL
    IsEraseDisabled = true
};

//evolve.Erase(); // Limpia completamente la base de datos
//evolve.Migrate(); // Aplica las migraciones

try
{
    evolve.Migrate();
    Console.WriteLine("Migraciones aplicadas correctamente.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error al aplicar migraciones: {ex.Message}");
}

//---------------------------------------------------------------------------------------
// REGISTRAR SERVICIOS
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddDbContext<EntityDbContext>(options => options.UseNpgsql(connectionString));

//habilitar Controladores
builder.Services.AddControllers();

//Registrar El repositorio
//builder.Services.AddScoped<ICartasCategoryRepository, CartasCategoryRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapControllers();
app.Run();