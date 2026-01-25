using Localizator.Auth.Domain.Configuration.Mode;
using Microsoft.AspNetCore.Identity;

namespace Localizator.Auth.Domain.Identity;

public class LocalizatorIdentityUser : IdentityUser
{
    public LocalizatorIdentityUser(string username, AuthMode mode) : base(username)
    {
        Mode = mode;
    }

    // For EF Core
    protected LocalizatorIdentityUser() { }

    public AuthMode Mode { get; set; }
}
