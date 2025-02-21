using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Transactions;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;

namespace SpendSmart.Application.Commands.Handlers.Transactions.UpdateTransaction;

/// <summary>
/// Represents the <see cref="UpdateTransactionCommand"/> handler.
/// </summary>
public sealed class UpdateTransactionCommandHandler : ICommandHandler<UpdateTransactionCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionDetailsValidator _transactionDetailsValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTransactionCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="transactionRepository">The transaction repository.</param>
    /// <param name="transactionDetailsValidator">The transaction details validator.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public UpdateTransactionCommandHandler(
        IUserRepository userRepository,
        ITransactionRepository transactionRepository,
        ITransactionDetailsValidator transactionDetailsValidator,
        IUnitOfWork unitOfWork,
        IUserInformationProvider userInformationProvider)
    {
        _userRepository = userRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _userInformationProvider = userInformationProvider;
        _transactionDetailsValidator = transactionDetailsValidator;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        Maybe<Transaction> maybeTransaction = await _transactionRepository
            .GetByIdWithUserAsync(request.TransactionId, cancellationToken);

        if (maybeTransaction.HasNoValue)
        {
            return Result.Failure(ValidationErrors.Transaction.NotFound);
        }

        Transaction transaction = maybeTransaction.Value;

        if (transaction.UserId != _userInformationProvider.UserId)
        {
            return Result.Failure(ValidationErrors.User.InvalidPermissions);
        }

        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(transaction.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        var validateTransactionDetailsRequest = new ValidateTransactionDetailsRequest(
            maybeUser.Value,
            request.Description,
            request.Category,
            request.Amount,
            request.Currency,
            request.OccurredOn,
            transaction.TransactionType.Value);

        Result<ITransactionDetails> transactionDetailsResult = _transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        if (transactionDetailsResult.IsFailure)
        {
            return Result.Failure(transactionDetailsResult.Error);
        }

        transaction.ChangeDetails(transactionDetailsResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
