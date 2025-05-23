﻿using System;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;
using TimeZoneConverter;

namespace SpendSmart.Application.Commands.Handlers.Users.SetupUser;

/// <summary>
/// Represents the <see cref="SetupUserCommand"/> handler.
/// </summary>
internal sealed class SetupUserCommandHandler : ICommandHandler<SetupUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetupUserCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public SetupUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(SetupUserCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        Currency currency = Currency.FromValue(request.Currency).Value;

        Result addCurrencyResult = maybeUser.Value.AddCurrency(currency);

        if (addCurrencyResult.IsFailure)
        {
            return Result.Failure(addCurrencyResult.Error);
        }

        TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo(request.TimeZoneId);

        maybeUser.Value.ChangeTimeZone(timeZoneInfo);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
