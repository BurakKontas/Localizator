using Localizator.Shared.Result;
using Microsoft.AspNetCore.Http;

namespace Localizator.Shared.Providers;

public static class MetaProvider
{
    private const string MetaKey = "Meta_Request_Context";
    private static readonly AsyncLocal<HttpContext?> _httpContextHolder = new();

    public static void SetContext(HttpContext context)
    {
        _httpContextHolder.Value = context;
    }

    public static void Set(Meta meta)
    {
        if (meta != null)
        {
            var context = _httpContextHolder.Value;
            if (context != null)
            {
                context.Items[MetaKey] = meta;
            }
        }
    }

    public static Meta Get()
    {
        var context = _httpContextHolder.Value;
        if (context != null && context.Items.TryGetValue(MetaKey, out var meta))
        {
            return (Meta)meta!;
        }

        var newMeta = Meta.Auto();
        Set(newMeta);
        return newMeta;
    }

    public static Meta GetFromContext(HttpContext context)
    {
        if (context != null && context.Items.TryGetValue(MetaKey, out var meta))
        {
            return (Meta)meta!;
        }

        var newMeta = Meta.Auto();
        if (context != null)
        {
            context.Items[MetaKey] = newMeta;
        }
        return newMeta;
    }

    public static void Clear()
    {
        var context = _httpContextHolder.Value;
        if (context != null)
        {
            context.Items.Remove(MetaKey);
        }
    }

    public static Meta Initialize(HttpContext context)
    {
        SetContext(context);
        var meta = Meta.Auto();
        Set(meta);
        return meta;
    }
}