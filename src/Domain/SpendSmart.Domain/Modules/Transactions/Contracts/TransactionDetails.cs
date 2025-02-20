using SpendSmart.Domain.Modules.Common;
using System;

namespace SpendSmart.Domain.Modules.Transactions.Contracts;

/// <summary>
/// Represents the transaction details.
/// </summary>
internal sealed class TransactionDetails : ITransactionDetails
{
    /// <inheritdoc />
    public Description Description { get; init; }

    /// <inheritdoc />
    public Category Category { get; init; }

    /// <inheritdoc />
    public Money Money { get; init; }

    /// <inheritdoc />
    public DateTime OccurredOn { get; init; }

    /// <inheritdoc />
    public TransactionType TransactionType { get; init; }
}
