using Localizator.Shared.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Localizator.Shared.Extensions;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator.Mediator>();
        return services;
    }

    public static IServiceCollection RegisterMediatorHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var handlerTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Select(t => new
            {
                Type = t,
                Interfaces = t.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                           i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                    .ToList()
            })
            .Where(x => x.Interfaces.Count != 0)
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            foreach (var interfaceType in handlerType.Interfaces)
            {
                services.AddScoped(interfaceType, handlerType.Type);
            }
        }

        return services;
    }

    public static IServiceCollection RegisterMediatorBehaviors(this IServiceCollection services, params Assembly[] assemblies)
    {
        var behaviorTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract)
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>)))
            .ToList();

        foreach (var behaviorType in behaviorTypes)
        {
            if (behaviorType.IsGenericTypeDefinition)
            {
                services.AddScoped(typeof(IPipelineBehavior<,>), behaviorType);
            }
            else
            {
                var interfaces = behaviorType.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                           i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>));

                foreach (var interfaceType in interfaces)
                {
                    services.AddScoped(interfaceType, behaviorType);
                }
            }
        }

        return services;
    }
}