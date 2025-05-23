﻿using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Domain.Modules.Budgets;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Persistence.Indexes.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.QueryProcessors.Budgets;

/// <summary>
/// Represents the <see cref="GetActiveBudgetsQuery"/> processor.
/// </summary>
internal sealed class GetActiveBudgetsQueryProcessor : IGetActiveBudgetsQueryProcessor
{
    private readonly IAsyncDocumentSession _session;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActiveBudgetsQueryProcessor"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public GetActiveBudgetsQueryProcessor(IAsyncDocumentSession session, IUserInformationProvider userInformationProvider)
    {
        _session = session;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BudgetListItemResponse>> Process(
        GetActiveBudgetsQuery query,
        CancellationToken cancellationToken = default)
    {
        if (_userInformationProvider.UserId != query.UserId)
        {
            return [];
        }

        var budgets = await _session.Query<Budget>()
            .Where(budget => budget.UserId == query.UserId && !budget.Expired)
            .Select(budget => new
            {
                budget.Id,
                budget.UserId,
                budget.Name,
                Categories = budget.Categories.Select(category => category.Value).ToArray(),
                budget.Money,
                budget.StartDate,
                budget.EndDate
            })
            .ToArrayAsync(cancellationToken);

        var budgetListItemResponses = new List<BudgetListItemResponse>();

        foreach (var budget in budgets)
        {
            int[] categoryValues = budget.Categories;

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

            Money totalExpense = expenseAmounts.Length != 0 
                ? Money.Sum(expenseAmounts) 
                : Money.Zero(budget.Money.Currency);

            var budgetListItemResponse = new BudgetListItemResponse
            {
                Id = budget.Id,
                Name = budget.Name.Value,
                UsedPercentage = budget.Money.PercentFrom(totalExpense),
                FormattedAmount = budget.Money.Format(),
                StartDate = budget.StartDate,
                EndDate = budget.EndDate
            };

            budgetListItemResponses.Add(budgetListItemResponse);
        }

        return budgetListItemResponses;
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
