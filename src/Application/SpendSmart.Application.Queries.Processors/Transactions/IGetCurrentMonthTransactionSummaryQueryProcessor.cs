using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Abstractions;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Processors.Transactions;

/// <summary>
/// Represents the <see cref="GetCurrentMonthTransactionSummaryQuery"/> processor interface.
/// </summary>
public interface IGetCurrentMonthTransactionSummaryQueryProcessor
    : IQueryProcessor<GetCurrentMonthTransactionSummaryQuery, Maybe<TransactionSummaryResponse>>
{
}
