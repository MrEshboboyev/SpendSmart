using SpendSmart.Common.Primitives.Result;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Modules.Transactions.Contracts;

namespace SpendSmart.Domain.Modules.Transactions;

/// <summary>
/// Represents the transaction factory.
/// </summary>
internal sealed class TransactionFactory : ITransactionFactory, ITransient
{
    private readonly ITransactionDetailsValidator _transactionDetailsValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionFactory"/> class.
    /// </summary>
    /// <param name="transactionDetailsValidator">The transaction details validator.</param>
    public TransactionFactory(ITransactionDetailsValidator transactionDetailsValidator) =>
        _transactionDetailsValidator = transactionDetailsValidator;

    /// <inheritdoc />
    public Result<Transaction> Create(CreateTransactionRequest createTransactionRequest)
    {
        var validateTransactionDetailsRequest = createTransactionRequest.ToValidateTransactionDetailsRequest();

        Result<ITransactionDetails> transactionDetailsResult = _transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        if (transactionDetailsResult.IsFailure)
        {
            return Result.Failure<Transaction>(transactionDetailsResult.Error);
        }

        var transaction = Transaction.Create(createTransactionRequest.User, transactionDetailsResult.Value);

        return transaction;
    }
}
