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

namespace SpendSmart.Application.Commands.Handlers.Users.AddUserCurrency;

/// <summary>
/// Represents the <see cref="AddUserCurrencyCommand"/> handler.
/// </summary>
public sealed class AddUserCurrencyCommandHandler : ICommandHandler<AddUserCurrencyCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserCurrencyCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    public AddUserCurrencyCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(AddUserCurrencyCommand request, CancellationToken cancellationToken)
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
