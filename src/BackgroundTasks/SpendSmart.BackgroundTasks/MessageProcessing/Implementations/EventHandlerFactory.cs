using System;
using System.Collections.Generic;
using System.Linq;
using SpendSmart.BackgroundTasks.MessageProcessing.Abstractions;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace SpendSmart.BackgroundTasks.MessageProcessing.Implementations;

/// <summary>
/// Represents the event handler factory.
/// </summary>
internal sealed class EventHandlerFactory : IEventHandlerFactory, IScoped
{
    private static readonly Type EventHandlerGenericType = typeof(IEventHandler<>);
    private static readonly Dictionary<Type, Type> EventHandlersDictionary = [];

    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventHandlerFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public EventHandlerFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    /// <inheritdoc />
    public IEnumerable<IEventHandler> GetHandlers(IEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        Type eventType = @event.GetType();

        if (!EventHandlersDictionary.TryGetValue(eventType, out Type eventHandlerType))
        {
            eventHandlerType = EventHandlerGenericType.MakeGenericType(eventType);

            EventHandlersDictionary.Add(eventType, eventHandlerType);
        }

        IEnumerable<IEventHandler> eventHandlers = _serviceProvider
            .GetServices(eventHandlerType)
            .Cast<IEventHandler>();

        return eventHandlers;
    }
}
