﻿using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Budgets;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Budgets;

namespace SpendSmart.Application.Commands.Handlers.Budgets.DeleteBudget;

/// <summary>
/// Represents the <see cref="DeleteBudgetCommand"/> handler.
/// </summary>
public sealed class DeleteBudgetCommandHandler : ICommandHandler<DeleteBudgetCommand, Result>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBudgetCommandHandler"/> class.
    /// </summary>
    /// <param name="budgetRepository">The budget repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public DeleteBudgetCommandHandler(
        IBudgetRepository budgetRepository,
        IUnitOfWork unitOfWork,
        IUserInformationProvider userInformationProvider)
    {
        _budgetRepository = budgetRepository;
        _unitOfWork = unitOfWork;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteBudgetCommand request, CancellationToken cancellationToken)
    {
        Maybe<Budget> maybeBudget = await _budgetRepository.GetByIdAsync(request.BudgetId, cancellationToken);

        if (maybeBudget.HasNoValue)
        {
            return Result.Failure(ValidationErrors.Budget.NotFound);
        }

        Budget budget = maybeBudget.Value;

        if (budget.UserId != _userInformationProvider.UserId)
        {
            return Result.Failure(ValidationErrors.User.InvalidPermissions);
        }

        _budgetRepository.Remove(budget);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
