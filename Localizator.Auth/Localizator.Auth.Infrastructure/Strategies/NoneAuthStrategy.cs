using Localizator.Auth.Domain.Configuration.Mode;
using Localizator.Auth.Domain.Identity;
using Localizator.Auth.Domain.Interfaces.Configuration;
using Localizator.Auth.Domain.Interfaces.Strategy;
using Localizator.Auth.Infrastructure.Strategies.Abstract;
using Localizator.Shared.Extensions;
using Localizator.Shared.Resources;
using Localizator.Shared.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Localizator.Auth.Infrastructure.Strategies;

public sealed class NoneAuthStrategy(
    IAuthOptionsProvider provider,
    ILogger<NoneAuthStrategy> logger,
    UserManager<LocalizatorIdentityUser> userManager,
    SignInManager<LocalizatorIdentityUser> signInManager) : AuthStrategyBase<INoneAuthOptions>(provider)
{
    private readonly ILogger<NoneAuthStrategy> _logger = logger;
    private readonly UserManager<LocalizatorIdentityUser> _userManager = userManager;
    private readonly SignInManager<LocalizatorIdentityUser> _signInManager = signInManager;

    public override async Task<Result<bool>> AuthenticateAsync(HttpContext context, CancellationToken ct = default)
    {
        var username = "devuser";
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            user = new LocalizatorIdentityUser(username, Options.Mode);
            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                string message = Messages.FailedToCreateDevUser.Format(string.Join(", ", result.Errors.Select(e => e.Description)));

                _logger.LogError(message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return Result<bool>.Failure(message);
            }
        }

        await _signInManager.SignInAsync(user, isPersistent: false);

        return Result<bool>.Success();
    }
}
