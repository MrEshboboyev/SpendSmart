using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Notification;
using SpendSmart.Application.Contracts.Notification;
using SpendSmart.Domain.Abstractions;
using SpendSmart.Domain.Modules.Messages.Events;

namespace SpendSmart.Application.Events.Handlers.Messages;

/// <summary>
/// Represents the <see cref="MessageRetryCountExceededEvent"/> handler.
/// </summary>
public sealed class SendNotificationEmail_MessageRetryCountExceededEventHandler : EventHandler<MessageRetryCountExceededEvent>
{
    private readonly IAlertSender _alertSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendNotificationEmail_MessageRetryCountExceededEventHandler"/> class.
    /// </summary>
    /// <param name="alertSender">The alert sender.</param>
    public SendNotificationEmail_MessageRetryCountExceededEventHandler(IAlertSender alertSender) => _alertSender = alertSender;

    /// <inheritdoc />
    public override async Task Handle(MessageRetryCountExceededEvent @event, CancellationToken cancellationToken = default)
    {
        var mailRequest = new AlertRequest
        {
            Subject = "SpendSmart - Message Retry Count Exceeded",
            Body = $"Message with identifier {@event.MessageId} has exceeded the allowed retry count."
        };

        await _alertSender.SendAsync(mailRequest, cancellationToken);
    }
}
