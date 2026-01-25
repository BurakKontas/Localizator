using Localizator.Auth.Domain.Configuration.Mode;
using Localizator.Auth.Domain.Interfaces.Configuration;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Localizator.Auth.Infrastructure.Strategies.Abstract;
using Localizator.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Localizator.Auth.Infrastructure.Strategies;

public sealed class ApiKeyAuthStrategy(IAuthOptionsProvider provider, ILogger<ApiKeyAuthStrategy> logger) : AuthStrategyBase<IApiKeyAuthOptions>(provider)
{
    private readonly ILogger<ApiKeyAuthStrategy> _logger = logger;

    public override async Task<Result<bool>> AuthenticateAsync(HttpContext context, CancellationToken ct = default)
    {
        // TODO:
        // - read API key from header
        // - scope validation

        _logger.LogInformation("ApiKey authentication strategy invoked.");
        _logger.LogInformation(Options.ToString());

        return Result<bool>.Success();
    }
}
