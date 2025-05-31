
using System.Threading.RateLimiting;
using crm_app.Repositories.Establishment;
using crm_app.Repositories.Person;
using crm_app.Repositories.Team;
using crm_app.Repositories.Typologies;
using crm_app.Repositories.TypologyRepository;
using crm_app.Repositories.User;
using crm_app.Repositories.UserTeam;
using crm_app.Utils;
using crm_app.Security;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddDbContext<EntityDbContext>(options => options.UseNpgsql(connectionString));

//habilitar Controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
    //para valores nulos en la salida json 05/25
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault;
});

// Jwt
builder.Services.AddSingleton(sp =>
    new CrmJwtService(
        builder.Configuration["Jwt:Key"],
        builder.Configuration["Jwt:Issuer"]
    ));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"], // Usa el mismo valor si solo tienes Issuer
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


//-------------------------------------------------
//Registrar El repositorio
builder.Services.AddScoped<ICrmTypologyRepository, CrmTypologyRepository>();
builder.Services.AddScoped<ICrmTeamRepository, CrmTeamRepository>();
builder.Services.AddScoped<ICrmEstablishmentRepository, CrmEstablishmentRepository>();
builder.Services.AddScoped<ICrmPersonRepository, CrmPersonRepository>();
builder.Services.AddScoped<ICrmUserRepository, CrmUserRepository>();
builder.Services.AddScoped<ICrmUserTeamRepository, CrmUserTeamRepository>();

//------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//habilitar cors
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(
                    "http://localhost:4200", // frontend Angular
                    "https://localhost:4200" // si usas https en Angular
                )
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();


//habilitar cors
app.UseCors(MyAllowSpecificOrigins);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();