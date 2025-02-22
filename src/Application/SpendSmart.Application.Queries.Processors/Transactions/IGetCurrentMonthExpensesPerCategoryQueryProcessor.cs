using System.Collections.Generic;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Abstractions;
using SpendSmart.Application.Queries.Transactions;

namespace SpendSmart.Application.Queries.Processors.Transactions;

/// <summary>
/// Represents the <see cref="GetCurrentMonthExpensesPerCategoryQuery"/> processor interface.
/// </summary>
public interface IGetCurrentMonthExpensesPerCategoryQueryProcessor
    : IQueryProcessor<GetCurrentMonthExpensesPerCategoryQuery, IEnumerable<ExpensePerCategoryResponse>>
{
}
