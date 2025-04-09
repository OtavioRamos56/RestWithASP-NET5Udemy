using Microsoft.EntityFrameworkCore;
using RestWithASPNETErudio.Model.Context;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNETErudio.Repository.Implementations;
using RestWithASPNETErudio.Repository;
using Serilog;
using MySqlConnector;
using EvolveDb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection, new MySqlServerVersion(new Version(9, 2))));

if (builder.Environment.IsDevelopment())
{
    MigrateDataBase(connection);
}


//Versioning API

builder.Services.AddApiVersioning();

//Dependency Injection

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
builder.Services.AddScoped<IBookBusiness, BooksBusinessImplementation>();
builder.Services.AddScoped<IBooksRepository, BooksRepositoryImplementation>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
void MigrateDataBase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true
        };
        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed:", ex);
        throw;
    }
}
