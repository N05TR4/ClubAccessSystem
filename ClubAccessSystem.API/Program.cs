using ClubAccessSystem.API.Configurations;
using ClubAccessSystem.API.Extension;
using ClubAccessSystem.API.Extension.Jwt;
using ClubAccessSystem.API.Logging;
using ClubAccessSystem.API.Service;
using ClubAccessSystem.IOC.Dependencies.Configuration;
using ClubAccessSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Db Connection
var environment = builder.Environment;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ClubContext>(options =>
{

    options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString));

    if (environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging(); // Solo en desarrollo
    }

});

// Dependency Injection
builder.Services.AddConfigurationDependency();
builder.Services.AddScoped<IAuthService, AuthService>();

//Automapper
builder.Services.AddAutoMapper(typeof(Program));

//JWT Config
builder.Services.AddJwtAuthentication(builder.Configuration);


//Swagger Config
builder.Services.AddSwaggerConfiguration();

//Cors
builder.Services.AddCorsConfiguration();


//logging
CustomLogger.ConfigureLogging(builder.Logging);

var app = builder.Build();

//Middleware Exception Manager
app.UseCustomExceptionHandler();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}


app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
