using Localizator.Auth.Application;
using Localizator.Auth.Application.Interfaces.Validators;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Localizator.Auth.Infrastructure;
using Localizator.Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthInfrastructure(builder.Configuration);
builder.Services.AddAuthApplication();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    dbContext.Database.Migrate();

    var optionsFactory = scope.ServiceProvider.GetRequiredService<IAuthOptionsProvider>();
    var validator = scope.ServiceProvider.GetRequiredService<IAuthOptionsValidatorResolver>();

    var options = optionsFactory.Get();
    validator.Validate(options);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
