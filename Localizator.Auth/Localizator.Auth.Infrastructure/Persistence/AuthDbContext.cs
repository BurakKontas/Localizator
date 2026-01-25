using Localizator.Auth.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Localizator.Auth.Infrastructure.Persistence;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<LocalizatorIdentityUser>(options) 
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

        base.OnModelCreating(builder);
    }
}

// dotnet ef migrations add InitialCreate --project Localizator.Auth\Localizator.Auth.Infrastructure  --startup-project Localizator.API -c AuthDbContext -o .\Localizator.Auth\Localizator.Auth.Infrastructure\Persistence\Migrations