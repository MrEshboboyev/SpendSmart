using SpendSmart.Common.Primitives.Result;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Modules.Budgets.Contracts;

namespace SpendSmart.Domain.Modules.Budgets;

/// <summary>
/// Represents the budget factory.
/// </summary>
internal sealed class BudgetFactory : IBudgetFactory, ITransient
{
    private readonly IBudgetDetailsValidator _budgetDetailsValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BudgetFactory"/> class.
    /// </summary>
    /// <param name="budgetDetailsValidator">The budget details validator.</param>
    public BudgetFactory(IBudgetDetailsValidator budgetDetailsValidator) => _budgetDetailsValidator = budgetDetailsValidator;

    /// <inheritdoc />
    public Result<Budget> Create(CreateBudgetRequest createBudgetRequest)
    {
        var validateBudgetDetailsRequest = createBudgetRequest.ToValidateBudgetDetailsRequest();

        Result<IBudgetDetails> budgetDetailsResult = _budgetDetailsValidator.Validate(validateBudgetDetailsRequest);

        if (budgetDetailsResult.IsFailure)
        {
            return Result.Failure<Budget>(budgetDetailsResult.Error);
        }

        var budget = Budget.Create(createBudgetRequest.User, budgetDetailsResult.Value);

        return budget;
    }
}
