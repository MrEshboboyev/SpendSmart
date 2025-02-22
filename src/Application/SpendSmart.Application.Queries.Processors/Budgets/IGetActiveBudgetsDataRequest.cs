using System.Collections.Generic;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Abstractions;

namespace SpendSmart.Application.Queries.Processors.Budgets;

/// <summary>
/// Represents the <see cref="GetActiveBudgetsQuery"/> processor interface.
/// </summary>
public interface IGetActiveBudgetsQueryProcessor : IQueryProcessor<GetActiveBudgetsQuery, IEnumerable<BudgetListItemResponse>>
{
}
