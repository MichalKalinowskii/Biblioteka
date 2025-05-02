using System.Reflection;
using Library.Api.Endpoints;
using Library.Api.Middlewares;
using Library.Application;
using Library.Infrastructure;
using Library.Infrastructure.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddApplication();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapEndpoints();
    
if (app.Environment.IsDevelopment())
{
    DataSeeder dataSeeder = new DataSeeder(app.Services);
    await dataSeeder.SeedDataAsync();
}

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
