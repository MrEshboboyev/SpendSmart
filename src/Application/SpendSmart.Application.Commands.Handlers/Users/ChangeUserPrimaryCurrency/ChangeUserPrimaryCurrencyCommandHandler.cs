﻿using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;

namespace SpendSmart.Application.Commands.Handlers.Users.ChangeUserPrimaryCurrency;

/// <summary>
/// Represents the <see cref="ChangeUserPrimaryCurrencyCommand"/> handler.
/// </summary>
public sealed class ChangeUserPrimaryCurrencyCommandHandler : ICommandHandler<ChangeUserPrimaryCurrencyCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeUserPrimaryCurrencyCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public ChangeUserPrimaryCurrencyCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(ChangeUserPrimaryCurrencyCommand request, CancellationToken cancellationToken)
    {
        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        Currency currency = Currency.FromValue(request.Currency).Value;

        Result changePrimaryCurrencyResult = maybeUser.Value.ChangePrimaryCurrency(currency);

        if (changePrimaryCurrencyResult.IsFailure)
        {
            return Result.Failure(changePrimaryCurrencyResult.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
