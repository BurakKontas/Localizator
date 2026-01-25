namespace Localizator.Auth.Domain.Configuration.Mode;

public enum AuthMode
{
    Oidc,
    Local,
    Header,
    ApiKey,
    Hybrid,
    None
}