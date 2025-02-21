using SpendSmart.Application.Commands.Budgets;
using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Budgets.DeleteBudget;

/// <summary>
/// Represents the <see cref="DeleteBudgetCommand"/> validator.
/// </summary>
public sealed class DeleteBudgetCommandValidator : AbstractValidator<DeleteBudgetCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBudgetCommandValidator"/> class.
    /// </summary>
    public DeleteBudgetCommandValidator() =>
        RuleFor(x => x.BudgetId).NotEmpty().WithError(ValidationErrors.Budget.IdentifierIsRequired);
}
