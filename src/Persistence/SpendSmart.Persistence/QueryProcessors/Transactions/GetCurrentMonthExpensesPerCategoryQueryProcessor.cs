using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Transactions;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Persistence.Indexes.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.QueryProcessors.Transactions;

/// <summary>
/// Represents the <see cref="GetCurrentMonthExpensesPerCategoryQuery"/> processor.
/// </summary>
internal class GetCurrentMonthExpensesPerCategoryQueryProcessor : IGetCurrentMonthExpensesPerCategoryQueryProcessor
{
    private readonly IAsyncDocumentSession _session;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCurrentMonthExpensesPerCategoryQueryProcessor"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public GetCurrentMonthExpensesPerCategoryQueryProcessor(
        IAsyncDocumentSession session,
        IUserInformationProvider userInformationProvider)
    {
        _session = session;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<ExpensePerCategoryResponse>> Process(
        GetCurrentMonthExpensesPerCategoryQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query.UserId != _userInformationProvider.UserId)
        {
            return [];
        }

        Transactions_Monthly.Result[] monthlyTransactions = await _session
            .Query<Transactions_Monthly.Result, Transactions_Monthly>()
            .Where(x =>
                x.UserId == query.UserId &&
                x.Year == query.StartOfMonth.Year &&
                x.Month == query.StartOfMonth.Month &&
                x.Currency == query.Currency &&
                x.TransactionType == TransactionType.Expense.Value)
            .ToArrayAsync(cancellationToken);

        Currency currency = Currency.FromValue(query.Currency).Value;

        ExpensePerCategoryResponse[] expensesPerCategory = [.. monthlyTransactions
            .Select(x => new ExpensePerCategoryResponse
            {
                Category = Category.FromValue(x.Category).Value.Name,
                Amount = x.Amount,
                FormattedAmount = currency.Format(x.Amount)
            })
            .OrderBy(x => x.Amount)];

        return expensesPerCategory;
    }
}
