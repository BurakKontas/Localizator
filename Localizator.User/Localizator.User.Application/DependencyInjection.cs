using Localizator.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Localizator.User.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUserApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.RegisterMediatorHandlers(assembly);
        services.RegisterMediatorBehaviors(assembly);

        return services;
    }
}
