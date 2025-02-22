using System;
using System.Collections.Generic;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Budgets;

/// <summary>
/// Represents the query for getting a list of active budgets.
/// </summary>
public sealed class GetActiveBudgetsQuery : IQuery<IEnumerable<BudgetListItemResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetActiveBudgetsQuery"/> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public GetActiveBudgetsQuery(Ulid userId) => UserId = userId;

    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public Ulid UserId { get; }
}
