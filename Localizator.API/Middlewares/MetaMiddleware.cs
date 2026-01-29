using Localizator.Shared.Providers;
using Localizator.Shared.Result;
using System.Diagnostics;

namespace Localizator.API.Middlewares;

public class MetaMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // initialize meta to ensure requestId is set
        Meta meta = MetaProvider.Initialize(context);

        await _next(context);
    }
}
