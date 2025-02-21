using System;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Result;

namespace SpendSmart.Application.Commands.Transactions;

/// <summary>
/// Represents the command for deleting a transaction.
/// </summary>
public sealed class DeleteTransactionCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTransactionCommand"/> class.
    /// </summary>
    /// <param name="transactionId">The transaction identifier.</param>
    public DeleteTransactionCommand(Ulid transactionId) => TransactionId = transactionId;

    /// <summary>
    /// Gets the transaction identifier.
    /// </summary>
    public Ulid TransactionId { get; }
}
