using MediatR;
using Microsoft.Extensions.Logging;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Common.Abstractions.Clock;
using SpendSmart.Common.Abstractions.Constants;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Errors;
using SpendSmart.Common.Primitives.Result;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Application.Abstractions.Behaviors;

/// <summary>
/// Represents the logging behavior middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, ICommand<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IUserInformationProvider _userInformationProvider;
    private readonly ISystemTime _systemTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    /// <param name="systemTime">The system time.</param>
    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        IUserInformationProvider userInformationProvider,
        ISystemTime systemTime)
    {
        _logger = logger;
        _systemTime = systemTime;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        string requestName = request.GetType().Name;

        _logger.LogInformation(GetRequestStartedLogMessage(),
                               [.. GetLogArguments(requestName)]);

        TResponse response = await next();

        if (response.IsFailure)
        {
            _logger.LogError(GetRequestFailureLogMessage(),
                             [.. GetLogArguments(requestName, response.Error)]);
        }

        _logger.LogInformation(GetRequestCompletedLogMessage(),
                               [.. GetLogArguments(requestName)]);

        return response;
    }

    #region Private Methods

    private string GetRequestStartedLogMessage() =>
        _userInformationProvider.IsAuthenticated
            ? "Request started {@UserId} {@RequestName} {@DateTimeUtc}."
            : "Request started {@RequestName} {@DateTimeUtc}.";

    private string GetRequestFailureLogMessage() =>
        _userInformationProvider.IsAuthenticated
            ? "Request failure {@UserId} {@RequestName} {@ErrorCode} {@DateTimeUtc}."
            : "Request failure {@RequestName} {@ErrorCode} {@DateTimeUtc}.";

    private string GetRequestCompletedLogMessage() =>
        _userInformationProvider.IsAuthenticated
            ? "Request completed {@UserId} {@RequestName} {@DateTimeUtc}."
            : "Request completed {@RequestName} {@DateTimeUtc}.";

    private IEnumerable<object> GetLogArguments(string requestName, Error error = null)
    {
        if (_userInformationProvider.IsAuthenticated)
        {
            yield return _userInformationProvider.UserId.ToString();
        }

        yield return requestName;

        if (error is not null)
        {
            yield return error.Code;
        }

        yield return _systemTime.UtcNow.ToString(DateTimeFormats.DateTimeWithMilliseconds, CultureInfo.InvariantCulture);
    }

    #endregion
}
