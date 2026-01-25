using Localizator.Auth.Domain.Configuration.Mode;

namespace Localizator.Auth.Domain.Interfaces.Configuration;

public interface IAuthOptions
{
    AuthMode Mode { get; }
}
