using Microsoft.AspNetCore.Authorization;

namespace Localizator.Auth.Application.LocalizatorAuthorize;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class LocalizatorAuthorizeAttribute : AuthorizeAttribute
{
    public LocalizatorAuthorizeAttribute()
    {
        Policy = "LocalizatorPolicy";
    }
}
