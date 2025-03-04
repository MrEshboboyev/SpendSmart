﻿using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Domain.Modules.Budgets;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Persistence.Indexes.Transactions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.QueryProcessors.Budgets;

/// <summary>
/// Represents the <see cref="GetTransactionByIdQuery"/> processor.
/// </summary>
internal sealed class GetBudgetDetailsByIdQueryProcessor : IGetBudgetDetailsByIdQueryProcessor
{
    private readonly IAsyncDocumentSession _session;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetDetailsByIdQueryProcessor"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public GetBudgetDetailsByIdQueryProcessor(IAsyncDocumentSession session, IUserInformationProvider userInformationProvider)
    {
        _session = session;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<Maybe<BudgetDetailsResponse>> Process(
        GetBudgetDetailsByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var budget = await _session
            .Query<Budget>()
            .Where(x => x.Id == query.BudgetId)
            .Select(x => new
            {
                x.Id,
                x.UserId,
                x.Name,
                x.Money,
                Categories = x.Categories.Select(c => new
                {
                    c.Value,
                    c.Name
                }).ToList(),
                x.StartDate,
                x.EndDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (budget is null || budget.UserId != _userInformationProvider.UserId)
        {
            return Maybe<BudgetDetailsResponse>.None;
        }

        int[] categoryValues = budget.Categories.Select(x => x.Value).ToArray();

        Money[] expenseAmounts = await _session.Query<Transaction, Transactions_Search>()
            .Where(
                TransactionsMatchExpression(
                    budget.UserId,
                    TransactionType.Expense.Value,
                    budget.StartDate,
                    budget.EndDate,
                    budget.Money.Currency.Value,
                    categoryValues))
            .Select(x => x.Money)
            .ToArrayAsync(cancellationToken);

        Money totalExpense = expenseAmounts.Length != 0 ? Money.Sum(expenseAmounts) : Money.Zero(budget.Money.Currency);

        var budgetDetailsResponse = new BudgetDetailsResponse
        {
            Id = budget.Id,
            Name = budget.Name,
            Amount = budget.Money.Format(),
            RemainingAmount = (budget.Money + totalExpense).Format(),
            UsedPercentage = budget.Money.PercentFrom(totalExpense),
            Categories = budget.Categories.Select(category => category.Name).ToArray(),
            StartDate = budget.StartDate,
            EndDate = budget.EndDate
        };

        return budgetDetailsResponse;
    }

    private static Expression<Func<Transaction, bool>> TransactionsMatchExpression(Ulid userId,
                                                                                   int transactionType,
                                                                                   DateTime startDate,
                                                                                   DateTime endDate,
                                                                                   int currency,
                                                                                   int[] categories)
    {
        if (categories.Length != 0)
        {
            return transaction =>
                transaction.UserId == userId &&
                transaction.TransactionType.Value == transactionType &&
                transaction.OccurredOn >= startDate &&
                transaction.OccurredOn <= endDate &&
                transaction.Money.Currency.Value == currency &&
                transaction.Category.Value.In(categories);
        }

        return transaction =>
            transaction.UserId == userId &&
            transaction.TransactionType.Value == transactionType &&
            transaction.OccurredOn >= startDate &&
            transaction.OccurredOn <= endDate &&
            transaction.Money.Currency.Value == currency;
    }
}
