using Localizator.Shared.Config;

namespace Localizator.Shared.Result;

public class RateLimit(int limit, int remaining, DateTime resetAt)
{
    public int Limit { get; set; } = limit;
    public int Remaining { get; set; } = remaining;
    public DateTime ResetAt { get; set; } = resetAt;

    public static RateLimit Auto(int remaining, DateTime resetAt)
    {
        return new RateLimit(AppConfig.DefaultRateLimit, remaining, resetAt);
    }

    public static RateLimit Empty()
    {
        return new RateLimit(AppConfig.DefaultRateLimit, AppConfig.DefaultRateLimit - 1, DateTime.Now);
    }
}
