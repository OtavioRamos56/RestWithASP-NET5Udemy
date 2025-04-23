using Microsoft.EntityFrameworkCore;
using RestWithASPNETErudio.Model.Context;
using RestWithASPNETErudio.Business;
using RestWithASPNETErudio.Business.Implementations;
using Serilog;
using MySqlConnector;
using EvolveDb;
using RestWithASPNETErudio.Repository.Generic;
using RestWithASPNETErudio.HyperMedia.Enricher;
using Microsoft.AspNetCore.Rewrite;
using RestWithASPNETErudio.HyperMedia.Filters;

var builder = WebApplication.CreateBuilder(args);
var appName = "Rest API's RESTful From 0 to zure with ASP.NET Core 8 and Docker";
var appVersion = "v1";
var appDescription = $"REST API RESTful developed in course '{appName}'";

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(appVersion,
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = appDescription,
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Otavio Ramos",
                Url = new Uri("https://github.com/OtavioRamos56")
            }
        });
});

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection, new MySqlServerVersion(new Version(9, 2))));

if (builder.Environment.IsDevelopment())
{
    MigrateDataBase(connection);
}

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
    .AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());

builder.Services.AddSingleton(filterOptions);

//Versioning API

builder.Services.AddApiVersioning();

//Dependency Injection

builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookBusiness, BooksBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();

app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json",
        $"{appName} - {appVersion}");

});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute("DefaltApi", "{controller=values}/v{version=apiVersion}/{id?}");

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
