using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Abstractions;
using SpendSmart.Application.Queries.Transactions;

namespace SpendSmart.Application.Queries.Processors.Transactions;

/// <summary>
/// Represents the <see cref="GetTransactionsQuery"/> processor interface.
/// </summary>
public interface IGetTransactionsQueryProcessor : IQueryProcessor<GetTransactionsQuery, TransactionListResponse>
{
}
