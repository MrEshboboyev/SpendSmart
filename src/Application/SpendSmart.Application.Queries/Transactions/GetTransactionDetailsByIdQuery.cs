using System;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Transactions;

/// <summary>
/// Represents the query for getting transaction details by identifier.
/// </summary>
public sealed class GetTransactionDetailsByIdQuery : IQuery<Maybe<TransactionDetailsResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTransactionDetailsByIdQuery"/> class.
    /// </summary>
    /// <param name="transactionId">The transaction identifier.</param>
    public GetTransactionDetailsByIdQuery(Ulid transactionId) => TransactionId = transactionId;

    /// <summary>
    /// Gets the transaction identifier.
    /// </summary>
    public Ulid TransactionId { get; }
}
