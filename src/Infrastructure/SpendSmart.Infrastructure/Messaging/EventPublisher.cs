using SpendSmart.Application.Abstractions.Messaging;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Abstractions;
using SpendSmart.Domain.Modules.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Infrastructure.Messaging;

/// <summary>
/// Represents the event publisher.
/// </summary>
public sealed class EventPublisher : IEventPublisher, IScoped
{
    private readonly IMessageRepository _messageRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventPublisher"/> class.
    /// </summary>
    /// <param name="messageRepository">The message repository.</param>
    public EventPublisher(IMessageRepository messageRepository)
        => _messageRepository = messageRepository;

    /// <inheritdoc />
    public async Task PublishAsync(IEvent @event,
                                   CancellationToken cancellationToken = default) =>
        await _messageRepository.AddAsync(new Message(@event), cancellationToken);
}
