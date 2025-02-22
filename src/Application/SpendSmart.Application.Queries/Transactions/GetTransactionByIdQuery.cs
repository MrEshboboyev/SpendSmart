using System;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Transactions;

/// <summary>
/// Represents the query for getting a transaction by identifier.
/// </summary>
public sealed class GetTransactionByIdQuery : IQuery<Maybe<TransactionResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTransactionByIdQuery"/> class.
    /// </summary>
    /// <param name="transactionId">The transaction identifier.</param>
    public GetTransactionByIdQuery(Ulid transactionId) => TransactionId = transactionId;

    /// <summary>
    /// Gets the transaction identifier.
    /// </summary>
    public Ulid TransactionId { get; }
}
