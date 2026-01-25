using Localizator.Auth.Domain.Configuration.Mode;
using Localizator.Auth.Domain.Identity;
using Localizator.Auth.Domain.Interfaces.Configuration;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Localizator.Auth.Infrastructure.Strategies.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Localizator.Auth.Infrastructure.Strategies;

public sealed class LocalAuthStrategy(
    IAuthOptionsProvider provider,
    ILogger<LocalAuthStrategy> logger,
    SignInManager<LocalizatorIdentityUser> signInManager,
    UserManager<LocalizatorIdentityUser> userManager) : AuthStrategyBase<ILocalAuthOptions>(provider)
{
    private readonly ILogger<LocalAuthStrategy> _logger = logger;
    private readonly SignInManager<LocalizatorIdentityUser> _signInManager = signInManager;
    private readonly UserManager<LocalizatorIdentityUser> _userManager = userManager;

    public override async Task<bool> AuthenticateAsync(HttpContext context, CancellationToken ct = default)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.TryAdd("WWW-Authenticate", "Basic realm=\"Localizator\"");
            return false;
        }

        var headerValue = authHeader.ToString();
        if (!headerValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.TryAdd("WWW-Authenticate", "Basic realm=\"Localizator\"");
            return false;
        }

        // Decode Base64 credentials
        var encodedCredentials = headerValue["Basic ".Length..].Trim();
        string decoded;
        try
        {
            var bytes = Convert.FromBase64String(encodedCredentials);
            decoded = Encoding.UTF8.GetString(bytes);
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return false;
        }

        var parts = decoded.Split(':', 2);
        if (parts.Length != 2)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return false;
        }

        var (username, password) = (parts[0], parts[1]);

        // Basic credential check
        if (username != Options.AdminUser || password != Options.AdminPassword)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Headers.TryAdd("WWW-Authenticate", "Basic realm=\"Localizator\"");
            return false;
        }

        // Identity user creation/check
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            user = new LocalizatorIdentityUser(username, Options.Mode);
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                _logger.LogError("Failed to create identity user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return false;
            }
        }

        // Sign in with Identity (creates session/cookie)
        await _signInManager.SignInAsync(user, isPersistent: false);

        return true;
    }
}
