using Localizator.Shared.Extensions;
using Microsoft.Extensions.Configuration;

namespace Localizator.Shared.Config;

public static class AppConfig
{
    public static string ApiVersion { get; private set; } = "0.0.0";
    public static int DefaultRateLimit { get; private set; } = 0;

    public static void Initialize(IConfiguration configuration)
    {
        var section = configuration.GetSection("AppConfig");
        ApiVersion = section["ApiVersion"] ?? "0.0.0";
        DefaultRateLimit = section["DefaultRateLimit"].AsInt();
    }
}
