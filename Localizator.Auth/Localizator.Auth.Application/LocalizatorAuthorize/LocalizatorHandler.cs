using Localizator.Auth.Domain.Configuration.Mode;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Localizator.Auth.Application.LocalizatorAuthorize;

public sealed class LocalizatorHandler(IAuthStrategy authStrategy) : AuthorizationHandler<LocalizatorRequirement>
{
    private readonly IAuthStrategy _authStrategy = authStrategy;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, LocalizatorRequirement requirement)
    {
        if (context.Resource is HttpContext httpContext)
        {
            bool isSuccess = await _authStrategy.AuthenticateAsync(httpContext);
            
            if(isSuccess)
            {
                context.Succeed(requirement);
            } 
            else
            {
                context.Fail();
            }
            return;
        }

        context.Fail();
    }
}
