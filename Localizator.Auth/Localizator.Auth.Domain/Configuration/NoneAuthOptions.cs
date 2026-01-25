using Localizator.Auth.Domain.Configuration.Mode;
using Localizator.Auth.Domain.Interfaces.Configuration;

namespace Localizator.Auth.Domain.Configuration;

public sealed class NoneAuthOptions : INoneAuthOptions
{
    public AuthMode Mode => AuthMode.None;

    public override string ToString()
    {
        return $"Mode: {Mode}";
    }
}
