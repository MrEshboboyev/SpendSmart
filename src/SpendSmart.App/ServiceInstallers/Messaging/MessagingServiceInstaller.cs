using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.Application.Abstractions.Behaviors;
using SpendSmart.Application.Commands.Handlers;
using SpendSmart.Application.Events.Handlers;
using SpendSmart.Application.Queries.Handlers;
using SpendSmart.Domain.Abstractions;
using System;

namespace SpendSmart.App.ServiceInstallers.Messaging;

/// <summary>
/// Represents the messaging services installer.
/// </summary>
public sealed class MessagingServiceInstaller : IServiceInstaller
{
    private const string EventHandlerPostfix = "EventHandler";

    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblies(
                CommandHandlersAssembly.Assembly, 
                QueryHandlersAssembly.Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        AddEventHandlers(services);
    }

    private static void AddEventHandlers(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(EventHandlersAssembly.Assembly)
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith(EventHandlerPostfix, StringComparison.Ordinal)))
                .As(eventHandlerType =>
                {
                    Type eventType = eventHandlerType!.BaseType!.GenericTypeArguments[0];

                    Type eventHandlerInterfaceType = typeof(IEventHandler<>).MakeGenericType(eventType);

                    return new[] { eventHandlerInterfaceType };
                })
                .WithScopedLifetime());
}
