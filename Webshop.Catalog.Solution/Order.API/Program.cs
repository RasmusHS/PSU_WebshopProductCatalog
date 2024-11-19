using Order.API.Utilities;
using Order.Application;
using Order.Infrastructure;
using Order.Persistence;
using Rebus.Config;
using Rebus.Routing.TypeBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// åben Package Manager Console
// Add-Migration
// Name: Initial
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();