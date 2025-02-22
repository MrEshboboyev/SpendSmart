using System;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Budgets;

/// <summary>
/// Represents the query for getting a budget by identifier.
/// </summary>
public sealed class GetBudgetByIdQuery : IQuery<Maybe<BudgetResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetByIdQuery"/> class.
    /// </summary>
    /// <param name="budgetId">The budget identifier.</param>
    public GetBudgetByIdQuery(Ulid budgetId) => BudgetId = budgetId.ToString();

    /// <summary>
    /// Gets the budget identifier.
    /// </summary>
    public string BudgetId { get; }
}
