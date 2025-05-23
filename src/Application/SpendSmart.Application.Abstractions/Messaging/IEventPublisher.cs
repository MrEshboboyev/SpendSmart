﻿using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Domain.Abstractions;
using SpendSmart.Domain.Primitives;

namespace SpendSmart.Application.Abstractions.Messaging;

/// <summary>
/// Represents the event publisher interface.
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// Publishes the specified event.
    /// </summary>
    /// <param name="event">The events.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The completed task.</returns>
    Task PublishAsync(IEvent @event, CancellationToken cancellationToken = default);
}
