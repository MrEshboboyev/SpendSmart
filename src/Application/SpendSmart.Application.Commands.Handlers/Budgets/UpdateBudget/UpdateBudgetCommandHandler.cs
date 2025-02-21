using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Budgets;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Budgets;
using SpendSmart.Domain.Modules.Budgets.Contracts;
using SpendSmart.Domain.Modules.Users;

namespace SpendSmart.Application.Commands.Handlers.Budgets.UpdateBudget;

/// <summary>
/// Represents the <see cref="UpdateBudgetCommand"/> handler.
/// </summary>
public sealed class UpdateBudgetCommandHandler : ICommandHandler<UpdateBudgetCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IBudgetRepository _budgetRepository;
    private readonly IBudgetDetailsValidator _budgetDetailsValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="budgetRepository">The database context.</param>
    /// <param name="budgetDetailsValidator">The budget details validator.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public UpdateBudgetCommandHandler(
        IUserRepository userRepository,
        IBudgetRepository budgetRepository,
        IBudgetDetailsValidator budgetDetailsValidator,
        IUnitOfWork unitOfWork,
        IUserInformationProvider userInformationProvider)
    {
        _userRepository = userRepository;
        _budgetRepository = budgetRepository;
        _budgetDetailsValidator = budgetDetailsValidator;
        _unitOfWork = unitOfWork;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        Maybe<Budget> maybeBudget = await _budgetRepository.GetByIdWithUserAsync(request.BudgetId, cancellationToken);

        if (maybeBudget.HasNoValue)
        {
            return Result.Failure(ValidationErrors.Budget.NotFound);
        }

        Budget budget = maybeBudget.Value;

        if (budget.UserId != _userInformationProvider.UserId)
        {
            return Result.Failure(ValidationErrors.User.InvalidPermissions);
        }

        Maybe<User> maybeUser = await _userRepository.GetByIdAsync(budget.UserId, cancellationToken);

        if (maybeUser.HasNoValue)
        {
            return Result.Failure(ValidationErrors.User.NotFound);
        }

        var validateBudgetDetailsRequest = new ValidateBudgetDetailsRequest(
            maybeUser.Value, request.Name, request.Categories, request.Amount, request.Currency, request.StartDate, request.EndDate);

        Result<IBudgetDetails> budgetDetailsResult = _budgetDetailsValidator.Validate(validateBudgetDetailsRequest);

        if (budgetDetailsResult.IsFailure)
        {
            return Result.Failure(budgetDetailsResult.Error);
        }

        budget.ChangeDetails(budgetDetailsResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
