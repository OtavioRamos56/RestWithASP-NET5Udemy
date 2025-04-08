using Microsoft.EntityFrameworkCore;
using RestWithASPNETErudio.Model.Context;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using RestWithASPNETErudio.Repository.Implementations;
using RestWithASPNETErudio.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection, new MySqlServerVersion(new Version(9, 2))));
//Versioning API

builder.Services.AddApiVersioning();

//Dependency Injection

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
