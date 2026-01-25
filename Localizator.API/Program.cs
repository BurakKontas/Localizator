using Localizator.API.Extensions;
using Localizator.API.Middlewares;
using Localizator.Auth.Application;
using Localizator.Auth.Infrastructure;
using Localizator.Shared.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AppConfig.Initialize(builder.Configuration);

// Add Localization services
builder.Services.AddLocalization(options => options.ResourcesPath = "Localizator.Shared/Resources");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddAuthInfrastructure(builder.Configuration);
builder.Services.AddAuthApplication();

var app = builder.Build();

app.AddLocalization();

app.UseMiddleware<RequestTimingMiddleware>();
app.UseMiddleware<MetaMiddleware>();
app.UseMiddleware<AuthorizationResponseMiddleware>();

await app.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
