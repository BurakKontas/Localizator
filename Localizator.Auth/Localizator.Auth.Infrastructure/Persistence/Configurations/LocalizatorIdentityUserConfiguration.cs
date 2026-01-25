using Localizator.Auth.Domain.Identity;
using Localizator.Auth.Domain.Configuration.Mode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Localizator.Auth.Infrastructure.Persistence.Configurations;

public class LocalizatorIdentityUserConfiguration : IEntityTypeConfiguration<LocalizatorIdentityUser>
{
    public void Configure(EntityTypeBuilder<LocalizatorIdentityUser> builder)
    {
        builder.Property(u => u.Mode)
               .HasConversion(new EnumToStringConverter<AuthMode>())
               .HasMaxLength(10)
               .IsRequired();
    }
}
