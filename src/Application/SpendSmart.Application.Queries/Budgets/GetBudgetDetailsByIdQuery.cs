using System;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Budgets;

/// <summary>
/// Represents the query for getting budget details by identifier.
/// </summary>
public sealed class GetBudgetDetailsByIdQuery : IQuery<Maybe<BudgetDetailsResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetDetailsByIdQuery"/> class.
    /// </summary>
    /// <param name="budgetId">The budget identifier.</param>
    public GetBudgetDetailsByIdQuery(Ulid budgetId) => BudgetId = budgetId.ToString();

    /// <summary>
    /// Gets the budget identifier.
    /// </summary>
    public string BudgetId { get; }
}
