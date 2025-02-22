using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Notification;
using SpendSmart.Application.Contracts.Notification;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Domain.Abstractions;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.Modules.Users.Events;

namespace SpendSmart.Application.Events.Handlers.Users;

/// <summary>
/// Represents the <see cref="UserCreatedEvent"/> handler.
/// </summary>
public sealed class SendWelcomeEmail_UserCreatedEventHandler : EventHandler<UserCreatedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailSender _emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="SendWelcomeEmail_UserCreatedEventHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="emailSender">The email sender.</param>
    public SendWelcomeEmail_UserCreatedEventHandler(IUserRepository userRepository, IEmailSender emailSender)
    {
        _userRepository = userRepository;
        _emailSender = emailSender;
    }

    /// <inheritdoc />
    public override async Task Handle(UserCreatedEvent @event, CancellationToken cancellationToken = default)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(@event.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return;
        }

        User user = maybeUser.Value;

        var mailRequest = new MailRequest
        {
            RecipientEmail = user.Email,
            Subject = "Welcome to SpendSmart! 🎉",
            Body = CreateEmailBody(user)
        };

        await _emailSender.SendAsync(mailRequest, cancellationToken);
    }

    private static string CreateEmailBody(User user)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Hello {user.GetFullName()} and welcome to SpendSmart!");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("We honestly hope that you will enjoy everything that we have to offer.");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Sincerely,");
        stringBuilder.AppendLine("The SpendSmart team");

        return stringBuilder.ToString();
    }
}
