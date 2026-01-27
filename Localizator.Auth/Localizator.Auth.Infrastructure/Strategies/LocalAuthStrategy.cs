using Localizator.Auth.Domain.Identity;
using Localizator.Auth.Domain.Interfaces.Configuration;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Localizator.Auth.Infrastructure.Strategies.Abstract;
using Localizator.Shared.Extensions;
using Localizator.Shared.Resources;
using Localizator.Shared.Result;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
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

    public override async Task<Result<bool>> AuthenticateAsync(HttpContext context, CancellationToken ct = default)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Result<bool>.Failure(Errors.AuthorizationHeaderNotFound);
        }

        var headerValue = authHeader.ToString();
        if (!headerValue.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Result<bool>.Failure(Errors.BasicAuthorizationHeaderInvalidFormat);
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
            return Result<bool>.Failure(Errors.Base64ConversionError);
        }

        var parts = decoded.Split(':', 2);
        if (parts.Length != 2)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return Result<bool>.Failure();
        }

        var (username, password) = (parts[0], parts[1]);

        // Basic credential check
        if (username != Options.AdminUser || password != Options.AdminPassword)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Result<bool>.Failure(Errors.BasicCredentialsDontMatch);
        }

        Result<bool> isLoggedIn = CheckIfUserLoggedIn(_signInManager, context, username);

        if(isLoggedIn.IsSuccess && isLoggedIn.Data)
        {
            return Result<bool>.Success(true);
        }
        else
        {
            await _signInManager.SignOutAsync();
        }

        return await SignInUserAsync(
            context,
            _signInManager,
            _userManager,
            username);
    }
}
