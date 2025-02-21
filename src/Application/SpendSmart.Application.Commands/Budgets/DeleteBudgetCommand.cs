using System;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Result;

namespace SpendSmart.Application.Commands.Budgets;

/// <summary>
/// Represents the command for deleting a budget.
/// </summary>
public sealed class DeleteBudgetCommand : ICommand<Result>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBudgetCommand"/> class.
    /// </summary>
    /// <param name="budgetId">The budget identifier.</param>
    public DeleteBudgetCommand(Ulid budgetId) => BudgetId = budgetId;

    /// <summary>
    /// Gets the budget identifier.
    /// </summary>
    public Ulid BudgetId { get; }
}
