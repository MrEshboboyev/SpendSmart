﻿using System;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Users;
using TimeZoneConverter;

namespace SpendSmart.Application.Commands.Handlers.Users.ChangeUserTimeZone;

/// <summary>
/// Represents the <see cref="ChangeUserTimeZoneCommand"/> handler.
/// </summary>
internal sealed class ChangeUserTimeZoneCommandHandler : ICommandHandler<ChangeUserTimeZoneCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeUserTimeZoneCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public ChangeUserTimeZoneCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(ChangeUserTimeZoneCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        TimeZoneInfo timeZoneInfo = TZConvert.GetTimeZoneInfo(request.TimeZoneId);

        maybeUser.Value.ChangeTimeZone(timeZoneInfo);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
