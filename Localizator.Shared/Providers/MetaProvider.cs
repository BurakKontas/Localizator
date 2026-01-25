using Localizator.Shared.Result;

namespace Localizator.Shared.Providers;

public static class MetaProvider
{
    private static readonly AsyncLocal<Meta?> _metaHolder = new();

    public static void Set(Meta meta)
    {
        if (meta != null)
            _metaHolder.Value = meta;
    }

    public static Meta Get()
    {
        _metaHolder.Value ??= Meta.Auto();
        return _metaHolder.Value;
    }

    public static void Clear() => _metaHolder.Value = null;

    public static Meta Initialize()
    {
        var meta = Meta.Auto();
        _metaHolder.Value = meta;
        return meta;
    }
}
