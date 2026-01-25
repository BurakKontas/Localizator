using Localizator.Shared.Providers;
using Localizator.Shared.Result;

namespace Localizator.API.Middlewares;

public class AuthorizationResponseMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Items.TryGetValue("AuthResult", out var value) && value is Result<bool> result)
        {
            context.Items.Remove("AuthResult");
            result.Meta = MetaProvider.Get();
            await context.Response.WriteAsJsonAsync(result);
        }
    }
}

