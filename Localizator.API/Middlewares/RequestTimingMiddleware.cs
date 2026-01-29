using Localizator.Shared.Providers;
using Localizator.Shared.Result;
using System.Diagnostics;

namespace Localizator.API.Middlewares;

public class RequestTimingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        Meta meta = MetaProvider.Get();

        await _next(context);

        stopwatch.Stop();

        TimeSpan ts = stopwatch.Elapsed;

        int ms = ts.Milliseconds;
        int us = (int)(ts.Ticks % TimeSpan.TicksPerMillisecond / 10);

        string result = $"{ts:hh\\:mm\\:ss}.{ms:000} {us:000}";

        meta.Duration = result;
    }
}
