using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Abstractions;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Processors.Budgets;

/// <summary>
/// Represents the <see cref="GetBudgetByIdQuery"/> processor interface.
/// </summary>
public interface IGetBudgetByIdQueryProcessor : IQueryProcessor<GetBudgetByIdQuery, Maybe<BudgetResponse>>
{
}
