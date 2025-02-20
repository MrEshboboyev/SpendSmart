using SpendSmart.Domain.Modules.Common;
using System;

namespace SpendSmart.Domain.Modules.Budgets.Contracts;

/// <summary>
/// Represents the budget details.
/// </summary>
internal sealed class BudgetDetails : IBudgetDetails
{
    /// <inheritdoc />
    public Name Name { get; init; }

    /// <inheritdoc />
    public Category[] Categories { get; init; }

    /// <inheritdoc />
    public Money Money { get; init; }

    /// <inheritdoc />
    public DateTime StartDate { get; init; }

    /// <inheritdoc />
    public DateTime EndDate { get; init; }
}
