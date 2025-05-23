﻿using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Domain.Modules.Budgets;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.QueryProcessors.Budgets;

/// <summary>
/// Represents the <see cref="GetTransactionByIdQuery"/> processor.
/// </summary>
internal sealed class GetBudgetByIdQueryProcessor : IGetBudgetByIdQueryProcessor
{
    private readonly IAsyncDocumentSession _session;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetByIdQueryProcessor"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public GetBudgetByIdQueryProcessor(IAsyncDocumentSession session, IUserInformationProvider userInformationProvider)
    {
        _session = session;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<Maybe<BudgetResponse>> Process(GetBudgetByIdQuery query, CancellationToken cancellationToken = default)
    {
        var budget = await _session
            .Query<Budget>()
            .Where(x => x.Id == query.BudgetId)
            .Select(x => new
            {
                x.Id,
                x.UserId,
                x.Name,
                Money = new
                {
                    x.Money.Amount,
                    Currency = x.Money.Currency.Value
                },
                Categories = x.Categories.Select(c => c.Value).ToArray(),
                x.StartDate,
                x.EndDate
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (budget is null || budget.UserId != _userInformationProvider.UserId)
        {
            return Maybe<BudgetResponse>.None;
        }

        var budgetResponse = new BudgetResponse
        {
            Id = budget.Id,
            Name = budget.Name,
            Amount = budget.Money.Amount,
            Currency = budget.Money.Currency,
            Categories = budget.Categories,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate
        };

        return budgetResponse;
    }
}
